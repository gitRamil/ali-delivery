using System.Net;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Refit;
using LoginRequest = Ali.Delivery.Location.Infrastructure.Interfaces.LoginRequest;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class AuthenticationService(IFileServiceForOrder fileServiceForOrder, ILogger<AuthenticationService> logger) : IAuthenticationService
{
    public async Task<AuthenticationResult> AuthenticateAsync(string login, string password)
    {
        try
        {
            var token = await fileServiceForOrder.LoginAsync(new LoginRequest(login, password));

            if (string.IsNullOrEmpty(token))
            {
                logger.LogWarning("Authentication failed for login: {Login}. Empty token received.", login);
                return new AuthenticationResult(AuthResult.InvalidCredentials);
            }

            var userInfo = await fileServiceForOrder.GetCurrentUserAsync(token);

            if (userInfo?.Login == null)
            {
                return new AuthenticationResult(AuthResult.RegistrationRequired);
            }

            if (string.IsNullOrEmpty(userInfo.Login))
            {
                logger.LogWarning("Empty login in user info for login: {Login}", login);
                return new AuthenticationResult(AuthResult.RegistrationRequired);
            }

            logger.LogInformation("Successfully authenticated user: {Login}", login);
            return new AuthenticationResult(AuthResult.Success, userInfo.Login);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            logger.LogWarning("Invalid credentials for login: {Login}", login);
            return new AuthenticationResult(AuthResult.InvalidCredentials);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Authentication error for login: {Login}", login);
            return new AuthenticationResult(AuthResult.Error);
        }
    }
}
