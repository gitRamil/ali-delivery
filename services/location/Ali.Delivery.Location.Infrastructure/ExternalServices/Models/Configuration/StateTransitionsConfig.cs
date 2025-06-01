namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration; // Ваш неймспейс

public class TransitionConfig
{
    public BotState NextState { get; set; }
    public NotificationType? Notification { get; set; }
}

// Этот класс будет представлять структуру вашего stateTransitions.json
// Ключи словаря - это строки типа "OnStart", "OnLogin"
// Внутренний словарь - это BotState (Initial, WaitingAuthCommand и т.д.)
public class StateTransitionsConfig : Dictionary<BotState, Dictionary<string, TransitionConfig>>
{
}
