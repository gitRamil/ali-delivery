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
    private readonly ConcurrentDictionary<long, string> _states = new();

    /// <inheritdoc />
    /// <remarks>
    /// Если состояние для указанного пользователя не найдено, метод возвращает состояние по умолчанию,
    /// определенное в константе <see cref="DefaultState" />.
    /// </remarks>
    public Task<string> GetUserStepId(long userId)
    {
        var state = _states.GetValueOrDefault(userId, DefaultState);
        return Task.FromResult(state);
    }

    /// <inheritdoc />
    public Task SetUserStep(long userId, string stateId)
    {
        _states[userId] = stateId;
        return Task.CompletedTask;
    }
}
