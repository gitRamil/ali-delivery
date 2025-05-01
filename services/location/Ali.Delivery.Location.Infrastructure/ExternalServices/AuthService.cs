using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

namespace Ali.Delivery.Location.Infrastructure.ExternalServices;

public class AuthService
{
    private readonly IFileService _fileService;

    public AuthService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<bool> AuthenticateUserAsync(string login, string password)
    {
        var token = await _fileService.LoginAsync(new LoginRequest { Login = login, Password = password });

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Ошибка логина: пустой токен");
            return false;
        }

        var userInfo = await _fileService.GetCurrentUserAsync("Bearer " + token);

        bool exists = await _fileService.IsUserExistAsync(userInfo.Id.ToString("D"));

        return exists;
    }
}