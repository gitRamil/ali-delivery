using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class StateManager : IStateManager
{
    private const string StateCacheKey = "user_state_{0}";
    
    private readonly IMemoryCache _stateCache;
    private readonly ILogger<StateManager> _logger;

    public StateManager(IMemoryCache stateCache, ILogger<StateManager> logger)
    {
        _stateCache = stateCache ?? throw new ArgumentNullException(nameof(stateCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<BotState> GetUserStateAsync(long userId)
    {
        var cacheKey = string.Format(StateCacheKey, userId);
        return _stateCache.GetOrCreateAsync(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
            _logger.LogInformation("User {UserId} state initialized to Initial as not found in cache", userId);
            return Task.FromResult(BotState.Initial);
        });
    }

    public Task SetUserStateAsync(long userId, BotState state)
    {
        var cacheKey = string.Format(StateCacheKey, userId);
        _stateCache.Set(cacheKey, state, TimeSpan.FromHours(24));
        _logger.LogInformation("User {UserId} state changed to {State}", userId, state);
        return Task.CompletedTask;
    }
}
