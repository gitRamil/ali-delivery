using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Domain.Entities;

namespace Ali.Delivery.Location.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class UserLocationTests
{
    [Fact]
    public void AddUserLocationShouldSucceedWhenAllArgumentsArePasses()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var user = fixture.Create<User>();
        var longitude = fixture.Create<double>();
        var latitude = fixture.Create<double>();

        //Act.
        var act = () => new UserLocation(id, user, longitude, latitude);

        //Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void AddUserLocationShouldThrowArgumentNullExceptionWhenUserIsNull()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        User user = null!;
        var longitude = fixture.Create<double>();
        var latitude = fixture.Create<double>();

        //Act.
        var act = () => new UserLocation(id, user, longitude, latitude);

        //Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(user));
    }

    [Fact]
    public void ProtectedConstructorShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange
        var type = typeof(UserLocation);
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var userLocation = (UserLocation)constructor.Invoke(null);

        // Assert
        userLocation.Should()
                    .NotBeNull();

        userLocation.Id.Should()
                    .Be(SequentialGuid.Empty);

        userLocation.User.Should()
                    .BeNull();

        userLocation.Longitude.Should()
                    .Be(0);

        userLocation.Latitude.Should()
                    .Be(0);
    }
}
