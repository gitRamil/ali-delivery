using Ali.Delivery.Location.Domain.Entities;


namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// </summary>
public interface IUserLocationRepository
{
    /// <summary>
    /// Добавляет новую локацию пользователя.
    /// </summary>
    Task AddAsync(UserLocation userLocation, CancellationToken cancellationToken);
    
    /// <summary>
    /// </summary>
    /// <param name="userLogin"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserLocation?> GetByUserLoginAsync(string userLogin, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет существующую локацию пользователя.
    /// </summary>
    Task UpdateAsync(UserLocation userLocation, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет, существует ли пользователь с указанным логином.
    /// </summary>
    Task<bool> UserExistsAsync(string userLogin, CancellationToken cancellationToken);
}
