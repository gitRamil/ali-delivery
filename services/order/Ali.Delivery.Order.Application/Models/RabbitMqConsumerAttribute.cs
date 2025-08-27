namespace Ali.Delivery.Order.Application.Models;

/// <summary>
/// Атрибут для пометки классов-обработчиков сообщений из RabbitMQ.
/// Используется для автоматической регистрации консумеров и их параметров.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RabbitMqConsumerAttribute : Attribute
{
    /// <summary>
    /// Создаёт новый экземпляр <see cref="RabbitMqConsumerAttribute" />.
    /// </summary>
    /// <param name="queue">Имя очереди для потребления сообщений.</param>
    /// <param name="exchange">Необязательное имя обменника.</param>
    /// <param name="routingKey">Необязательный маршрутный ключ.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="queue" /> равен <c>null</c>.
    /// </exception>
    public RabbitMqConsumerAttribute(string queue, string? exchange = null, string? routingKey = null)
    {
        Queue = queue ?? throw new ArgumentNullException(nameof(queue));
        Exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
        RoutingKey = routingKey ?? throw new ArgumentNullException(nameof(routingKey));
    }

    /// <summary>
    /// Признак автоматического подтверждения получения сообщения (autoAck).
    /// По умолчанию — <see langword="false" />.
    /// </summary>
    public bool AutoAck { get; init; } = false;

    /// <summary>
    /// Имя обменника (exchange), через который маршрутизируются сообщения.
    /// </summary>
    public string? Exchange { get; }

    /// <summary>
    /// Количество сообщений, которые потребитель будет предварительно извлекать (prefetch).
    /// Значение по умолчанию: 1.
    /// </summary>
    public ushort PrefetchCount { get; init; } = 1;

    /// <summary>
    /// Имя очереди, из которой будет потребляться сообщение.
    /// </summary>
    public string Queue { get; }

    /// <summary>
    /// Маршрутный ключ (routing key), по которому сообщения будут попадать в очередь.
    /// </summary>
    public string? RoutingKey { get; }
}
