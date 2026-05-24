using EduMeilleurAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduMeilleur.Tests.Client
{
    [Collection("Sequential")]
    public class SubjectTests : IClassFixture<ApiFixture>
    {
        private readonly HttpClient _client;

        public SubjectTests(ApiFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task GetSubjects_ReturnsListOfSubjects()
        {
            var response = await _client.GetAsync("/api/Subjects/GetSubjects");
            var result = await response.Content.ReadFromJsonAsync<List<SubjectDisplayDTO>>();

            Assert.NotNull(result);
            Assert.Equal(10, result.Count);
            Assert.Contains(result, s => s.Name == "SN4");
        }

        [Fact]
        public async Task GetSubject_ValidId()
        {
            var response = await _client.GetAsync("/api/Subjects/GetSubject/1");
            var result = await response.Content.ReadFromJsonAsync<SubjectDisplayDTO>();

            Assert.NotNull(result);
            Assert.Equal("SN4", result.Name);
        }

        [Fact]
        public async Task GetSubject_InvalidId_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/Subjects/GetSubject/99999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

   
}
