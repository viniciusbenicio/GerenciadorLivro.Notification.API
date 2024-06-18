namespace GerenciadorLivro.Notification.API.Services
{
    public interface IEmailService
    {
        Task<bool> Send(string toName, string toEmail, string subject, string body, string fromName = "", string fromEmail = "");
    }
}
