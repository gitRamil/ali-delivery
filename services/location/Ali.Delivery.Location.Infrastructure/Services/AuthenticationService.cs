using System.Net;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Application.Models.Authentication;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Представляет реализацию сервиса аутентификации.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IFileServiceForOrder _fileServiceForOrder;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthenticationService" />.
    /// </summary>
    /// <param name="fileServiceForOrder">Refit-клиент для отправки запросов к внешнему сервису.</param>
    public AuthenticationService(IFileServiceForOrder fileServiceForOrder)
    {
        _fileServiceForOrder = fileServiceForOrder ?? throw new ArgumentNullException(nameof(fileServiceForOrder));
    }

    /// <inheritdoc />
    public async Task<AuthenticationResult> GetAuthenticationResult(string login, string password)
    {
        try
        {
            var token = await _fileServiceForOrder.LoginAsync(new LoginRequest(login, password));

            if (string.IsNullOrWhiteSpace(token)) return new AuthenticationResult(AuthResult.InvalidCredentials, null);

            var userInfo = await _fileServiceForOrder.GetCurrentUserAsync($"Bearer {token}");

            return userInfo == null
                ? new AuthenticationResult(AuthResult.RegistrationRequired, null)
                : new AuthenticationResult(AuthResult.Success, userInfo.Id);
        }
        catch (ApiException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized)
                return new AuthenticationResult(AuthResult.InvalidCredentials, null);

            return new AuthenticationResult(AuthResult.Error, null);
        }
    }
}