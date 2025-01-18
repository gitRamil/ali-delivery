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
    public static readonly OrderStatus Created = new(new Guid("3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a"), new OrderStatusCode("created"), new OrderStatusName("Создана"));

    /// <summary>
    /// Возвращает статус заказа: Завершен.
    /// </summary>
    public static readonly OrderStatus Finished = new(new Guid("3a174d9d-c0df-65fa-4178-e9b514ce133d"), new OrderStatusCode("finished"), new OrderStatusName("Завершена"));

    /// <summary>
    /// Возвращает статус заказа: Создан.
    /// </summary>
    public static readonly OrderStatus InProgress = new(new Guid("3a174d9d-c0e1-358f-01db-927e1290e9f1"), new OrderStatusCode("inProgress"), new OrderStatusName("В процессе"));

    private static readonly Dictionary<OrderStatusCode, OrderStatus> OrderStatuses = new()
    {
        [Created.Code] = Created,
        [InProgress.Code] = InProgress,
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
