using System.Net;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Application.Models.Authentication;
using Microsoft.Extensions.Logging;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Представляет реализацию сервиса аутентификации.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IFileServiceForOrder _fileServiceForOrder;
    private readonly ILogger<AuthenticationService> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthenticationService" />.
    /// </summary>
    /// <param name="fileServiceForOrder">Refit-клиент для отправки запросов к внешнему сервису.</param>
    /// <param name="logger">Логгер для записи событий и ошибок.</param>
    public AuthenticationService(IFileServiceForOrder fileServiceForOrder, ILogger<AuthenticationService> logger)
    {
        _fileServiceForOrder = fileServiceForOrder ?? throw new ArgumentNullException(nameof(fileServiceForOrder));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<AuthenticationResult> GetAuthenticationResult(string login, string password)
    {
        try
        {
            var token = await _fileServiceForOrder.LoginAsync(new LoginRequest(login, password));

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationResult(AuthResult.InvalidCredentials, null);
            }

            var userInfo = await _fileServiceForOrder.GetCurrentUserAsync($"Bearer {token}");

            return userInfo == null ? new AuthenticationResult(AuthResult.RegistrationRequired, null) : new AuthenticationResult(AuthResult.Success, userInfo.Id);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new AuthenticationResult(AuthResult.InvalidCredentials, null);
        }
        catch (Exception ex)
        {
            return new AuthenticationResult(AuthResult.Error, null);
        }
    }
}
