using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;

namespace Ali.Delivery.Order.Domain.Entities;

public class Order : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="T:Ali.Delivery.Domain.Core.Entity`1" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// Возникает, если <paramref name="id" /> равен <c>null</c>.
    /// </exception>
    public Order(SequentialGuid id)
        : base(id)
    {
    }

    protected Order()
        : base(SequentialGuid.Empty)
    {
    }

    public bool Test { get; }
}
