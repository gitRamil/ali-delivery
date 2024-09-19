using Ali.Delivery.Domain.Core.Primitives;

namespace Ali.Delivery.Order.Domain.Entities;

public class Order
{
    public Order(SequentialGuid id)
    {
        Id = id;
    }

    protected Order()
    {
    }
    
    public SequentialGuid Id { get; private set; } 
    
    public bool Test { get; }
}
