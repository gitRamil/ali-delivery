using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.DeleteOrder;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace Ali.Delivery.Order.Application.Tests.UseCases.DeleteOrder;

[Trait("Category", "Unit")]
public class DeleteOrderCommandHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new DeleteOrderCommandHandler(context);

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
        mocks.GetMock<ICurrentUser>();

        var sut = mocks.CreateInstance<DeleteOrderCommandHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'command')");
    }

    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenOrderNotinBase()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.Created;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, null);

        mocks.MockDbSet(o => o.Orders);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<DeleteOrderCommandHandler>();

        var command = new DeleteOrderCommand(order.Id);

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task OrderShouldBeDeleted()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);

        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.Created;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver, null);

        mocks.MockDbSet(o => o.Orders, order);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var command = new DeleteOrderCommand(order.Id);

        var sut = mocks.CreateInstance<DeleteOrderCommandHandler>();

        // Act.
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert.
        mocks.Verify();

        mocks.GetMock<IAppDbContext>()
             .Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotEqual(Guid.Empty, result);
    }
}
