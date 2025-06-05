using Refit;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IFileServiceForOrder
{
    [Get("/api/v1/user/get-current-user")]
    Task<UserInfo?> GetCurrentUserAsync([Header("Authorization")] string authorization);

    [Post("/api/v1/user/login")]
    Task<string?> LoginAsync([Body] LoginRequest loginRequest);
}

public record UserInfo(string? Login);

public record LoginRequest(string Login, string Password);
