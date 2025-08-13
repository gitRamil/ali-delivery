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

namespace Ali.Delivery.Order.Infrastructure.services;


public class RabbitMQConsumerService : IMessageConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RabbitMQConsumerService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly Dictionary<string, Type> _messageHandlers;
    private readonly Dictionary<string, ConsumerInfo> _consumers;
    private bool _isRunning;
    private bool _disposed;

    public bool IsRunning => _isRunning;

    public RabbitMQConsumerService(
        IConnection connection,
        IServiceProvider serviceProvider,
        ILogger<RabbitMQConsumerService> logger)
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
            var consumerAttribute = handlerType.GetCustomAttribute<RabbitMQConsumerAttribute>();
            
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

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (_isRunning) return;

        try
        {
            foreach (var consumerInfo in _consumers.Values)
            {
                await SetupConsumer(consumerInfo);
            }

            _isRunning = true;
            _logger.LogInformation("RabbitMQ Consumer started successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start RabbitMQ Consumer");
            throw;
        }
    }

    private async Task SetupConsumer(ConsumerInfo consumerInfo)
    {
        _channel.QueueDeclare(
            queue: consumerInfo.Queue,
            durable: true,
            exclusive: false,
            autoDelete: false);

        if (!string.IsNullOrEmpty(consumerInfo.Exchange))
        {
            _channel.ExchangeDeclare(
                exchange: consumerInfo.Exchange,
                type: "direct",
                durable: true,
                autoDelete: false);

            _channel.QueueBind(
                queue: consumerInfo.Queue,
                exchange: consumerInfo.Exchange,
                routingKey: consumerInfo.RoutingKey ?? "");
        }

        _channel.BasicQos(prefetchSize: 0, prefetchCount: consumerInfo.PrefetchCount, global: false);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            await ProcessMessage(ea, consumerInfo);
        };

        _channel.BasicConsume(
            queue: consumerInfo.Queue,
            autoAck: consumerInfo.AutoAck,
            consumer: consumer);

        _logger.LogInformation("Started consuming from queue {Queue}", consumerInfo.Queue);
    }

    private async Task ProcessMessage(BasicDeliverEventArgs ea, ConsumerInfo consumerInfo)
    {
        using var scope = _serviceProvider.CreateScope();
        
        try
        {
            var messageBody = Encoding.UTF8.GetString(ea.Body.ToArray());
            var message = JsonSerializer.Deserialize(messageBody, consumerInfo.MessageType, _jsonOptions);

            var handlerKey = $"{consumerInfo.Queue}:{consumerInfo.MessageType.Name}";
            if (_messageHandlers.TryGetValue(handlerKey, out var handlerType))
            {
                var handler = scope.ServiceProvider.GetRequiredService(handlerType);
                var handleMethod = handlerType.GetMethod("HandleAsync");
                
                if (handleMethod != null)
                {
                    await (Task)handleMethod.Invoke(handler, new[] { message, CancellationToken.None });
                    
                    if (!consumerInfo.AutoAck)
                    {
                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }

                    _logger.LogInformation("Successfully processed message {MessageType} from queue {Queue}",
                        consumerInfo.MessageType.Name, consumerInfo.Queue);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message from queue {Queue}", consumerInfo.Queue);
            
            if (!consumerInfo.AutoAck)
            {
                _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        if (!_isRunning) return;

        try
        {
            _isRunning = false;
            _logger.LogInformation("RabbitMQ Consumer stopped");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error stopping RabbitMQ Consumer");
            throw;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;

        try
        {
            StopAsync().Wait();
            _channel?.Close();
            _channel?.Dispose();
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

    private class ConsumerInfo
    {
        public string Queue { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public ushort PrefetchCount { get; set; }
        public bool AutoAck { get; set; }
        public Type MessageType { get; set; }
        public Type HandlerType { get; set; }
    }
}