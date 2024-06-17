using GerenciadorLivro.Notification.API.DTO;
using GerenciadorLivro.Notification.API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GerenciadorLivro.Notification.API.Consumers
{
    public class SendEmailConsumer : BackgroundService
    {
        private const string QUEUE = "Emails";
        private const string SEND_MAIL_SUCESSS_QUEUE = "MailSendSucess";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        public SendEmailConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: SEND_MAIL_SUCESSS_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, e) =>
            {
                var byteArray = e.Body.ToArray();
                var sendMailInfoJson = Encoding.UTF8.GetString(byteArray);

                var mailInfo = JsonSerializer.Deserialize<MailInfoInputModel>(sendMailInfoJson);

                ProcessSendMail(mailInfo);

                var emailSendSucess = new MailSendSucessIntegrationEvent(mailInfo.IdMail);
                var emailSendSucessJson = JsonSerializer.Serialize<dynamic>(emailSendSucess);
                var emailSendSucessJsonBytes = Encoding.UTF8.GetBytes(emailSendSucessJson);
                _channel.BasicPublish(exchange: "", routingKey: SEND_MAIL_SUCESSS_QUEUE, basicProperties: null, body: emailSendSucessJsonBytes);

                _channel.BasicAck(e.DeliveryTag, false);


            };

            _channel.BasicConsume(QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        private void ProcessSendMail(MailInfoInputModel model)
        {
            using var scope = _serviceProvider.CreateScope();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            emailService.Send(model.toName, model.toEmail, model.subject, model.body, model.fromName, model.fromEmail);
        }
    }
}
