using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Abstractions;

public interface ILookupProvider
{
    Task<Dictionary<string, string>> GetUserLanguagesAsync();
    Task<Dictionary<Guid, LocationInfo>> GetUserLocationsAsync();
    Task Reset();
}