using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Interfaces2._0;

public interface ITransitionResolver
{
    StateTransition? ResolveTransition(BotState currentState, string? transitionKey);
    StateTransitionResult HandleUnknownAction(BotState currentState, Update update);
}