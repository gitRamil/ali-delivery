using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Application.Services;

/// <summary>
/// Представляет реализацию репозитория для управления сущностями местоположения пользователя с использованием Entity
/// Framework Core.
/// </summary>
public class UserLocationRepository : IUserLocationRepository
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserLocationRepository" />.
    /// </summary>
    /// <param name="context">Контекст базы данных <see cref="IAppDbContext" />, используемый для операций с данными.</param>
    public UserLocationRepository(IAppDbContext context) => _context = context;

    /// <inheritdoc />
    public async Task AddUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken)
    {
        _context.UserLocations.Add(userLocation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<UserLocation?> GetUserAsync(string userLogin, CancellationToken cancellationToken) =>
        _context.UserLocations.FirstOrDefaultAsync(u => u.TelegramLogin == userLogin, cancellationToken);

    /// <inheritdoc />
    public async Task UpdateUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken)
    {
        _context.UserLocations.Update(userLocation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
