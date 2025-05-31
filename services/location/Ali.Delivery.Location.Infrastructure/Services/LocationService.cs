using System.Globalization;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Services.WriteToDataBase;
using Microsoft.Extensions.Caching.Memory;

namespace Ali.Delivery.Location.Infrastructure.Services;

// Services/LocationService.cs
public class LocationService(IWriteToDatabase dbWriter, IMemoryCache cache) : ILocationService
{
    public async Task<bool> SaveLocationAsync(long userId, string userLogin, double latitude, double longitude)
    {
        // Сохранение в кэш
        cache.Set($"last_location_{userId}", 
                   (latitude, longitude), 
                   TimeSpan.FromMinutes(30));
        
        // Сохранение в БД через API
        return await dbWriter.UpsertUserLocation(userLogin, 
                                                  latitude.ToString(CultureInfo.InvariantCulture),
                                                  longitude.ToString(CultureInfo.InvariantCulture));
    }
}

