using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public int SubjectId { get; set; }
        [JsonIgnore]
        public virtual Subject Subject { get; set; } = null!;

        public virtual List<Exercise> Exercise { get; set; } = new List<Exercise>();
    }
}
