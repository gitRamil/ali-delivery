namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для сервиса, управляющего сохранением и извлечением состояния пользователя
/// (например, текущий шаг в диалоге и логин).
/// </summary>
public interface IUserStateService // TODO: Сделать единую сессию.
{
    /// <summary>
    /// Асинхронно получает язык пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <returns>Языковой код пользователя (например, "ru", "en") или <c>null</c>, если язык не установлен.</returns>
    Task<string?> GetUserLanguageAsync(long userId);

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
    /// Асинхронно сохраняет (устанавливает или обновляет) язык для указанного пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <param name="languageCode">Языковой код (например, "ru", "en").</param>
    Task SetUserLanguageAsync(long userId, string languageCode);

    /// <summary>
    /// Асинхронно устанавливает новый текущий шаг для пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <param name="stateId">Идентификатор нового шага, в который переходит пользователь.</param>
    Task SetUserStepAsync(long userId, string stateId);
}
