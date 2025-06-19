using Ali.Delivery.Location.Infrastructure.Models;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IAuthenticationService // TODO: Подумать над упрощением.
{
    Task<AuthenticationResult> AuthenticateAsync(string login, string password);
}
