namespace Ali.Delivery.Order.Infrastructure.Services;

/// <summary>
/// Конфигурация подключения и параметров RabbitMQ.
/// Содержит общие настройки брокера, а также коллекции Exchanges и Queues.
/// </summary>
public class RabbitMqConfiguration
{
    /// <summary>
    /// Словарь с конфигурациями Exchanges.
    /// </summary>
    public Dictionary<string, ExchangeConfiguration> Exchanges { get; set; } = new();

    /// <summary>
    /// Имя хоста RabbitMQ.
    /// </summary>
    public string HostName { get; set; } = "localhost";

    /// <summary>
    /// Пароль пользователя для подключения к RabbitMQ.
    /// </summary>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// Порт подключения к RabbitMQ.
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// Словарь с конфигурациями очередей.
    /// </summary>
    public Dictionary<string, QueueConfiguration> Queues { get; set; } = new();

    /// <summary>
    /// Имя пользователя для аутентификации в RabbitMQ.
    /// </summary>
    public string UserName { get; set; } = "guest";

    /// <summary>
    /// Виртуальный хост RabbitMQ.
    /// </summary>
    public string VirtualHost { get; set; } = "/";
}

/// <summary>
/// Конфигурация обменника (Exchange) в RabbitMQ.
/// Определяет основные параметры обменника.
/// </summary>
public class ExchangeConfiguration
{
    /// <summary>
    /// Определяет, удаляется ли обменник автоматически, когда в нем больше нет очередей-подписчиков.
    /// </summary>
    public bool AutoDelete { get; set; } = false;

    /// <summary>
    /// Флаг, определяющий, является ли обменник долговечным (сохраняется при перезапуске брокера).
    /// </summary>
    public bool Durable { get; set; } = true;

    /// <summary>
    /// Имя обменника.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Тип обменника: direct, topic, fanout или headers.
    /// По умолчанию — direct.
    /// </summary>
    public string? Type { get; set; } = "direct";
}

/// <summary>
/// Конфигурация очереди (Queue) в RabbitMQ.
/// Описывает имя, привязку и параметры очереди.
/// </summary>
public class QueueConfiguration
{
    /// <summary>
    /// Определяет, удаляется ли очередь автоматически, когда у нее больше нет потребителей.
    /// </summary>
    public bool AutoDelete { get; set; } = false;

    /// <summary>
    /// Флаг, указывающий, должна ли очередь сохраняться при перезапуске брокера.
    /// </summary>
    public bool Durable { get; set; } = true;

    /// <summary>
    /// Имя обменника, к которому привязывается очередь.
    /// </summary>
    public string? Exchange { get; set; }

    /// <summary>
    /// Если true — очередь будет использоваться только одним соединением и удалится при его закрытии.
    /// </summary>
    public bool Exclusive { get; set; } = false;

    /// <summary>
    /// Имя очереди.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Маршрутный ключ (routing key), по которому сообщения будут попадать в очередь.
    /// </summary>
    public string? RoutingKey { get; set; }
}
