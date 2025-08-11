namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для сервиса, управляющего сохранением и извлечением состояния пользователя
/// (например, текущий шаг в диалоге и логин).
/// </summary>
public interface IUserStateService // TODO: Сделать единую сессию.
{
    /// <summary>
    /// Получает идентификатор текущего шага пользователя в диалоге.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    Task<string> GetUserStepId(long userId);

    /// <summary>
    /// Устанавливает новый текущий шаг для пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <param name="stateId">Идентификатор нового шага, в который переходит пользователь.</param>
    Task SetUserStep(long userId, string stateId);
}
