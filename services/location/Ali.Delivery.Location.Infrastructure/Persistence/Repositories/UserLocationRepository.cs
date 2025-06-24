using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Infrastructure.Persistence.Repositories;

public class UserLocationRepository : IUserLocationRepository
{
    private readonly IAppDbContext _context;

    public UserLocationRepository(IAppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Находит локацию пользователя по его логину.
    /// </summary>
    public Task<UserLocation?> GetByUserLoginAsync(string userLogin, CancellationToken cancellationToken)
    {
        return _context.UserLocations.FirstOrDefaultAsync(u => u.TelegramLogin == userLogin, cancellationToken);
    }
    
    public Task<bool> UserExistsAsync(string userLogin, CancellationToken cancellationToken)
    {
        return _context.UserLocations.AnyAsync(u => u.TelegramLogin == userLogin, cancellationToken);
    }

    public async Task AddAsync(UserLocation userLocation, CancellationToken cancellationToken)
    {
        await _context.UserLocations.AddAsync(userLocation, cancellationToken);
    }

    public Task UpdateAsync(UserLocation userLocation, CancellationToken cancellationToken) => Task.CompletedTask;
}