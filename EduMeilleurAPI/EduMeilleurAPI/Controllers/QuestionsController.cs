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
using EduMeilleurAPI.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SixLabors.ImageSharp;
using EduMeilleurAPI.Services.Interfaces;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly UserManager<User> _userManager;
        private readonly IPictureService _pictureService;
        private readonly AttachmentService _attachmentService;
        private readonly EmailService _emailService; 

        public QuestionsController(QuestionService questionService, UserManager<User> userManager, IPictureService pictureService, AttachmentService attachmentService, EmailService emailService)
        {
            _questionService = questionService;
            _userManager = userManager;
            _pictureService = pictureService;
            _attachmentService = attachmentService;
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

            var pictures = new List<Picture>();
            var attachments = new List<Attachment>();

            var question = new QuestionTeacher()
            {
                Id = 0,
                Title = title,
                Message = message,
                user = user
            };

            try
            {
                var newQuestion = await _questionService.CreateQuestionTeacher(question);
                
                IFormCollection formcollection = await Request.ReadFormAsync();
                await _questionService.SaveFilesAndAttachements(formcollection, pictures, attachments, question);
                await _emailService.SendQuestionConfirmation(userEmail, userName, title, message);
                
                newQuestion.Pictures = pictures;
                newQuestion.Attachments = attachments;
                
                question = newQuestion;
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

            string? Title = Request.Form["title"];
            string? Message = Request.Form["message"];

            if (Title == null || Message == null) return BadRequest();

            var pictures = new List<Picture>();
            var attachments = new List<Attachment>();

            Feedback feedback = new Feedback()
            {
                Id = 0,
                Title = Title,
                Message = Message,
                user = user
            };

            var newFeedback = await _questionService.CreateFeedback(feedback);

            try
            {
                IFormCollection formcollection = await Request.ReadFormAsync();
                await _questionService.SaveFilesAndAttachements(formcollection, pictures, attachments, feedback);

            }
            catch (Exception e)
            {
                throw;
            }

            newFeedback.Pictures = pictures;
            newFeedback.Attachments = attachments;


            return Ok(newFeedback);
        }


    }
}
