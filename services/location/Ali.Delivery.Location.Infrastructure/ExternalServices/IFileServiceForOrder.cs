using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.ExternalServices;

public interface IFileServiceForOrder
{
    [Post("/api/v1/user/login")]
        Task<string> LoginAsync([Body] LoginRequest loginRequest);
        
    [Get("/api/v1/user/get-current-user")]
        Task<UserInfo> GetCurrentUserAsync([Header("Authorization")] string authorization);
    
        
}