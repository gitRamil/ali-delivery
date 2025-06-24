using System.Net;
using Ali.Delivery.Location.Application.Interfaces;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Refit;
using Telegram.Bot;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IFileServiceForOrder _api;
    private readonly ITelegramBotClient _bot;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly INotificationService _notification;
    private readonly IUserStateService _userStateService;

    public AuthenticationService(IFileServiceForOrder api,
                                 ILogger<AuthenticationService> logger,
                                 IUserStateService userStateService,
                                 ITelegramBotClient bot,
                                 INotificationService notification)
    {
        _api = api ?? throw new ArgumentNullException(nameof(api));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userStateService = userStateService ?? throw new ArgumentNullException(nameof(userStateService));
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
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

    public async Task<CommandResult> LoginAsync(long chatId, string text)
    {
        var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
        {
            await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N0_EnterCredentials));

            return new CommandResult(string.Empty);
        }

        var (login, password) = (parts[0], parts[1]);
        var auth = await AuthenticateAsync(login, password);

        switch (auth.Status)
        {
            case AuthResult.Success:
                await _userStateService.SetUserLoginAsync(chatId, login);
                await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N4_AuthenticationComplete));
                return new CommandResult("AuthComplete");

            case AuthResult.InvalidCredentials:
                await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N2_InvalidCredentials));

                return new CommandResult(string.Empty);

            default:
                await _bot.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidCommand));

                return new CommandResult(string.Empty);
        }
    }
}
