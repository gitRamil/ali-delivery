using System.Collections.Concurrent;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Представляет потокобезопасную реализацию <see cref="IUserStateService" />, которая хранит
/// данные о состоянии, логинах и языке пользователей в памяти, используя <see cref="ConcurrentDictionary{TKey,TValue}" />.
/// </summary>
public sealed class InMemoryUserStateService : IUserStateService
{
    private const string DefaultState = Steps.Start;
    private readonly ConcurrentDictionary<long, string> _languages = new();
    private readonly ConcurrentDictionary<long, string> _states = new();

    /// <inheritdoc />
    public Task<string?> GetUserLanguageAsync(long userId) => Task.FromResult(_languages!.GetValueOrDefault(userId, null));

    /// <inheritdoc />
    /// <remarks>
    /// Если состояние для указанного пользователя не найдено, метод возвращает состояние по умолчанию,
    /// определенное в константе <see cref="DefaultState" />.
    /// </remarks>
    public Task<string> GetUserStepIdAsync(long userId)
    {
        var state = _states.GetValueOrDefault(userId, DefaultState);
        return Task.FromResult(state);
    }

    /// <inheritdoc />
    public Task SetUserLanguageAsync(long userId, string languageCode)
    {
        _languages[userId] = languageCode;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task SetUserStepAsync(long userId, string stateId)
    {
        _states[userId] = stateId;
        return Task.CompletedTask;
    }
}
