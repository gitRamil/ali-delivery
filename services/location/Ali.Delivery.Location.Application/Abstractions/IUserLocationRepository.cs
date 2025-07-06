using Ali.Delivery.Location.Domain.Entities;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для репозитория, управляющего сущностями местоположения пользователя.
/// </summary>
public interface IUserLocationRepository
{
    /// <summary>
    /// Асинхронно добавляет новую сущность местоположения пользователя в хранилище.
    /// </summary>
    /// <param name="userLocation">Сущность местоположения пользователя для добавления.</param>
    /// <param name="cancellationToken">Маркер отмены для прерывания асинхронной операции.</param>
    Task AddUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно получает сущность местоположения пользователя по его логину.
    /// </summary>
    /// <param name="userLogin">Логин пользователя, местоположение которого необходимо найти.</param>
    /// <param name="cancellationToken">Маркер отмены для прерывания асинхронной операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит сущность <see cref="UserLocation" />, если пользователь найден; в противном случае —
    /// <c>null</c>.
    /// </returns>
    Task<UserLocation?> GetUserAsync(string userLogin, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно обновляет существующую сущность местоположения пользователя в хранилище.
    /// </summary>
    /// <param name="userLocation">Сущность местоположения пользователя с обновленными данными.</param>
    /// <param name="cancellationToken">Маркер отмены для прерывания асинхронной операции.</param>
    Task UpdateUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken);
}
