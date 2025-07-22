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
using EduMeilleurAPI.Services.Interfaces;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDisplayDTO>>> GetSubjects()
        {
            List<SubjectDisplayDTO>? subjects = await _subjectService.GetAllAsync();
            if (subjects == null) return StatusCode(StatusCodes.Status500InternalServerError);
           
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDisplayDTO>> GetSubject(int id)
        {
            Subject? subject = await _subjectService.GetAsync(id);
            if (subject == null) return NotFound();

            return Ok(new SubjectDisplayDTO(subject));
        }


        
    }
}
