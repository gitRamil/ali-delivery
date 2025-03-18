using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.UpdateOrder;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace Ali.Delivery.Order.Application.Tests.UseCases.UpdateOrder;

[Trait("Category", "Unit")]
public class UpdateOrderCommandHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new UpdateOrderCommandHandler(context);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(context));
    }

    [Fact]
    public async Task ConstructorShouldThrowsArgumentNullExceptionWhenCommandIsNull()
    {
        // Arrange.
        var mocks = new AutoMocker(MockBehavior.Strict);

        mocks.GetMock<IAppDbContext>();

        var sut = mocks.CreateInstance<UpdateOrderCommandHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'command')");

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenOrderNotinBase()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var newOrderName = fixture.Create<OrderName>();
        var newWeight = fixture.Create<OrderInfoWeight>();
        var newPrice = fixture.Create<OrderInfoPrice>();
        var newAddressFrom = fixture.Create<OrderInfoAddressFrom>();
        var newAddressTo = fixture.Create<OrderInfoAddressTo>();
        var newSize = SizeCode.Medium;
        var newOrderStatus = OrderStatusCode.Created;

        var order = new Domain.Entities.Order(fixture.Create<SequentialGuid>(),
                                              fixture.Create<OrderName>(),
                                              fixture.Create<OrderInfo>(),
                                              OrderStatus.InProgress,
                                              fixture.Create<User>(),
                                              fixture.Create<User>(),
                                              null,
                                              null);
        mocks.MockDbSet(o => o.Orders);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<UpdateOrderCommandHandler>();

        var command = new UpdateOrderCommand(order.Id, newOrderName, newWeight, newSize, newPrice, newAddressFrom, newAddressTo, newOrderStatus);

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandlerShouldUpdateOrder()
    {
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var newOrderName = fixture.Create<OrderName>();
        var newWeight = fixture.Create<OrderInfoWeight>();
        var newPrice = fixture.Create<OrderInfoPrice>();
        var newAddressFrom = fixture.Create<OrderInfoAddressFrom>();
        var newAddressTo = fixture.Create<OrderInfoAddressTo>();
        var newSize = SizeCode.Medium;
        var newOrderStatus = OrderStatusCode.Created;

        var order = new Domain.Entities.Order(fixture.Create<SequentialGuid>(),
                                              fixture.Create<OrderName>(),
                                              fixture.Create<OrderInfo>(),
                                              OrderStatus.InProgress,
                                              fixture.Create<User>(),
                                              fixture.Create<User>(),
                                              null,
                                              null);
        mocks.MockDbSet(o => o.Orders, order);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var command = new UpdateOrderCommand(order.Id, newOrderName, newWeight, newSize, newPrice, newAddressFrom, newAddressTo, newOrderStatus);

        var sut = mocks.CreateInstance<UpdateOrderCommandHandler>();

        // Act.
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert.
        mocks.GetMock<IAppDbContext>()
             .Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        result.Name.Should()
              .Be(newOrderName);

        mocks.GetMock<IAppDbContext>()
             .Verify(db => db.Orders, Times.Once);
    }
}
