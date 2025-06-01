using Microsoft.AspNetCore.Identity;

namespace EduMeilleurAPI.Models
{
    public class User : IdentityUser
    {
        public string? Bio { get; set; }
        public int IQPoints { get; set; }
        public string? School { get; set; }
        public string? SchoolYear { get; set; }

        //Pfp
        public string? FileName { get; set; }
        public string? MimeType { get; set; }
    }
}
