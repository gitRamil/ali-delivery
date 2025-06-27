using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Application.Models.Authentication;

namespace Ali.Delivery.Location.Application.Interfaces;

public interface IAuthenticationService // TODO: Подумать над упрощением.
{
    Task<AuthenticationResult> AuthenticateAsync(string login, string password);

    Task<CommandResult> LoginAsync(long chatId, string text);
}
