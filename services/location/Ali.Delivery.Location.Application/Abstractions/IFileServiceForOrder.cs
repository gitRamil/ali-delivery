using Ali.Delivery.Location.Application.Models;
using Refit;

namespace Ali.Delivery.Location.Application.Abstractions;

public interface IFileServiceForOrder
{
    [Get("/api/v1/user/get-current-user")]
    Task<UserInfo?> GetCurrentUserAsync([Header("Authorization")] string authorization);

    [Post("/api/v1/user/login")]
    Task<string?> LoginAsync([Body] LoginRequest loginRequest);
}
