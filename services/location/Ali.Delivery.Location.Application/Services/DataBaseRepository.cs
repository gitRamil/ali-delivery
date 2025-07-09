using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Exceptions;
using Ali.Delivery.Location.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Application.Services;

/// <summary>
/// Представляет реализацию репозитория для управления сущностями местоположения пользователя с использованием Entity
/// Framework Core.
/// </summary>
public class DataBaseRepository : IDataBaseRepository
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DataBaseRepository" />.
    /// </summary>
    /// <param name="context">Контекст базы данных <see cref="IAppDbContext" />, используемый для операций с данными.</param>
    public DataBaseRepository(IAppDbContext context) => _context = context;

    /// <inheritdoc />
    public async Task AddUserAsync(User user, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.ChatId == user.ChatId, cancellationToken);

        if (existingUser != null)
        {
            return;
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken)
    {
        _context.UserLocations.Add(userLocation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<UserLocation?> GetUserAsync(User user, CancellationToken cancellationToken) => _context.UserLocations.FirstOrDefaultAsync(u => u.User == user, cancellationToken);

    /// <inheritdoc />
    public async Task<User> GetUserByChatIdAsync(string chatId, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(u => u.ChatId == chatId)
                             .FirstOrDefaultAsync(cancellationToken) ??
               throw new NotFoundException(typeof(User), chatId);
    }

    /// <inheritdoc />
    public async Task UpdateUserLocationAsync(UserLocation userLocation, CancellationToken cancellationToken)
    {
        _context.UserLocations.Update(userLocation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
