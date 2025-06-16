using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IStateMachine
{
    Task ProcessUpdateAsync(Update update);
}
