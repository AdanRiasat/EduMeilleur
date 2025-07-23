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
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockPictureService = new Mock<IPictureService>();

            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                store.Object, null, null, null, null, null, null, null, null);

            _controller = new UsersController(_mockUserManager.Object, _mockPictureService.Object);
        }

        private async Task<RegisterDTO> ArrangeNewUser(string username, string password, string email)
        {
            var registerDTO = new RegisterDTO
            {
                Username = username,
                Email = email,
                Password = password,
            };

            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<User>(), registerDTO.Password)).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(u => u.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>());

            return registerDTO;
        }

        [Fact] //Change to theory later
        public async Task RegisterNewUserOK()
        {
            // Arrange
            var registerDTO = await ArrangeNewUser("testUser", "password123!!", "myEmail@adjna.com");

            //var loginResponse = new { Token = "fake-jwt" };

            // Act
            var result = await _controller.Register(registerDTO);

            // Assert
            var okRersult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginExistingUserOK()
        {
            // Arrange
            await ArrangeNewUser("testUser", "password123!!", "myEmail@adjna.com");

            var loginDTO = new LoginDTO
            {
                Username = "testUser",
                Password = "password321!!"
            };

            var user = new User
            {
                Id = "testUser123",
                UserName = loginDTO.Username,
            };

            _mockUserManager.Setup(u => u.FindByNameAsync(loginDTO.Username)).ReturnsAsync((user));

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
