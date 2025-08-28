using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PicturesController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMdImage(int id)
        {
            Picture? picture = await _pictureService.GetAsync(null, id);
            if (picture == null) return StatusCode(StatusCodes.Status500InternalServerError);

            byte[] bytes = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/images/notes/" + picture.FileName);
            return File(bytes, picture.MimeType);
        }
    }
}
