using BankingService.Models;
using Microsoft.OpenApi.MicrosoftExtensions;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BankingService.Logic
{
    public class RabbitMQClient
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly ILogger<RabbitMQClient> _logger;

        public RabbitMQClient(ILogger<RabbitMQClient> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            _channel.QueueDeclareAsync(queue: "auth_queue", durable: true, exclusive: false, autoDelete: false, arguments: null).GetAwaiter().GetResult();
            _channel.QueueDeclareAsync(queue: "auth_response_queue", durable: true, exclusive: false, autoDelete: false, arguments: null).GetAwaiter().GetResult();
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                var properties = new BasicProperties
                {
                    ContentType = "application/json",
                    DeliveryMode = (DeliveryModes)2 // Persistent delivery mode
                };

                var body = Encoding.UTF8.GetBytes(message);

                await _channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: "auth_queue",
                    mandatory: false,
                    basicProperties: properties,
                    body: body
                ).ConfigureAwait(true);

                _logger.LogInformation("Message sent to queue 'auth_queue': {ErrorMessage}", JsonSerializer.Serialize(message));
            }
            catch (RabbitMQ.Client.Exceptions.AlreadyClosedException ex)
            {
                _logger.LogError("RabbitMQ connection already closed: {ErrorMessage}", ex.Message);
            }
            catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException ex)
            {
                _logger.LogError("RabbitMQ broker unreachable: {ErrorMessage}", ex.Message);
            }
        }

        public async Task CloseAsync()
        {
            await _channel.CloseAsync().ConfigureAwait(true);
            await _connection.CloseAsync().ConfigureAwait(true);
        }
    }
}
