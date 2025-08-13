using RabbitMQ.Client;

namespace Ali.Delivery.Order.Infrastructure.Services;

/// <summary>
/// Фабрика для создания подключений к RabbitMQ.
/// Использует <see cref="RabbitMqConfiguration" /> для конфигурации подключения.
/// </summary>
public class RabbitMqConnectionFactory
{
    private readonly RabbitMqConfiguration _configuration;

    /// <summary>
    /// Создаёт экземпляр фабрики подключения к RabbitMQ.
    /// </summary>
    /// <param name="configuration">Конфигурация подключения к RabbitMQ.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="configuration" /> равен <c>null</c>.
    /// </exception>
    public RabbitMqConnectionFactory(RabbitMqConfiguration configuration) => _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    /// <summary>
    /// Создаёт и возвращает новое подключение к RabbitMQ.
    /// </summary>
    public IConnection CreateConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration.HostName,
            Port = _configuration.Port,
            UserName = _configuration.UserName,
            Password = _configuration.Password,
            VirtualHost = _configuration.VirtualHost,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };

        return factory.CreateConnection();
    }
}
