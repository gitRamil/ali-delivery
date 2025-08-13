namespace Ali.Delivery.Order.Application.Models;

[AttributeUsage(AttributeTargets.Class)]
public class RabbitMQConsumerAttribute : Attribute
{
    public string Queue { get; set; }
    public string Exchange { get; set; }
    public string RoutingKey { get; set; }
    public ushort PrefetchCount { get; set; } = 1;
    public bool AutoAck { get; set; } = false;

    public RabbitMQConsumerAttribute(string queue, string exchange = null, string routingKey = null)
    {
        Queue = queue;
        Exchange = exchange;
        RoutingKey = routingKey;
    }
}