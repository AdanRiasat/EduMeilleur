using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EduMeilleurAPI.Services;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EduMeilleurAPIContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EduMeilleurAPIContext") ?? throw new InvalidOperationException("Connection string 'EduMeilleurAPIContext' not found."));
    options.UseLazyLoadingProxies(); 
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.SaveToken = true; 
    options.RequireHttpsMetadata = false; // Lors du développement on peut laisser à false
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = "http://localhost:4200", // Audience : Client
        ValidIssuer = "https://localhost:7027", // ⛔ Issuer : Serveur -> HTTPS VÉRIFIEZ le PORT de votre serveur dans launchsettings.json !
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes("LooOOongue Phrase SiNoN Ça ne Marchera PaAaAAAaAas !")) // Clé pour déchiffrer les tokens
    };
});

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<EduMeilleurAPIContext>();


builder.Services.Configure<IdentityOptions>(options => //modify later
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<NotesService>();
builder.Services.AddScoped<QuestionService>();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
