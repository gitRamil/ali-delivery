using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.StepHandlers;

public class LoginStepHandler : IStepHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly ICommandMethods _method;
    private readonly INotificationService _notification;

    public LoginStepHandler(ITelegramBotClient bot, ICommandMethods method, INotificationService notification)
    {
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _method = method ?? throw new ArgumentNullException(nameof(method));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
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

        var res = await _method.LoginAsync(chatId, text);

        return new HandlerResult(res.NextStepKey);
    }
}
