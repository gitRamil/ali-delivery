namespace Ali.Delivery.Location.Application.Interfaces;

public interface IUserStateService // TODO: Сделать единую сессию.
{
    Task<string?> GetUserLoginAsync(long userId);

    Task<string> GetUserStepIdAsync(long userId);

    Task SetUserLoginAsync(long userId, string login);

    Task SetUserStepAsync(long userId, string stateId);
}
