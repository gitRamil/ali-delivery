namespace Ali.Delivery.Location.Application.Models.Messages;

/// <summary>
/// Атрибут для указания параметров RabbitMQ (exchange и routing key) для классов сообщений.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RabbitMqMessageAttribute : Attribute
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RabbitMqMessageAttribute"/>.
    /// </summary>
    /// <param name="exchange">Имя exchange для публикации сообщения.</param>
    /// <param name="routingKey">Ключ маршрутизации для публикации сообщения.</param>
    public RabbitMqMessageAttribute(string exchange, string routingKey)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
    }

    /// <summary>
    /// Получает или задает имя exchange для публикации сообщения.
    /// </summary>
    public string Exchange { get; set; }

    /// <summary>
    /// Получает или задает ключ маршрутизации для публикации сообщения.
    /// </summary>
    public string RoutingKey { get; set; }
}