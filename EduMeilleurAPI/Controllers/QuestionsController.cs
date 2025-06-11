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

        public QuestionsController(QuestionService questionService, UserManager<User> userManager, PictureService pictureService)
        {
            _questionService = questionService;
            _userManager = userManager;
            _pictureService = pictureService;
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
                } 
            } 
            catch (Exception e)
            {
                throw;
            }


            newQuestion.Pictures = files;

            

            return Ok(newQuestion);
        }


    }
}
