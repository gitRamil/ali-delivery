using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IStateMachine
{
    Task<StateTransitionResult> ProcessUpdateAsync(long userId, Update update);
    Task<BotState> GetUserStateAsync(long userId);
    Task SetUserStateAsync(long userId, BotState state);
}
