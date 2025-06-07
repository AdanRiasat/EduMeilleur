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

        public virtual List<Exercise> Exercises { get; set; } = new List<Exercise>();
        public virtual List<Notes> Notes { get; set; } = new List<Notes>();
        public virtual List<Video> Videos { get; set; } = new List<Video>();
    }
}
