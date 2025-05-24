using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

namespace Ali.Delivery.Location.Infrastructure.ExternalServices;

public class AuthService
{
    private readonly IFileServiceForOrder _fileServiceForOrder;

    public AuthService(IFileServiceForOrder fileServiceForOrder) => _fileServiceForOrder = fileServiceForOrder;

    public async Task<string?> AuthenticateUserAsync(string login, string password)
    {
        var token = await _fileServiceForOrder.LoginAsync(new LoginRequest
        {
            Login = login,
            Password = password
        });

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Ошибка логина: пустой токен");
            return null;
        }

        var userInfo = await _fileServiceForOrder.GetCurrentUserAsync("Bearer " + token);

        return userInfo.Login;
    }
}
