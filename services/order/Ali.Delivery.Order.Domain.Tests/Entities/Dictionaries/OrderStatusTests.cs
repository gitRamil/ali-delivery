using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Domain.Tests.Entities.Dictionaries;

[Trait("Category", "Unit")]
public class OrderStatusTests
{
    [Fact]
    public void GetAllValuesShouldReturnAllValues()
    {
        //Arrange.

        //Act.
        var values = OrderStatus.GetAllValues().ToList();

        //Assert.
        values[0].Id.Should().Be(OrderStatus.Created.Id);
    }
}
