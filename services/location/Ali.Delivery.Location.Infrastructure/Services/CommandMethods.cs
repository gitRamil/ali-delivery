using System.Globalization;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class CommandMethods : ICommandMethods // TODO: Подумать над упрощением. Провести рефакторинг.
{
    private readonly IAuthenticationService _auth;
    private readonly ITelegramBotClient _bot;
    private readonly ILocationService _location;
    private readonly INotificationService _notification;
    private readonly IUserStateService _userStateService;

    public CommandMethods(ITelegramBotClient bot, IAuthenticationService auth, INotificationService notification, ILocationService location, IUserStateService userStateService)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _auth = auth ?? throw new ArgumentNullException(nameof(auth));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _location = location ?? throw new ArgumentNullException(nameof(location));
        _userStateService = userStateService ?? throw new ArgumentNullException(nameof(userStateService));
    }

    public async Task<CommandResult> GeoSharingAsync(long chatId, string text)
    {
        text = text.Replace(',', '.');
        var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2 ||
            !double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var lat) ||
            !double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var lon))
        {
            await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N7_InvalidLocation));

            return new CommandResult(string.Empty);
        }

        var userLogin = await _userStateService.GetUserLoginAsync(chatId);

        if (string.IsNullOrEmpty(userLogin))
        {
            await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N2_InvalidCredentials));

            return new CommandResult("Authorization");
        }

        var saved = await _location.SaveLocationAsync(chatId, userLogin, lat, lon);

        var msgType = saved ? NotificationType.N6_LocationReceived : NotificationType.N7_InvalidLocation;

        await _bot.SendMessage(chatId,
                               _notification.GenerateNotificationMessage(msgType,
                                                                         new Dictionary<string, object>
                                                                         {
                                                                             ["Latitude"] = lat,
                                                                             ["Longitude"] = lon
                                                                         }));

        return new CommandResult(string.Empty);
    }

    public async Task<CommandResult> LoginAsync(long chatId, string text)
    {
        var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
        {
            await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N0_EnterCredentials));

            return new CommandResult(string.Empty);
        }

        var (login, password) = (parts[0], parts[1]);
        var auth = await _auth.AuthenticateAsync(login, password);

        switch (auth.Status)
        {
            case AuthResult.Success:
                await _userStateService.SetUserLoginAsync(chatId, login);
                await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N4_AuthenticationComplete));
                return new CommandResult("AuthComplete");

            case AuthResult.InvalidCredentials:
                await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N2_InvalidCredentials));

                return new CommandResult(string.Empty);

            default:
                await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidCommand));

                return new CommandResult(string.Empty);
        }
    }
}
