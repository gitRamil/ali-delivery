using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

namespace Ali.Delivery.Location.Infrastructure.Interfaces2._0;

public interface IStateManager
{
    Task<BotState> GetUserStateAsync(long userId);
    Task SetUserStateAsync(long userId, BotState state);
}
