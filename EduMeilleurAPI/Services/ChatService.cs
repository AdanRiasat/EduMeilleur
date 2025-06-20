using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
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

            chatMessage.TimeStamp = DateTime.UtcNow;
            chatMessage.IsUser = true;
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            var request = new
            {
                model = "deepseek/deepseek-r1-0528:free",
                messages = new[]
                {
                    new { role = "user", content = chatMessage.Texte }
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
                Texte = aiText ?? "No response.",
                ChatId = chatMessage.ChatId,
                TimeStamp = DateTime.UtcNow,
                IsUser = false
            };

            _context.ChatMessages.Add(aiMessage);
            await _context.SaveChangesAsync();

            return aiMessage;
        }
    }
}
