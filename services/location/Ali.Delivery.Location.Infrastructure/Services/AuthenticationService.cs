using System.Net;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IFileServiceForOrder _api;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(IFileServiceForOrder api, ILogger<AuthenticationService> logger)
    {
        _api = api ?? throw new ArgumentNullException(nameof(api));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AuthenticationResult> AuthenticateAsync(string login, string password)
    {
        try
        {
            // var token = await api.LoginAsync(new LoginRequest(login, password));
            //
            // if (string.IsNullOrWhiteSpace(token))
            // {
            //     logger.LogWarning("Empty token for {Login}", login);
            //     return new AuthenticationResult(AuthResult.InvalidCredentials);
            // }
            //
            // var user = await api.GetCurrentUserAsync($"Bearer {token}");
            //
            // if (string.IsNullOrWhiteSpace(user?.Login))
            // {
            //     logger.LogWarning("User must register: {Login}", login);
            //     return new AuthenticationResult(AuthResult.RegistrationRequired);
            // }
            //
            // logger.LogInformation("Login OK: {Login}", user.Login);
            return new AuthenticationResult(AuthResult.Success);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogWarning("401 for {Login}", login);
            return new AuthenticationResult(AuthResult.InvalidCredentials);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Auth error for {Login}", login);
            return new AuthenticationResult(AuthResult.Error);
        }
    }
}
