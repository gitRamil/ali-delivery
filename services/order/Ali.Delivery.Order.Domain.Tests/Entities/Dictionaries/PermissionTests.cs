using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Domain.Tests.Entities.Dictionaries;

[Trait("Category", "Unit")]
public class PermissionTests
{
    [Fact]
    public void GetAllValuesShouldReturnAllValues()
    {
        //Arrange.

        //Act.
        var values = Permission.GetAllValues().ToList();

        //Assert.
        values[0].Id.Should().Be(Permission.UserManagement.Id);
        values[0].Name.Should().Be(Permission.UserManagement.Name);
    }
}
