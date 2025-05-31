using Ali.Delivery.Location.Infrastructure.Interfaces; // Убедитесь, что неймспейс правильный
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers; // Или ваш неймспейс для обработчиков

public class StartCommandHandler : ICommandHandler
{
    public string Command => "/start"; // Команда, на которую он реагирует

    // Этот обработчик не асинхронный, так как не делает внешних вызовов
    public Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        // Реагируем на команду /start только если текущее состояние Initial
        // или любое другое, с которого вы хотите разрешить "перезапуск" через /start
        if (update.Message?.Text == this.Command && 
            (currentState == BotState.Initial || currentState == BotState.Terminated)) 
        {
            // Возвращаем ключ перехода, который соответствует конфигурации в appsettings.json
            return Task.FromResult(new CommandResult(true, "OnStart")); 
        }

        return Task.FromResult(new CommandResult(false, null)); // Команда не обработана этим хендлером
    }
}

