using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class UserDataManager : IUserDataManager
{
    private const string LoginCacheKey = "user_login_{0}";
    
    private readonly IMemoryCache _cache;
    private readonly ILogger<UserDataManager> _logger;

    public UserDataManager(IMemoryCache cache, ILogger<UserDataManager> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void SaveUserData(long userId, Dictionary<string, object> userData)
    {
        if (userData.TryGetValue("Login", out var loginObj) && loginObj is string login)
        {
            if (!string.IsNullOrEmpty(login))
            {
                _cache.Set(
                    string.Format(LoginCacheKey, userId),
                    login,
                    TimeSpan.FromHours(24));

                _logger.LogInformation("Cached login '{Login}' for user {UserId}", login, userId);
            }
            else
            {
                _logger.LogWarning("Attempted to cache empty login for user {UserId}", userId);
            }
        }
        else
        {
            _logger.LogTrace("UserData for user {UserId} does not contain a 'Login' entry or it's not a string", userId);
        }
    }

    public Task<string?> GetUserLoginAsync(long userId)
    {
        var cacheKey = string.Format(LoginCacheKey, userId);
        var login = _cache.Get<string>(cacheKey);
        return Task.FromResult(login);
    }

    public void ClearUserData(long userId)
    {
        var loginCacheKey = string.Format(LoginCacheKey, userId);
        _cache.Remove(loginCacheKey);
        _logger.LogInformation("Cleared user data for user {UserId}", userId);
    }
}
