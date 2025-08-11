using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.Extensions;
using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Application.Models.Authentication;
using Ali.Delivery.Location.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Application.StepHandlers;

/// <summary>
/// Представляет обработчик для шага аутентификации пользователя ("Authorization").
/// Его задача — получить учетные данные от пользователя, передать их сервису аутентификации
/// и вернуть результат для перехода в следующее состояние.
/// </summary>
public class LoginStepHandler : IStepHandler
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAppDbContext _dbContext;
    private readonly INotificationService _notification;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LoginStepHandler" />.
    /// </summary>
    /// <param name="notification">Сервис для отправки уведомлений пользователю.</param>
    /// <param name="authenticationService">Сервис для выполнения логики аутентификации.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
    public LoginStepHandler(INotificationService notification, IAuthenticationService authenticationService, IAppDbContext dbContext)
    {
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc />
    public async Task<HandlerResult> HandleAsync(MessageInfo messageInfo, CancellationToken cancellationToken = default)
    {
        var chatId = messageInfo.ChatId;

        if (chatId == 0)
        {
            return new HandlerResult(string.Empty);
        }

        if (messageInfo is not { Text: { } text })
        {
            await _notification.SendNotificationMessageAsync(chatId, NotificationType.InvalidAuthCommand, cancellationToken: cancellationToken);
            return new HandlerResult(string.Empty);
        }

        switch (text.MessageToCommand())
        {
            case Commands.Start:
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.Welcome, cancellationToken: cancellationToken);
                return new HandlerResult(Steps.Start);
            default:
                return await AuthenticateUser();
        }

        async Task<HandlerResult> AuthenticateUser()
        {
            var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                await _notification.SendNotificationMessageAsync(chatId, NotificationType.EnterCredentials, cancellationToken: cancellationToken);
                return new HandlerResult(string.Empty);
            }

            var (login, password) = (parts[0], parts[1]);

            var authenticationResult = await _authenticationService.GetAuthenticationResult(login, password);

            switch (authenticationResult.AuthResult)
            {
                case AuthResult.Success:
                    await _notification.SendNotificationMessageAsync(chatId, NotificationType.AuthenticationComplete, cancellationToken: cancellationToken);
                    await _notification.SendNotificationMessageAsync(chatId, NotificationType.LanguagePrompt, cancellationToken: cancellationToken);
                    await CreateNewUserAsync(authenticationResult.UserId, login, chatId, cancellationToken);
                    return new HandlerResult(Steps.LanguageSelection);
                case AuthResult.InvalidCredentials:
                    await _notification.SendNotificationMessageAsync(chatId, NotificationType.InvalidCredentials, cancellationToken: cancellationToken);
                    return new HandlerResult(string.Empty);
                default:
                    await _notification.SendNotificationMessageAsync(chatId, NotificationType.InvalidCommand, cancellationToken: cancellationToken);
                    return new HandlerResult(string.Empty);
            }
        }
    }

    private async Task CreateNewUserAsync(Guid? userId, string login, long chatId, CancellationToken cancellationToken)
    {
        if (userId == null)
        {
            return;
        }

        var isUserExist = await _dbContext.Users.AnyAsync(u => u.Id == userId, cancellationToken);

        if (isUserExist)
        {
            return;
        }

        var newUser = new User(userId.Value, login, chatId);
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
