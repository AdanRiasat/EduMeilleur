using Microsoft.AspNetCore.Mvc;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using EduMeilleurAPI.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using EduMeilleurAPI.Services;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService; 

        public QuestionsController(QuestionService questionService, UserManager<User> userManager, IEmailService emailService)
        {
            _questionService = questionService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<QuestionTeacher>> PostQuestionTeacher()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();
            
            var userEmail = user.Email;
            var userName = user.UserName;
            
            if (userEmail == null || userName == null) return BadRequest();
            
            IFormCollection formCollection = await Request.ReadFormAsync();
            string? title = Request.Form["title"];
            string? message = Request.Form["message"];

            if (title == null || message == null) return BadRequest();

            var question = new QuestionTeacher
            {
                Title = title,
                Message = message,
                user = user
            };

            try
            {
                var error = await _questionService.CreateQuestionTeacher(question, formCollection);
                if (error != null) return BadRequest(error);
                
                var filePaths = _questionService.GetFilePaths(question);
                
                await _emailService.SendQuestionConfirmation(userEmail, userName, title, message, filePaths);
                await _emailService.SendQuestionToTeacher(title, message, userName, userEmail, filePaths);
            } 
            catch (Exception e)
            {
                return Problem(e.Message);
            }
           
            return Ok(question);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<QuestionTeacher>> PostFeedback()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();
            
            var userEmail = user.Email;
            var userName = user.UserName;
            
            if (userEmail == null || userName == null) return BadRequest();

            IFormCollection formCollection = await Request.ReadFormAsync();
            string? title = Request.Form["title"];
            string? message = Request.Form["message"];

            if (title == null || message == null) return BadRequest();

            Feedback feedback = new Feedback()
            {
                Title = title,
                Message = message,
                user = user
            };

            try
            {
                var error = await _questionService.CreateFeedback(feedback, formCollection);
                if (error != null) return BadRequest(error);

                var filePaths = _questionService.GetFilePaths(feedback);
                
                await _emailService.SendFeedbackConfirmation(userEmail, userName, title, message, filePaths);
                await _emailService.SendFeedbackToAdmin(title, message, userName, userEmail, filePaths);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }


            return Ok(feedback);
        }
    }
}
