using System.Globalization;
using Ali.Delivery.Location.Infrastructure.ExternalServices;
using Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;
using Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services.MyBotClient;

public class MyBotClient : IMyBotClient
{
    private readonly ITelegramBotClient _botClient;
    private readonly IWriteToDatabase _dbService;
    private readonly AuthService _authService;

    private readonly Dictionary<long, UserState> _userStates = new();
    private readonly Dictionary<long, string> _userLogins = new();

    public MyBotClient(IMyConfigurationService config, IWriteToDatabase dbService, AuthService authService)
    {
        _botClient = new TelegramBotClient(config.GetTgToken());
        _dbService = dbService;
        _authService = authService;
    }

    public void RunBot() => _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync);

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
    {
        var chatId = update.Message?.Chat.Id ?? 0;

        try
        {
            if (update.Message?.Location != null)
            {
                await HandleLocationAsync(chatId, update.Message.Location, ct);
                return;
            }

            if (update.Message?.Text is { } messageText)
            {
                switch (messageText.Split(' ')[0])
                {
                    case "/start":
                        await HandleStartCommandAsync(chatId, ct);
                        break;

                    case "/login":
                        await HandleLoginCommandAsync(chatId, ct);
                        break;

                    default:
                        await HandleOtherMessagesAsync(chatId, update.Message, ct);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update error: {ex.Message}");
        }
    }

    private async Task HandleStartCommandAsync(long chatId, CancellationToken ct)
    {
        _userStates[chatId] = UserState.WaitingForCommand;

        await _botClient.SendMessage(
            chatId: chatId,
            text: "🚀 Добро пожаловать! Я готов к работе.\n" +
                  "Используйте команды:\n" +
                  "/login - авторизация\n" +
                  "/help - справка",
            cancellationToken: ct);
    }

    private async Task HandleLoginCommandAsync(long chatId, CancellationToken ct)
    {
        _userStates[chatId] = UserState.WaitingForUsername;

        await _botClient.SendMessage(
            chatId: chatId,
            text: "🔑 Введите ваш логин для авторизации:",
            cancellationToken: ct);
    }

    private async Task HandleOtherMessagesAsync(long chatId, Message message, CancellationToken ct)
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

                await _botClient.SendMessage(
                    chatId: chatId,
                    text: "🔒 Теперь введите ваш пароль:",
                    cancellationToken: ct);
                break;

            case UserState.WaitingForPassword:
                var login = _userLogins[chatId];
                var password = text;

                var isAuthenticated = await _authService.AuthenticateUserAsync(login, password);

                if (isAuthenticated != null)
                {
                    _userStates[chatId] = UserState.Authorized;

                    await _botClient.SendMessage(
                        chatId: chatId,
                        text: $"✅ Авторизация успешна, {login}!\nТеперь вы можете делиться своей геопозицией",
                        cancellationToken: ct);
                }
                else
                {
                    _userStates.Remove(chatId);
                    _userLogins.Remove(chatId);

                    await _botClient.SendMessage(
                        chatId: chatId,
                        text: "❌ Неверный логин или пароль. Попробуйте снова: /login",
                        cancellationToken: ct);
                }
                break;

            case UserState.Authorized:
                await _botClient.SendMessage(
                    chatId: chatId,
                    text: "Вы уже авторизованы. Отправьте свою локацию или используйте команды.",
                    cancellationToken: ct);
                break;

            default:
                await _botClient.SendMessage(
                    chatId: chatId,
                    text: "⚠️ Неизвестная команда\nИспользуйте /help для списка команд",
                    cancellationToken: ct);
                break;
        }
    }

    private async Task HandleLocationAsync(long chatId, Telegram.Bot.Types.Location location, CancellationToken ct)
    {
        if (!_userStates.TryGetValue(chatId, out var state) || state != UserState.Authorized)
        {
            await _botClient.SendMessage(chatId: chatId, text: "🔒 Требуется авторизация!\nИспользуйте /login", cancellationToken: ct);
            return;
        }

        if (_userLogins.TryGetValue(chatId, out var login))
        {
            var success = await _dbService.UpsertUserLocation(login,
                                                              location.Latitude.ToString(CultureInfo.InvariantCulture),
                                                              location.Longitude.ToString(CultureInfo.InvariantCulture));

            if (success)
            {
                await _botClient.SendMessage(chatId: chatId,
                                             text: $"📍 Координаты обновлены:\n" + $"Широта: {location.Latitude}\n" + $"Долгота: {location.Longitude}",
                                             cancellationToken: ct);
            }
            else
            {
                await _botClient.SendMessage(chatId: chatId, text: "❌ Не удалось обновить координаты. Повторите попытку позже.", cancellationToken: ct);
            }
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient _, Exception ex, CancellationToken __)
    {
        Console.WriteLine($"Bot error: {ex.Message}");
        return Task.CompletedTask;
    }

    private enum UserState
    {
        WaitingForCommand,
        WaitingForUsername,
        WaitingForPassword,
        Authorized
    }
}
