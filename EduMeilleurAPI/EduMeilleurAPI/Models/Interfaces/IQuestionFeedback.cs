namespace EduMeilleurAPI.Models.Interfaces;

public interface IQuestionFeedback
{
    int Id { get; set; }
    string Title { get; set; }
    string Message { get; set; }

    User user { get; set; }
    List<Picture> Pictures { get; set; }
    List<Attachment> Attachments { get; set; }
}