using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Domain.Tests.Entities.Dictionaries;

[Trait("Category", "Unit")]
public class PassportTypeTests
{
    [Fact]
    public void GetAllValuesShouldReturnAllValues()
    {
        //Arrange.

        //Act.
        var values = PassportType.GetAllValues().ToList();

        //Assert.
        values[0].Id.Should().Be(PassportType.Internal.Id);
    }
}
