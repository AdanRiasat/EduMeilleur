using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EduMeilleurAPI.Data
{
    public class EduMeilleurAPIContext : IdentityDbContext<User>
    {
        public EduMeilleurAPIContext (DbContextOptions<EduMeilleurAPIContext> options)
            : base(options)
        {
        }

        public DbSet<EduMeilleurAPI.Models.Picture> Picture { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Subject> Subject { get; set; } = default!;
        public DbSet<EduMeilleurAPI.Models.Notes> Notes { get; set; } = default!;
    }
}
