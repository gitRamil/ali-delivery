using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class LocationHandler(ILocationService locationService, IUserDataManager userDataManager) : ICommandHandler
{
    public string Command => "location";

    public async Task<CommandResult> HandleAsync(long userId, Update update, BotState currentState)
    {
        if (update.Message?.Location == null || currentState != BotState.GeosharingActive)
        {
            return new CommandResult(false, null);
        }

        var userLogin = await userDataManager.GetUserLoginAsync(userId);

        if (string.IsNullOrEmpty(userLogin))
        {
            return new CommandResult(true, "OnRegistrationRequired");
        }

        var success = await locationService.SaveLocationAsync(userId, userLogin, update.Message.Location.Latitude, update.Message.Location.Longitude);

        if (!success)
        {
            return new CommandResult(true, "OnInvalidLocation");
        }

        var userData = new Dictionary<string, object>
        {
            ["Latitude"] = update.Message.Location.Latitude,
            ["Longitude"] = update.Message.Location.Longitude,
            ["Login"] = userLogin
        };

        return new CommandResult(true, "OnLocationReceived", userData);
    }
}
