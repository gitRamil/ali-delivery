namespace Ali.Delivery.Location.Application.Models.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class RabbitMqMessageAttribute : Attribute
{
    public RabbitMqMessageAttribute(string exchange, string routingKey)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
    }

    public string Exchange { get; set; }
    public string RoutingKey { get; set; }
}
