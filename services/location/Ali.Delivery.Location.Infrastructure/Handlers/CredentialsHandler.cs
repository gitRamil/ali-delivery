using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class CredentialsHandler(IAuthenticationService authService, ILogger<CredentialsHandler> logger) : ICommandHandler
{
    public string Command => "credentials";

    public async Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        if (currentState == BotState.WaitingCredentials && update.Message?.Text != null)
        {
            var credentials = ParseCredentials(update.Message.Text);

            if (!credentials.HasValue)
            {
                logger.LogWarning("Invalid credentials format from user {UserId}", userId);
                return new CommandResult(true, "OnInvalidCredentials");
            }

            try
            {
                var authResult = await authService.AuthenticateAsync(credentials.Value.login, credentials.Value.password);

                return authResult.Status switch
                {
                    AuthResult.Success => new CommandResult(true,
                                                            "OnValidCredentials",
                                                            new Dictionary<string, object>
                                                            {
                                                                ["Login"] = authResult.Login!
                                                            }),

                    AuthResult.InvalidCredentials => new CommandResult(true, "OnInvalidCredentials"),

                    AuthResult.RegistrationRequired => new CommandResult(true, "OnRegistrationRequired"),

                    _ => new CommandResult(true, "OnInvalidCredentials")
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Authentication error for user {UserId}", userId);
                return new CommandResult(true, "OnInvalidCredentials");
            }
        }

        return new CommandResult(false, null);
    }

    private static (string login, string password)? ParseCredentials(string text)
    {
        var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length == 2 
                   ? (login: parts[0], password: parts[1]) 
                   : null;
    }
}