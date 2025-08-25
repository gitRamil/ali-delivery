namespace Ali.Delivery.Order.Application.Models;

/// <summary>
/// Сообщение о созданной локации.
/// Передаётся от сервиса локаций через RabbitMQ.
/// </summary>
/// <param name="Id">Уникальный идентификатор локации.</param>
/// <param name="Latitude">Широта.</param>
/// <param name="Longitude">Долгота.</param>
/// <param name="CreatedAt">Дата и время создания локации (UTC).</param>
public sealed record LocationCreatedMessage(Guid Id, double Latitude, double Longitude, DateTime CreatedAt);
