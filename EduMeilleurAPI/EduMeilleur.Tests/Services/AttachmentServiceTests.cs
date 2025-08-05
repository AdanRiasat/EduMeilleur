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
                {"Teacher:Password", "alloo123" }
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
                Filename = "test.pdf",
                MimeType = "application/pdf"
            };

            // Act
            var result = await service.CreateAttachment(attachment);

            //Asert
            Assert.NotNull(result);
            Assert.Equal(attachment.Filename, result.Filename);

            var fromDb = await context.Attachments.FindAsync(result.Id);
            Assert.NotNull(fromDb);
            Assert.Equal(attachment.Filename, fromDb.Filename);
        }

        [Fact]
        public async Task CreateValidAttachmentNullContextKO()
        {
            // Arange
            var service = new AttachmentService(null);

            var attachment = new Attachment
            {
                Id = 0,
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

