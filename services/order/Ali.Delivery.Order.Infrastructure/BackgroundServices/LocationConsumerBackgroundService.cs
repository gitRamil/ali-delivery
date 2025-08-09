using System.Text;
using System.Text.Json;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Models;
using Ali.Delivery.Order.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ali.Delivery.Order.Infrastructure.BackgroundServices;

/// <summary>
/// Фоновый сервис для получения и обработки геолокационных данных из очереди RabbitMQ.
/// Работает как потребитель (Consumer) в архитектуре обмена сообщениями.
/// </summary>
public class LocationConsumerBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly RabbitMqConnectionService _rabbitMqConnection;
    private IChannel? _channel;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса потребителя локаций.
    /// </summary>
    /// <param name="serviceProvider">Провайдер сервисов для DI.</param>
    /// <param name="rabbitMqConnection">Сервис подключения к RabbitMQ.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="serviceProvider"/> или 
    /// <paramref name="rabbitMqConnection"/> равен <c>null</c>.
    /// </exception>
    public LocationConsumerBackgroundService(
        IServiceProvider serviceProvider,
        RabbitMqConnectionService rabbitMqConnection)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _rabbitMqConnection = rabbitMqConnection ?? throw new ArgumentNullException(nameof(rabbitMqConnection));
    }

    /// <summary>
    /// Основной метод выполнения фонового сервиса.
    /// Настраивает подключение к очереди и начинает прослушивание сообщений.
    /// </summary>
    /// <param name="cancellationToken">Токен для остановки сервиса</param>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _channel = await _rabbitMqConnection.Connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            "locationQueue",
            false,
            false,
            false,
            null);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var locationData = JsonSerializer.Deserialize<ReceivedLocation>(message, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                var location = new ReceivedLocation
                {
                    Id = Guid.NewGuid(),
                    ChatId = locationData!.ChatId,
                    Longitude = locationData.Longitude,
                    Latitude = locationData.Latitude,
                    Timestamp = locationData.Timestamp,
                    ReceivedAt = DateTime.UtcNow
                };
                using var scope = _serviceProvider.CreateScope();
                var locationStorage = scope.ServiceProvider.GetRequiredService<ILocationStorageService>();
                await locationStorage.AddLocationAsync(location);

                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch (Exception)
            {
                await _channel.BasicNackAsync(ea.DeliveryTag, false, false);
            }
        };

        await _channel.BasicConsumeAsync(
            "locationQueue",
            false, // Ручное подтверждение обработки
            consumer);

        await Task.Delay(Timeout.Infinite, cancellationToken);
    }

    /// <summary>
    /// Корректно останавливает сервис и освобождает ресурсы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Task для асинхронного выполнения</returns>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
        {
            await _channel.CloseAsync();
            await _channel.DisposeAsync();
        }

        await base.StopAsync(cancellationToken);
    }
}