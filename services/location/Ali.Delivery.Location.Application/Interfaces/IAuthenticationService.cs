using Ali.Delivery.Location.Infrastructure.Models;

namespace Ali.Delivery.Location.Application.Interfaces;

public interface IAuthenticationService // TODO: Подумать над упрощением.
{
    Task<AuthenticationResult> AuthenticateAsync(string login, string password);

    Task<CommandResult> LoginAsync(long chatId, string text);
}
