using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Models;
using Microsoft.Extensions.Logging;

namespace Ali.Delivery.Order.Application.Handlers;

public class LocationCreatedMessage
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedAt { get; set; }
}

[RabbitMQConsumer("location.created.queue", "location.exchange", "location.created")]
public class LocationCreatedHandler : IMessageHandler<LocationCreatedMessage>
{
    private readonly ILogger<LocationCreatedHandler> _logger;

    public LocationCreatedHandler(ILogger<LocationCreatedHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(LocationCreatedMessage message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("========== ПОЛУЧЕНО СООБЩЕНИЕ О НОВОЙ ЛОКАЦИИ ==========");
        _logger.LogInformation("ID: {LocationId}", message.Id);
        _logger.LogInformation("Координаты: Широта {Latitude}, Долгота {Longitude}", 
            message.Latitude, message.Longitude);
        _logger.LogInformation("Время создания: {CreatedAt}", message.CreatedAt);
        _logger.LogInformation("Время получения: {ReceivedAt}", DateTime.UtcNow);
        _logger.LogInformation("==================================================");

        // Имитируем небольшую задержку обработки
        await Task.Delay(100, cancellationToken);
        
        _logger.LogInformation("✅ Локация {LocationId} успешно обработана (залогирована)", message.Id);
    }
}