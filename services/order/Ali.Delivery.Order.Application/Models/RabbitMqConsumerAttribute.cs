namespace Ali.Delivery.Order.Application.Models;

/// <summary>
/// Атрибут для пометки классов-обработчиков сообщений из RabbitMQ.
/// Используется для автоматической регистрации консумеров и их параметров.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RabbitMqConsumerAttribute : Attribute
{
    /// <summary>
    /// Создаёт новый экземпляр <see cref="RabbitMqConsumerAttribute" />.
    /// </summary>
    /// <param name="queue">Имя очереди для потребления сообщений.</param>
    /// <param name="exchange">Необязательное имя обменника.</param>
    /// <param name="routingKey">Необязательный маршрутный ключ.</param>
    public RabbitMqConsumerAttribute(string queue, string? exchange = null, string? routingKey = null)
    {
        Queue = queue;
        Exchange = exchange;
        RoutingKey = routingKey;
    }

    /// <summary>
    /// Признак автоматического подтверждения получения сообщения (autoAck).
    /// По умолчанию — <see langword="false" />.
    /// </summary>
    public bool AutoAck { get; set; } = false;

    /// <summary>
    /// Имя обменника (exchange), через который маршрутизируются сообщения.
    /// Может быть <see langword="null" />, если используется только имя очереди.
    /// </summary>
    public string? Exchange { get; set; }

    /// <summary>
    /// Количество сообщений, которые потребитель будет предварительно извлекать (prefetch).
    /// Значение по умолчанию: 1.
    /// </summary>
    public ushort PrefetchCount { get; set; } = 1;

    /// <summary>
    /// Имя очереди, из которой будет потребляться сообщение.
    /// </summary>
    public string Queue { get; set; }

    /// <summary>
    /// Маршрутный ключ (routing key), по которому сообщения будут попадать в очередь.
    /// Может быть <see langword="null" />, если используется биндинг по умолчанию.
    /// </summary>
    public string? RoutingKey { get; set; }
}
