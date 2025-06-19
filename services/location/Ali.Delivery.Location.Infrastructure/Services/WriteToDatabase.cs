using System.Net;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class WriteToDatabase : IWriteToDatabase
{
    private readonly IFileServiceForLocation _fileServiceForLocation;

    public WriteToDatabase(IFileServiceForLocation serviceForLocation) =>
        _fileServiceForLocation = serviceForLocation ?? throw new ArgumentNullException(nameof(serviceForLocation));

    public async Task<bool> UpsertUserLocation(string userLogin, string? e = null, string? s = null)
    {
        try
        {
            var updateResponse = await _fileServiceForLocation.UpdateUserLocationAsync(userLogin, e ?? string.Empty, s ?? string.Empty);

            if (updateResponse.StatusCode == HttpStatusCode.NotFound)
            {
                var createResponse = await _fileServiceForLocation.CreateUserLocationAsync(userLogin, e ?? string.Empty, s ?? string.Empty);
                return createResponse.IsSuccessStatusCode;
            }

            if (updateResponse.IsSuccessStatusCode)
            {
                return true;
            }

            Console.WriteLine($"Update failed: {updateResponse.StatusCode}");
            return false;
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
            return false;
        }
    }
}
