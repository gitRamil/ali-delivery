using System.Globalization;
using Ali.Delivery.Location.Infrastructure.Services.MyConfiguration;
using Ali.Delivery.Location.Infrastructure.Services.WriteToDataBased;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services.MyBotClient;

public class MyBotClient(IMyConfigurationService config, IWriteToDatabase dbService) : IMyBotClient
{
    private readonly ITelegramBotClient _botClient = new TelegramBotClient(config.GetTgToken());
    private readonly Dictionary<long, UserState> _userStates = new();
    private readonly Dictionary<long, string> _userLogins = new();

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

        if (state == UserState.WaitingForUsername && !string.IsNullOrEmpty(message.Text))
        {
            var login = message.Text.Trim();
            var telegramUsername = message.From?.Username;
            var exists = await dbService.CheckUserExists(login,password);
            
            if (exists)
            {
                var created = telegramUsername != null && await dbService.CreateUserLocationIfNotExists(telegramUsername);
                
                if (created)
                {
                    _userStates[chatId] = UserState.Authorized;
                    _userLogins[chatId] = telegramUsername!;
                    await _botClient.SendMessage(
                        chatId: chatId,
                        text: $"✅ Авторизация успешна, {telegramUsername}!\n" +
                              "Теперь вы можете делиться своей геопозицией",
                        cancellationToken: ct);
                }
                else
                {
                    await _botClient.SendMessage(
                        chatId: chatId,
                        text: "⚠️ Ошибка активации профиля",
                        cancellationToken: ct);
                }
            }
            else
            {
                _userStates.Remove(chatId);
                await _botClient.SendMessage(
                    chatId: chatId,
                    text: "❌ Пользователь не найден\nПопробуйте снова: /login",
                    cancellationToken: ct);
            }
        }
        else
        {
            await _botClient.SendMessage(
                chatId: chatId,
                text: "⚠️ Неизвестная команда\nИспользуйте /help для списка команд",
                cancellationToken: ct);
        }
    }

    private async Task HandleLocationAsync(long chatId, Telegram.Bot.Types.Location location, CancellationToken ct)
    {
        if (!_userStates.TryGetValue(chatId, out var state) || state != UserState.Authorized)
        {
            await _botClient.SendMessage(
                chatId: chatId,
                text: "🔒 Требуется авторизация!\nИспользуйте /login",
                cancellationToken: ct);
            return;
        }

        if (_userLogins.TryGetValue(chatId, out var telegramUsername))
        {
            await dbService.SetCoordinates(
                telegramUsername,
                location.Latitude.ToString(CultureInfo.InvariantCulture),
                location.Longitude.ToString(CultureInfo.InvariantCulture));

            await _botClient.SendMessage(
                chatId: chatId,
                text: $"📍 Координаты обновлены:\n" +
                      $"Широта: {location.Latitude}\n" +
                      $"Долгота: {location.Longitude}",
                cancellationToken: ct);
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
        Authorized
    }
}
