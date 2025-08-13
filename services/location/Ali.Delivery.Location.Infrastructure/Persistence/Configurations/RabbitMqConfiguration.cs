namespace Ali.Delivery.Location.Infrastructure.Persistence.Configurations;

public class RabbitMqConfiguration
{
    public Dictionary<string, ExchangeConfiguration> Exchanges { get; set; } = new();
    public string HostName { get; set; } = "localhost";
    public string Password { get; set; } = "guest";
    public int Port { get; set; } = 5672;
    public Dictionary<string, QueueConfiguration> Queues { get; set; } = new();
    public string UserName { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
}

public class ExchangeConfiguration
{
    public bool AutoDelete { get; set; } = false;
    public bool Durable { get; set; } = true;
    public string? Name { get; set; }
    public string Type { get; set; } = "direct";
}

public class QueueConfiguration
{
    public bool AutoDelete { get; set; } = false;
    public bool Durable { get; set; } = true;
    public string? Exchange { get; set; }
    public bool Exclusive { get; set; } = false;
    public string? Name { get; set; }
    public string? RoutingKey { get; set; }
}
