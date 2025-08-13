using Ali.Delivery.Location.Infrastructure.Persistence.Configurations;
using RabbitMQ.Client;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class RabbitMqConnectionFactory
{
    private readonly RabbitMqConfiguration _configuration;

    public RabbitMqConnectionFactory(RabbitMqConfiguration configuration) => _configuration = configuration;

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
