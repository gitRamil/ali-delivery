namespace Ali.Delivery.Order.Application.Abstractions;

public interface IMessageConsumer : IDisposable
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
    bool IsRunning { get; }
}

public interface IMessageHandler<in TMessage>
{
    Task HandleAsync(TMessage message, CancellationToken cancellationToken = default);
}