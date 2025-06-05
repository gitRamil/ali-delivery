using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class LoginCommandHandler : ICommandHandler
{
    public string Command => "/login";

    public Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        if (update.Message?.Text == "/login" && currentState == BotState.WaitingAuthCommand)
        {
            return Task.FromResult(new CommandResult(true, "OnLogin"));
        }

        return Task.FromResult(new CommandResult(false, null));
    }
}
