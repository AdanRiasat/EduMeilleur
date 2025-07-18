using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace EduMeilleur.Tests.Services
{
    public class AttachmentSercviceTests
    {
        private EduMeilleurAPIContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EduMeilleurAPIContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_" + Guid.NewGuid())
                .Options;

            return new EduMeilleurAPIContext(options);
        }

        [Fact]
        public async Task CreateValidAttachmentOK()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new AttachmentService(context);

            var attachment = new Attachment
            {
                Id = 1,
                Filename = "test.pdf",
                MimeType = "application/pdf"
            };

            // Act
            var result = await service.CreateAttachment(attachment);

            //Asert
            Assert.NotNull(result);
            Assert.Equal("test.pdf", result.Filename);

            var fromDb = await context.Attachments.FindAsync(1);
            Assert.NotNull(fromDb);
            Assert.Equal("test.pdf", fromDb.Filename);
        }

        [Fact]
        public async Task CreateValidAttachmentNullContextKO()
        {
            // Arange
            var service = new AttachmentService(null);

            var attachment = new Attachment
            {
                Id = 2,
                Filename = "test2.pdf",
                MimeType = "application/pdf"
            };

            // Act
            var result = await service.CreateAttachment(attachment);

            // Asert
            Assert.Null(result);
        }
    }

}

