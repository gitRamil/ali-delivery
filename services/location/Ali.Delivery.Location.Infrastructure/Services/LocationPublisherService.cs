using System.Reflection;
using System.Text.Json;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.WebApi;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class RabbitMQConnectionFactory
{
    private readonly RabbitMQConfiguration _configuration;

    public RabbitMQConnectionFactory(RabbitMQConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IConnection CreateConnection()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration.HostName,
            Port = _configuration.Port,
            UserName = _configuration.UserName,
            Password = _configuration.Password,
            VirtualHost = _configuration.VirtualHost,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };

        return factory.CreateConnection();
    }
}

[RabbitMQMessage("user.exchange", "user.created")]
public class UserCreatedMessage
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class RabbitMQPublisherService : IPublisherService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabbitMQConfiguration _configuration;
    private readonly ILogger<RabbitMQPublisherService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public RabbitMQPublisherService(
        IConnection connection,
        RabbitMQConfiguration configuration,
        ILogger<RabbitMQPublisherService> logger)
    {
        _connection = connection;
        _channel = _connection.CreateModel();
        _configuration = configuration;
        _logger = logger;
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        InitializeInfrastructure();
    }

    private void InitializeInfrastructure()
    {
        // Создаем exchanges
        foreach (var exchange in _configuration.Exchanges.Values)
        {
            _channel.ExchangeDeclare(
                exchange: exchange.Name,
                type: exchange.Type,
                durable: exchange.Durable,
                autoDelete: exchange.AutoDelete);
        }

        // Создаем queues и bindings
        foreach (var queue in _configuration.Queues.Values)
        {
            _channel.QueueDeclare(
                queue: queue.Name,
                durable: queue.Durable,
                exclusive: queue.Exclusive,
                autoDelete: queue.AutoDelete);

            if (!string.IsNullOrEmpty(queue.Exchange))
            {
                _channel.QueueBind(
                    queue: queue.Name,
                    exchange: queue.Exchange,
                    routingKey: queue.RoutingKey ?? "");
            }
        }
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
    {
        var messageType = typeof(TMessage);
        var attribute = messageType.GetCustomAttribute<RabbitMQMessageAttribute>();
        
        if (attribute == null)
        {
            throw new InvalidOperationException($"Message type {messageType.Name} must have RabbitMQMessageAttribute");
        }

        await PublishAsync(message, attribute.Exchange, attribute.RoutingKey, cancellationToken);
    }

    public async Task PublishAsync<TMessage>(TMessage message, string routingKey, CancellationToken cancellationToken = default)
    {
        // Используем exchange по умолчанию для типа сообщения или первый доступный
        var messageType = typeof(TMessage);
        var attribute = messageType.GetCustomAttribute<RabbitMQMessageAttribute>();
        var exchange = attribute?.Exchange ?? _configuration.Exchanges.Keys.FirstOrDefault() ?? "";

        await PublishAsync(message, exchange, routingKey, cancellationToken);
    }

    public async Task PublishAsync<TMessage>(TMessage message, string exchange, string routingKey, CancellationToken cancellationToken = default)
    {
        try
        {
            var messageBody = JsonSerializer.SerializeToUtf8Bytes(message, _jsonOptions);
            
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.MessageId = Guid.NewGuid().ToString();
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            properties.Type = typeof(TMessage).Name;
            properties.ContentType = "application/json";

            await Task.Run(() =>
            {
                _channel.BasicPublish(
                    exchange: exchange,
                    routingKey: routingKey,
                    basicProperties: properties,
                    body: messageBody);
            }, cancellationToken);

            _logger.LogInformation("Published message {MessageType} to exchange {Exchange} with routing key {RoutingKey}",
                typeof(TMessage).Name, exchange, routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish message {MessageType} to exchange {Exchange} with routing key {RoutingKey}",
                typeof(TMessage).Name, exchange, routingKey);
            throw;
        }
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
    }
}