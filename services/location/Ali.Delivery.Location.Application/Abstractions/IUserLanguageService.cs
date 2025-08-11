using Ali.Delivery.Location.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Определяет контракт для работы с языковыми настройками пользователей.
/// Предоставляет методы для получения, установки и сохранения языковых предпочтений пользователей.
/// </summary>
public interface IUserLanguageService
{
    /// <summary>
    /// Асинхронно сохраняет язык пользователя в базу данных.
    /// </summary>
    /// <param name="chatId">Идентификатор чата пользователя.</param>
    /// <param name="languageDictionary">Языковой код для сохранения (например, "ru", "en").</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpsertUserLanguageAsync(long chatId, LanguageDictionary languageDictionary, CancellationToken cancellationToken = default);
}
