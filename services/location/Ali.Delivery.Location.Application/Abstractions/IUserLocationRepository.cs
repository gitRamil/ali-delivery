using Ali.Delivery.Location.Domain.Entities;

// Используем доменную сущность

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IUserLocationRepository
{
    /// <summary>
    /// Проверяет, существует ли пользователь с указанным логином.
    /// </summary>
    Task<bool> UserExistsAsync(string userLogin, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новую локацию пользователя.
    /// </summary>
    Task AddAsync(UserLocation userLocation, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет существующую локацию пользователя.
    /// </summary>
    Task UpdateAsync(UserLocation userLocation, CancellationToken cancellationToken);
    
    // // (Опционально) Можно добавить метод для получения пользователя, чтобы не делать лишний запрос
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userLogin"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserLocation?> GetByUserLoginAsync(string userLogin, CancellationToken cancellationToken);
}