using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class GeosharingCommandHandler : ICommandHandler
{
    public string Command => "/geosharing";

    public Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        var result = update.Message?.Text == "/geosharing" && currentState == BotState.Authenticated ? new CommandResult(true, "OnGeosharing") : new CommandResult(false, null);

        return Task.FromResult(result);
    }
}
