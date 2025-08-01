using EduMeilleurAPI.Models;
using EduMeilleurAPI.Models.DTO;
using EduMeilleurAPI.Services;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SixLabors.ImageSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPictureService _pictureService;
        private readonly SchoolService _schoolService;

        public UsersController(UserManager<User> userManager, IPictureService pictureService, SchoolService schoolService)
        {
            _userManager = userManager;
            _pictureService = pictureService;
            _schoolService = schoolService;
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

            var isIdValid = await _schoolService.IsSchoolIdValid(register.SchoolId);
            if (isIdValid == null) return StatusCode(StatusCodes.Status500InternalServerError);

            if ((bool)isIdValid)
            {
                var school = await _schoolService.GetSchool((int)register.SchoolId);
                if (school == null) return StatusCode(StatusCodes.Status500InternalServerError);

                user.School = school;
            }

            if (register.SchoolYear >= 1 && register.SchoolYear <= 5)
            {
                user.SchoolYear = register.SchoolYear;
            }

            IdentityResult identityResult = await _userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {
                var firstError = identityResult.Errors.FirstOrDefault();

                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Message = firstError?.Code ?? "An unknown error occurred."
                });
            }

            return Ok(await GenerateLoginResponse(user));
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            User? user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
                user = await _userManager.FindByEmailAsync(login.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return Ok(await GenerateLoginResponse(user));
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "Le nom d'utilisateur ou le mot de passe est invalide." });
            }
        }

        private async Task<ActionResult> GenerateLoginResponse(User user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = new List<Claim>();
            foreach (string role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            }
            authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes("LooOOongue Phrase SiNoN Ça ne Marchera PaAaAAAaAas !")); // Phrase identique dans Program.cs
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://localhost:7027",
                audience: "http://localhost:4200",
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                validTo = token.ValidTo,
                profile = new ProfileDisplayDTO(user),
                Roles = roles
            });
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
            }
               

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

            await _userManager.UpdateAsync(user);

            return Ok(new ProfileDisplayDTO(user));
        }
    }
}
