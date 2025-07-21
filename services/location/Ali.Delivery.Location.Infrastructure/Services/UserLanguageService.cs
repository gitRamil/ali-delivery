using Ali.Delivery.Location.Application.Abstractions;
using LazyCache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Реализует сервис для работы с языковыми настройками пользователей.
/// Поддерживает кэширование для повышения производительности и работу с базой данных для постоянного хранения.
/// </summary>
public class UserLanguageService : IUserLanguageService
{
    private const string LanguageCachePrefix = "UserLanguage_";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(24);
    private readonly IAppCache _cache;
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserLanguageService" />.
    /// </summary>
    /// <param name="cache">Сервис кэширования для временного хранения языковых настроек.</param>
    /// <param name="context">Контекст базы данных для постоянного хранения языковых настроек.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="cache" /> или <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public UserLanguageService(IAppCache cache, IAppDbContext context)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<string?> GetUserLanguageAsync(long userId)
    {
        var cacheKey = $"{LanguageCachePrefix}{userId}";

        var language = await _cache.GetOrAddAsync(cacheKey,
                                                  async entry =>
                                                  {
                                                      entry.SetAbsoluteExpiration(CacheExpiration);
                                                      return await LoadUserLanguageFromDbAsync(userId);
                                                  });

        return language?.ToLowerInvariant();
    }

    /// <inheritdoc />
    public async Task SaveUserLanguageToDbAsync(long chatId, string languageCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(languageCode))
        {
            throw new ArgumentException("Код языка не может быть пустым.", nameof(languageCode));
        }

        var user = await _context.Users.Include(u => u.UserConfigs)
                                 .Where(u => u.ChatId == chatId.ToString())
                                 .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new InvalidOperationException($"Пользователь с ChatId {chatId} не найден.");
        }

        user.AddOrUpdateUserConfig(languageCode);
        await _context.SaveChangesAsync(cancellationToken);

        var cacheKey = $"{LanguageCachePrefix}{chatId}";
        _cache.Add(cacheKey, languageCode.ToLowerInvariant(), CacheExpiration);
    }

    /// <inheritdoc />
    public Task SetUserLanguage(long userId, string languageCode)
    {
        if (string.IsNullOrWhiteSpace(languageCode))
        {
            throw new ArgumentException("Код языка не может быть пустым.", nameof(languageCode));
        }

        var cacheKey = $"{LanguageCachePrefix}{userId}";
        _cache.Add(cacheKey, languageCode.ToLowerInvariant(), CacheExpiration);
        return Task.CompletedTask;
    }

    private async Task<string?> LoadUserLanguageFromDbAsync(long chatId)
    {
        return await _context.Users.Where(u => u.ChatId == chatId.ToString())
                             .SelectMany(u => u.UserConfigs)
                             .Where(uc => true)
                             .Select(uc => uc.Language.Code)
                             .FirstOrDefaultAsync();
    }
}
