using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Moq.Protected;
using Microsoft.Identity.Client;

namespace EduMeilleur.Tests.Services
{
    public class ChatServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly EduMeilleurAPIContext _context;
        private readonly ChatService _chatService;
        private User _user;

        public ChatServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["OpenRouter:ApiKey"]).Returns("fake-api-key");
            _configuration = configMock.Object;

            _context = GetInMemoryDbContext();

            _chatService = new ChatService(_context, _httpClient, _configuration);

            _user = new User();
        }

        private EduMeilleurAPIContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EduMeilleurAPIContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_" + Guid.NewGuid())
                .Options;

            return new EduMeilleurAPIContext(options);
        }

        private async Task<Chat> CreateAndSaveChatAsync(string title)
        {
            var chat = new Chat
            {
                User = _user,
                Title = title,
            };

            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();

            return chat;
        }

        private async Task<ChatMessage> CreateAndSaveMessageAsync(string Text, DateTime timeStamp, int chatId, bool isUser = true)
        {
            var message = new ChatMessage
            {
                ChatId = chatId,
                Text = Text,
                IsUser = isUser,
                TimeStamp = timeStamp
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        [Fact]
        public async Task PostValidMessageAndReturnResponseOK()
        {
            // Arrange
            var chat = await CreateAndSaveChatAsync("test");

            var userMessage = new ChatMessage
            {
                ChatId = chat.Id,
                Text = "test",
                TimeStamp = DateTime.UtcNow,
                IsUser = true
            };

            var mockResponse = new
            {
                choices = new[] {
                    new {
                        message = new {
                            content = "Hi there!"
                        }
                    }
                }
            };

            var mockResponseJson = JsonSerializer.Serialize(mockResponse);
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(mockResponseJson, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", // name of the protected method
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            // Act
            var aiResponse = await _chatService.PostMessage(userMessage);

            // Assert
            Assert.NotNull(aiResponse);
            Assert.False(aiResponse.IsUser);

            var expectedResponse = mockResponse.choices[0].message.content;
            Assert.Equal(expectedResponse, aiResponse.Text);
        }

        [Fact]
        public async Task CreateChatOK()
        {
            // Arrange
            var chat = new Chat
            {
                Title = "newChat",
                User = _user
            };

            // Act
            var newChat = await _chatService.PostChat(chat);

            // Assert
            Assert.NotNull(newChat);
            Assert.Equal(_user, newChat.User);
            Assert.Equal(chat.Title, newChat.Title);
        }

        [Fact]
        public async Task DeleteChatOK()
        {
            // Arrange
            var chatTitle = "chatToDelete";
            var chat = await CreateAndSaveChatAsync(chatTitle);
            Assert.NotNull(await _context.Chat.Where(c => c.Title == chatTitle).FirstOrDefaultAsync());

            // Act
            await _chatService.DeleteChat(chat.Id);
            
            // Assert
            Assert.Null(await _context.Chat.Where(c => c.Title == chatTitle).FirstOrDefaultAsync());
        }

        [Fact]
        public async Task GetAllChatsOK()
        {
            // Arrange
            await CreateAndSaveChatAsync("chat1");
            await CreateAndSaveChatAsync("chat2");

            // Act 
            var chats = await _chatService.GetChats(_user);

            // Assert
            Assert.NotNull(chats);
            Assert.Equal(2, chats.Count);
        }

        [Fact]
        public async Task GetAllMessagesOK()
        {
            // Arrange
            var chat = await CreateAndSaveChatAsync("chatWithMessages");
            await CreateAndSaveMessageAsync("message1", DateTime.UtcNow, chat.Id);
            await CreateAndSaveMessageAsync("message2", DateTime.UtcNow, chat.Id);

            // Act
            var messages = await _chatService.GetMessages(chat.Id);

            // Assert
            Assert.NotNull(messages);
            Assert.Equal(2, messages.Count);
            Assert.All(messages, m => Assert.Equal(chat.Id, m.ChatId));
        }
    }
}
