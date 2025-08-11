using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace EduMeilleurAPI.Data
{
    public class EduMeilleurAPIContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _config;

        public EduMeilleurAPIContext (DbContextOptions<EduMeilleurAPIContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "admin", NormalizedName = "ADMIN"},
                new IdentityRole { Id = "2", Name = "teacher", NormalizedName = "TEACHER" }
            );
            PasswordHasher<User> hasher = new PasswordHasher<User>();

            User u1 = new User
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "admin",
                Email = _config["Admin:Email"],
                NormalizedUserName = "ADMIN",
                NormalizedEmail = _config["Admin:Email"].ToUpper(),
                School = null
            };
            User u2 = new User
            {
                Id = "11111111-1111-1111-1111-111111111112",
                UserName = "Robert Lebois",
                Email = "bobibo@mail.com",
                NormalizedUserName = "ROBERT LEBOIS",
                NormalizedEmail = "BOBIBO@MAIL.COM",
                School = null
            };
            User u3 = new User
            {
                Id = "11111111-1111-1111-1111-111111111113",
                UserName = "Jerome Laplante",
                Email = "hmmm@mail.com",
                NormalizedUserName = "JEROME LAPLANTE",
                NormalizedEmail = "HMMM@MAIL.COM",
                School = null
            };
            User u4 = new User
            {
                Id = "11111111-1111-1111-1111-111111111114",
                UserName = "Emily Tremblay",
                Email = "teacher3@mail.com",
                NormalizedUserName = "EMILY TREMBLAY",
                NormalizedEmail = "TEACHER3@MAIL.COM",
                School = null
            };
            User u5 = new User
            {
                Id = "11111111-1111-1111-1111-111111111115",
                UserName = "Tyrone Rochelieu",
                Email = "teacher4@mail.com",
                NormalizedUserName = "TYRONE ROCHELIEU",
                NormalizedEmail = "teacher4@MAIL.COM",
                School = null
            };
            User u6 = new User
            {
                Id = "11111111-1111-1111-1111-111111111116",
                UserName = "Sarah Laide",
                Email = "teacher5@mail.com",
                NormalizedUserName = "SARAH LAIDE",
                NormalizedEmail = "teacher5@MAIL.COM",
                School = null
            };

            u1.PasswordHash = hasher.HashPassword(u1, _config["Admin:Password"]); 
            builder.Entity<User>().HasData(u1);

            u2.PasswordHash = hasher.HashPassword(u2, _config["Teacher:Password"]); 
            builder.Entity<User>().HasData(u2);

            u3.PasswordHash = hasher.HashPassword(u3, _config["Teacher2:Password"]);
            builder.Entity<User>().HasData(u3);

            u4.PasswordHash = hasher.HashPassword(u4, _config["Teacher3:Password"]);
            builder.Entity<User>().HasData(u4);

            u5.PasswordHash = hasher.HashPassword(u5, _config["Teacher4:Password"]);
            builder.Entity<User>().HasData(u5);

            u6.PasswordHash = hasher.HashPassword(u6, _config["Teacher5:Password"]);
            builder.Entity<User>().HasData(u6);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = u1.Id, RoleId = "1" },
                new IdentityUserRole<string> { UserId = u2.Id, RoleId = "2"},
                new IdentityUserRole<string> { UserId = u3.Id, RoleId = "2"},
                new IdentityUserRole<string> { UserId = u4.Id, RoleId = "2"},
                new IdentityUserRole<string> { UserId = u5.Id, RoleId = "2"},
                new IdentityUserRole<string> { UserId = u6.Id, RoleId = "2"}
            );

            builder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "SN4", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Math" },
                new Subject { Id = 2, Name = "SN5", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Math" },
                new Subject { Id = 3, Name = "CST4", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Math" },
                new Subject { Id = 4, Name = "CST5", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Math" },
                new Subject { Id = 5, Name = "TS4", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Math" },
                new Subject { Id = 6, Name = "TS5", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Math" },
                new Subject { Id = 7, Name = "ST", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Science" },
                new Subject { Id = 8, Name = "STE", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Science" },
                new Subject { Id = 9, Name = "Chimie", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Science" },
                new Subject { Id = 10, Name = "Physique", Description = "hello my name is jonh and this is a placeholder because im a pretty little flower and I like to swim yes I really do", Type = "Science" }
            );

            builder.Entity<Chapter>()
            .HasOne(n => n.Subject)
            .WithMany(s => s.Chapters)
            .HasForeignKey("SubjectId");

            builder.Entity<Chapter>().HasData(
                new {Id = 1, Title = "the first one", SubjectId = 1},
                new {Id = 2, Title = "the second one", SubjectId = 1},
                new {Id = 3, Title = "the third one", SubjectId = 1}
            );

            builder.Entity<Notes>().HasData(
                new Notes { Id = 1, Title = "1.1 sigma time with me", Content = "test.md", ChapterId = 1,},
                new Notes {Id = 2, Title = "1.2 erm what the skibidi", Content = "test.md", ChapterId = 1 },
                new Notes { Id = 3, Title = "1* REVISION on skibidi", Content = "test.md", ChapterId = 1 },
                new Notes { Id = 4, Title = "2.1 is he bothering you?", Content = "test.md", ChapterId = 2,}
            );
            
            builder.Entity<Exercise>().HasData(
                new Exercise {Id = 1, Title = "Pythagore with friends", Content = "testExerc.md", ChapterId = 1},
                new Exercise { Id = 2, Title = "Find the function", Content = "testExerc.md", ChapterId = 1},
                new Exercise { Id = 3, Title = "Simplification", Content = "testExerc.md", ChapterId = 1 }
            );

            builder.Entity<Video>().HasData(
                new Video { Id = 1, Title = "hmmm I cant quite remember", Content = "vidExample.md", ChapterId = 1},
                new Video { Id = 2, Title = "Favorite color?", Content = "vidExample.md", ChapterId = 1},
                new Video { Id = 3, Title = "Sigma vs Alpha", Content = "vidExample.md", ChapterId = 2}
            );


            builder.Entity<Chat>().HasData(
                new Chat { Id = 1, Title = "My first Chat", UserId = u1.Id },
                new Chat { Id = 2, Title = "My second Chat", UserId = u1.Id }
            );

            builder.Entity<School>().HasData(
                new School { Id = 1, Name = "Antoine-Brossard"},
                new School { Id = 2, Name = "Lucille-Teasdale"}
            );
        }

        public DbSet<EduMeilleurAPI.Models.Picture> Picture { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Feedback> Feedbacks { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Attachment> Attachments { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Subject> Subject { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Notes> Notes { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.QuestionTeacher> QuestionTeacher { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Exercise> Exercise { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Video> Video { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Chat> Chat { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Chapter> Chapters { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.ChatMessage> ChatMessages { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.School> Schools { get; set; }

    }
}
