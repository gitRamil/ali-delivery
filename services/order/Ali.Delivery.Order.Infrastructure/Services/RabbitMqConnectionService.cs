using RabbitMQ.Client;

namespace Ali.Delivery.Order.Infrastructure.Services;

/// <summary>
/// Сервис для управления соединением с RabbitMQ.
/// </summary>
public class RabbitMqConnectionService
{
    /// <summary>
    /// Активное соединение с RabbitMQ.
    /// </summary>
    public IConnection Connection { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="RabbitMqConnectionService"/> с указанным соединением.
    /// </summary>
    /// <param name="connection">Соединение с RabbitMQ.</param>
    public RabbitMqConnectionService(IConnection connection)
    {
        Connection = connection;
    }
}