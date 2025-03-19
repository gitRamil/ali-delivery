using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.GetOrder;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetOrder;

public class GetOrderQueryHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new GetOrderQueryHandler(context);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(context));
    }

    [Fact]
    public async Task ConstructorShouldThrowsArgumentNullExceptionWhenQueryIsNull()
    {
        // Arrange.
        var mocks = new AutoMocker(MockBehavior.Strict);

        mocks.GetMock<IAppDbContext>();
        mocks.GetMock<ICurrentUser>();

        var sut = mocks.CreateInstance<GetOrderQueryHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'query')");
    }

    [Fact]
    public async Task HandlerShouldReturnOrder()
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

        mocks.MockDbSet(o => o.Orders, order);

        var sut = mocks.CreateInstance<GetOrderQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetOrderQuery(order.Id), CancellationToken.None);

        // Assert.
        mocks.Verify();

        result.Id.Should()
              .Be(order.Id);
    }

    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenOrderNotinBase()
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

        mocks.MockDbSet(o => o.Orders);

        var sut = mocks.CreateInstance<GetOrderQueryHandler>();

        var command = new GetOrderQuery(order.Id);

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));
    }
}
