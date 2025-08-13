namespace Ali.Delivery.Location.Application.Models;

public class RabbitMQConfiguration
{
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
    public Dictionary<string, ExchangeConfiguration> Exchanges { get; set; } = new();
    public Dictionary<string, QueueConfiguration> Queues { get; set; } = new();
}
public class ExchangeConfiguration
{
    public string Name { get; set; }
    public string Type { get; set; } = "direct"; // direct, topic, fanout, headers
    public bool Durable { get; set; } = true;
    public bool AutoDelete { get; set; } = false;
}

public class QueueConfiguration
{
    public string Name { get; set; }
    public string Exchange { get; set; }
    public string RoutingKey { get; set; }
    public bool Durable { get; set; } = true;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
}

[AttributeUsage(AttributeTargets.Class)]
public class RabbitMQMessageAttribute : Attribute
{
    public string Exchange { get; set; }
    public string RoutingKey { get; set; }

    public RabbitMQMessageAttribute(string exchange, string routingKey)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
    }
}