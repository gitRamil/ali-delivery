using Ali.Delivery.Location.Infrastructure.Persistence.Configurations;
using RabbitMQ.Client;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Фабрика для создания подключений к RabbitMQ.
/// </summary>
public class RabbitMqConnectionFactory
{
    private readonly RabbitMqConfiguration _configuration;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RabbitMqConnectionFactory"/>.
    /// </summary>
    /// <param name="configuration">Конфигурация подключения к RabbitMQ.</param>
    public RabbitMqConnectionFactory(RabbitMqConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Создает новое подключение к RabbitMQ с использованием настроек из конфигурации.
    /// Подключение создается с включенным автоматическим восстановлением и интервалом восстановления 10 секунд.
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