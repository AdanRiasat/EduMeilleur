namespace EduMeilleurAPI.Services.Interfaces;

public interface IEmailService
{
    Task SendQuestionConfirmation(string userEmail, string userName, string questionTitle, string questionMessage,
        List<string>? attachmentPaths = null, CancellationToken ct = default);

    Task SendQuestionToTeacher(string questionTitle, string questionMessage, string studentUserName,
        string studentEmail, List<string>? attachmentPaths = null, string userName = "Amir", string userEmail = "amirhal@outlook.fr", CancellationToken ct = default);

    Task SendFeedbackConfirmation(string userEmail, string userName, string feedbackTitle, string feedbackMessage,
        List<string>? attachmentPaths = null, CancellationToken ct = default);

    Task SendFeedbackToAdmin(string feedbackTitle, string feedbackMessage, string studentUserName,
        string studentEmail, List<string>? attachmentPaths = null, CancellationToken ct = default);

}