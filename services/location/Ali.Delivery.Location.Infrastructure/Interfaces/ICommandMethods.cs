using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface ICommandMethods
{
    Task<CommandResult> GeoSharingAsync(long chatId, string text);

    Task<CommandResult> LoginAsync(long chatId, string text);
}
