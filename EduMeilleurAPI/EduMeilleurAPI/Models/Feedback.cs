using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;

        [JsonIgnore]
        public virtual User user { get; set; } = null!;
        [JsonIgnore]
        public virtual List<Picture> Pictures { get; set; } = new List<Picture>();
        [JsonIgnore]
        public virtual List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
