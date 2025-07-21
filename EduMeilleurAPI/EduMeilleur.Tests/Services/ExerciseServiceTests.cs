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
    public class ExerciseServiceTests
    {
        private readonly EduMeilleurAPIContext _context;
        private readonly ExerciseService _exerciseService;
        private Chapter _chapter;
        private Subject _subject;

        public ExerciseServiceTests()
        {
            _context = GetInMemoryDbContext();
            _exerciseService = new ExerciseService(_context);
        }

        private EduMeilleurAPIContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EduMeilleurAPIContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_" + Guid.NewGuid())
                .Options;

            return new EduMeilleurAPIContext(options);
        }

        private async Task SetupSubjectAndChapterAsync()
        {
            _subject = new Subject
            {
                Name = "Math",
                Description = "Mathematics",
                Type = "Core"
            };
            _context.Subject.Add(_subject);
            await _context.SaveChangesAsync();

            _chapter = new Chapter
            {
                Title = "Algebra",
                SubjectId = _subject.Id
            };
            _context.Chapters.Add(_chapter);
            await _context.SaveChangesAsync();
        }

        private async Task<Exercise> AddExercisetAsync(string title, string content)
        {
            var exercise = new Exercise
            {
                Title = title,
                Content = content,
                ChapterId = _chapter.Id
            };

            _context.Exercise.Add(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }

        [Fact]
        public async Task GetAllExercisesOK()
        {
            // Arrange
            await SetupSubjectAndChapterAsync();
            await AddExercisetAsync("exer1", "content");
            await AddExercisetAsync("exer2", "content");

            // Act
            var exercises = await _exerciseService.GetAllAsync(_subject.Id);

            // Assert
            Assert.NotNull(exercises);
            Assert.Equal(2, exercises.Count);
            Assert.All(exercises, e => Assert.Equal(_chapter.Id, e.ChapterId));
        }

        [Fact]
        public async Task GetOneExerciseOK()
        {
            // Arrange
            await SetupSubjectAndChapterAsync();
            var newExercise = await AddExercisetAsync("exer3", "content");

            // Act
            var result = await _exerciseService.GetAsync(newExercise.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newExercise.Id, result.Id);
            Assert.Equal(_chapter.Id, result.ChapterId);
        }
    }
}
