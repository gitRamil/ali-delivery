namespace Ali.Delivery.Location.Application.StateMachineConfigurations;

public class StepConfiguration
{
    public required string Id { get; init; }
    public required Dictionary<string, string> SelectNextStep { get; init; }
}
