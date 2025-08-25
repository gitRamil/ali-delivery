using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Domain.Tests.Entities.Dictionaries;

[Trait("Category", "Unit")]
public class SizeTests
{
    [Fact]
    public void GetAllValuesShouldReturnAllValues()
    {
        //Arrange. 

        //Act.
        var values = Size.GetAllValues()
                         .ToList();

        //Assert.
        values[0]
            .Id.Should()
            .Be(Size.Small.Id);

        values[0]
            .Name.Should()
            .Be(Size.Small.Name);
    }
}
