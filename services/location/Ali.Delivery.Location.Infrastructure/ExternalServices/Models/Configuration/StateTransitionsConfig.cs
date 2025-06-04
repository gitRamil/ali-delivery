namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;

/// <summary>
/// Конфигурация перехода между состояниями (immutable)
/// </summary>
/// <param name="NextState">Следующее состояние после перехода</param>
/// <param name="Notification">Тип уведомления для пользователя</param>
public record TransitionConfig(
    BotState NextState,
    NotificationType? Notification = null
);

/// <summary>
/// Конфигурация переходов состояний для State Machine
/// </summary>
public class StateTransitionsConfig : Dictionary<BotState, Dictionary<string, TransitionConfig>>
{
    /// <summary>
    /// Безопасное получение конфигурации перехода
    /// </summary>
    /// <param name="currentState">Текущее состояние</param>
    /// <param name="transitionKey">Ключ перехода (например, "OnStart", "OnLogin")</param>
    /// <returns>Конфигурация перехода или null, если не найдена</returns>
    public TransitionConfig? GetTransition(BotState currentState, string transitionKey) =>
        TryGetValue(currentState, out var stateTransitions) && 
        stateTransitions.TryGetValue(transitionKey, out var transition)
            ? transition 
            : null;

    /// <summary>
    /// Получение всех доступных переходов для состояния
    /// </summary>
    /// <param name="currentState">Текущее состояние</param>
    /// <returns>Словарь доступных переходов</returns>
    public Dictionary<string, TransitionConfig> GetAvailableTransitions(BotState currentState) =>
        TryGetValue(currentState, out var transitions) 
            ? transitions 
            : new Dictionary<string, TransitionConfig>();
}
