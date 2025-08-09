using System.Text;
using System.Text.Json;
using Ali.Delivery.Location.Application.Abstractions;
using RabbitMQ.Client;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Сервис для публикации геолокационных данных в очередь RabbitMQ.
/// Отправляет координаты пользователей в асинхронную очередь для дальнейшей обработки.
/// </summary>
public class LocationPublisherService : ILocationPublisherService, IDisposable
{
    private readonly IChannel _channel;
    private bool _disposed;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса публикации локаций.
    /// Создает канал RabbitMQ и объявляет очередь для сообщений.
    /// </summary>
    /// <param name="rabbitMqConnection">Сервис подключения к RabbitMQ.</param>
    public LocationPublisherService(RabbitMqConnectionService rabbitMqConnection)
    {
        var connection = rabbitMqConnection.Connection;
        _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

        _channel.QueueDeclareAsync("locationQueue",
            false,
            false,
            false,
            null).GetAwaiter().GetResult();
    }

    /// <inheritdoc />
    public async Task PublishLocationAsync(long chatId, double longitude, double latitude)
    {
        var messageObj = new
        {
            ChatId = chatId,
            Longitude = longitude,
            Latitude = latitude,
            Timestamp = DateTime.UtcNow
        };

        var message = JsonSerializer.Serialize(messageObj);
        var body = Encoding.UTF8.GetBytes(message);

        await _channel.BasicPublishAsync("",
            "locationQueue",
            body);
    }

    /// <summary>
    /// Освобождает все используемые ресурсы.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Защищенный метод для освобождения ресурсов.
    /// Вызывается из публичного Dispose() и деструктора (если есть).
    /// </summary>
    /// <param name="disposing">True если вызывается из Dispose(), false если из финализатора.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed || !disposing) return;

        _channel.Dispose();
        _disposed = true;
    }
}