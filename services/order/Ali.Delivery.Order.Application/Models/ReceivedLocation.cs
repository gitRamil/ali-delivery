namespace Ali.Delivery.Order.Application.Models;

/// <summary>
/// Представляет полученную локацию с информацией о координатах и времени.
/// </summary>
public class ReceivedLocation
{
    /// <summary>
    /// Уникальный идентификатор локации.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор чата, к которому принадлежит локация.
    /// </summary>
    public long ChatId { get; set; }

    /// <summary>
    /// Долгота.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Широта.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Время, когда была зафиксирована локация.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Время получения локации сервисом.
    /// </summary>
    public DateTime ReceivedAt { get; set; }
}