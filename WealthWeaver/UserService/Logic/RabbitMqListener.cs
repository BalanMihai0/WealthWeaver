using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Services
{
    public class RabbitMqListener
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly ITokenVerifier _tokenVerifier;
        private readonly ILogger<RabbitMqListener> _logger;

        public RabbitMqListener(ITokenVerifier tokenVerifier, ILogger<RabbitMqListener> logger)
        {
            _tokenVerifier = tokenVerifier;
            _logger = logger;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.QueueDeclareAsync(queue: "auth_queue", durable: true, exclusive: false, autoDelete: false, arguments: null).GetAwaiter().GetResult();
        }

        public void StartListening()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var validateTokenMessage = JsonSerializer.Deserialize<ValidateTokenRequest>(message);

                if (validateTokenMessage != null)
                {
                    _logger.LogInformation("Received message from 'auth_queue': {Message}", message);
                    bool isValid = await _tokenVerifier.VerifyTokenAsync(validateTokenMessage.Token).ConfigureAwait(true);
                    var responseMessage = new { IsValid = isValid };
                    var responseBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(responseMessage));

                    var properties = new BasicProperties
                    {
                        ContentType = "application/json",
                        DeliveryMode = (DeliveryModes)2 // Persistent
                    };

                    await _channel.BasicPublishAsync(
                        exchange: "",
                        routingKey: validateTokenMessage.ResponseQueue,
                        mandatory: false,
                        basicProperties: properties,
                        body: responseBody
                    ).ConfigureAwait(true);

                    _logger.LogInformation("Sent response to queue '{Queue}': {Response}", validateTokenMessage.ResponseQueue, JsonSerializer.Serialize(responseMessage));
                }

                // Acknowledge the message
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false).ConfigureAwait(true);
            };

            _channel.BasicConsumeAsync(
                queue: "auth_queue",
                autoAck: false,
                consumer: consumer
            ).GetAwaiter().GetResult();
        }

        public async Task StopListeningAsync()
        {
            await _channel.CloseAsync().ConfigureAwait(true);
            await _connection.CloseAsync().ConfigureAwait(true);
        }
    }
}
