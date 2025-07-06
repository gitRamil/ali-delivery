namespace Ali.Delivery.Location.Application.Configurations;

/// <summary>
/// Представляет корневой объект конфигурации для всей машины состояний,
/// загружаемый из файла (например, stateTransitions.json).
/// </summary>
public class StateMachineConfiguration
{
    /// <summary>
    /// Получает список конфигураций для всех шагов (состояний), определенных в системе.
    /// </summary>
    public required List<StepConfiguration> Steps { get; init; }
}
