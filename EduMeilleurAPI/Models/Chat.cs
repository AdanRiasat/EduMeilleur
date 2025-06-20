using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        [JsonIgnore]
        public virtual User User { get; set; } = null!;

        public virtual List<ChatMessage>? Messages { get; set; }
    }
}
