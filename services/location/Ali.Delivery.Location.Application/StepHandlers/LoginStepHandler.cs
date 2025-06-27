using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.StepHandlers;

public class LoginStepHandler : IStepHandler
{
    private readonly IAuthenticationService _authenticationService;
    private readonly INotificationService _notification;

    public LoginStepHandler(INotificationService notification, IAuthenticationService authenticationService)
    {
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
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.N1_InvalidAuthCommand);
            return new HandlerResult(string.Empty);
        }

        if (string.Equals(messageInfo.Text, "/stop", StringComparison.OrdinalIgnoreCase))
        {
            await _notification.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N_SessionEnded);
            return new HandlerResult("StopStep");
        }

        var res = await _authenticationService.LoginAsync(chatId, text);

        return new HandlerResult(res.NextStepKey);
    }
}
