using Refit;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IFileServiceForOrder
{
    [Get("/api/v1/user/get-current-user")]
    Task<UserInfo?> GetCurrentUserAsync([Header("Authorization")] string authorization); // Добавлен nullable-маркер

    [Post("/api/v1/user/login")]
    Task<string?> LoginAsync([Body] LoginRequest loginRequest); // Добавлен nullable-маркер
}

public record UserInfo(string? Login); // Пример модели с nullable-полями
public record LoginRequest(string Login, string Password);