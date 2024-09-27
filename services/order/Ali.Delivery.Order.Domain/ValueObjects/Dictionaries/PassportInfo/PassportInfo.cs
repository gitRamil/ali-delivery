using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;

namespace Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.PassportInfo;

public class PassportInfo: Entity<SequentialGuid>
{
    public PassportInfo(SequentialGuid id) : base(id)
    {
    }
}