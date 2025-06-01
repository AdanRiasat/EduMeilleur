using Azure.Identity;

namespace EduMeilleurAPI.Models.DTO
{
    public class ProfileDisplayDTO
    {
        public string Username { get; set; } = null!;
        public string Bio {  get; set; } = null!;
        public string? School { get; set; } 
        public string? SchoolYear { get; set; } 

        public ProfileDisplayDTO(string username, string bio, string school, string schoolYear)
        {
            Username = username;
            Bio = bio;
            School = school;
            SchoolYear = schoolYear;
        }
    }
}
