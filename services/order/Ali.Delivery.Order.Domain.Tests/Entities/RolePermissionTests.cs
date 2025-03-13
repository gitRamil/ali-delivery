using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class RolePermissionTests
{
    [Fact]

    public void CreateRolePermissionShouldSucceedWhenAllValidArgumentsPassed()
    {
        //Arrange.
        var fixture = new Fixture();
        
        var id = fixture.Create<SequentialGuid>();
        var roleId = Role.BasicUser.Id;
        var permission = Permission.UserOrderManagement.Id;
        
        //Act.
        
        var act = () => new RolePermission(id, roleId, permission);
        
        //Assert.
        
        act.Should().NotThrow();
    }
}
