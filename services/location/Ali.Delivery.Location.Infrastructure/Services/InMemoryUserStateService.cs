using System.Collections.Concurrent;
using Ali.Delivery.Location.Infrastructure.Interfaces;

namespace Ali.Delivery.Location.Infrastructure.Services;

public sealed class InMemoryUserStateService : IUserStateService // TODO: Объединить в одну user - сессию.
{
    private const string DefaultState = "StartStep";
    private readonly ConcurrentDictionary<long, string?> _logins = new();
    private readonly ConcurrentDictionary<long, string> _states = new();

    public Task<string?> GetUserLoginAsync(long userId) => Task.FromResult(_logins.GetValueOrDefault(userId, null));

    public Task<string> GetUserStepIdAsync(long userId)
    {
        var state = _states.GetValueOrDefault(userId, DefaultState);
        return Task.FromResult(state);
    }

    public Task SetUserLoginAsync(long userId, string login)
    {
        _logins[userId] = login;
        return Task.CompletedTask;
    }

    public Task SetUserStepAsync(long userId, string stateId)
    {
        _states[userId] = stateId;
        return Task.CompletedTask;
    }
}
