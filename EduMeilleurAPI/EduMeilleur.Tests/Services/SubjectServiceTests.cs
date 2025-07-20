using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Models.DTO;
using EduMeilleurAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduMeilleur.Tests.Services
{
    public class SubjectServiceTests
    {
        private readonly EduMeilleurAPIContext _context;
        private readonly SubjectService _subjectService;

        public SubjectServiceTests()
        {
            _context = GetInMemoryDbContext();
            _subjectService = new SubjectService(_context);
        }

        private EduMeilleurAPIContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EduMeilleurAPIContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_" + Guid.NewGuid())
                .Options;

            return new EduMeilleurAPIContext(options);
        }

        private async Task<Subject> AddSubjectAsync(string name, string description, string type)
        {
            var subject = new Subject
            {
                Name = name,
                Description = description,
                Type = type
            };

            _context.Subject.Add(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        [Fact]
        public async Task GetAllSubjectsOK()
        {
            // Arrange
            var subject1 = await AddSubjectAsync("s1", "description", "Math");            
            var subject2 = await AddSubjectAsync("s2", "description", "Math");            

            // Act
            var allSubjects = await _subjectService.GetAllAsync();

            // Assert
            Assert.NotNull(allSubjects);
            Assert.Equal(2, allSubjects.Count);
        }

        [Fact]
        public async Task GetOneTaskOK()
        {
            // Arrange
            var subject1 = await AddSubjectAsync("s3", "description", "Math");

            var newSubject = await _context.Subject.Where(s => s.Name == subject1.Name).FirstOrDefaultAsync(); 
            Assert.NotNull(newSubject);

            // Act
            var FromService = await _subjectService.GetAsync(newSubject.Id);

            // Assert
            Assert.NotNull(FromService);
            Assert.Equal(newSubject.Id, FromService.Id);
            Assert.Equal(newSubject.Name, FromService.Name);
        }
    }
}
