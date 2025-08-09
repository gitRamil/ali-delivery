namespace Ali.Delivery.Location.Application.Extensions;

/// <summary>
/// Расширение для обработки сообщений.
/// </summary>
public static class MessageInfoExtensions
{
    /// <summary>
    /// Метод для приведения сообщения.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <returns></returns>
    public static string MessageToCommand(this string message)
    {
        return message.Trim()
            .ToLowerInvariant();
    }
}