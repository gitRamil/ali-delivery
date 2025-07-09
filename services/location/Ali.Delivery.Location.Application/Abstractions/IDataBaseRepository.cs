using Ali.Delivery.Location.Domain.Entities;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для репозитория, управляющего сущностями местоположения пользователя.
/// </summary>
public interface IDataBaseRepository
{
    /// <summary>
    /// Добавляет нового пользователя в систему.
    /// </summary>
    /// <param name="user">Пользователь для добавления в систему.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task AddUserAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно добавляет новую сущность местоположения пользователя в хранилище.
    /// </summary>
    /// <param name="userLocation">Сущность местоположения пользователя для добавления.</param>
    /// <param name="cancellationToken">Маркер отмены для прерывания асинхронной операции.</param>
    Task AddUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно получает сущность местоположения пользователя по его логину.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <param name="cancellationToken">Маркер отмены для прерывания асинхронной операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит сущность <see cref="UserLocation" />, если пользователь найден; в противном случае —
    /// <c>null</c>.
    /// </returns>
    Task<UserLocation?> GetUserAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Получает пользователя по идентификатору чата.
    /// </summary>
    /// <param name="chatId">Идентификатор чата для поиска пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<User> GetUserByChatIdAsync(string chatId, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно обновляет существующую сущность местоположения пользователя в хранилище.
    /// </summary>
    /// <param name="userLocation">Сущность местоположения пользователя с обновленными данными.</param>
    /// <param name="cancellationToken">Маркер отмены для прерывания асинхронной операции.</param>
    Task UpdateUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken);
}
