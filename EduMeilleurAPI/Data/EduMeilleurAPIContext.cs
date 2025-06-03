using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EduMeilleurAPI.Data
{
    public class EduMeilleurAPIContext : IdentityDbContext<User>
    {
        public EduMeilleurAPIContext (DbContextOptions<EduMeilleurAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "teacher", NormalizedName = "TEACHER" }
            );
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            User u1 = new User
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "admin",
                Email = "bobibou@mail.com",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "BOBIBOU@MAIL.COM"
            };
            User u2 = new User
            {
                Id = "11111111-1111-1111-1111-111111111112",
                UserName = "teacher1",
                Email = "bobibo@mail.com",
                NormalizedUserName = "TEACHER1",
                NormalizedEmail = "BOBIBO@MAIL.COM"
            };
            u1.PasswordHash = hasher.HashPassword(u1, "alloo"); //change later, please
            builder.Entity<User>().HasData(u1);

            u2.PasswordHash = hasher.HashPassword(u2, "alloo"); //change later, please
            builder.Entity<User>().HasData(u2);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = u1.Id, RoleId = "1" },
                new IdentityUserRole<string> { UserId = u2.Id, RoleId = "2"}
            );
        }



        public DbSet<EduMeilleurAPI.Models.Picture> Picture { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Subject> Subject { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Notes> Notes { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.QuestionTeacher> QuestionTeacher { get; set; } = default!;
    }
}
