using Azure.Identity;

namespace EduMeilleurAPI.Models.DTO
{
    public class ProfileDisplayDTO
    {
        public string Username { get; set; } = null!;
        public string? Bio {  get; set; } = null!;
        public string? School { get; set; } 
        public string? SchoolYear { get; set; } 

        public ProfileDisplayDTO(User user)
        {
            Username = user.UserName;
            Bio = user.Bio;
            School = user.School;
            SchoolYear = user.SchoolYear;
        }
    }
}
