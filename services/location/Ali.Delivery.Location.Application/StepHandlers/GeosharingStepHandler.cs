using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Exceptions;
using Ali.Delivery.Location.Application.Extensions;
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
    private readonly ILocationPublisherService _locationPublisherService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GeosharingStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    /// <param name="dbContext">Контекст БД.</param>
    /// <param name="locationPublisherService">Сервис для публикации локаций.</param>
    public GeosharingStepHandler(INotificationService notification, IAppDbContext dbContext,
        ILocationPublisherService locationPublisherService)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _locationPublisherService = locationPublisherService ??
                                    throw new ArgumentNullException(nameof(locationPublisherService));
    }

    /// <inheritdoc />
    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken)
    {
        var chatId = messageInfo.ChatId;

        if (messageInfo.Location is { } loc)
            return await ProcessLocationAsync(messageInfo.ChatId, loc.Longitude, loc.Latitude, cancellationToken);

        if (messageInfo.Text is not { } text)
        {
            await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.RequestLocation,
                cancellationToken: cancellationToken);
            return new HandlerResult(string.Empty);
        }

        switch (text.MessageToCommand())
        {
            case Commands.StopGeosharing:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.AuthenticationComplete,
                    cancellationToken: cancellationToken);
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.AuthCompleteCommandHelp,
                    cancellationToken: cancellationToken);
                return new HandlerResult(Steps.AuthComplete);
            case Commands.Stop:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.SessionEnded,
                    cancellationToken: cancellationToken);
                return new HandlerResult(Steps.Stop);
            default:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.RequestLocation,
                    cancellationToken: cancellationToken);
                return new HandlerResult(string.Empty);
        }
    }

    private async Task<HandlerResult> ProcessLocationAsync(long chatId, double longitude, double latitude,
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.Where(u => u.ChatId == chatId)
                       .FirstOrDefaultAsync(cancellationToken) ??
                   throw new NotFoundException(typeof(User), chatId);

        user.AddUserLocation(longitude, latitude);
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _locationPublisherService.PublishLocationAsync(chatId, longitude, latitude);

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
}