namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

public record class MessageInfo
{
    public long? CallbackQueryChatId { get; init; }
    public long ChatId { get; init; }
    public Location? Location { get; init; }
    public string? Text { get; init; }
}

public record class Location
{
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
}
