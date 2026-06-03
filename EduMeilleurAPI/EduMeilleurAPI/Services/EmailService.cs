using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using brevo_csharp.Api;
using brevo_csharp.Model;
using EduMeilleurAPI.Services.Interfaces;
using Configuration = brevo_csharp.Client.Configuration;
using Task = System.Threading.Tasks.Task;

namespace EduMeilleurAPI.Services;

public class EmailService : IEmailService
{
    private const int QUESTION_CONFIRMATION_TEMPLATE_ID = 4;
    private const int FEEDBACK_CONFIRMATION_TEMPLATE_ID = 9;
    private const int FOR_TEACHER_TEMPLATE_ID = 10;
    private const int FOR_ADMIN_TEMPLATE_ID = 11;
    
    private readonly TransactionalEmailsApi _api;
    private readonly string _senderEmail;
    private readonly string _senderName;
    private readonly string _adminEmail;
    private readonly string _adminName;

    public EmailService(IConfiguration config)
    {
        var apiKey = config["BREVO:API:KEY"] ?? throw new InvalidOperationException("BREVO_API_KEY missing");

        Configuration.Default.ApiKey["api-key"] = apiKey;
        _api = new TransactionalEmailsApi();

        _senderEmail  = config["BREVO:SENDER:EMAIL"]  ?? throw new InvalidOperationException("BREVO_SENDER_EMAIL missing");
        _senderName   = config["BREVO:SENDER:NAME"]   ?? "EduMeilleur";
        _adminEmail   = config["Admin:Email"]   ?? throw new InvalidOperationException("BREVO_ADMIN_EMAIL missing");
        _adminName    = config["ADMIN:NAME"]    ?? "Admin";
    }

    private async Task SendEmail(string toEmail, string toName, long templateId, object templateParams, List<string>? filePaths = null, CancellationToken ct = default)
    {
        var emailAttachments = new List<SendSmtpEmailAttachment>();

        if (filePaths != null)
        {
            foreach (var path in filePaths)
            {
                var bytes = await File.ReadAllBytesAsync(path, ct);
                emailAttachments.Add(new SendSmtpEmailAttachment(null, bytes, Path.GetFileName(path)));
            }
        }
        
        var email = new SendSmtpEmail(
            sender: new SendSmtpEmailSender(_senderName, _senderEmail),
            to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(toEmail, toName) },
            templateId: templateId,
            _params: templateParams,
            attachment: emailAttachments.Any() ? emailAttachments : null
        );

        await Task.Run(() => _api.SendTransacEmail(email), ct);
    }
    
    public async Task SendQuestionConfirmation(string userEmail, string userName, string questionTitle, string questionMessage, List<string>? attachmentPaths = null, CancellationToken ct = default)
    {
        await SendEmail(
            toEmail: userEmail,
            toName: userName,
            templateId: QUESTION_CONFIRMATION_TEMPLATE_ID,
            templateParams: new
            {
                userName = userName,
                questionTitle = questionTitle,
                questionMessage = questionMessage
            },
            filePaths: attachmentPaths,
            ct: ct
        );
    }
    
    public async Task SendQuestionToTeacher(string questionTitle, string questionMessage, string studentUserName, string studentEmail, List<string>? attachmentPaths = null, string userName = "Amir", string userEmail = "amirhal@outlook.fr", CancellationToken ct = default)
    {
        await SendEmail(
            toEmail: userEmail,
            toName: userName,
            templateId: FOR_TEACHER_TEMPLATE_ID,
            templateParams: new
            {
                userName = userName,
                questionTitle = questionTitle,
                questionMessage = questionMessage,
                studentUserName = studentUserName,
                studentEmail = studentEmail
            },
            filePaths: attachmentPaths,
            ct: ct
        );
    }
    
    public async Task SendFeedbackConfirmation(string userEmail, string userName, string feedbackTitle, string feedbackMessage, List<string>? attachmentPaths = null, CancellationToken ct = default)
    {
        await SendEmail(
            toEmail: userEmail,
            toName: userName,
            templateId: FEEDBACK_CONFIRMATION_TEMPLATE_ID,
            templateParams: new
            {
                userName = userName,
                feedbackTitle = feedbackTitle,
                feedbackMessage = feedbackMessage
            },
            filePaths: attachmentPaths,
            ct: ct
        );
    }
    
    public async Task SendFeedbackToAdmin(string feedbackTitle, string feedbackMessage, string studentUserName, string studentEmail, List<string>? attachmentPaths = null, CancellationToken ct = default)
    {
        await SendEmail(
            toEmail: _adminEmail,
            toName: _adminName,
            templateId: FOR_ADMIN_TEMPLATE_ID,
            templateParams: new
            {
                userName = _adminName,
                feedbackTitle = feedbackTitle,
                feedbackMessage = feedbackMessage,
                studentUserName = studentUserName,
                studentEmail = studentEmail
            },
            filePaths: attachmentPaths,
            ct: ct
        );
    }
    
    
}