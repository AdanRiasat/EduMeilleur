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
        public async Task<ActionResult> GetAllVideos(int id)
        {
            var videos = await _videoService.GetAllAsync(id);
            if (videos == null) return StatusCode(StatusCodes.Status500InternalServerError);

            var items = videos.Select(v => new ItemResponseDTO(v, ItemTypes.Videos));

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetVideos(int id) //s for simpler frontend
        {
            var video = await _videoService.GetAsync(id);
            if (video == null) return StatusCode(StatusCodes.Status500InternalServerError);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Videos", video.Content);
            if (!System.IO.File.Exists(filePath)) return NotFound("Markdown file not found");

            video.Content = await System.IO.File.ReadAllTextAsync(filePath);

            
            var item = new ItemResponseDTO(video, ItemTypes.Videos);
            return Ok(item);
        }
    }
}
