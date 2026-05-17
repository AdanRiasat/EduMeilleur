using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class NoteExercise
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        [JsonIgnore]
        public virtual Notes Note {  get; set; }
        public int ExerciseId { get; set; }
        [JsonIgnore]
        public virtual Exercise? Exercise { get; set; }
    }
}
