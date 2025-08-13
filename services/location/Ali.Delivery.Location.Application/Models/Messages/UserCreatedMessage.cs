namespace Ali.Delivery.Location.Application.Models.Messages;

[RabbitMqMessage("location.exchange", "location.created")]
public class UserCreatedMessage
{
    public long ChatId { get; set; }
    public DateTime CreatedAt { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
