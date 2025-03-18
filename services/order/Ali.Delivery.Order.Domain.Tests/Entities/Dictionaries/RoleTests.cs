using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Domain.Tests.Entities.Dictionaries;

[Trait("Category", "Unit")]
public class RoleTests
{
    [Fact]
    public void GetAllValuesShouldReturnAllValues()
    {
        //Arrange.

        //Act.
        var values = Role.GetAllValues().ToList();

        //Assert.
        values[0].Id.Should().Be(Role.Courier.Id);
    }
}
