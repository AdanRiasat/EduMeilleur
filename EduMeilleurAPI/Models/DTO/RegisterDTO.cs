namespace EduMeilleurAPI.Models.DTO
{
    public class RegisterDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        
        public string? School { get; set; } 
        public string? SchoolYear { get; set; }
    }
}
