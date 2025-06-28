using Ali.Delivery.Location.Domain.Entities;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// </summary>
public interface IUserLocationRepository
{
    /// <summary>
    /// Добавляет новую локацию пользователя.
    /// </summary>
    Task AddUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken);

    /// <summary>
    /// </summary>
    /// <param name="userLogin"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserLocation?> GetUserAsync(string userLogin, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет существующую локацию пользователя.
    /// </summary>
    Task UpdateUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет, существует ли пользователь с указанным логином.
    /// </summary>
    Task<bool> IsUserExistsAsync(string userLogin, CancellationToken cancellationToken);
}
