namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IUserStateService // TODO: Сделать единую сессию.
{
    Task<string?> GetUserLoginAsync(long userId);

    Task<string> GetUserStateAsync(long userId);

    Task SetUserLoginAsync(long userId, string login);

    Task SetUserStateAsync(long userId, string stateId);
}
