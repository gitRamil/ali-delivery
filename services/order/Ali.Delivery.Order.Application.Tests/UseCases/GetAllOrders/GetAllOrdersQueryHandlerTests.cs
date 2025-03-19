using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.UseCases.GetAllOrders;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetAllOrders;

[Trait("Category", "Unit")]
public class GetAllOrdersQueryHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new GetAllOrdersQueryHandler(context);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(context));
    }

    [Fact]
    public async Task HandlerShouldReturnAllOrders()
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
        var sut = mocks.CreateInstance<GetAllOrdersQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetAllOrdersQuery(), CancellationToken.None);

        // Assert.
        mocks.Verify();

        result.Should()
              .NotBeNull()
              .And.HaveCount(6);
    }
}
