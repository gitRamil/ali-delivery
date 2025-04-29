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
        // 1. Логинимся
        var loginResponse = await _fileService.LoginAsync(new LoginRequest { Login = login, Password = password });

        if (string.IsNullOrEmpty(loginResponse.Token))
        {
            Console.WriteLine($"Ошибка логина: {loginResponse.Error}");
            return false;
        }

        // 2. Получаем текущего пользователя по токену
        var userInfo = await _fileService.GetCurrentUserAsync("Bearer " + loginResponse.Token);

        // 3. Проверяем существует ли пользователь по ID
        bool exists = await _fileService.IsUserExistAsync(userInfo.Id);

        return exists;
    }
}