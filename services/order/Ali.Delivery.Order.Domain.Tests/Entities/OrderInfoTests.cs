using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;

namespace Ali.Delivery.Order.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class OrderInfoTests
{
    [Fact]
    public void CreateOrderInfoShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var weight = fixture.Create<OrderInfoWeight>();
        var size = Size.Medium;
        var price = fixture.Create<OrderInfoPrice>();
        var addressFrom = fixture.Create<OrderInfoAddressFrom>();
        var addressTo = fixture.Create<OrderInfoAddressTo>();

        // Act. 
        var act = () => new OrderInfo(id, weight, size, price, addressFrom, addressTo);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void CreateOrderInfoShouldThrowArgumentNullExceptionWhenAddressFromIsNull()
    {
        // Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var weight = fixture.Create<OrderInfoWeight>();
        var size = Size.Medium;
        var price = fixture.Create<OrderInfoPrice>();
        OrderInfoAddressFrom addressFrom = null!;
        var addressTo = fixture.Create<OrderInfoAddressTo>();

        // Act. 
        var act = () => new OrderInfo(id, weight, size, price, addressFrom, addressTo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(addressFrom));
    }

    [Fact]
    public void CreateOrderInfoShouldThrowArgumentNullExceptionWhenAddressToIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var weight = fixture.Create<OrderInfoWeight>();
        var size = Size.Medium;
        var price = fixture.Create<OrderInfoPrice>();
        var addressFrom = fixture.Create<OrderInfoAddressFrom>();
        OrderInfoAddressTo addressTo = null!;

        // Act. 
        var act = () => new OrderInfo(id, weight, size, price, addressFrom, addressTo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(addressTo));
    }

    [Fact]
    public void CreateOrderInfoShouldThrowArgumentNullExceptionWhenPriceIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var weight = fixture.Create<OrderInfoWeight>();
        var size = Size.Medium;
        OrderInfoPrice price = null!;
        var addressFrom = fixture.Create<OrderInfoAddressFrom>();
        var addressTo = fixture.Create<OrderInfoAddressTo>();

        // Act. 
        var act = () => new OrderInfo(id, weight, size, price, addressFrom, addressTo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(price));
    }

    [Fact]
    public void CreateOrderInfoShouldThrowArgumentNullExceptionWhenSizeIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var weight = fixture.Create<OrderInfoWeight>();
        Size size = null!;
        var price = fixture.Create<OrderInfoPrice>();
        var addressFrom = fixture.Create<OrderInfoAddressFrom>();
        var addressTo = fixture.Create<OrderInfoAddressTo>();

        // Act. 
        var act = () => new OrderInfo(id, weight, size, price, addressFrom, addressTo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(size));
    }

    [Fact]
    public void CreateOrderInfoShouldThrowArgumentNullExceptionWhenWeightIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        OrderInfoWeight weight = null!;
        var size = Size.Medium;
        var price = fixture.Create<OrderInfoPrice>();
        var addressFrom = fixture.Create<OrderInfoAddressFrom>();
        var addressTo = fixture.Create<OrderInfoAddressTo>();

        // Act. 
        var act = () => new OrderInfo(id, weight, size, price, addressFrom, addressTo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(weight));
    }

    [Fact]
    public void ProtectedConstructorShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange
        var type = typeof(OrderInfo);
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var orderInfo = (OrderInfo)constructor.Invoke(null);

        // Assert
        orderInfo.Should()
                 .NotBeNull();

        orderInfo.Id.Should()
                 .Be(SequentialGuid.Empty);

        orderInfo.Weight.Should()
                 .BeNull();

        orderInfo.Size.Should()
                 .BeNull();

        orderInfo.Price.Should()
                 .BeNull();

        orderInfo.AddressFrom.Should()
                 .BeNull();

        orderInfo.AddressTo.Should()
                 .BeNull();
    }

    [Fact]
    public void UpdateOrderInfoShouldSucceedUpdateOrderInfo()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var weight = fixture.Create<OrderInfoWeight>();
        var size = Size.Medium;
        var price = fixture.Create<OrderInfoPrice>();
        var addressFrom = fixture.Create<OrderInfoAddressFrom>();
        var addressTo = fixture.Create<OrderInfoAddressTo>();

        var weight1 = fixture.Create<OrderInfoWeight>();
        var size1 = Size.Medium;
        var price1 = fixture.Create<OrderInfoPrice>();
        var addressFrom1 = fixture.Create<OrderInfoAddressFrom>();
        var addressTo1 = fixture.Create<OrderInfoAddressTo>();

        var orderInfo = new OrderInfo(id, weight, size, price, addressFrom, addressTo);

        // Act. 
        var act = () => orderInfo.UpdateOrderInfo(weight1, price1, addressFrom1, addressTo1, size1);

        // Assert.
        act.Should()
           .NotThrow();

        orderInfo.Weight.Should()
                 .Be(weight1);

        orderInfo.Price.Should()
                 .Be(price1);

        orderInfo.AddressFrom.Should()
                 .Be(addressFrom1);

        orderInfo.AddressTo.Should()
                 .Be(addressTo1);

        orderInfo.Size.Should()
                 .Be(size1);
    }
}
