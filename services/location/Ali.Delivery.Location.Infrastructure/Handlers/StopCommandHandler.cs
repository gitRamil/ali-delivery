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
            // Всегда возвращаем OnStop, вне зависимости от состояния
            return Task.FromResult(new CommandResult(
                                       true,
                                       "OnStop",
                                       new Dictionary<string, object>
                                       {
                                           ["Action"] = "SessionStopped"
                                       }));
        }

        return Task.FromResult(new CommandResult(false, null));
    }
}
