using EduMeilleurAPI.Models;
using EduMeilleurAPI.Models.DTO;
using EduMeilleurAPI.Services;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SixLabors.ImageSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const int MINIMUM_SCHOOL_YEAR = 1;
        private const int MAXIMUM_SCHOOL_YEAR = 5;
        
        private readonly UserManager<User> _userManager;
        private readonly IPictureService _pictureService;
        private readonly ISchoolService _schoolService;
        private readonly IConfiguration _config;

        public UsersController(UserManager<User> userManager, IPictureService pictureService, ISchoolService schoolService, IConfiguration config)
        {
            _userManager = userManager;
            _pictureService = pictureService;
            _schoolService = schoolService;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            User user = new User
            {
                UserName = register.Username,
                Email = register.Email,
                IQPoints = 0,
            };
            
            if (register.SchoolId.HasValue)
            {
                var school = await _schoolService.GetSchool(register.SchoolId.Value);
                if (school == null) return StatusCode(StatusCodes.Status500InternalServerError);
                
                user.School = school;
            }

            if (register.SchoolYear >= MINIMUM_SCHOOL_YEAR && register.SchoolYear <= MAXIMUM_SCHOOL_YEAR)
            {
                user.SchoolYear = register.SchoolYear;
            }

            IdentityResult result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => new { e.Code, e.Description }));

            return Ok(await GenerateLoginResponse(user));
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            User? user = await _userManager.FindByNameAsync(login.Username) ?? await _userManager.FindByEmailAsync(login.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return Ok(await GenerateLoginResponse(user));
            } 
            
            return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Le nom d'utilisateur ou le mot de passe est invalide." });
        }
        
        [HttpGet]
        public IActionResult GoogleLogin(string? returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(GoogleCallback));
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "Google");
        }

        [HttpGet]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Google authentication failed." });

            var claims    = result.Principal!;
            var email     = claims.FindFirstValue(ClaimTypes.Email)!;
            var externalId = claims.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var name      = claims.FindFirstValue(ClaimTypes.Name) ?? email;
            
            var user = await _userManager.FindByLoginAsync("Google", externalId); // check if already linked

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = name.Replace(" ", "_"),
                        Email    = email,
                        IQPoints = 0,
                        EmailConfirmed = true,
                    };

                    var createResult = await _userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                        return BadRequest(createResult.Errors.Select(e => new { e.Code, e.Description }));
                }
                
                await _userManager.AddLoginAsync(user, new UserLoginInfo("Google", externalId, "Google"));
            }
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var data = await GenerateLoginResponse(user);
            return Redirect($"{_config["JWT:Audience"]}/oauth-callback?token={data.Token}&refreshToken={data.RefreshToken}&username={data.Username}");
        }

        private async Task<LoginResponseDTO> GenerateLoginResponse(User user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = new List<Claim>();
            foreach (string role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8 .GetBytes(_config["JWT:Key"])); 
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(14);
            await _userManager.UpdateAsync(user);

            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo,
                RefreshToken = user.RefreshToken,
                Username = user.UserName,
                Roles = roles
            };
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshExpiredToken(RefreshRequestDTO refreshRequest)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshRequest.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized();

            return Ok(await GenerateLoginResponse(user));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfilePicture(string username)
        {
            User? user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound();

            if (user.FileName == null)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/images/pfp/default.png");
                return File(bytes, "image/png");
            }
            else
            {
                byte[] bytes = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/images/pfp/" + user.FileName);
                return File(bytes, user.MimeType);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetProfile()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();
            
            return Ok(new ProfileDisplayDTO(user));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ProfileDisplayDTO>> EditProfile()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();

            string? email = Request.Form["email"];
            string? bio = Request.Form["bio"];
            string? schoolId = Request.Form["school"];
            string? schoolYear = Request.Form["schoolYear"];

            if (email == null || email == "") return BadRequest();
            
            user.Email = email;

            
            if (bio != null)
            {
                user.Bio = bio;
            } 
            else
            {
                user.Bio = "";
            }
                

            if (schoolId != null && schoolId != "")
            {
                user.School = await _schoolService.GetSchool(int.Parse(schoolId));
            }
            else
            {
                user.School = null;
                user.SchoolId = null;
            }
               
            //TODO bug where schoolyear gets set to null somehow
            if (schoolYear != null && schoolYear != "")
            {
                user.SchoolYear = int.Parse(schoolYear);
            }
            else {
                user.SchoolYear = null;
            }
                

            try
            {
                IFormCollection formcollection = await Request.ReadFormAsync();
                IFormFile? file = formcollection.Files.GetFile("pfp");
                if (file == null)
                {
                    await _userManager.UpdateAsync(user);
                    return Ok(new ProfileDisplayDTO(user));
                }

                Picture? oldPicture = await _pictureService.GetAsync(user);
                if (oldPicture != null)
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "/images/pfp/" + oldPicture.FileName);
                    await _pictureService.Delete(oldPicture);
                }

                Image image = Image.Load(file.OpenReadStream());

                Picture picture = new Picture
                {
                    Id = 0,
                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                    MimeType = file.ContentType
                };

                image.Save(Directory.GetCurrentDirectory() + "/images/pfp/" + picture.FileName);

                Picture? newPicture = await _pictureService.CreatePicture(picture);
                if (newPicture == null) return StatusCode(StatusCodes.Status500InternalServerError);

                user.FileName = newPicture.FileName;
                user.MimeType = newPicture.MimeType;
            }
            catch (Exception e)
            {
                throw;
            }

            var result = await _userManager.UpdateAsync(user);

            return Ok(new ProfileDisplayDTO(user));
        }

        [HttpGet]
        public async Task<ActionResult> GetTeachers()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("teacher");
            var teachersNames = new List<string>();

            foreach (var teacher in teachers)
            {
                teachersNames.Add(teacher.UserName);
            }

            return Ok(teachersNames);
        }
    }
}
