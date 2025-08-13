using Ali.Delivery.Location.WebApi;
using RabbitMQ.Client;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class RabbitMQConnectionFactory
{
    private readonly RabbitMQConfiguration _configuration;

    public RabbitMQConnectionFactory(RabbitMQConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IConnection CreateConnection()
    {
        var factory = new ConnectionFactory()
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