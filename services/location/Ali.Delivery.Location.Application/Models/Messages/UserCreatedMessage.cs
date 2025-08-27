namespace Ali.Delivery.Location.Application.Models.Messages;

/// <summary>
/// Сообщение о создании пользователя с информацией о его местоположении.
/// Публикуется в exchange "location.exchange" с routing key "location.created".
/// </summary>
[RabbitMqMessage("location.exchange", "location.created")]
public class UserCreatedMessage
{
    /// <summary>
    /// Получает или задает идентификатор чата пользователя.
    /// </summary>
    public long ChatId { get; set; }

    /// <summary>
    /// Получает или задает дату и время создания пользователя.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Получает или задает широту местоположения пользователя.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Получает или задает долготу местоположения пользователя.
    /// </summary>
    public double Longitude { get; set; }
}