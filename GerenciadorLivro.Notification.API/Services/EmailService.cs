
using System.Net.Mail;
using System.Net;
using GerenciadorLivro.Notification.API.DTO;

namespace GerenciadorLivro.Notification.API.Services
{
    public class EmailService : IEmailService
    {
        public bool Send(string toName, string toEmail, string subject, string body, string fromName = "", string fromEmail = "")
        {
            var smptClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);

            smptClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.EnableSsl = true;

            var mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail, toName));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                smptClient.Send(mail);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
