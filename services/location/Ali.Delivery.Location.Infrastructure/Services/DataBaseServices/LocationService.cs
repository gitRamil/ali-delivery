using System.Globalization;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Services.WriteToDataBase;
using Microsoft.Extensions.Caching.Memory;

namespace Ali.Delivery.Location.Infrastructure.Services.DataBaseServices;

public class LocationService(IWriteToDatabase dbWriter, IMemoryCache cache) : ILocationService
{
    public async Task<bool> SaveLocationAsync(long userId, string userLogin, double latitude, double longitude)
    {
        cache.Set($"last_location_{userId}", (latitude, longitude), TimeSpan.FromMinutes(30));

        return await dbWriter.UpsertUserLocation(userLogin, latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture));
    }
}
