using Azure.Identity;

namespace EduMeilleurAPI.Models.DTO
{
    public class ProfileDisplayDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Bio {  get; set; } = null!;
        public string? School { get; set; } 
        public string? SchoolYear { get; set; } 
        public int IQPoints { get; set; }

        public ProfileDisplayDTO(User user)
        {
            Username = user.UserName;
            Email = user.Email;
            Bio = user.Bio;
            School = user.School;
            SchoolYear = user.SchoolYear;
            IQPoints = user.IQPoints;
        }
    }
}
