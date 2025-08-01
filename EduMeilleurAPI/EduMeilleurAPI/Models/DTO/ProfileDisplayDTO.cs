using Azure.Identity;

namespace EduMeilleurAPI.Models.DTO
{
    public class ProfileDisplayDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Bio {  get; set; } = null!;
        public string? School { get; set; } 
        public int? SchoolId { get; set; }
        public int? SchoolYear { get; set; } 
        public int IQPoints { get; set; }

        public ProfileDisplayDTO(User user)
        {
            Username = user.UserName;
            Email = user.Email;
            Bio = user.Bio;
            IQPoints = user.IQPoints;

            if (user.School != null)
            {
                School = user.School.Name;
                SchoolId = user.School.Id;
            }
                

            if (user.SchoolYear != null)
                SchoolYear = user.SchoolYear;
        }
    }
}
