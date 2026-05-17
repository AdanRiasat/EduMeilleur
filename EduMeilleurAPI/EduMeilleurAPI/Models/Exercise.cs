using EduMeilleurAPI.Models.Interfaces;
using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class Exercise : IEducationalItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public int ChapterId { get; set; }
        [JsonIgnore]
        public virtual Chapter Chapter { get; set; }

        [JsonIgnore]
        public virtual List<NoteExercise> NoteExercises { get; set; } = new();
    }
}
