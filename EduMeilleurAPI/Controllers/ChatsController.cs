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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using EduMeilleurAPI.Models.DTO;

namespace EduMeilleurAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatsController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly UserManager<User> _userManager;

        public ChatsController(ChatService chatService, UserManager<User> userManager)
        {
            _chatService = chatService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(ChatMessage message)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();

            if (message == null) return BadRequest();

            var response = await _chatService.PostMessage(message);
            if (response == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChat(CreateChatDTO dto)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();

            Chat chat = new Chat()
            {
                Id = 0,
                Title = GenerateTitle(dto.InitialMessage),
                User = user,
                Messages = new List<ChatMessage>()
            };

            var newChat = await _chatService.PostChat(chat);
            if (newChat == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(newChat);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(int id)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();

            if (!user.Chats.Any(c => c.Id == id)) return Unauthorized();

            var oldChat = await _chatService.DeleteChat(id);
            if (oldChat == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Chat>>> GetChats()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return NotFound();

            var chats = await _chatService.GetChats(user);
            if (chats == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(chats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ChatMessage>>> GetMessages(int id)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null) return Unauthorized();

            if (!user.Chats.Any(c => c.Id == id)) return BadRequest();

            var messages = await _chatService.GetMessages(id);
            if (messages == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(messages);
        }

        private string GenerateTitle(string message)
        {
            // Take the first sentence or up to 6 words, as a simple heuristic
            var words = message.Split(new[] { ' ', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Take(6)) + (words.Length > 6 ? "..." : "");
        }

    }
}
