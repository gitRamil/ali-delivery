using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.FinishDelivery;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace Ali.Delivery.Order.Application.Tests.UseCases.FinishDelivery;

[Trait("Category", "Unit")]
public class FinishDeliveryCommandHandlerTests
{
    [Fact]
    public async Task HandlerShouldFinishDelivery()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var courier = fixture.Create<User>();

        var order = new Domain.Entities.Order(fixture.Create<SequentialGuid>(),
                                              fixture.Create<OrderName>(),
                                              fixture.Create<OrderInfo>(),
                                              OrderStatus.InProgress,
                                              fixture.Create<User>(),
                                              fixture.Create<User>(),
                                              null,
                                              courier);

        mocks.MockDbSet(s => s.Orders, order);
        mocks.MockDbSet(s => s.Users, courier);
        mocks.CurrentUserSet(courier.Id);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<FinishDeliveryCommandHandler>();

        // Act.
        var result = await sut.Handle(new FinishDeliveryCommand(order.Id), default);

        // Assert.
        mocks.Verify();

        result.Should()
              .Be(order.Id);

        order.Courier.Should()
             .Be(courier);

        order.OrderStatus.Should()
             .Be(OrderStatus.Finished);
    }

    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;
        var currentUser = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new FinishDeliveryCommandHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(context));
    }

    [Fact]
    public void ConstructorShouldFailWhenNullArgumentCurrentUserPassed()
    {
        // Arrange.
        var context = Mock.Of<IAppDbContext>();
        ICurrentUser currentUser = null!;

        // Act.
        var act = () => new FinishDeliveryCommandHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(currentUser));
    }

    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenCurrentUserNotinBase()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var courier = fixture.Create<User>();

        var order = new Domain.Entities.Order(fixture.Create<SequentialGuid>(),
                                              fixture.Create<OrderName>(),
                                              fixture.Create<OrderInfo>(),
                                              OrderStatus.InProgress,
                                              fixture.Create<User>(),
                                              fixture.Create<User>(),
                                              null,
                                              courier);

        mocks.MockDbSet(s => s.Orders, order);
        mocks.MockDbSet(s => s.Users);
        mocks.CurrentUserSet(courier.Id);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<FinishDeliveryCommandHandler>();

        var command = new FinishDeliveryCommand(order.Id);

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenOrderNotinBase()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var courier = fixture.Create<User>();

        var order = new Domain.Entities.Order(fixture.Create<SequentialGuid>(),
                                              fixture.Create<OrderName>(),
                                              fixture.Create<OrderInfo>(),
                                              OrderStatus.InProgress,
                                              fixture.Create<User>(),
                                              fixture.Create<User>(),
                                              null,
                                              courier);

        mocks.MockDbSet(o => o.Orders);
        mocks.MockDbSet(u => u.Users, courier);
        mocks.CurrentUserSet(courier.Id);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<FinishDeliveryCommandHandler>();

        var command = new FinishDeliveryCommand(order.Id);

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
