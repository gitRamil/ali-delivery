namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.RabbitMqConfigurations;

/// <summary>
/// Конфигурация очереди (Queue) в RabbitMQ.
/// Описывает имя, привязку и параметры очереди.
/// </summary>
public class QueueConfiguration
{
    /// <summary>
    /// Определяет, удаляется ли очередь автоматически, когда у нее больше нет потребителей.
    /// </summary>
    public bool AutoDelete { get; init; }

    /// <summary>
    /// Флаг, указывающий, должна ли очередь сохраняться при перезапуске брокера.
    /// </summary>
    public bool Durable { get; init; }

    /// <summary>
    /// Имя обменника, к которому привязывается очередь.
    /// </summary>
    public string? Exchange { get; init; }

    /// <summary>
    /// Если true — очередь будет использоваться только одним соединением и удалится при его закрытии.
    /// </summary>
    public bool Exclusive { get; init; }

    /// <summary>
    /// Имя очереди.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Маршрутный ключ (routing key), по которому сообщения будут попадать в очередь.
    /// </summary>
    public string? RoutingKey { get; init; }
}
