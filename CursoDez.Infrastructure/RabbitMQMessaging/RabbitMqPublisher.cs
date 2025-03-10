using CursoDez.Application.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace CursoDez.Infrastructure.RabbitMQMessaging
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly RabbitMqConfigSettings _rabbitMqConfigSettings;

        public RabbitMqPublisher(IOptions<RabbitMqConfigSettings> rabbitMqConfigSettings)
        {
            _rabbitMqConfigSettings = rabbitMqConfigSettings.Value;
        }

        public async Task PublishMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqConfigSettings.HostName,
                Port = _rabbitMqConfigSettings.Port,
                UserName = _rabbitMqConfigSettings.UserName,
                Password = _rabbitMqConfigSettings.Password,
                VirtualHost = _rabbitMqConfigSettings.VirtualHost
            };

            try
            {
                using (var connection = await factory.CreateConnectionAsync())
                using (var channel = await connection.CreateChannelAsync())
                {
                    // Declara a fila
                    await channel.QueueDeclareAsync(queue: _rabbitMqConfigSettings.QueueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    // Envia a mensagem para a fila
                    await channel.BasicPublishAsync(exchange: "",
                                         routingKey: _rabbitMqConfigSettings.QueueName,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine($"Erro ao conectar ao RabbitMQ: {ex.Message}");
            }
        }
    }
}
