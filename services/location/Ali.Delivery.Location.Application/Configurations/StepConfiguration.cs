namespace Ali.Delivery.Location.Application.Configurations;

/// <summary>
/// Представляет конфигурацию для одного шага (состояния) в конечном автомате.
/// </summary>
public class StepConfiguration
{
    /// <summary>
    /// Получает уникальный строковый идентификатор шага.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Получает словарь, определяющий возможные переходы из текущего шага.
    /// Ключ — это результат, возвращаемый обработчиком, а значение — идентификатор (<see cref="Id"/>) следующего шага.
    /// </summary>
    public required Dictionary<string, string> SelectNextStep { get; init; }
}