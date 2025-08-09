using Ali.Delivery.Order.Application.Models;

namespace Ali.Delivery.Order.Application.Abstractions;

/// <summary>
/// Интерфейс для сервиса хранения локаций в памяти.
/// </summary>
public interface ILocationStorageService
{
    /// <summary>
    /// Добавляет новую локацию асинхронно.
    /// </summary>
    /// <param name="location">Объект с данными локации.</param>
    Task AddLocationAsync(ReceivedLocation location);

    /// <summary>
    /// Получает список всех сохранённых локаций.
    /// </summary>
    Task<List<ReceivedLocation>> GetAllLocationsAsync();

    /// <summary>
    /// Получает список локаций для заданного chatId.
    /// </summary>
    /// <param name="chatId">Идентификатор чата.</param>
    Task<List<ReceivedLocation>> GetLocationsByChatIdAsync(long chatId);

    /// <summary>
    /// Получает список последних добавленных локаций.
    /// </summary>
    /// <param name="count">Количество локаций для возврата (по умолчанию 10).</param>
    Task<List<ReceivedLocation>> GetRecentLocationsAsync(int count = 10);

    /// <summary>
    /// Очищает все сохранённые локации.
    /// </summary>
    Task ClearAllLocationsAsync();
}