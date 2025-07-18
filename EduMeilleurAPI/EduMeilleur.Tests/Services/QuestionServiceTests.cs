using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduMeilleur.Tests.Services
{
    public class QuestionServiceTests
    {
        private readonly QuestionService _questionService;
        private readonly EduMeilleurAPIContext _context;
        private User _user;

        public QuestionServiceTests()
        {
            _context = GetInMemoryDbContext();
            var pictureService = new PictureService(_context);
            var attachementService = new AttachmentService(_context);

            _questionService = new QuestionService(_context, pictureService, attachementService);

            _user = new User
            {
                Id = "11111111-1111-1111-1111-111111111117",
                UserName = "user1",
                Email = "bobibou@mail.com",
                NormalizedUserName = "USER1",
                NormalizedEmail = "BOBIBOU@MAIL.COM"
            };
        }

        private EduMeilleurAPIContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EduMeilleurAPIContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_" + Guid.NewGuid())
                .Options;

            return new EduMeilleurAPIContext(options);
        }

        [Fact]
        public async Task CreateValidQuestionOK()
        {
            // Arrange
            var question = new QuestionTeacher
            {
                Id = 0,
                Title = "CreateValidQuestionOK",
                Message = "Test",
                user = _user,
            };

            // Act
            var result = await _questionService.CreateQuestionTeacher(question);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("CreateValidQuestionOK", result.Title);

            var fromDb = await _context.QuestionTeacher.FindAsync(1);

            Assert.NotNull(fromDb);
            Assert.Equal("CreateValidQuestionOK", fromDb.Title);
        }

        [Fact]
        public async Task CreateValidFeedbackOK()
        {
            // Arrange
            var feedback = new Feedback
            {
                Id = 0,
                Title = "CreateValidFeedbackOK",
                Message = "Test",
                user = _user,
            };

            //Assert.Equal(0, _context.Feedbacks.Count());

            // Act
            var result = await _questionService.CreateFeedback(feedback);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("CreateValidFeedbackOK", result.Title);

            var fromDb = await _context.Feedbacks.FindAsync(1);

            Assert.NotNull(fromDb);
            Assert.Equal("CreateValidFeedbackOK", fromDb.Title);
        }

        [Theory]
        [InlineData("image/png", ".png", typeof(QuestionTeacher))]
        [InlineData("application/pdf", ".pdf", typeof(Feedback))]
        public async Task SaveValidFilesAndAttachmentsOK(string mimeType, string extension, Type targetType)
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();

            byte[] imageBytes = File.ReadAllBytes("../../../images/testImage.png"); 
            var content = new MemoryStream(imageBytes);

            fileMock.Setup(f => f.OpenReadStream()).Returns(content);
            fileMock.Setup(f => f.FileName).Returns("test" + extension);
            fileMock.Setup(f => f.ContentType).Returns(mimeType);
            fileMock.Setup(f => f.Name).Returns("file1");

            var formFileCollection = new FormFileCollection
            {
                fileMock.Object
            };

            var formCollection = new FormCollection(new Dictionary<string, StringValues>(), formFileCollection);

            var pictures = new List<Picture>();
            var attachments = new List<Attachment>();

            object entity;
            if (targetType == typeof(QuestionTeacher))
            {
                entity = new QuestionTeacher
                {
                    Id = 0,
                    Title = "TestQuestion",
                    Message = "Test",
                    user = _user,
                };
            } 
            else
            {
                entity = new Feedback
                {
                    Id = 0,
                    Title = "TestFeedback",
                    Message = "Test",
                    user = _user,
                };
            }

            // Act
            await _questionService.SaveFilesAndAttachements(formCollection, pictures, attachments, entity);

            // Assert
            if (mimeType.StartsWith("image/"))
            {
                Assert.Single(pictures);
                Assert.Empty(attachments);
            }
            else
            {
                Assert.Empty(pictures);
                Assert.Single(attachments);
            }


        }
    }
}
