namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для сервиса, управляющего сохранением и извлечением состояния пользователя
/// (например, текущий шаг в диалоге и логин).
/// </summary>
public interface IUserStateService // TODO: Сделать единую сессию.
{
    /// <summary>
    /// Асинхронно получает логин пользователя по его ID.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя (например, ChatId).</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит логин пользователя, если он был сохранен; в противном случае — <c>null</c>.
    /// </returns>
    Task<string?> GetUserLoginAsync(long userId);

    /// <summary>
    /// Асинхронно получает идентификатор текущего шага пользователя в диалоге.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит строковый идентификатор текущего шага.
    /// </returns>
    Task<string> GetUserStepIdAsync(long userId);

    /// <summary>
    /// Асинхронно сохраняет (устанавливает или обновляет) логин для указанного пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <param name="login">Логин, который необходимо сохранить.</param>
    Task SetUserLoginAsync(long userId, string login);

    /// <summary>
    /// Асинхронно устанавливает новый текущий шаг для пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <param name="stateId">Идентификатор нового шага, в который переходит пользователь.</param>
    Task SetUserStepAsync(long userId, string stateId);
}
