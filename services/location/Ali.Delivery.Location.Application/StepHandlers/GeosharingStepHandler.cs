using System.Globalization;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Domain.Entities;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Представляет обработчик для состояния "GeoSharing".
/// Этот обработчик отвечает за прием и сохранение геолокационных данных пользователя.
/// </summary>
public class GeosharingStepHandler : IStepHandler
{
    private readonly INotificationService _notification;
    private readonly IUserLocationRepository _repository;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GeosharingStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    /// <param name="repository">Репозиторий для выполнения операций с базой данных местоположений.</param>
    public GeosharingStepHandler(INotificationService notification, IUserLocationRepository repository)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <inheritdoc />
    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken)
    {
        if (messageInfo.Location is { } loc)
        {
            return await ProcessLocationAsync(messageInfo.ChatId,
                                              loc.Longitude.ToString(CultureInfo.InvariantCulture),
                                              loc.Latitude.ToString(CultureInfo.InvariantCulture),
                                              cancellationToken);
        }

        if (messageInfo.Text is { } text)
        {
            return await ProcessTextCommandAsync(messageInfo.ChatId, text, cancellationToken);
        }

        await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N5_RequestLocation, cancellationToken: cancellationToken);
        return new HandlerResult(string.Empty);
    }

    private async Task<HandlerResult> ProcessLocationAsync(long chatId, string longitude, string latitude, CancellationToken cancellationToken)
    {
        var userLogin = chatId.ToString();
        var userLocation = await _repository.GetUserAsync(userLogin, cancellationToken);

        if (userLocation is not null)
        {
            userLocation.UpdateCoordinates(longitude, latitude);
            await _repository.UpdateUserLocationAsync(userLocation, cancellationToken);
        }
        else
        {
            var newUserLocation = new UserLocation(SequentialGuid.Create(), userLogin);
            newUserLocation.UpdateCoordinates(longitude, latitude);
            await _repository.AddUserLocationAsync(newUserLocation, cancellationToken);
        }

        await _notification.SendNotificationMessageAsync(chatId,
                                                         NotificationType.N6_LocationReceived,
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
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N4_AuthenticationComplete, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.AuthComplete);
            case Commands.Stop:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N_SessionEnded, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.Stop);
            default:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.N5_RequestLocation, cancellationToken: cancellationToken);
                return new HandlerResult(string.Empty);
        }
    }
}
