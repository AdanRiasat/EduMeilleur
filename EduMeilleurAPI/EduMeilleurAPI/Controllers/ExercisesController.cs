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
using EduMeilleurAPI.Models.DTO;

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
        public async Task<ActionResult> GetAllExercises(int id)
        {
            var exercises = await _exerciseService.GetAllAsync(id);
            if (exercises == null) return StatusCode(StatusCodes.Status500InternalServerError);

            var items = exercises.Select(e => new ItemResponseDTO(e, ItemTypes.Exercises, ItemTypes.Notes, e.NoteExercises.Select(ne => new RelatedItemDTO(ne.NoteId, ne.Note.Code)).ToList()));

            return Ok(items);
        }

        // GET: api/Exercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetExercises(int id) //s for simpler frontend
        {
            var exercise = await _exerciseService.GetAsync(id);
            if (exercise == null) return StatusCode(StatusCodes.Status500InternalServerError);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Exercises", exercise.Content);
            if (!System.IO.File.Exists(filePath)) return NotFound("Markdown file not found");

            exercise.Content = await System.IO.File.ReadAllTextAsync(filePath);

            var item = new ItemResponseDTO(exercise, ItemTypes.Exercises, ItemTypes.Notes, exercise.NoteExercises.Select(ne => new RelatedItemDTO(ne.NoteId, ne.Note.Code)).ToList());
            return Ok(item);
        }

       
    }
}
