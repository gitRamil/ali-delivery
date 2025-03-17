using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.UseCases.GetAllCreatedOrders;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetAllCreatedOrders;

[Trait("Category", "Unit")]
public class GetAllCreatedOrdersQueryHandlerTests
{
    [Fact]
    public async Task HandlerShouldReturnAllCreatedOrders()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var sender = fixture.Create<User>();
        var courier = fixture.Create<User>();

        var orders = new[]
        {
            fixture.CreateOrder(sender, OrderStatus.Created),
            fixture.CreateOrder(sender, OrderStatus.Created),
            fixture.CreateOrder(sender, OrderStatus.Created),
            fixture.CreateOrder(sender, OrderStatus.InProgress, courier),
            fixture.CreateOrder(sender, OrderStatus.InProgress, courier),
            fixture.CreateOrder(sender, OrderStatus.Finished, courier)
        };

        mocks.MockDbSet(o => o.Orders, orders);
        var sut = mocks.CreateInstance<GetAllCreatedOrdersQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetAllCreatedOrdersQuery(), CancellationToken.None);

        // Assert.
        mocks.Verify();

        result.Should()
              .NotBeNull()
              .And.HaveCount(3);

        foreach (var order in result)
        {
            order.OrderStatusName.Should()
                 .Be(OrderStatus.Created.Name);
        }
    }

    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new GetAllCreatedOrdersQueryHandler(context);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(context));
    }
}
