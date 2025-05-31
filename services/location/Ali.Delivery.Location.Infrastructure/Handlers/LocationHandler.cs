using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;
public class LocationHandler(ILocationService locationService, IMemoryCache cache) : ICommandHandler
{
    public string Command => "location"; // Реализация интерфейсного свойства

    public async Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        if (update.Message?.Location != null && currentState == BotState.GeosharingActive)
        {
            var userLogin = cache.Get<string>($"user_login_{userId}");
            if (string.IsNullOrEmpty(userLogin))
            {
                return new CommandResult(true, "OnRegistrationRequired");
            }

            var success = await locationService.SaveLocationAsync(
                              userId, 
                              userLogin,
                              update.Message.Location.Latitude,
                              update.Message.Location.Longitude);

            return success 
                       ? new CommandResult(true, "OnLocationReceived") 
                       : new CommandResult(true, "OnInvalidLocation");
        }
        return new CommandResult(false, null);
    }
}

