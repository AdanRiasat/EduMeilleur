using EduMeilleurAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduMeilleur.Tests.Client
{
    [Collection("Sequential")]
    public class QuestionTests : IClassFixture<ApiFixture>
    {
        private readonly HttpClient _client;
        private readonly ApiFixture _fixture;

        public QuestionTests(ApiFixture fixture)
        {
            _client = fixture.CreateClient();
            _fixture = fixture;
        }

        [Fact]
        public async Task PostQuestionTeacher_WithoutToken_ReturnsUnauthorized()
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Test Title"), "title");
            form.Add(new StringContent("Test Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostQuestionTeacher", form);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostQuestionTeacher_WithToken_ReturnsOk()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Test Title"), "title");
            form.Add(new StringContent("Test Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostQuestionTeacher", form);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostQuestionTeacher_MissingTitle_ReturnsBadRequest()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Test Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostQuestionTeacher", form);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostFeedback_WithToken_ReturnsOk()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Feedback Title"), "title");
            form.Add(new StringContent("Feedback Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostFeedback", form);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostFeedback_WithoutToken_ReturnsUnauthorized()
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Feedback Title"), "title");
            form.Add(new StringContent("Feedback Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostFeedback", form);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostFeedback_MissingTitle_ReturnsBadRequest()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Test Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostFeedback", form);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public async Task PostQuestionTeacher_TitleTooLong_ReturnsBadRequest()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent(new string('a', 51)), "title");
            form.Add(new StringContent("Test Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostQuestionTeacher", form);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostQuestionTeacher_MessageTooLong_ReturnsBadRequest()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Test Title"), "title");
            form.Add(new StringContent(new string('a', 1501)), "message");

            var response = await _client.PostAsync("/api/Questions/PostQuestionTeacher", form);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostQuestionTeacher_TitleAtLimit_ReturnsOk()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent(new string('a', 50)), "title");
            form.Add(new StringContent("Test Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostQuestionTeacher", form);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostQuestionTeacher_MessageAtLimit_ReturnsOk()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Test Title"), "title");
            form.Add(new StringContent(new string('a', 1500)), "message");

            var response = await _client.PostAsync("/api/Questions/PostQuestionTeacher", form);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostFeedback_TitleTooLong_ReturnsBadRequest()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent(new string('a', 51)), "title");
            form.Add(new StringContent("Test Message"), "message");

            var response = await _client.PostAsync("/api/Questions/PostFeedback", form);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostFeedback_MessageTooLong_ReturnsBadRequest()
        {
            var token = await _fixture.GetTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var form = new MultipartFormDataContent();
            form.Add(new StringContent("Test Title"), "title");
            form.Add(new StringContent(new string('a', 1501)), "message");

            var response = await _client.PostAsync("/api/Questions/PostFeedback", form);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

    public record LoginResponse(string Token, DateTime ValidTo, object Profile, string RefreshToken, List<string> Roles);
}
