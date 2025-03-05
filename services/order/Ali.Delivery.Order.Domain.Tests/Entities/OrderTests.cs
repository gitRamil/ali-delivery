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
    public void CreateOrderShouldThrowArgumentNullExceptionWhenNameIsNull()
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
    public void CreateOrderShouldThrowArgumentNullExceptionWhenPassportInfoIsNull()
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
    public void CreateOrderShouldSucceedWhenValidArgumentsPassed()
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

    [Fact]
    public void CreateOrderShouldThrowArgumentNullExceptionWhenReceiverIsNull()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.Created;
        var sender = fixture.Create<User>();
        User receiver = null!;
        NotAuthUser? notAuthReceiver = null;
        User? courier = null;

        // Act.
        var act = () => new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Assert.
        act.Should()
           .Throw<InvalidOperationException>("Должен быть указан либо зарегистрированный, либо незарегистрированный получатель.");
    }

    [Fact]
    public void CreateOrderShouldThrowArgumentNullExceptionWhenOrderInfoIsNull()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        OrderInfo orderInfo = null!;
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
           .WithParameterName("orderInfo");
    }

    [Fact]
    public void CreateOrderShouldThrowArgumentNullExceptionWhenOrderStatusIsNull()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        OrderStatus orderStatus = null!;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        User? courier = null;

        // Act.
        var act = () => new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName("orderStatus");
    }

    [Fact]
    public void FinishDeliveryShouldSucceedWhenCurrentUserIsCourier()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.InProgress;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        var courier = fixture.Create<User>();

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Act.
        var act = () => order.FinishDelivery(courier);

        // Assert.
        act.Should()
           .NotThrow();

        order.OrderStatus.Should()
             .Be(OrderStatus.Finished);
    }

    [Fact]
    public void FinishDeliveryShouldThrowUnauthorizedAccessExceptionWhenCurrentUserIsNotCourier()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.InProgress;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        var courier = fixture.Create<User>();
        var anotherUser = fixture.Create<User>();

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Act.
        var act = () => order.FinishDelivery(anotherUser);

        // Assert.
        act.Should()
           .Throw<UnauthorizedAccessException>("Текущий пользователь не является назначенным курьером для этого заказа.");
    }

    [Fact]
    public void SetCourierShouldSucceedWhenCourierHasPassportInfoAndOrderIsInValidStatus()
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
        var courier = fixture.Create<User>();

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver);

        // Act.
        var act = () => order.SetCourier(courier);

        // Assert.
        act.Should()
           .NotThrow();

        order.OrderStatus.Should()
             .Be(OrderStatus.InProgress);

        order.Courier.Should()
             .Be(courier);
    }

    [Fact]
    public void SetCourierShouldThrowInvalidOperationExceptionWhenCourierHasNoPassportInfo()
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

        var courier = new User(fixture.Create<SequentialGuid>(),
                               fixture.Create<UserLogin>(),
                               fixture.Create<UserPassword>(),
                               fixture.Create<Role>(),
                               fixture.Create<UserBirthDay>(),
                               fixture.Create<UserFirstName>(),
                               fixture.Create<UserLastName>());

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver);

        // Act.
        var act = () => order.SetCourier(courier);

        // Assert.
        act.Should()
           .Throw<InvalidOperationException>("Пожалуйста заполните паспортные данные для продолжения работы");
    }

    [Fact]
    public void SetCourierShouldThrowInvalidOperationExceptionWhenOrderIsInNotAllowedStatus()
    {
        // Arrange.
        var fixture = new AppFixture();
        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.InProgress;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        var courier = fixture.Create<User>();

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver);

        // Act.
        var act = () => order.SetCourier(courier);

        // Assert.
        act.Should()
           .Throw<InvalidOperationException>()
           .WithMessage($"Нельзя назначить курьера, если заказ находится в статусах: В процессе, Завершена");
    }

    [Fact]
    public void UnassignCourierShouldThrowUnauthorizedAccessExceptionWhenCurrentUserIsNotCourier()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.InProgress;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        var courier = fixture.Create<User>();
        var anotherUser = fixture.Create<User>();

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Act.
        var act = () => order.UnassignCourier(anotherUser);

        // Assert.
        act.Should()
           .Throw<UnauthorizedAccessException>("Текущий пользователь не является назначенным курьером для этого заказа.");
    }

    [Fact]
    public void UnassignCourierShouldSucceedWhenCurrentUserIsCourier()
    {
        // Arrange.
        var fixture = new AppFixture();

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.InProgress;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        var courier = fixture.Create<User>();

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Act.
        var act = () => order.UnassignCourier(courier);

        // Assert.
        act.Should()
           .NotThrow();

        order.OrderStatus.Should()
             .Be(OrderStatus.Created);
    }

    [Fact]
    public void UpdateOrderStatusShouldSucceedWhenValidArgumentsPassed()
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
        var orderStatusNew = OrderStatus.InProgress;

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver);

        // Act.
        var act = () => order.UpdateOrderStatus(orderStatusNew);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void UpdateOrderStatusShouldThrowArgumentNullExceptionWhenNewOrderStatusIsNull()
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
        OrderStatus orderStatusNew = null!;

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver);

        // Act.
        var act = () => order.UpdateOrderStatus(orderStatusNew);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(orderStatusNew));
    }

    [Fact]
    public void UpdateOrderNameShouldThrowArgumentNullExceptionWhenNewOrderNameIsNull()
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
        OrderName orderNameNew = null!;

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver);

        // Act.
        var act = () => order.UpdateOrderName(orderNameNew);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(orderNameNew));
    }

    [Fact]
    public void UpdateOrderNameShouldSucceedWhenValidArgumentsPassed()
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
        var orderNameNew = fixture.Create<OrderName>();

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver);

        // Act.
        var act = () => order.UpdateOrderName(orderNameNew);

        // Assert.
        act.Should()
           .NotThrow();
    }
}
