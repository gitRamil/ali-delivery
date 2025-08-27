namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.RabbitMqConfigurations;

/// <summary>
/// Конфигурация подключения и параметров RabbitMQ.
/// Содержит общие настройки брокера, а также коллекции Exchanges и Queues.
/// </summary>
public class RabbitMqConfiguration
{
    /// <summary>
    /// Словарь с конфигурациями Exchanges.
    /// </summary>
    public Dictionary<string, ExchangeConfiguration>? Exchanges { get; init; }

    /// <summary>
    /// Имя хоста RabbitMQ.
    /// </summary>
    public string HostName { get; init; } = null!;

    /// <summary>
    /// Пароль пользователя для подключения к RabbitMQ.
    /// </summary>
    public string Password { get; init; } = null!;

    /// <summary>
    /// Порт подключения к RabbitMQ.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Словарь с конфигурациями очередей.
    /// </summary>
    public Dictionary<string, QueueConfiguration>? Queues { get; init; }

    /// <summary>
    /// Имя пользователя для аутентификации в RabbitMQ.
    /// </summary>
    public string UserName { get; init; } = null!;

    /// <summary>
    /// Виртуальный хост RabbitMQ.
    /// </summary>
    public string VirtualHost { get; init; } = null!;
}
