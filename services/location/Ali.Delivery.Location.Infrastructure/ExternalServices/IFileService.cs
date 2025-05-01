using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.ExternalServices;

public interface IFileService
{
    [Post("/api/v1/user/login")]
        Task<string> LoginAsync([Body] LoginRequest loginRequest);
        
    [Get("/api/v1/user/is-user-exist")]
        Task<bool> IsUserExistAsync(Guid id);
    
    [Get("/api/v1/user/get-current-user")]
        Task<UserInfo> GetCurrentUserAsync([Header("Authorization")] string authorization);
    
        
}