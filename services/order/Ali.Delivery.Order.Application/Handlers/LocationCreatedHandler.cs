using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Models;

namespace Ali.Delivery.Order.Application.Handlers;

/// <summary>
/// Обработчик события <see cref="LocationCreatedMessage" />.
/// Отправляет обновления локаций через SignalR.
/// </summary>
[RabbitMqConsumer("location.created.queue", "location.exchange", "location.created")]
public class LocationCreatedHandler : IMessageHandler<LocationCreatedMessage>
{
    private readonly ILocationNotificationService _locationNotificationService;

    /// <summary>
    /// Создаёт экземпляр <see cref="LocationCreatedHandler" />.
    /// </summary>
    /// <param name="locationNotificationService">Сервис уведомлений о локациях.</param>
    public LocationCreatedHandler(ILocationNotificationService locationNotificationService) => _locationNotificationService = locationNotificationService;

    /// <summary>
    /// Обрабатывает полученное сообщение о созданной локации.
    /// </summary>
    /// <param name="message">Сообщение с данными локации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task HandleAsync(LocationCreatedMessage message, CancellationToken cancellationToken = default)
    {
        await _locationNotificationService.NotifyLocationCreated(message);
    }
}
