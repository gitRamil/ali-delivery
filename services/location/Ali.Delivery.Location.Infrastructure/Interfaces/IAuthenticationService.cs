namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticationResult> AuthenticateAsync(string login, string password);
}

public record AuthenticationResult(AuthResult Status, string? Login = null);

public enum AuthResult
{
    Success,
    InvalidCredentials,
    RegistrationRequired,
    Error
}
