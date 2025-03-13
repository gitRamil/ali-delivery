using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.UseCases.DeleteOrder;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.DeleteOrder;


[Trait("Category", "Unit")]
public class DeleteOrderCommandHandlerTests
{
    [Fact]

    public async Task OrderShouldBeDeleted()
    {
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        
        var id = fixture.Create<SequentialGuid>();
        var orderName = fixture.Create<OrderName>();
        var orderInfo = fixture.Create<OrderInfo>();
        var orderStatus = OrderStatus.Created;
        var sender = fixture.Create<User>();
        var receiver = fixture.Create<User>();
        NotAuthUser? notAuthReceiver = null;
        var order = new Domain.Entities.Order(id, orderName, orderInfo, orderStatus, sender, receiver, notAuthReceiver,null);

        
        mocks.MockDbSet(o=>o.Orders, order);
        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var command = new DeleteOrderCommand(order.Id);
        
        var sut = mocks.CreateInstance<DeleteOrderCommandHandler>();
        
        // Act.
        var result = await sut.Handle(command, default);
        
        // Assert.
        mocks.Verify();
        
        mocks.GetMock<IAppDbContext>()
             .Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.NotEqual(Guid.Empty, result);

    }
    
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
}
