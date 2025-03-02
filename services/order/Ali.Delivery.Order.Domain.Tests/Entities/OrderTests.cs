using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using Inno.Air.PerformanceManagement.Tests.Shared;

namespace Ali.Delivery.Order.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class OrderTests
{
    [Fact]
    public void ConstructorShouldFailWhenNameIsNull()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        OrderName orderName = null!;
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.Created;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        User? courier = null;

        // Act.
        var act = () => new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName("orderName");
    }

    [Fact]
    public void ConstructorShouldFailWhenPassportInfoIsNull()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.Created;

        var sender = new User(fixture.Create<SequentialGuid>(),
                              fixture.Create<UserLogin>(),
                              fixture.Create<UserPassword>(),
                              fixture.Create<Role>(),
                              fixture.Create<UserBirthDay>(),
                              fixture.Create<UserFirstName>(),
                              fixture.Create<UserLastName>());
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        User? courier = null;

        // Act.
        var act = () => new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Assert.
        act.Should()
           .Throw<InvalidOperationException>("Пожалуйста заполните паспортные данные для создания заказа");
    }

    [Fact]
    public void ConstructorShouldPassWhenValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.Created;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        User? courier = null;

        // Act.
        var act = () => new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Assert.
        act.Should()
           .NotThrow();
    }
}
