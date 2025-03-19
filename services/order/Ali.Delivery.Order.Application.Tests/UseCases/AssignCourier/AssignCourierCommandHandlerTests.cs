using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.AssignCourier;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;
using OrderStatus = Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus;

namespace Ali.Delivery.Order.Application.Tests.UseCases.AssignCourier;

[Trait("Category", "Unit")]
public class AssignCourierCommandHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;
        var currentUser = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new AssignCourierCommandHandler(context, currentUser);

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
        var act = () => new AssignCourierCommandHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(currentUser));
    }

    [Fact]
    public async Task HandlerShouldSetCourier()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);

        var order = new Domain.Entities.Order(fixture.Create<SequentialGuid>(),
                                              fixture.Create<OrderName>(),
                                              fixture.Create<OrderInfo>(),
                                              OrderStatus.Created,
                                              fixture.Create<User>(),
                                              fixture.Create<User>(),
                                              null,
                                              null);
        var courier = fixture.Create<User>();

        mocks.MockDbSet(o => o.Orders, order);
        mocks.MockDbSet(u => u.Users, courier);
        mocks.CurrentUserSet(courier.Id);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<AssignCourierCommandHandler>();

        // Act.
        var result = await sut.Handle(new AssignCourierCommand(order.Id), CancellationToken.None);

        // Assert.
        mocks.Verify();

        result.Should()
              .Be(order.Id);

        order.Courier.Should()
             .Be(courier);
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

        mocks.MockDbSet(o => o.Orders, order);
        mocks.MockDbSet(u => u.Users);
        mocks.CurrentUserSet(courier.Id);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<AssignCourierCommandHandler>();

        var command = new AssignCourierCommand(order.Id);

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

        var sut = mocks.CreateInstance<AssignCourierCommandHandler>();

        var command = new AssignCourierCommand(order.Id);

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
