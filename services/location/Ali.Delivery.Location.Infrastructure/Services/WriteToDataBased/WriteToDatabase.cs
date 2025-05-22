using System.Net;
using Ali.Delivery.Location.Infrastructure.ExternalServices;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;

public class WriteToDatabase(IFileServiceForLocation serviceForLocation) : IWriteToDatabase
{
    public async Task<bool> UpsertUserLocation(string userLogin, string? e = null, string? s = null)
    {
        try
        {
            var updateResponse = await serviceForLocation.UpdateUserLocationAsync(
                userLogin,
                e ?? "",
                s ?? ""
            );

            if (updateResponse.StatusCode == HttpStatusCode.NotFound)
            {
                var createResponse = await serviceForLocation.CreateUserLocationAsync(
                    userLogin,
                    e ?? "",
                    s ?? ""
                );
                return createResponse.IsSuccessStatusCode;
            }

            if (!updateResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Update failed: {updateResponse.StatusCode}");
                return false;
            }

            return true;
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
            return false;
        }
    }
}