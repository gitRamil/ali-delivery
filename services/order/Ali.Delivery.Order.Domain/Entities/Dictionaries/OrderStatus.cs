using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.OrderStatus;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

/// <summary>
/// Представляет статусы заказа.
/// </summary>
public class OrderStatus : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает статус заказа: Создан.
    /// </summary>
    public static readonly OrderStatus Created = new(new Guid("3a15d9e1-c989-2e49-e8d3-55a56db7a2e1"), new OrderStatusCode("created"), new OrderStatusName("Создана"));

    /// <summary>
    /// Возвращает статус заказа: Завершен.
    /// </summary>
    public static readonly OrderStatus Finished = new(new Guid("3a15d9e1-c99e-6357-1416-7c7be54dd2a5"), new OrderStatusCode("finished"), new OrderStatusName("Завершена"));

    private static readonly Dictionary<OrderStatusCode, OrderStatus> OrderStatuses = new()
    {
        [Created.Code] = Created,
        [Finished.Code] = Finished
    };

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderStatus" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="code">Код.</param>
    /// <param name="name">Наименование.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="code" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>Конструктор для EF.</remarks>
    protected OrderStatus(SequentialGuid id, OrderStatusCode code, OrderStatusName name)
        : base(id)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Возвращает код.
    /// </summary>
    public OrderStatusCode Code { get; }

    /// <summary>
    /// Возвращает наименование.
    /// </summary>
    public OrderStatusName Name { get; }

    /// <summary>
    /// Возвращает все значения перечисления.
    /// </summary>
    public static IReadOnlyCollection<OrderStatus> GetAllValues() => OrderStatuses.Values;
}
