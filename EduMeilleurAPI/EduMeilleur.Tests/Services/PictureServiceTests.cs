using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduMeilleur.Tests.Services
{
    public class PictureServiceTests
    {
        private readonly EduMeilleurAPIContext _context;
        private readonly PictureService _pictureService;
        private User _user;

        public PictureServiceTests()
        {
            _context = GetInMemoryDbContext();
            _pictureService = new PictureService(_context);
           
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
        public async Task CreateValidPictureOK()
        {
            // Arrange
            var picture = new Picture
            {
                Id = 0,
                FileName = "test.png",
                MimeType = "image/png"
            };

            // Act
            var result = await _pictureService.CreatePicture(picture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(picture.FileName, result.FileName);

            var fromDb = await _context.Picture.FindAsync(result.Id);
            
            Assert.NotNull(fromDb);
            Assert.Equal(picture.FileName, fromDb.FileName);
        }

        [Fact]
        public async Task DeleteExistingPictureOK()
        {
            // Arrange
            var picture = new Picture
            {
                Id = 0,
                FileName = "testDelete.png",
                MimeType = "image/png"
            };

            // Act
            var newPicture = await _pictureService.CreatePicture(picture);
            Assert.NotNull(await _context.Picture.Where(p => p.FileName == picture.FileName).FirstOrDefaultAsync());

            await _pictureService.Delete(newPicture);

            // Assert
            Assert.Null(await _context.Picture.Where(p => p.FileName == picture.FileName).FirstOrDefaultAsync());

        }

        [Fact]
        public async Task GetPictureOK()
        {
            // Arrange
            var picture = new Picture
            {
                Id = 0,
                FileName = "testDelete.png",
                MimeType = "image/png"
            };

            // Act
            var newPicture = await _pictureService.CreatePicture(picture);
            Assert.NotNull(await _context.Picture.Where(p => p.FileName == picture.FileName).FirstOrDefaultAsync());

            _user.FileName = newPicture.FileName;
            _user.MimeType = newPicture.MimeType;

            var pictureFromUser = await _pictureService.GetAsync(_user);

            //Assert
            Assert.NotNull(newPicture);
            Assert.NotNull(pictureFromUser);
            Assert.Equal(newPicture.Id, pictureFromUser.Id);
        }
    }
}
