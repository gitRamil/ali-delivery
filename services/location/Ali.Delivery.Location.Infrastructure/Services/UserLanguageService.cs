using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Exceptions;
using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Реализует сервис для работы с языковыми настройками пользователей.
/// Использует UserLanguagesLookup для кэширования всех языковых настроек в виде словаря.
/// </summary>
public class UserLanguageService : IUserLanguageService
{
    private readonly IAppDbContext _context;
    private readonly ILookupProvider _lookupProvider;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserLanguageService" />.
    /// </summary>
    /// <param name="lookupProvider">Провайдер для получения кэшированных данных пользователей.</param>
    /// <param name="context">Контекст базы данных для постоянного хранения языковых настроек.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="lookupProvider" /> или <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public UserLanguageService(ILookupProvider lookupProvider, IAppDbContext context)
    {
        _lookupProvider = lookupProvider ?? throw new ArgumentNullException(nameof(lookupProvider));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task UpsertUserLanguageAsync(long chatId, LanguageDictionary languageDictionary, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Where(u => u.ChatId == chatId)
                                 .FirstOrDefaultAsync(cancellationToken) ??
                   throw new NotFoundException(typeof(User), chatId);

        user.UpsertUserLanguage(languageDictionary);
        await _context.SaveChangesAsync(cancellationToken);

        await _lookupProvider.Reset();
    }
}
