namespace GerenciadorLivro.Notification.API.Services
{
    public interface IEmailService
    {
        bool Send(string toName, string toEmail, string subject, string body, string fromName = "", string fromEmail = "");
    }
}
