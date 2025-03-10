using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BankingService.Logic
{
    public class RabbitMqResponseListener
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly ILogger<RabbitMqResponseListener> _logger;

        public RabbitMqResponseListener(ILogger<RabbitMqResponseListener> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.QueueDeclareAsync(queue: "auth_response_queue", durable: true, exclusive: false, autoDelete: false, arguments: null).GetAwaiter().GetResult();
        }

        public void StartListening()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var response = JsonSerializer.Deserialize<dynamic>(message);

                if (response != null)
                {
                    _logger.LogInformation("BANKING RECEIVED MESSAGE RESPONSE");
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false).ConfigureAwait(true);
            };

            _channel.BasicConsumeAsync(
                queue: "auth_response_queue",
                autoAck: false,
                consumer: consumer
            ).GetAwaiter().GetResult();
        }

        public async Task CloseAsync()
        {
            await _channel.CloseAsync().ConfigureAwait(true);
            await _connection.CloseAsync().ConfigureAwait(true);
        }
    }
}
