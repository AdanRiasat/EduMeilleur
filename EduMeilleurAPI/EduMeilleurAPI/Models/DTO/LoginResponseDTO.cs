namespace EduMeilleurAPI.Models.DTO;

public class LoginResponseDTO
{
    public string Token { get; set; } = null!;
    public DateTime ValidTo { get; set; }
    public string RefreshToken { get; set; } = null!;
    public string Username { get; set; } = null!;
    public IList<string> Roles { get; set; } = [];
}