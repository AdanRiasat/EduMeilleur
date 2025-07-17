using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services;
using EduMeilleurAPI.Migrations;
using EduMeilleurAPI.Models.DTO;
using static Azure.Core.HttpHeader;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly ExerciseService _exerciseService;

        public ExercisesController(ExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        // GET: api/Exercises
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetAllExercises(int id)
        {
            var exercises = await _exerciseService.GetAllAsync(id);
            if (exercises == null) return StatusCode(StatusCodes.Status500InternalServerError);

            var items = new List<ItemDisplayDTO>();

            foreach (var item in exercises)
            {
                items.Add(new ItemDisplayDTO(item.Id, item.Title, item.Content, item.Chapter.Title));
            }

            return Ok(items);
        }

        // GET: api/Exercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExercises(int id) //s for simpler frontend
        {
            var exercise = await _exerciseService.GetAsync(id);
            if (exercise == null) return StatusCode(StatusCodes.Status500InternalServerError);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Exercises", exercise.Content);
            if (!System.IO.File.Exists(filePath)) return NotFound("Markdown file not found");

            exercise.Content = await System.IO.File.ReadAllTextAsync(filePath);

            return Ok(exercise);
        }

       
    }
}
