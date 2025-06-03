using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EduMeilleurAPI.Services;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly UserManager<User> _userManager;

        public QuestionsController(QuestionService questionService, UserManager<User> userManager)
        {
            _questionService = questionService;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<QuestionTeacher>> PostQuestionTeacher(QuestionTeacher question)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();

            question.user = user;

            var newQuestion = await _questionService.CreateQuestionTeacher(question);

            return Ok(newQuestion);
        }



       
    }
}
