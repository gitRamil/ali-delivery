using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class StopCommandHandler : ICommandHandler
{
    public string Command => "/stop";

    public Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        if (update.Message?.Text?.Trim() == "/stop")
        {
            return Task.FromResult(currentState switch
            {
                BotState.Authenticated or BotState.GeosharingActive => new CommandResult(true,
                                                                                         "OnStop",
                                                                                         new Dictionary<string, object>
                                                                                         {
                                                                                             ["Action"] = "SessionStopped"
                                                                                         }),

                BotState.Terminated => new CommandResult(true,
                                                         "OnInitial",
                                                         new Dictionary<string, object>
                                                         {
                                                             ["Action"] = "RestartSession"
                                                         }),

                _ => new CommandResult(false, null)
            });
        }

        return Task.FromResult(new CommandResult(false, null));
    }
}
