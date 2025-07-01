namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Представляет унифицированную и упрощенную информацию о входящем сообщении
/// для дальнейшей обработки в системе.
/// </summary>
public record MessageInfo
{
    /// <summary>
    /// Получает уникальный идентификатор чата, в котором было отправлено сообщение.
    /// </summary>
    public long ChatId { get; init; }

    /// <summary>
    /// Получает уникальный идентификатор пользователя, отправившего сообщение.
    /// </summary>
    public long FromId { get; init; }

    /// <summary>
    /// Получает географическую локацию, прикрепленную к сообщению.
    /// Возвращает <c>null</c>, если локация отсутствует.
    /// </summary>
    public MessageLocation? Location { get; init; }

    /// <summary>
    /// Получает текстовое содержимое сообщения.
    /// Возвращает <c>null</c>, если сообщение не содержит текста (например, это геолокация или стикер).
    /// </summary>
    public string? Text { get; init; }
}
