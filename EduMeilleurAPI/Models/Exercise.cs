using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string MimeType { get; set; } = null!;

        [JsonIgnore]
        public virtual Subject Subject { get; set; } = null!;
    }
}
