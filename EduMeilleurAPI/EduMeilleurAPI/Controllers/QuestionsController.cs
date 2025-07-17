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

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly UserManager<User> _userManager;
        private readonly PictureService _pictureService;
        private readonly AttachmentService _attachmentService;

        public QuestionsController(QuestionService questionService, UserManager<User> userManager, PictureService pictureService, AttachmentService attachmentService)
        {
            _questionService = questionService;
            _userManager = userManager;
            _pictureService = pictureService;
            _attachmentService = attachmentService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<QuestionTeacher>> PostQuestionTeacher()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();

            string? Title = Request.Form["title"];
            string? Message = Request.Form["message"];

            if (Title == null || Message == null) return BadRequest();

            var pictures = new List<Picture>();
            var attachments = new List<Attachment>();

            QuestionTeacher question = new QuestionTeacher()
            {
                Id = 0,
                Title = Title,
                Message = Message,
                user = user
            };

            var newQuestion = await _questionService.CreateQuestionTeacher(question);

            try
            {
                IFormCollection formcollection = await Request.ReadFormAsync();
                await _questionService.SaveFilesAndAttachements(formcollection, pictures, attachments, question);
                
            } 
            catch (Exception e)
            {
                throw;
            }

            newQuestion.Pictures = pictures;
            newQuestion.Attachments = attachments;

           
            return Ok(newQuestion);
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
