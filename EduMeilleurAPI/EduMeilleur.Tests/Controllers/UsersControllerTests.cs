using Azure.Identity;
using EduMeilleurAPI.Controllers;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Models.DTO;
using EduMeilleurAPI.Services;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduMeilleur.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IPictureService> _mockPictureService;
        private readonly Mock<ISchoolService> _mockSchoolService;
        private readonly UsersController _controller;


        public UsersControllerTests()
        {
            _mockPictureService = new Mock<IPictureService>();
            _mockSchoolService = new Mock<ISchoolService>();

            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                store.Object, null, null, null, null, null, null, null, null);

            _controller = new UsersController(_mockUserManager.Object, _mockPictureService.Object, _mockSchoolService.Object);
        }

        private async Task<RegisterDTO> ArrangeNewUser(string username, string password, string email, int? schoolId = null, int? schoolYear = null)
        {
            var registerDTO = new RegisterDTO
            {
                Username = username,
                Email = email,
                Password = password,
                SchoolId = schoolId,
                SchoolYear = schoolYear
            };

            _mockSchoolService.Setup(s => s.IsSchoolIdValid(It.IsAny<int>())).ReturnsAsync(true);
            _mockSchoolService.Setup(s => s.IsSchoolIdValid(null)).ReturnsAsync(false);
            _mockSchoolService.Setup(s => s.GetSchool(It.IsAny<int>())).ReturnsAsync(new School { Id = 1, Name = "Test School" });

            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<User>(), registerDTO.Password)).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(u => u.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>());

            return registerDTO;
        }

        [Fact] //Change to theory later
        public async Task RegisterNewUserOK()
        {
            // Arrange
            var registerDTO = await ArrangeNewUser("testUser", "password123!!", "myEmail@adjna.com");

            // Act
            var result = await _controller.Register(registerDTO);

            // Assert
            var okRersult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okRersult);
        }

        [Fact]
        public async Task LoginExistingUserOK()
        {
            // Arrange
            var registerDTO = await ArrangeNewUser("testUser", "password123!!", "myEmail@adjna.com");

            var user = new User
            {
                Id = "testUser123",
                UserName = registerDTO.Username,
            };

            _mockUserManager.Setup(u => u.FindByNameAsync(registerDTO.Username)).ReturnsAsync(user);
            _mockUserManager.Setup(u => u.CheckPasswordAsync(user, registerDTO.Password)).ReturnsAsync(true);

            var loginDTO = new LoginDTO
            {
                Username = registerDTO.Username,
                Password = registerDTO.Password,
            };

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task LoginInvalidCredentialsKO()
        {
            // Arrange
            var registerDTO = await ArrangeNewUser("testUser", "password123!!", "myEmail@adjna.com");

            var loginDTO = new LoginDTO
            {
                Username = "wrongUsername",
                Password = "WrongUsername"
            };

            _mockUserManager.Setup(u => u.FindByNameAsync(loginDTO.Username)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            var badRequest = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);

        }
    }
}
