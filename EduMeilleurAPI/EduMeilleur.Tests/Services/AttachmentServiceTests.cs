using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "FakeKeyForTestssssssssssss 12312312333232232323"},
                {"Jwt:Issuer", "https://localhost:7027"},
                {"Jwt:Audience", "http://localhost:4200"},
                {"Admin:Password", "alloo123" },
                {"Admin:Email", "hellooo@gmail.com" },
                {"Teacher:Password", "alloo123" },
                {"Teacher2:Password",  "Test123!"},
                {"Teacher3:Password",  "Test123!"},
                {"Teacher4:Password",  "Test123!"},
                {"Teacher5:Password",  "Test123!"},
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            return new EduMeilleurAPIContext(options, config);
        }

        [Fact]
        public async Task CreateValidAttachmentOK()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new AttachmentService(context);

            var attachment = new Attachment
            {
                Id = 0,
                FileName = "test.pdf",
                MimeType = "application/pdf"
            };

            // Act
            var result = await service.CreateAttachment(attachment);

            //Asert
            Assert.NotNull(result);
            Assert.Equal(attachment.FileName, result.FileName);

            var fromDb = await context.Attachments.FindAsync(result.Id);
            Assert.NotNull(fromDb);
            Assert.Equal(attachment.FileName, fromDb.FileName);
        }

        [Fact]
        public async Task CreateValidAttachmentNullContextKO()
        {
            // Arange
            var service = new AttachmentService(null);

            var attachment = new Attachment
            {
                Id = 0,
                FileName = "test2.pdf",
                MimeType = "application/pdf"
            };

            // Act
            var result = await service.CreateAttachment(attachment);

            // Asert
            Assert.Null(result);
        }
    }

}

