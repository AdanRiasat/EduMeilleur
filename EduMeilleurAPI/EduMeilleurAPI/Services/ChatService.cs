using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace EduMeilleurAPI.Services
{
    public class ChatService
    {
        private readonly EduMeilleurAPIContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        private const string Url = "https://openrouter.ai/api/v1/chat/completions";

        public ChatService(EduMeilleurAPIContext context, HttpClient httpClient, IConfiguration config)
        {
            _context = context;
            _httpClient = httpClient;
            _config = config;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.Chat != null && _context.ChatMessages != null; 
        }

        public async Task<ChatMessage?> PostMessage(ChatMessage chatMessage)
        {
            if (!IsConstextValid()) return null;

            string apiKey = _config["OpenRouter:ApiKey"];

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            var request = new
            {
                model = "deepseek/deepseek-chat-v3.1:free",
                messages = new[]
                {
                    new { role = "user", content = chatMessage.Text }
                }
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Url);
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            httpRequest.Headers.Add("HTTP-Referer", "https://localhost:7027");
            httpRequest.Headers.Add("X-Title", "EduMeilleur");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("AI service failed to respond.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseBody);
            string? aiText = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            var aiMessage = new ChatMessage
            {
                Text = aiText ?? "No response.",
                ChatId = chatMessage.ChatId,
                TimeStamp = DateTime.UtcNow,
                IsUser = false
            };

            _context.ChatMessages.Add(aiMessage);
            await _context.SaveChangesAsync();

            return aiMessage;
        }

        public async Task<Chat?> PostChat(Chat chat)
        {
            if (!IsConstextValid()) return null;

            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();

            return chat;
        }

        public async Task<Chat?> DeleteChat(int id)
        {
            if (!IsConstextValid()) return null;

            Chat? chat = await _context.Chat.FindAsync(id);
            if (chat == null) return null;

            if (chat.Messages != null && chat.Messages.Count > 0)
            {
                List<ChatMessage> messages = chat.Messages.ToList();
                foreach (ChatMessage message in messages)
                {
                    chat.Messages.Remove(message);
                    _context.ChatMessages.Remove(message);
                    await _context.SaveChangesAsync();
                }
            }

            _context.Chat.Remove(chat);
            await _context.SaveChangesAsync();

            return chat;
        }

        public async Task<List<Chat>?> GetChats(User user)
        {
            if (!IsConstextValid()) return null;

            return await _context.Chat.Where(c => c.User == user).ToListAsync();
        }

        public async Task<List<ChatMessage>?> GetMessages(int chatId)
        {
            if (!IsConstextValid()) return null;

            return await _context.ChatMessages.Where(c => c.ChatId == chatId).ToListAsync();
        }
    }
}
