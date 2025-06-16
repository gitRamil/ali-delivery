using System.Collections.Concurrent;
using Ali.Delivery.Location.Infrastructure.Interfaces;

namespace Ali.Delivery.Location.Infrastructure.Services;

public sealed class InMemoryUserStateService : IUserStateService // TODO: Объединить в одну user - сессию.
{
    private readonly ConcurrentDictionary<long, string> _states = new();
    private readonly ConcurrentDictionary<long, string?> _logins = new();
    private const string DefaultState = "StartStep";

    public Task<string> GetUserStateAsync(long telegramUserId)
    {
        var state = _states.GetValueOrDefault(telegramUserId, DefaultState);
        return Task.FromResult(state);
    }

    public Task SetUserStateAsync(long telegramUserId, string stateId)
    {
        _states[telegramUserId] = stateId;
        return Task.CompletedTask;
    }
    
    public Task SetUserLoginAsync(long userId, string login)
    {
        _logins[userId] = login;
        return Task.CompletedTask;
    }

    public Task<string?> GetUserLoginAsync(long userId)
        => Task.FromResult(_logins.GetValueOrDefault(userId, null));
}

