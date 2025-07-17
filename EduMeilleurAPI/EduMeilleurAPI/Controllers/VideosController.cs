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
    public class VideosController : ControllerBase
    {
        private readonly VideoService _videoService;

        public VideosController(VideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Video>>> GetAllVideos(int id)
        {
            var exercises = await _videoService.GetAllAsync(id);
            if (exercises == null) return StatusCode(StatusCodes.Status500InternalServerError);

            var items = new List<ItemDisplayDTO>();

            foreach (var item in exercises)
            {
                items.Add(new ItemDisplayDTO(item.Id, item.Title, item.Content, item.Chapter.Title));
            }

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideos(int id) //s for simpler frontend
        {
            var video = await _videoService.GetAsync(id);
            if (video == null) return StatusCode(StatusCodes.Status500InternalServerError);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Videos", video.Content);
            if (!System.IO.File.Exists(filePath)) return NotFound("Markdown file not found");

            video.Content = await System.IO.File.ReadAllTextAsync(filePath);

            return Ok(video);
        }
    }
}
