using System.Globalization;
using Ali.Delivery.Location.Infrastructure.ExternalServices;
using Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;
using Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services.MyBotClient;

public class MyBotClient : IMyBotClient
{
    private const string AlreadyAuthorized = "Вы уже авторизованы. Отправьте свою локацию или используйте команды.";
    private const string AuthorizationIsRequired = "🔒 Требуется авторизация!\nИспользуйте /login";
    private const string AuthorizationMessage = "🔑 Введите ваш логин для авторизации:";
    private const string CannotUpdateLocation = "❌ Не удалось обновить координаты. Повторите попытку позже.";
    private const string PasswordMessage = "🔒 Теперь введите ваш пароль:";
    private const string UnsupportedCommand = "⚠️ Неизвестная команда\nИспользуйте /help для списка команд";
    private const string WelcomeMessage = "🚀 Добро пожаловать! Я готов к работе.\n Используйте команды:\n login - авторизация\n help - справка";
    private const string WrongPasswordMessage = "❌ Неверный логин или пароль. Попробуйте снова: /login";
    private readonly AuthService _authService;
    private readonly ITelegramBotClient _botClient;
    private readonly IWriteToDatabase _dbService;
    private readonly Dictionary<long, string> _userLogins = new();
    private readonly Dictionary<long, UserState> _userStates = new();

    public MyBotClient(IMyConfigurationService config, IWriteToDatabase dbService, AuthService authService)
    {
        _botClient = new TelegramBotClient(config.GetTgToken());
        _dbService = dbService;
        _authService = authService;
    }

    public async Task RunBot(CancellationToken cancellationToken)
    {
        _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, cancellationToken: cancellationToken);
        await Task.CompletedTask;
    }

    private static Task HandleErrorAsync(ITelegramBotClient _, Exception ex, CancellationToken __)
    {
        Console.WriteLine($"Bot error: {ex.Message}");
        return Task.CompletedTask;
    }

    private async Task HandleLocationAsync(long chatId, Telegram.Bot.Types.Location location, CancellationToken cancellationToken)
    {
        if (!_userStates.TryGetValue(chatId, out var state) || state != UserState.Authorized)
        {
            await _botClient.SendMessage(chatId, AuthorizationIsRequired, cancellationToken: cancellationToken);
            return;
        }

        if (_userLogins.TryGetValue(chatId, out var login))
        {
            var success = await _dbService.UpsertUserLocation(login,
                                                              location.Latitude.ToString(CultureInfo.InvariantCulture),
                                                              location.Longitude.ToString(CultureInfo.InvariantCulture));

            if (success)
            {
                await _botClient.SendMessage(chatId,
                                             $"📍 Координаты обновлены:\n" + $"Широта: {location.Latitude}\n" + $"Долгота: {location.Longitude}",
                                             cancellationToken: cancellationToken);
            }
            else
            {
                await _botClient.SendMessage(chatId, CannotUpdateLocation, cancellationToken: cancellationToken);
            }
        }
    }

    private async Task HandleOtherMessagesAsync(long chatId, Message message, CancellationToken cancellationToken)
    {
        if (!_userStates.TryGetValue(chatId, out var state))
        {
            return;
        }

        var text = message.Text?.Trim();

        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        switch (state)
        {
            case UserState.WaitingForUsername:
                _userLogins[chatId] = text;
                _userStates[chatId] = UserState.WaitingForPassword;

                await SentTelegramMessage(chatId, PasswordMessage, cancellationToken);
                break;

            case UserState.WaitingForPassword:
                var login = _userLogins[chatId];
                var password = text;

                var isAuthenticated = await _authService.AuthenticateUserAsync(login, password);

                if (isAuthenticated != null)
                {
                    _userStates[chatId] = UserState.Authorized;

                    await SentTelegramMessage(chatId, $"✅ Авторизация успешна, {login}!\nТеперь вы можете делиться своей геопозицией", cancellationToken);
                }
                else
                {
                    _userStates.Remove(chatId);
                    _userLogins.Remove(chatId);

                    await SentTelegramMessage(chatId, WrongPasswordMessage, cancellationToken);
                }

                break;

            case UserState.Authorized:
                await _botClient.SendMessage(chatId, AlreadyAuthorized, cancellationToken: cancellationToken);
                break;

            default:
                await _botClient.SendMessage(chatId, UnsupportedCommand, cancellationToken: cancellationToken);
                break;
        }
    }

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        var chatId = update.Message?.Chat.Id ?? 0;

        try
        {
            if (update.Message?.Location != null)
            {
                await HandleLocationAsync(chatId, update.Message.Location, cancellationToken);
                return;
            }

            if (update.Message?.Text is { } messageText)
            {
                switch (messageText.Split(' ')[0])
                {
                    case "/start":
                        await SentTelegramMessage(chatId, WelcomeMessage, cancellationToken);
                        break;

                    case "/login":
                        await SentTelegramMessage(chatId, AuthorizationMessage, cancellationToken);
                        break;

                    default:
                        await HandleOtherMessagesAsync(chatId, update.Message, cancellationToken);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update error: {ex.Message}");
        }
    }

    private async Task SentTelegramMessage(long chatId, string message, CancellationToken cancellationToken)
    {
        _userStates[chatId] = UserState.WaitingForCommand;

        await _botClient.SendMessage(chatId, message, cancellationToken: cancellationToken);
    }

    private enum UserState
    {
        WaitingForCommand,
        WaitingForUsername,
        WaitingForPassword,
        Authorized
    }
}
