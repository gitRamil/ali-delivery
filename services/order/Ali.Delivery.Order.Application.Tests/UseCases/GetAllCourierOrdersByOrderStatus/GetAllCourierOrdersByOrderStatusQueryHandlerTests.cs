using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.UseCases.GetAllCourierOrdersByOrderStatus;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetAllCourierOrdersByOrderStatus;

[Trait("Category", "Unit")]
public class GetAllCourierOrdersByOrderStatusQueryHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;
        var currentUser = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new GetAllCourierOrdersByOrderStatusQueryHandler(context, currentUser);

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
        var act = () => new GetAllCourierOrdersByOrderStatusQueryHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(currentUser));
    }

    [Fact]
    public async Task HandlerShouldGetAllCourierOrdersByOrderStatus()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var sender = fixture.Create<User>();
        var courier = fixture.Create<User>();

        var orders = new[]
        {
            fixture.CreateOrder(sender, OrderStatus.Created),
            fixture.CreateOrder(sender, OrderStatus.InProgress, courier),
            fixture.CreateOrder(sender, OrderStatus.InProgress, courier),
            fixture.CreateOrder(sender, OrderStatus.Finished, courier)
        };

        mocks.CurrentUserSet(courier.Id);
        mocks.MockDbSet(o => o.Orders, orders);
        var sut = mocks.CreateInstance<GetAllCourierOrdersByOrderStatusQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetAllCourierOrdersByOrderStatusQuery(OrderStatusCode.InProgress), CancellationToken.None);

        // Assert.
        mocks.Verify();

        result.Should()
              .NotBeNull()
              .And.HaveCount(2);

        foreach (var order in result)
        {
            order.OrderStatusName.Should()
                 .Be(OrderStatus.InProgress.Name);
        }
    }
}
