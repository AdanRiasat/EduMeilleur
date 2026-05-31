using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using brevo_csharp.Api;
using brevo_csharp.Model;
using Configuration = brevo_csharp.Client.Configuration;
using Task = System.Threading.Tasks.Task;

namespace EduMeilleurAPI.Services;

public class EmailService
{
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
        _adminName    = config["BREVO:ADMIN:NAME"]    ?? "EduMeilleur Support";
    }

    private async Task SendWithTemplate(string toEmail, string toName, long templateId, object templateParams, List<string>? filePaths = null, CancellationToken ct = default)
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
        await SendWithTemplate(
            toEmail: userEmail,
            toName: userName,
            templateId: 4,
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
}