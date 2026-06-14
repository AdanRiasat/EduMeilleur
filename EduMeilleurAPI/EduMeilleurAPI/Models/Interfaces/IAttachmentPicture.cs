namespace EduMeilleurAPI.Models.Interfaces;

public interface IAttachmentPicture
{
    int Id { get; set; }
    string FileName { get; set; }
    string MimeType { get; set; }

    int? QuestionTeacherId { get; set; }
    QuestionTeacher? QuestionTeacher { get; set; }

    int? FeedbackId { get; set; }
    Feedback? Feedback { get; set; }
}