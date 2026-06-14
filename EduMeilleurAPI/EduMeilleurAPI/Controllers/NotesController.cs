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
    public class NotesController : ControllerBase
    {
        private readonly NotesService _notesService;

        public NotesController(NotesService notesService)
        {
            _notesService = notesService;
        }

        // GET: api/Notes
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAllNotes(int id)
        {
            List<Notes>? notes = await _notesService.GetAllAsync(id);
            if (notes == null) return StatusCode(StatusCodes.Status500InternalServerError);

            var items = notes.Select(n => new ItemResponseDTO(n, ItemTypes.Notes, ItemTypes.Exercises, n.NoteExercises.Select(ne => new RelatedItemDTO(ne.ExerciseId)).ToList(), n.Code));
            return Ok(items);
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetNotes(int id)
        {
            var notes = await _notesService.GetAsync(id);
            if (notes == null) return NotFound();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Notes", notes.Content);
            if (!System.IO.File.Exists(filePath)) return NotFound("Markdown file not found");

            string markdown = await System.IO.File.ReadAllTextAsync(filePath);

            notes.Content = markdown;

            var item = new ItemResponseDTO(notes, ItemTypes.Notes, ItemTypes.Exercises, notes.NoteExercises.Select(ne => new RelatedItemDTO(ne.ExerciseId)).ToList(), notes.Code); 
            return Ok(item);
        }
    }
}
