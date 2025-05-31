using Refit;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IFileServiceForLocation
{
    [Post("/api/v1/userlocation/create-location")]
    Task<ApiResponse<string>> CreateUserLocationAsync([AliasAs("userLogin")][Query] string userLogin, [AliasAs("e")][Query] string e, [AliasAs("s")][Query] string s);

    [Put("/api/v1/userlocation/update-location")]
    Task<ApiResponse<string>> UpdateUserLocationAsync([AliasAs("userLogin")][Query] string userLogin, [AliasAs("e")][Query] string e, [AliasAs("s")][Query] string s);
}
