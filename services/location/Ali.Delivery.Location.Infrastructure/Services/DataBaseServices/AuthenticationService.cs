using System.Net;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.Services.DataBaseServices;

public class AuthenticationService(IFileServiceForOrder api, ILogger<AuthenticationService> logger) : IAuthenticationService
{
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
            logger.LogWarning("401 for {Login}", login);
            return new AuthenticationResult(AuthResult.InvalidCredentials);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Auth error for {Login}", login);
            return new AuthenticationResult(AuthResult.Error);
        }
    }
}
