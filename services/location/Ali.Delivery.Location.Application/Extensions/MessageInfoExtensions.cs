namespace Ali.Delivery.Location.Application.Extensions;

public static class MessageInfoExtensions
{
    public static string MessageToCommand(this string message) =>
        message.Trim()
               .ToLowerInvariant();
}
