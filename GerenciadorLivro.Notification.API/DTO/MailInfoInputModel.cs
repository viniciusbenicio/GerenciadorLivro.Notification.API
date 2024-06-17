namespace GerenciadorLivro.Notification.API.DTO
{
    public class MailInfoInputModel
    {
        public int IdMail { get; set; }
        public string toName { get; set; } = string.Empty;
        public string toEmail { get; set; } = string.Empty;
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
        public string fromName { get; set; } = string.Empty;
        public string fromEmail { get; set; } = string.Empty;
    }
}
