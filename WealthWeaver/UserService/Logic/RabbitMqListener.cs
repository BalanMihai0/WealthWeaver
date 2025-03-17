using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UserService.Interfaces;

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
            _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false).GetAwaiter().GetResult();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    if (message != null)
                    {
                        _logger.LogInformation("Received message from 'auth_queue': {Message}", message);
                        bool responseIsTokenValid = await _tokenVerifier.VerifyTokenAsync(message).ConfigureAwait(true);
                        string responseMessage = responseIsTokenValid.ToString(Constants.DefaultCulture);
                        var responseBody = Encoding.UTF8.GetBytes(responseMessage);

                        var properties = new BasicProperties
                        {
                            ContentType = "application/json",
                            DeliveryMode = (DeliveryModes)2 // Persistent
                        };

                        await _channel.BasicPublishAsync(
                            exchange: "",
                            routingKey: "auth_response_queue",
                            mandatory: false,
                            basicProperties: properties,
                            body: responseBody
                        ).ConfigureAwait(false);

                        _logger.LogInformation("Sent response to queue '{Queue}': {Response}", "auth_response_queue", responseIsTokenValid);
                    }

                    // Acknowledge the message
                    await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message");
                    // Nack the message and don't requeue it if it's a persistent error
                    await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false).ConfigureAwait(false);
                }
            };

            _channel.BasicConsumeAsync(
                queue: "auth_queue",
                autoAck: false,
                consumer: consumer
            ).GetAwaiter().GetResult();
        }

        public async Task StopListeningAsync()
        {
            await _channel.CloseAsync().ConfigureAwait(false);
            await _connection.CloseAsync().ConfigureAwait(false);
        }
    }
}
