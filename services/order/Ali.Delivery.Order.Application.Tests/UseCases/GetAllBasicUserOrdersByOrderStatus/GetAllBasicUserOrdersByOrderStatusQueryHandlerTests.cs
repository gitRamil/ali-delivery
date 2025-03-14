using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.UseCases.GetAllBasicUserOrdersByOrderStatus;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetAllBasicUserOrdersByOrderStatus;

[Trait("Category", "Unit")]
public class GetAllBasicUserOrdersByOrderStatusQueryHandlerTests
{
    [Fact]
    public async Task HandlerShouldReturnOrdersByOrderStatus()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var sender = fixture.Create<User>();

        var orders = new[]
        {
            fixture.CreateOrder(sender, OrderStatus.Created),
            fixture.CreateOrder(sender, OrderStatus.Created),
            fixture.CreateOrder(sender, OrderStatus.InProgress, fixture.Create<User>()),
            fixture.CreateOrder(sender, OrderStatus.Finished, fixture.Create<User>())
        };

        mocks.CurrentUserSet(sender.Id);
        mocks.MockDbSet(o => o.Orders, orders);
        var sut = mocks.CreateInstance<GetAllBasicUserOrdersByOrderStatusQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetAllBasicUserOrdersByOrderStatusQuery(Dtos.Enums.OrderStatus.Created), default);

        // Assert.
        mocks.Verify();

        result.Should()
              .NotBeNull()
              .And.HaveCount(2);

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
        var currentUser = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new GetAllBasicUserOrdersByOrderStatusQueryHandler(context, currentUser);

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
        var act = () => new GetAllBasicUserOrdersByOrderStatusQueryHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(currentUser));
    }
}
