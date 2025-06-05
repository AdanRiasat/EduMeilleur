using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        [JsonIgnore]
        public virtual Notes Notes { get; set; } = null!;
    }
}
