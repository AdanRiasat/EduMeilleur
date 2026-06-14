using System.Text.Json.Serialization;
using EduMeilleurAPI.Models.Interfaces;

namespace EduMeilleurAPI.Models
{
    public class Feedback : IQuestionFeedback
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
