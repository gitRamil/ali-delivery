using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Models;
using Microsoft.Extensions.Logging;

namespace Ali.Delivery.Order.Application.Handlers;

/// <summary>
/// Сообщение о созданной локации.
/// Передаётся от сервиса локаций через RabbitMQ.
/// </summary>
public class LocationCreatedMessage
{
    /// <summary>
    /// Дата и время создания локации (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Уникальный идентификатор локации.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Широта.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Долгота.
    /// </summary>
    public double Longitude { get; set; }
}

/// <summary>
/// Обработчик события <see cref="LocationCreatedMessage" />.
/// Логирует полученные координаты и данные о локации.
/// </summary>
[RabbitMqConsumer("location.created.queue", "location.exchange", "location.created")]
public class LocationCreatedHandler : IMessageHandler<LocationCreatedMessage>
{
    private readonly ILogger<LocationCreatedHandler> _logger;

    /// <summary>
    /// Создаёт экземпляр <see cref="LocationCreatedHandler" />.
    /// </summary>
    /// <param name="logger">Сервис логирования.</param>
    public LocationCreatedHandler(ILogger<LocationCreatedHandler> logger) => _logger = logger;

    /// <summary>
    /// Обрабатывает полученное сообщение о созданной локации.
    /// </summary>
    /// <param name="message">Сообщение с данными локации.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    public async Task HandleAsync(LocationCreatedMessage message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("========== ПОЛУЧЕНО СООБЩЕНИЕ О НОВОЙ ЛОКАЦИИ ==========");
        _logger.LogInformation("ID: {LocationId}", message.Id);
        _logger.LogInformation("Координаты: Широта {Latitude}, Долгота {Longitude}", message.Latitude, message.Longitude);
        _logger.LogInformation("Время создания: {CreatedAt}", message.CreatedAt);
        _logger.LogInformation("Время получения: {ReceivedAt}", DateTime.UtcNow);
        _logger.LogInformation("==================================================");

        await Task.Delay(100, cancellationToken);

        _logger.LogInformation("Локация {LocationId} успешно обработана (залогирована)", message.Id);
    }
}
