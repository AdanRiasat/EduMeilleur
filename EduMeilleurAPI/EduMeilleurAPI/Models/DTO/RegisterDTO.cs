namespace EduMeilleurAPI.Models.DTO
{
    public class RegisterDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        
        public int? SchoolId { get; set; } 
        public int? SchoolYear { get; set; }
    }
}
