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
    public static readonly OrderStatus Created = new(new Guid("aa37a123-940e-45c6-a06a-658a976ac744"), new OrderStatusCode("created"), new OrderStatusName("Создана"));

    /// <summary>
    /// Возвращает статус заказа: Завершена.
    /// </summary>
    public static readonly OrderStatus Finished = new(new Guid("fcdd8817-a5d9-4bd1-adf4-9e6abfc842af"), new OrderStatusCode("finished"), new OrderStatusName("Завершена"));

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
