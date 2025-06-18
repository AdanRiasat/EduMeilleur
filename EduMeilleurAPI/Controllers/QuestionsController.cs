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
using Microsoft.AspNetCore.Razor.Language;
using System.Net.Mail;

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

            var files = new List<Picture>();
            var attachments = new List<Models.Attachment>();

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
                for(int i = 1; i <= formcollection.Files.Count; i++)
                {
                    IFormFile? file = formcollection.Files.GetFile("file" + i);
                    if (file != null)
                    {
                        var extension = Path.GetExtension(file.FileName).ToLower();
                        var mimeType = file.ContentType;
                        var safeFileName = Guid.NewGuid().ToString() + extension;
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", safeFileName);

                        if (mimeType.StartsWith("image/"))
                        {
                            try
                            {
                                Image image = Image.Load(file.OpenReadStream());
                                Picture picture = new Picture
                                {
                                    Id = 0,
                                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                                    MimeType = file.ContentType,
                                    QuestionTeacher = question
                                };
                                files.Add(picture);
                                image.Save(Directory.GetCurrentDirectory() + "/images/full/" + picture.FileName);
                                picture.QuestionTeacher = question;
                                await _pictureService.CreatePicture(picture);
                            }
                            catch
                            {
                                //log errors
                            }
                        }
                        else
                        {
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var attachment = new Models.Attachment
                            {
                                Id = 0,
                                Filename = safeFileName,
                                MimeType = mimeType,
                                QuestionTeacher = question
                            };

                            attachments.Add(attachment);
                            await _attachmentService.CreateAttachment(attachment);
                        }
                    }
                } 
            } 
            catch (Exception e)
            {
                throw;
            }

            newQuestion.Pictures = files;
            newQuestion.Attachments = attachments;

           
            return Ok(newQuestion);
        }


    }
}
