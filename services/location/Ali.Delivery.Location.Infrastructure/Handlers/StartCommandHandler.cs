using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot.Types;
// Убедитесь, что неймспейс правильный

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class StartCommandHandler : ICommandHandler
{
    public string Command => "/start";

    public Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        if (update.Message?.Text == Command && (currentState == BotState.Initial || currentState == BotState.Terminated))
        {
            return Task.FromResult(new CommandResult(true, "OnStart"));
        }

        return Task.FromResult(new CommandResult(false, null));
    }
}
