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
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectService _subjectService;

        public SubjectsController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDisplayDTO>>> GetSubjects()
        {
            List<SubjectDisplayDTO>? subjects = await _subjectService.GetAllAsync();
            if (subjects == null) return StatusCode(StatusCodes.Status500InternalServerError);
           
            return subjects;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDisplayDTO>> GetSubject(int id)
        {
            Subject? subject = await _subjectService.GetAsync(id);
            if (subject == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return new SubjectDisplayDTO(subject);
        }

        

        
    }
}
