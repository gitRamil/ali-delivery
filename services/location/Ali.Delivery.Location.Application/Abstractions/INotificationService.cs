using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Представляет контракт для сервиса, отвечающего за отправку уведомлений пользователям.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Асинхронно отправляет типизированное уведомление пользователю в указанный чат.
    /// </summary>
    /// <param name="chatId">Уникальный идентификатор чата, куда будет отправлено сообщение.</param>
    /// <param name="notification">Тип отправляемого уведомления из перечисления <see cref="NotificationType" />.</param>
    /// <param name="userData">
    /// Необязательный словарь с данными для персонализации сообщения (например, для подстановки в
    /// шаблон).
    /// </param>
    /// <param name="cancellationToken">Маркер отмены для прерывания асинхронной операции отправки.</param>
    Task SendNotificationMessageAsync(long chatId, NotificationType notification, Dictionary<string, object>? userData = null, CancellationToken cancellationToken = default);
}
