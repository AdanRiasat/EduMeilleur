using Microsoft.AspNetCore.Mvc;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EduMeilleurAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly UserManager<User> _userManager;
        private readonly EmailService _emailService; 

        public QuestionsController(QuestionService questionService, UserManager<User> userManager, EmailService emailService)
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
                IFormCollection formcollection = await Request.ReadFormAsync();
                
                var error = await _questionService.CreateQuestionTeacher(question, formcollection);
                if (error != null) return StatusCode(StatusCodes.Status413PayloadTooLarge, error);
                
                await _emailService.SendQuestionConfirmation(userEmail, userName, title, message);
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
                IFormCollection formcollection = await Request.ReadFormAsync();
                var error = await _questionService.CreateFeedback(feedback, formcollection);
                if (error != null) return StatusCode(StatusCodes.Status413PayloadTooLarge, error);

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }


            return Ok(feedback);
        }


    }
}
