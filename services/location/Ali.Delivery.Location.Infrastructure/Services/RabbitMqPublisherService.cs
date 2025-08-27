using System.Reflection;
using System.Text.Json;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models.Messages;
using Ali.Delivery.Location.Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Реализация службы публикации сообщений для RabbitMQ.
/// Обеспечивает публикацию сообщений в различные exchange с поддержкой автоматической настройки инфраструктуры.
/// </summary>
public class RabbitMqPublisherService : IPublisherService, IDisposable
{
    private readonly IModel _channel;
    private readonly RabbitMqConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<RabbitMqPublisherService> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RabbitMqPublisherService"/>.
    /// </summary>
    /// <param name="connection">Подключение к RabbitMQ.</param>
    /// <param name="configuration">Конфигурация RabbitMQ.</param>
    /// <param name="logger">Логгер для записи операций.</param>
    public RabbitMqPublisherService(IConnection connection, RabbitMqConfiguration configuration, ILogger<RabbitMqPublisherService> logger)
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

    /// <summary>
    /// Освобождает управляемые и неуправляемые ресурсы, используемые <see cref="RabbitMqPublisherService"/>.
    /// </summary>
    public void Dispose()
    {
        _channel.Close();
        _channel.Dispose();
    }

    /// <inheritdoc />
    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
    {
        var messageType = typeof(TMessage);
        var attribute = messageType.GetCustomAttribute<RabbitMqMessageAttribute>();

        if (attribute == null)
        {
            throw new InvalidOperationException($"Message type {messageType.Name} must have RabbitMQMessageAttribute");
        }

        await PublishAsync(message, attribute.Exchange, attribute.RoutingKey, cancellationToken);
    }

    /// <inheritdoc />
    public async Task PublishAsync<TMessage>(TMessage message, string routingKey, CancellationToken cancellationToken = default)
    {
        var messageType = typeof(TMessage);
        var attribute = messageType.GetCustomAttribute<RabbitMqMessageAttribute>();
        var exchange = attribute?.Exchange ?? _configuration.Exchanges.Keys.FirstOrDefault() ?? "";

        await PublishAsync(message, exchange, routingKey, cancellationToken);
    }

    /// <inheritdoc />
    public async Task PublishAsync<TMessage>(TMessage message, string exchange, string routingKey, CancellationToken cancellationToken = default)
    {
        try
        {
            var messageBody = JsonSerializer.SerializeToUtf8Bytes(message, _jsonOptions);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            properties.MessageId = Guid.NewGuid()
                                       .ToString();
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            properties.Type = typeof(TMessage).Name;
            properties.ContentType = "application/json";

            await Task.Run(() =>
                           {
                               _channel.BasicPublish(exchange, routingKey, properties, messageBody);
                           },
                           cancellationToken);

            _logger.LogInformation("Published message {MessageType} to exchange {Exchange} with routing key {RoutingKey}", typeof(TMessage).Name, exchange, routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish message {MessageType} to exchange {Exchange} with routing key {RoutingKey}", typeof(TMessage).Name, exchange, routingKey);
            throw;
        }
    }

    private void InitializeInfrastructure()
    {
        foreach (var exchange in _configuration.Exchanges.Values)
        {
            _channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable, exchange.AutoDelete);
        }

        foreach (var queue in _configuration.Queues.Values)
        {
            _channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete);

            if (!string.IsNullOrEmpty(queue.Exchange))
            {
                _channel.QueueBind(queue.Name, queue.Exchange, queue.RoutingKey);
            }
        }
    }
}
