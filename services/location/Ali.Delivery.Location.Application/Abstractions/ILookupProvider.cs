namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Провайдер для работы с кэшированными данными.
/// </summary>
public interface ILookupProvider
{
    /// <summary>
    /// Получает словарь из кэша или базы данных.
    /// </summary>
    Task<Dictionary<string, string>> GetAsync();

    /// <summary>
    /// Сбрасывает кэш.
    /// </summary>
    Task Reset();
}
