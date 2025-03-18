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
        act.Should()
           .NotThrow();
    }
    
    [Fact]
    public void GetAllValuesShouldReturnAllValues()
    {
        //Arrange.

        //Act.
        var values = RolePermission.GetAllValues().ToList();

        //Assert.
        values[0].RoleId.Should().Be(Role.NotAuthUser.Id);
        values[1].RoleId.Should().Be(Role.BasicUser.Id);
    }

    [Fact]
    public void ProtectedConstructorShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange
        var type = typeof(RolePermission);
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var rolePermission = (RolePermission)constructor.Invoke(null);

        // Assert
        rolePermission.Should()
                      .NotBeNull();

        rolePermission.Id.Should()
                      .Be(SequentialGuid.Empty);

        rolePermission.RoleId.Should()
                      .Be(SequentialGuid.Empty);

        rolePermission.PermissionId.Should()
                      .Be(SequentialGuid.Empty);

        rolePermission.Permission.Should()
                      .BeNull();

        rolePermission.Role.Should()
                      .BeNull();
    }
}
