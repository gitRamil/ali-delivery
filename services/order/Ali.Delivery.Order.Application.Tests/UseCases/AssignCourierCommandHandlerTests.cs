using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.UseCases.AssignCourier;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using OrderStatus = Ali.Delivery.Order.Domain.Entities.Dictionaries.OrderStatus;

namespace Ali.Delivery.Order.Application.Tests.UseCases;

[Trait("Category", "Unit")]
public class AssignCourierCommandHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext goalMapsApprovedService = null!;
        var context = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new AssignCourierCommandHandler(goalMapsApprovedService, context);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(goalMapsApprovedService));
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
        var user = fixture.Create<User>();

        mocks.MockDbSet(s => s.Orders, order);
        mocks.MockDbSet(s => s.Users, user);
        mocks.CurrentUserSet(user.Id);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<AssignCourierCommandHandler>();

        // Act.
        var result = await sut.Handle(new AssignCourierCommand(order.Id), default);

        // Assert.
        mocks.Verify();

        result.Should()
              .Be(order.Id);

        order.Courier.Should()
             .Be(user);
    }
}
