namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Определяет контракт для службы публикации сообщений в очереди сообщений.
/// </summary>
public interface IPublisherService
{
    /// <summary>
    /// Публикует сообщение.
    /// </summary>
    /// <typeparam name="TMessage">Тип публикуемого сообщения.</typeparam>
    /// <param name="message">Сообщение для публикации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Публикует сообщение с указанным routing key. Exchange определяется из атрибута сообщения или конфигурации по умолчанию.
    /// </summary>
    /// <typeparam name="TMessage">Тип публикуемого сообщения.</typeparam>
    /// <param name="message">Сообщение для публикации.</param>
    /// <param name="routingKey">Ключ маршрутизации для публикации сообщения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task PublishAsync<TMessage>(TMessage message, string routingKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Публикует сообщение в указанный exchange с указанным routing key.
    /// </summary>
    /// <typeparam name="TMessage">Тип публикуемого сообщения.</typeparam>
    /// <param name="message">Сообщение для публикации.</param>
    /// <param name="exchange">Имя exchange для публикации сообщения.</param>
    /// <param name="routingKey">Ключ маршрутизации для публикации сообщения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task PublishAsync<TMessage>(TMessage message, string exchange, string routingKey,
        CancellationToken cancellationToken = default);
}