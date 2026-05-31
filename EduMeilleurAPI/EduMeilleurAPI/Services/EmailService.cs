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
        var apiKey = config["BREVO_API_KEY"] ?? throw new InvalidOperationException("BREVO_API_KEY missing");

        Configuration.Default.ApiKey["api-key"] = apiKey;
        _api = new TransactionalEmailsApi();

        _senderEmail  = config["BREVO_SENDER_EMAIL"]  ?? throw new InvalidOperationException("BREVO_SENDER_EMAIL missing");
        _senderName   = config["BREVO_SENDER_NAME"]   ?? "EduMeilleur";
        _adminEmail   = config["Admin_Email"]   ?? throw new InvalidOperationException("BREVO_ADMIN_EMAIL missing");
        _adminName    = config["BREVO_ADMIN_NAME"]    ?? "EduMeilleur Support";
    }

    private async Task SendWithTemplate(string toEmail, string toName, long templateId, object templateParams, CancellationToken ct = default)
    {
        var email = new SendSmtpEmail(
            sender: new SendSmtpEmailSender(_senderName, _senderEmail),
            to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(toEmail, toName) },
            templateId: templateId,
            _params: templateParams
        );

        await Task.Run(() => _api.SendTransacEmail(email), ct);
    }
    
    public async Task SendQuestionConfirmation(
        string userEmail, string userName,
        string questionTitle, string questionMessage,
        CancellationToken ct = default)
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
            ct: ct
        );
    }
}