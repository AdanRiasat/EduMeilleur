using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using EduMeilleurAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EduMeilleur.Tests.Client
{
    public class ApiFixture : WebApplicationFactory<Program>
    {
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
                    ["JWT:Key"] = "your-test-secret-key-long-enough",
                    ["JWT:Issuer"] = "test-issuer",
                    ["JWT:Audience"] = "test-audience",
                });
            });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<EduMeilleurAPIContext>));
                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<EduMeilleurAPIContext>(opts =>
                {
                    opts.UseNpgsql("Host=localhost;Database=edmeilleur_test;Username=postgres;Password=zZ1029384756Zz?");
                    opts.UseLazyLoadingProxies();
                });
            });
        }
    }
}
