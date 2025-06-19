using System.Globalization;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class LocationService : ILocationService
{
    private readonly IMemoryCache _cache;
    private readonly IWriteToDatabase _dbWriter;

    public LocationService(IMemoryCache cache, IWriteToDatabase dbWriter)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _dbWriter = dbWriter ?? throw new ArgumentNullException(nameof(dbWriter));
    }

    public async Task<bool> SaveLocationAsync(long userId, string userLogin, double latitude, double longitude)
    {
        _cache.Set($"last_location_{userId}", (latitude, longitude), TimeSpan.FromMinutes(30));

        return await _dbWriter.UpsertUserLocation(userLogin, latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture));
    }
}
