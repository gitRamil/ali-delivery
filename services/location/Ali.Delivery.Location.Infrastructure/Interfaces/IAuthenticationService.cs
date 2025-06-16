namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IAuthenticationService // TODO: Подумать над упрощением.
{
    Task<AuthenticationResult> AuthenticateAsync(string login, string password);
}

public record AuthenticationResult(AuthResult Status);

public enum AuthResult
{
    Success,
    InvalidCredentials,
    RegistrationRequired,
    Error
}
