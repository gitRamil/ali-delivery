namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для создания Publisher для локаций в RabbitMQ.
/// </summary>
public interface ILocationPublisherService
{
    /// <summary>
    /// Публикует сообщение с локациями в очередь locationQueue.
    /// </summary>
    /// <param name="chatId">Уникальный идентификатор чата.</param>
    /// <param name="longitude">Долгота.</param>
    /// <param name="latitude">Широта.</param>
    /// <returns></returns>
    Task PublishLocationAsync(long chatId, double longitude, double latitude);
}