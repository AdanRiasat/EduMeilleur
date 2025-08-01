using Microsoft.AspNetCore.Identity;

namespace EduMeilleurAPI.Models
{
    public class User : IdentityUser
    {
        public string? Bio { get; set; }
        public int IQPoints { get; set; }
        public int? SchoolYear { get; set; }

        //Pfp
        public string? FileName { get; set; }
        public string? MimeType { get; set; }

        public int? SchoolId { get; set; }
        public virtual School? School { get; set; } = null!;

        public virtual List<QuestionTeacher>? Questions { get; set; }
        public virtual List<Feedback>? Feedbacks { get; set; }
        public virtual List<Chat>? Chats { get; set; }
        
    }
}
