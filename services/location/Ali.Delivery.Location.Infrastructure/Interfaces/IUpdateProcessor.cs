using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Interfaces2._0;

public interface IUpdateProcessor
{
    Task<CommandResult> ProcessUpdateWithHandlersAsync(long userId, Update update, BotState currentState);
}
