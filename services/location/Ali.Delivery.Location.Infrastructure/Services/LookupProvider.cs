using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;
using LazyCache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class LookupProvider : ILookupProvider
{
    private readonly IAppCache _cache;
    private readonly IAppDbContext _context;
    
    private const string UserLanguagesCacheKey = "UserLanguages";
    private const string UserLocationsCacheKey = "UserLocations";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(1);

    public LookupProvider(IAppCache cache, IAppDbContext context)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает словарь языков пользователей из кэша или базы данных
    /// </summary>
    public async Task<Dictionary<string, string>> GetUserLanguagesAsync()
    {
        return await _cache.GetOrAddAsync(UserLanguagesCacheKey,
            async entry =>
            {
                entry.SetAbsoluteExpiration(CacheExpiration);
                return await LoadUserLanguagesFromDbAsync();
            });
    }

    /// <summary>
    /// Получает словарь локаций пользователей из кэша или базы данных
    /// </summary>
    public async Task<Dictionary<Guid, LocationInfo>> GetUserLocationsAsync()
    {
        return await _cache.GetOrAddAsync(UserLocationsCacheKey,
            async entry =>
            {
                entry.SetAbsoluteExpiration(CacheExpiration);
                return await LoadUserLocationsFromDbAsync();
            });
    }

    /// <summary>
    /// Инвалидирует кэш (очищает все кэшированные данные)
    /// </summary>
    public async Task Reset()
    {
        _cache.Remove(UserLanguagesCacheKey);
        _cache.Remove(UserLocationsCacheKey);
        await Task.CompletedTask;
    }


    private async Task<Dictionary<string, string>> LoadUserLanguagesFromDbAsync()
    {
        var userLanguages = await _context.Users
            .Include(u => u.UserConfigs)
                .ThenInclude(uc => uc.Language)
            .Where(u => u.UserConfigs.Any(c => true))
            .Select(u => new
            {
                UserId = u.ChatId,
                LanguageCode = u.UserConfigs
                    .Where(c => true)
                    .Select(c => c.Language.Code)
                    .FirstOrDefault()
            })
            .Where(x => !string.IsNullOrEmpty(x.LanguageCode))
            .ToListAsync();

        return userLanguages.ToDictionary(
            x => x.UserId,
            x => x.LanguageCode!.ToString().ToLowerInvariant()
        );
    }
    
    private async Task<Dictionary<Guid, LocationInfo>> LoadUserLocationsFromDbAsync()
    {
        var userLocations = await _context.UserLocations
            .Select(ul => new
            {
                ul.User.Id,
                LocationInfo = new LocationInfo
                {
                    Latitude = ul.Latitude,
                    Longitude = ul.Longitude,
                }
            })
            .ToListAsync();

        return userLocations.ToDictionary(
            x => (Guid)x.Id,
            x => x.LocationInfo
        );
    }
}