using EduMeilleurAPI.Controllers;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Models.DTO;
using EduMeilleurAPI.Services;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduMeilleur.Tests.Controllers
{
    public class SubjectsControllerTests
    {
        private readonly Mock<ISubjectService> _mockSubjectService;
        private readonly SubjectsController _controller;

        public SubjectsControllerTests()
        {
            _mockSubjectService = new Mock<ISubjectService>();
            _controller = new SubjectsController(_mockSubjectService.Object);
        }

        [Fact]
        public async Task GetAllSubjectsOK()
        {
            // Arrange
            var subjects = new List<SubjectDisplayDTO>
            {
                new SubjectDisplayDTO {Id = 1, Name = "SN1"},
                new SubjectDisplayDTO {Id = 2, Name = "SN2"}
            };

            _mockSubjectService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(subjects);

            // Act
            var result = await _controller.GetSubjects();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSubjects = Assert.IsAssignableFrom<IEnumerable<SubjectDisplayDTO>>(okResult.Value);
            Assert.Equal(2, returnedSubjects.Count());
        }

        [Fact]
        public async Task GetNullSubjectsKO()
        {
            // Arrange
            _mockSubjectService
               .Setup(s => s.GetAllAsync())
               .ReturnsAsync((List<SubjectDisplayDTO>?)null);

            // Act 
            var result = await _controller.GetSubjects();

            //Assert
            var statusResult = Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(500, statusResult.StatusCode);
        }

        [Fact]
        public async Task GetOneSubjectOK()
        {
            // Arrange
            var subject = new Subject
            {
                Id = 3,
                Name = "SN3"
            };

            _mockSubjectService.Setup(s => s.GetAsync(subject.Id)).ReturnsAsync(subject);

            // Act
            var result = await _controller.GetSubject(subject.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSubject = Assert.IsAssignableFrom<SubjectDisplayDTO>(okResult.Value);
            Assert.Equal(subject.Id, returnedSubject.Id);
        }

        [Fact]
        public async Task GetSubjectWithInvalidIdKO()
        {
            // Act
            var result = await _controller.GetSubject(99);

            // Assert
            var statusResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, statusResult.StatusCode);
        }
    }
}
