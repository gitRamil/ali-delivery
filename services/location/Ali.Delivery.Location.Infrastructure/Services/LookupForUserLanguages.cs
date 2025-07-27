using Ali.Delivery.Location.Application.Abstractions;
using LazyCache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Реализация провайдера для получения языков пользователей с кэшированием.
/// </summary>
public class LookupForUserLanguages : ILookupProvider
{
    private const string UserLanguagesCacheKey = "UserLanguages";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(1);
    private readonly IAppCache _cache;
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LookupForUserLanguages" />.
    /// </summary>
    /// <param name="cache">Сервис кэширования.</param>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, когда один из параметров равен null.</exception>
    public LookupForUserLanguages(IAppCache cache, IAppDbContext context)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает словарь языков пользователей из кэша или базы данных.
    /// </summary>
    public async Task<Dictionary<string, string>> GetAsync()
    {
        return await _cache.GetOrAddAsync(UserLanguagesCacheKey,
                                          async entry =>
                                          {
                                              entry.SetAbsoluteExpiration(CacheExpiration);
                                              return await LoadUserLanguagesFromDbAsync();
                                          });
    }

    /// <summary>
    /// Сбрасывает кэш языков пользователей.
    /// </summary>
    public async Task Reset()
    {
        _cache.Remove(UserLanguagesCacheKey);
        await Task.CompletedTask;
    }

    private async Task<Dictionary<string, string>> LoadUserLanguagesFromDbAsync()
    {
        var users = await _context.Users.Include(u => u.UserConfig)
                                  .ThenInclude(uc => uc!.Language)
                                  .Where(u => u.UserConfig != null)
                                  .ToListAsync();

        var userLanguages = users.Where(u => u.UserConfig?.Language.Code != null)
                                 .Select(u => new
                                 {
                                     UserId = u.ChatId,
                                     LanguageCode = u.UserConfig!.Language.Code.ToString()
                                 })
                                 .Where(x => !string.IsNullOrEmpty(x.LanguageCode))
                                 .ToList();

        return userLanguages.ToDictionary(x => x.UserId, x => x.LanguageCode.ToLowerInvariant());
    }
}
