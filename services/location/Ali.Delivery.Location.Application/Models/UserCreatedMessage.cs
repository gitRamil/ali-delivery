namespace Ali.Delivery.Location.Application.Models;

public class UserCreatedMessage
{
    public long UserId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTime CreatedAt { get; set; }
}
