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

            return Ok(exercises);
        }

        // GET: api/Exercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExercise(int id)
        {
            var exercise = await _exerciseService.GetAsync(id);
            if (exercise == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(exercise);
        }

       
    }
}
