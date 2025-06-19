namespace Ali.Delivery.Location.Infrastructure.Models.Configuration;

public class StepConfiguration
{
    public required string Id { get; init; }
    public required Dictionary<string, string> SelectNextStep { get; init; } = new();
}
