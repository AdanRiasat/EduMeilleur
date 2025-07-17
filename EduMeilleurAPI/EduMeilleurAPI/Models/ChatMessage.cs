using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public bool IsUser { get; set; }
        public DateTime TimeStamp { get; set; }

        public int ChatId { get; set; }
        [JsonIgnore]
        public virtual Chat? Chat { get; set; } = null!;
    }
}
