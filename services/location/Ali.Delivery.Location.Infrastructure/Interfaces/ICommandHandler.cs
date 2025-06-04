using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface ICommandHandler
{
    Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState);
}
