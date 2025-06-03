using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class QuestionTeacher
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;

        [JsonIgnore]
        public virtual User user { get; set; } = null!; 
    }
}
