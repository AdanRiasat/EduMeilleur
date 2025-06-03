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
    public class NotesController : ControllerBase
    {
        private readonly NotesService _notesService;

        public NotesController(NotesService notesService)
        {
            _notesService = notesService;
        }

        // GET: api/Notes
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Notes>>> GetAllNotes(int id)
        {
            List<Notes>? notes = await _notesService.GetAllAsync(id);
            if (notes == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return notes;
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notes>> GetNotes(int id)
        {
            var notes = await _notesService.GetAsync(id);

            if (notes == null) return NotFound();

            return notes;
        }
    }
}
