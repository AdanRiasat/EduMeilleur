using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Linq;

namespace EduMeilleur.Tests.Client
{
    public class ApiFixture : WebApplicationFactory<Program>
    {
        public const string TestUsername = "TestUser1";
        public const string TestEmail = "Test@email.com";
        public const string TestPassword = "Test123!";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Admin:Email"] = "admin@test.com",
                    ["Admin:Password"] = "Test123!",
                    ["Teacher:Password"] = "Test123!",
                    ["Teacher2:Password"] = "Test123!",
                    ["Teacher3:Password"] = "Test123!",
                    ["Teacher4:Password"] = "Test123!",
                    ["Teacher5:Password"] = "Test123!",
                });
            });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<EduMeilleurAPIContext>));
                if (descriptor != null) services.Remove(descriptor);

                var connString = Environment.GetEnvironmentVariable("ConnectionStrings__EduMeilleurAPIContext")
                    ?? "Host=localhost;Database=edmeilleur_test;Username=test;Password=alloo";

                services.AddDbContext<EduMeilleurAPIContext>(opts =>
                {
                    opts.UseNpgsql(connString);
                    opts.UseLazyLoadingProxies();
                });
            });

            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Warning);
            });
        }

        private async Task SeedTestUserAsync()
        {
            using var scope = Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (await userManager.FindByEmailAsync(TestEmail) != null) return;

            var user = new User
            {
                UserName = TestUsername,
                Email = TestEmail,
                NormalizedUserName = TestUsername.ToUpper(),
                NormalizedEmail = TestEmail.ToUpper(),
            };

            await userManager.CreateAsync(user, "Test123!");
        }

        public async Task<string> GetTokenAsync(HttpClient client)
        {
            await SeedTestUserAsync();

            var dto = new LoginDTO { Username = TestEmail, Password = TestPassword };
            var response = await client.PostAsJsonAsync("/api/Users/Login", dto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!.Token;
        }
    }
}
