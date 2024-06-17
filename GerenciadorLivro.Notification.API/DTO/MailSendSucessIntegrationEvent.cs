namespace GerenciadorLivro.Notification.API.DTO
{
    public class MailSendSucessIntegrationEvent
    {
        public MailSendSucessIntegrationEvent(int idMail)
        {
            IdMail = idMail;
        }

        public int IdMail { get; set; }
    }
}
