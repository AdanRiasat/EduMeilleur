namespace EduMeilleurAPI.Services.Interfaces;

public interface IEmailService
{
    Task SendQuestionConfirmation(string userEmail, string userName, string questionTitle, string questionMessage,
        List<string>? attachmentPaths = null, CancellationToken ct = default);
}