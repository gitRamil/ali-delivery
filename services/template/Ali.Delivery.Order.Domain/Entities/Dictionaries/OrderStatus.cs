using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.OrderStatus;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

/// <summary>
/// Статус заказа.
/// </summary>
public class OrderStatus : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает статус заказа: Создана.
    /// </summary>
    public static readonly OrderStatus Created = new(new Guid("5ded1989-e506-448f-987d-7ad50574148b"), new OrderStatusCode("created"), new OrderStatusName("Создана"));

    /// <summary>
    /// Возвращает статус заказа: Завершена.
    /// </summary>
    public static readonly OrderStatus Finished = new(new Guid("50ff84d1-016a-4a2b-ba53-51d6dd70459d"), new OrderStatusCode("finished"), new OrderStatusName("Завершена"));

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
