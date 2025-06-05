using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface ITransitionResolver
{
    StateTransitionResult HandleUnknownAction(BotState currentState, Update update);

    StateTransition? ResolveTransition(BotState currentState, string? transitionKey);
}
