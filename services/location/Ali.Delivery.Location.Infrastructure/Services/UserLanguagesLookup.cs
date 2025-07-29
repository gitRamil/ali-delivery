using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Domain.ValueObjects.Dictionaries.LanguageDictionary;
using LazyCache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Реализация провайдера для получения языков пользователей с кэшированием.
/// </summary>
public class UserLanguagesLookup : ILookupProvider
{
    private const string UserLanguagesCacheKey = "UserLanguages";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(1);
    private readonly IAppCache _cache;
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserLanguagesLookup" />.
    /// </summary>
    /// <param name="cache">Сервис кэширования.</param>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, когда один из параметров равен null.</exception>
    public UserLanguagesLookup(IAppCache cache, IAppDbContext context)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<Dictionary<long, LanguageCode>> GetAsync()
    {
        return await _cache.GetOrAddAsync(UserLanguagesCacheKey,
                                          async entry =>
                                          {
                                              entry.SetAbsoluteExpiration(CacheExpiration);
                                              return await LoadUserLanguagesFromDbAsync();
                                          });
    }

    /// <inheritdoc />
    public async Task Reset()
    {
        _cache.Remove(UserLanguagesCacheKey);
        await Task.CompletedTask;
    }

    private async Task<Dictionary<long, LanguageCode>> LoadUserLanguagesFromDbAsync() =>
        await _context.Users.Where(u => u.UserConfig != null)
                      .ToDictionaryAsync(u => u.ChatId, u => u.UserConfig!.Language.Code);
}
