namespace Ali.Delivery.Order.Application.Abstractions;

/// <summary>
/// Интерфейс для сервиса, потребляющего сообщения из очередей RabbitMQ.
/// </summary>
public interface IMessageConsumer : IDisposable
{
    /// <summary>
    /// Показывает, запущен ли в данный момент консумер.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Запускает процесс потребления сообщений.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Останавливает процесс потребления сообщений.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task StopAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Интерфейс обработчика определённого типа сообщения.
/// </summary>
/// <typeparam name="TMessage">Тип обрабатываемого сообщения.</typeparam>
public interface IMessageHandler<in TMessage>
{
    /// <summary>
    /// Асинхронно обрабатывает полученное сообщение.
    /// </summary>
    /// <param name="message">Сообщение для обработки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task HandleAsync(TMessage message, CancellationToken cancellationToken = default);
}
