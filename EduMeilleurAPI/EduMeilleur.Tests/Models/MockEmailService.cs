using EduMeilleurAPI.Services.Interfaces;

namespace EduMeilleur.Tests.Models;

public class MockEmailService : IEmailService
{
    public Task SendQuestionConfirmation(
        string userEmail, string userName,
        string questionTitle, string questionMessage,
        List<string>? attachmentPaths = null,
        CancellationToken ct = default)
        => Task.CompletedTask;
}