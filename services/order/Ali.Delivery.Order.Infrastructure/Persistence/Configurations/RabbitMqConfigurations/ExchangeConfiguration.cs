namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.RabbitMqConfigurations;

/// <summary>
/// Конфигурация обменника (Exchange) в RabbitMQ.
/// Определяет основные параметры обменника.
/// </summary>
public class ExchangeConfiguration
{
    /// <summary>
    /// Определяет, удаляется ли обменник автоматически, когда в нем больше нет очередей-подписчиков.
    /// </summary>
    public bool AutoDelete { get; init; }

    /// <summary>
    /// Флаг, определяющий, является ли обменник долговечным (сохраняется при перезапуске брокера).
    /// </summary>
    public bool Durable { get; init; }

    /// <summary>
    /// Имя обменника.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Тип обменника: direct, topic, fanout или headers.
    /// По умолчанию — direct.
    /// </summary>
    public string? Type { get; init; }
}
