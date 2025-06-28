namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для сервиса, который сопоставляет идентификатор шага (состояния)
/// с его конкретной реализацией обработчика (<see cref="IStepHandler" />).
/// </summary>
public interface IStepHandlerMapping
{
    /// <summary>
    /// Возвращает экземпляр обработчика, соответствующий указанному идентификатору шага.
    /// </summary>
    /// <param name="stepId">Строковый идентификатор шага (состояния), для которого требуется найти обработчик.</param>
    /// <returns>Экземпляр обработчика, реализующий интерфейс <see cref="IStepHandler" />.</returns>
    IStepHandler GetHandler(string stepId);
}
