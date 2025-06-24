using Ali.Delivery.Location.Application.Interfaces;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.StepHandlers;

public class LoginStepHandler : IStepHandler
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITelegramBotClient _bot;
    private readonly INotificationService _notification;

    public LoginStepHandler(ITelegramBotClient bot, INotificationService notification, IAuthenticationService authenticationService)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo)
    {
        var chatId = messageInfo.ChatId;

        if (chatId == 0)
        {
            return new HandlerResult(string.Empty);
        }

        if (messageInfo is not { Text: { } text })
        {
            await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidAuthCommand));
            return new HandlerResult(string.Empty);
        }

        if (string.Equals(messageInfo.Text, "/stop", StringComparison.OrdinalIgnoreCase))
        {
            await _bot.SendMessage(messageInfo.ChatId, _notification.GenerateNotificationMessage(NotificationType.N_SessionEnded));
            return new HandlerResult("StopStep");
        }

        var res = await _authenticationService.LoginAsync(chatId, text);

        return new HandlerResult(res.NextStepKey);
    }
}
