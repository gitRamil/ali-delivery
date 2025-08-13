using System.Reflection;
using System.Text;
using System.Text.Json;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IModel = RabbitMQ.Client.IModel;

namespace Ali.Delivery.Order.Infrastructure.Services;

/// <summary>
/// Сервис для асинхронного потребления сообщений из RabbitMQ.
/// Автоматически настраивает обработчики сообщений на основе зарегистрированных <see cref="IMessageHandler{TMessage}" />.
/// </summary>
public class RabbitMqConsumerService : IMessageConsumer
{
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly Dictionary<string, ConsumerInfo> _consumers;
    private bool _disposed;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<RabbitMqConsumerService> _logger;
    private readonly Dictionary<string, Type> _messageHandlers;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Создаёт экземпляр <see cref="RabbitMqConsumerService" />.
    /// </summary>
    /// <param name="connection">Активное соединение с RabbitMQ.</param>
    /// <param name="serviceProvider">DI-контейнер для создания экземпляров обработчиков.</param>
    /// <param name="logger">Логгер для ведения сервисных сообщений.</param>
    public RabbitMqConsumerService(IConnection connection, IServiceProvider serviceProvider, ILogger<RabbitMqConsumerService> logger)
    {
        _connection = connection;
        _channel = _connection.CreateModel();
        _serviceProvider = serviceProvider;
        _logger = logger;
        _messageHandlers = new Dictionary<string, Type>();
        _consumers = new Dictionary<string, ConsumerInfo>();

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        RegisterMessageHandlers();
    }

    /// <summary>
    /// Освобождает ресурсы, связанные с RabbitMQ каналом и подключением.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        try
        {
            StopAsync()
                .Wait();
            _channel.Close();
            _channel.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disposing RabbitMQ Consumer");
        }
        finally
        {
            _disposed = true;
        }
    }

    /// <inheritdoc />
    public bool IsRunning { get; private set; }

    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (IsRunning)
        {
            return;
        }

        try
        {
            foreach (var consumerInfo in _consumers.Values)
            {
                await SetupConsumer(consumerInfo);
            }

            IsRunning = true;
            _logger.LogInformation("RabbitMQ Consumer started successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start RabbitMQ Consumer");
            throw;
        }
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        if (!IsRunning)
        {
            return Task.CompletedTask;
        }

        try
        {
            IsRunning = false;
            _logger.LogInformation("RabbitMQ Consumer stopped");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error stopping RabbitMQ Consumer");
            throw;
        }

        return Task.CompletedTask;
    }

    private async Task ProcessMessage(BasicDeliverEventArgs ea, ConsumerInfo consumerInfo)
    {
        using var scope = _serviceProvider.CreateScope();

        try
        {
            var messageBody = Encoding.UTF8.GetString(ea.Body.ToArray());

            if (consumerInfo.MessageType != null)
            {
                var message = JsonSerializer.Deserialize(messageBody, consumerInfo.MessageType, _jsonOptions);

                var handlerKey = $"{consumerInfo.Queue}:{consumerInfo.MessageType.Name}";

                if (_messageHandlers.TryGetValue(handlerKey, out var handlerType))
                {
                    var handler = scope.ServiceProvider.GetRequiredService(handlerType);
                    var handleMethod = handlerType.GetMethod("HandleAsync");

                    if (handleMethod != null)
                    {
                        await (Task)handleMethod.Invoke(handler, [message, CancellationToken.None])!;

                        if (!consumerInfo.AutoAck)
                        {
                            _channel.BasicAck(ea.DeliveryTag, false);
                        }

                        _logger.LogInformation("Successfully processed message {MessageType} from queue {Queue}", consumerInfo.MessageType.Name, consumerInfo.Queue);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message from queue {Queue}", consumerInfo.Queue);

            if (!consumerInfo.AutoAck)
            {
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        }
    }

    private void RegisterMessageHandlers()
    {
        var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(a => a.GetTypes())
                                    .Where(t => t.GetInterfaces()
                                                 .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessageHandler<>)))
                                    .ToList();

        foreach (var handlerType in handlerTypes)
        {
            var messageHandlerInterface = handlerType.GetInterfaces()
                                                     .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessageHandler<>));

            var messageType = messageHandlerInterface.GetGenericArguments()[0];
            var consumerAttribute = handlerType.GetCustomAttribute<RabbitMqConsumerAttribute>();

            if (consumerAttribute != null)
            {
                var key = $"{consumerAttribute.Queue}:{messageType.Name}";
                _messageHandlers[key] = handlerType;

                _consumers[consumerAttribute.Queue] = new ConsumerInfo
                {
                    Queue = consumerAttribute.Queue,
                    Exchange = consumerAttribute.Exchange,
                    RoutingKey = consumerAttribute.RoutingKey,
                    PrefetchCount = consumerAttribute.PrefetchCount,
                    AutoAck = consumerAttribute.AutoAck,
                    MessageType = messageType,
                    HandlerType = handlerType
                };
            }
        }
    }

    private Task SetupConsumer(ConsumerInfo consumerInfo)
    {
        _channel.QueueDeclare(consumerInfo.Queue, true, false, false);

        if (!string.IsNullOrEmpty(consumerInfo.Exchange))
        {
            _channel.ExchangeDeclare(consumerInfo.Exchange, "direct", true, false);

            _channel.QueueBind(consumerInfo.Queue, consumerInfo.Exchange, consumerInfo.RoutingKey ?? "");
        }

        _channel.BasicQos(0, consumerInfo.PrefetchCount, false);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (_, ea) =>
        {
            await ProcessMessage(ea, consumerInfo);
        };

        _channel.BasicConsume(consumerInfo.Queue, consumerInfo.AutoAck, consumer);

        _logger.LogInformation("Started consuming from queue {Queue}", consumerInfo.Queue);
        return Task.CompletedTask;
    }

    private class ConsumerInfo
    {
        public bool AutoAck { get; set; }
        public string? Exchange { get; set; }
        public Type? HandlerType { get; set; }
        public Type? MessageType { get; set; }
        public ushort PrefetchCount { get; set; }
        public string? Queue { get; set; }
        public string? RoutingKey { get; set; }
    }
}
