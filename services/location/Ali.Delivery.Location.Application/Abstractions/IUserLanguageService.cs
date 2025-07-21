namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Определяет контракт для работы с языковыми настройками пользователей.
/// Предоставляет методы для получения, установки и сохранения языковых предпочтений пользователей.
/// </summary>
public interface IUserLanguageService
{
    /// <summary>
    /// Асинхронно получает язык пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <returns>Языковой код пользователя (например, "ru", "en") или <c>null</c>, если язык не установлен.</returns>
    Task<string?> GetUserLanguageAsync(long userId);

    /// <summary>
    /// Асинхронно сохраняет язык пользователя в базу данных.
    /// </summary>
    /// <param name="chatId">Идентификатор чата пользователя.</param>
    /// <param name="languageCode">Языковой код для сохранения (например, "ru", "en").</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task SaveUserLanguageToDbAsync(long chatId, string languageCode, CancellationToken cancellationToken = default);

    /// <summary>
    /// Сохраняет (устанавливает или обновляет) язык для указанного пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <param name="languageCode">Языковой код (например, "ru", "en").</param>
    Task SetUserLanguage(long userId, string languageCode);
}
