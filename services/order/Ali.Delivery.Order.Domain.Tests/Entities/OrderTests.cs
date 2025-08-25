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
           .Throw<ArgumentNullException>(nameof(orderName));
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
           .Throw<ArgumentNullException>(nameof(orderInfo));
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
           .Throw<ArgumentNullException>(nameof(orderStatus));
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
    public void CreateOrderShouldThrowArgumentNullExceptionWhenSenderIsNull()
    {
        // Arrange.
        var fixture = new AppFixture();
        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.Created;
        User sender = null!;
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        User? courier = null;

        // Act.
        var act = () => new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, courier);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(sender));
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

        order.Name.ToString()
             .Should()
             .Be(orderName);
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
    public void ProtectedConstructorShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange
        var type = typeof(Domain.Entities.Order);
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var order = (Domain.Entities.Order)constructor.Invoke(null);

        // Assert
        order.Should()
             .NotBeNull();

        order.Id.Should()
             .Be(SequentialGuid.Empty);

        order.Name.Should()
             .BeNull();

        order.OrderStatus.Should()
             .BeNull();

        order.OrderInfo.Should()
             .BeNull();

        order.Sender.Should()
             .BeNull();

        order.Receiver.Should()
             .BeNull();

        order.Courier.Should()
             .BeNull();

        order.NotAuthReceiver.Should()
             .BeNull();
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
        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, null);

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

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, null);

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
        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, null);

        // Act.
        var act = () => order.SetCourier(courier);

        // Assert.
        act.Should()
           .Throw<InvalidOperationException>()
           .WithMessage("Нельзя назначить курьера, если заказ находится в статусах: В процессе, Завершена");
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

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, null);

        // Act.
        var act = () => order.UpdateOrderName(orderNameNew);

        // Assert.
        act.Should()
           .NotThrow();

        order.Name.Should()
             .Be(orderNameNew);
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

        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, null);

        // Act.
        var act = () => order.UpdateOrderStatus(orderStatusNew);

        // Assert.
        act.Should()
           .NotThrow();

        order.OrderStatus.Should()
             .Be(orderStatusNew);
    }
}
