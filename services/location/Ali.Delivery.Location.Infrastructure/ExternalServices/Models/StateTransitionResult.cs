namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

public record StateTransitionResult(
    BotState NewState,
    NotificationType? Notification,
    string? ResponseMessage,
    Dictionary<string, object>? UserData = null  
);


public record CommandResult(
    bool IsHandled,
    string? NextTransition,
    Dictionary<string, object>? UserData = null  
);