using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class StopGeosharingCommandHandler : ICommandHandler
{
    public string Command => "/stop_geosharing";

    public Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        if (update.Message?.Text == Command && currentState == BotState.GeosharingActive)
        {
            return Task.FromResult(new CommandResult(true, "OnStopGeosharing"));
        }

        return Task.FromResult(new CommandResult(false, null));
    }
}

