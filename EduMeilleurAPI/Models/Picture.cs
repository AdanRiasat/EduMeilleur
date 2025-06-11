namespace EduMeilleurAPI.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string MimeType { get; set; } = null!;

        public int? QuestionTeacherId { get; set; }
        public virtual QuestionTeacher? QuestionTeacher { get; set; }
    }
}
