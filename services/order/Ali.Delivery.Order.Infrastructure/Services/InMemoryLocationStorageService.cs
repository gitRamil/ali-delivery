using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Models;

namespace Ali.Delivery.Order.Infrastructure.services;

/// <summary>
/// Реализация сервиса хранения локаций в памяти.
/// </summary>
public class InMemoryLocationStorageService : ILocationStorageService
{
    private readonly List<ReceivedLocation> _locations = new();
    private readonly object _lock = new();

    /// <inheritdoc />
    public Task AddLocationAsync(ReceivedLocation location)
    {
        lock (_lock)
        {
            _locations.Add(location);

            if (_locations.Count > 1000) _locations.RemoveRange(0, _locations.Count - 1000);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<List<ReceivedLocation>> GetAllLocationsAsync()
    {
        lock (_lock)
        {
            return Task.FromResult(_locations.OrderByDescending(l => l.ReceivedAt).ToList());
        }
    }

    /// <inheritdoc />
    public Task<List<ReceivedLocation>> GetLocationsByChatIdAsync(long chatId)
    {
        lock (_lock)
        {
            return Task.FromResult(_locations
                .Where(l => l.ChatId == chatId)
                .OrderByDescending(l => l.ReceivedAt)
                .ToList());
        }
    }

    /// <inheritdoc />
    public Task<List<ReceivedLocation>> GetRecentLocationsAsync(int count = 10)
    {
        lock (_lock)
        {
            return Task.FromResult(_locations
                .OrderByDescending(l => l.ReceivedAt)
                .Take(count)
                .ToList());
        }
    }

    /// <inheritdoc />
    public Task ClearAllLocationsAsync()
    {
        lock (_lock)
        {
            _locations.Clear();
        }

        return Task.CompletedTask;
    }
}