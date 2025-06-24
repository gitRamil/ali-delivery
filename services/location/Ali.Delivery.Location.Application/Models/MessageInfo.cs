using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Infrastructure.Models;

public record MessageInfo
{
    public long ChatId { get; init; }
    public long FromId { get; init; }
    public MessageLocation? Location { get; init; }
    public string? Text { get; init; }
}
