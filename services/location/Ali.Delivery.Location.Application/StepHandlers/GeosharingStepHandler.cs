using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Exceptions;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Представляет обработчик для состояния "GeoSharing".
/// Этот обработчик отвечает за прием и сохранение геолокационных данных пользователя.
/// </summary>
public class GeosharingStepHandler : IStepHandler
{
    private readonly IAppDbContext _dbContext;
    private readonly INotificationService _notification;
    private readonly IUserStateService _userStateService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GeosharingStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    /// <param name="dbContext">Контекст БД.</param>
    /// <param name="userStateService"> Сервис управляющий сохранением и извлечением конфигураций пользователя.</param>
    public GeosharingStepHandler(INotificationService notification, IAppDbContext dbContext, IUserStateService userStateService)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userStateService = userStateService;
    }

    /// <inheritdoc />
    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken)
    {
        if (messageInfo.Location is { } loc)
        {
            return await ProcessLocationAsync(messageInfo.ChatId, loc.Longitude, loc.Latitude, cancellationToken);
        }

        if (messageInfo.Text is { } text)
        {
            return await ProcessTextCommandAsync(messageInfo.ChatId, text, cancellationToken);
        }

        await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.RequestLocation, cancellationToken: cancellationToken);
        return new HandlerResult(string.Empty);
    }

    private async Task<HandlerResult> ProcessLocationAsync(long chatId, double longitude, double latitude, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.Include(u => u.UserConfigs)
                                   .ThenInclude(uc => uc.Language)
                                   .Where(u => u.ChatId == chatId.ToString())
                                   .FirstOrDefaultAsync(cancellationToken) ??
                   throw new NotFoundException(typeof(User), chatId);
        var languageCode = await _userStateService.GetUserLanguageAsync(chatId);
        user.AddOrUpdateUserConfig(languageCode);
        user.AddUserLocation(longitude, latitude);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _notification.SendNotificationMessageAsync(chatId,
                                                         NotificationType.LocationReceived,
                                                         new Dictionary<string, object>
                                                         {
                                                             ["Latitude"] = latitude,
                                                             ["Longitude"] = longitude
                                                         },
                                                         cancellationToken);

        return new HandlerResult(string.Empty);
    }

    private async Task<HandlerResult> ProcessTextCommandAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        var command = text.Trim()
                          .ToLowerInvariant();

        switch (command)
        {
            case Commands.StopGeosharing:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.AuthenticationComplete, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.AuthComplete);
            case Commands.Stop:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.SessionEnded, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.Stop);
            default:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.RequestLocation, cancellationToken: cancellationToken);
                return new HandlerResult(string.Empty);
        }
    }
}
