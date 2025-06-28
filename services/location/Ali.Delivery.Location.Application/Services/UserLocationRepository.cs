using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Application.Services;

public class UserLocationRepository : IUserLocationRepository
{
    private readonly IAppDbContext _context;

    public UserLocationRepository(IAppDbContext context) => _context = context;

    public async Task AddUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken)
    {
        await _context.UserLocations.AddAsync(userLocation, cancellationToken);
    }

    /// <summary>
    /// Находит локацию пользователя по его логину.
    /// </summary>
    public Task<UserLocation?> GetUserAsync(string userLogin, CancellationToken cancellationToken)
    {
        return _context.UserLocations.FirstOrDefaultAsync(u => u.TelegramLogin == userLogin, cancellationToken);
    }

    public Task UpdateUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken) => Task.CompletedTask;

    public Task<bool> IsUserExistsAsync(string userLogin, CancellationToken cancellationToken)
    {
        return _context.UserLocations.AnyAsync(u => u.TelegramLogin == userLogin, cancellationToken);
    }
}
