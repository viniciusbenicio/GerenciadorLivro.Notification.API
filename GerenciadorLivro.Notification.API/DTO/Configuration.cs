namespace GerenciadorLivro.Notification.API.DTO
{
    public class Configuration
    {
        public static SmtpConfiguration Smtp = new();
        public class SmtpConfiguration
        {
            public string Host { get; set; } = string.Empty;    
            public int Port { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}
