namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

public record MessageInfo
{
    public long ChatId { get; init; }
    public long FromId { get; init; }
    public MessageLocation? Location { get; init; }
    public string? Text { get; init; }
}
