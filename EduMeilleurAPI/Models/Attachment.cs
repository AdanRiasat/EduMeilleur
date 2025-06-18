namespace EduMeilleurAPI.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Filename { get; set; } = null!;
        public string MimeType { get; set; } = null!;

        public int? QuestionTeacherId { get; set; }
        public virtual QuestionTeacher? QuestionTeacher { get; set; }

        public int? FeedbackId { get; set; }
        public virtual Feedback? Feedback { get; set; }
    }
}
