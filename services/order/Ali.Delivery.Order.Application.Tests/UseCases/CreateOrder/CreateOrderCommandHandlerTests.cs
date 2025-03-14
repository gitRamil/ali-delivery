using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Application.UseCases.CreateOrder;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.CreateOrder;

[Trait("Category", "Unit")]
public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task HandlerShouldCreateOrder()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);

        var sender = fixture.Create<User>();
        var size = SizeCode.Medium;
        var receiver = fixture.Create<User>();

        var orderInfo = new OrderInfo(SequentialGuid.Create(),
                                      fixture.Create<OrderInfoWeight>(),
                                      size.ToSize(),
                                      fixture.Create<OrderInfoPrice>(),
                                      fixture.Create<OrderInfoAddressFrom>(),
                                      fixture.Create<OrderInfoAddressTo>());

        var command = new CreateOrderCommand(fixture.Create<OrderName>(), orderInfo.Weight, size, orderInfo.Price, orderInfo.AddressFrom, orderInfo.AddressTo, receiver.Id);

        mocks.MockDbSet(s => s.Orders);
        mocks.MockDbSet(s => s.Users, sender, receiver);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();
        mocks.CurrentUserSet(sender.Id);

        var sut = mocks.CreateInstance<CreateOrderCommandHandler>();

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
        var currentUser = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new CreateOrderCommandHandler(context, currentUser);

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
        var act = () => new CreateOrderCommandHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(currentUser));
    }

    [Fact]
    public async Task ConstructorShouldThrowsArgumentNullExceptionWhenCommandIsNull()
    {
        // Arrange.
        var mocks = new AutoMocker(MockBehavior.Strict);

        mocks.GetMock<IAppDbContext>();
        mocks.GetMock<ICurrentUser>();

        var sut = mocks.CreateInstance<CreateOrderCommandHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'command')");

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenReceiverIsNotInBase()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);

        var sender = fixture.Create<User>();
        var size = SizeCode.Medium;
        var receiver = fixture.Create<User>();

        var orderInfo = new OrderInfo(SequentialGuid.Create(),
                                      fixture.Create<OrderInfoWeight>(),
                                      size.ToSize(),
                                      fixture.Create<OrderInfoPrice>(),
                                      fixture.Create<OrderInfoAddressFrom>(),
                                      fixture.Create<OrderInfoAddressTo>());

        var command = new CreateOrderCommand(fixture.Create<OrderName>(), orderInfo.Weight, size, orderInfo.Price, orderInfo.AddressFrom, orderInfo.AddressTo, receiver.Id);

        mocks.MockDbSet(s => s.Orders);
        mocks.MockDbSet(s => s.Users, sender);
        mocks.MockDbSet(s => s.NotAuthUsers);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();
        mocks.CurrentUserSet(sender.Id);

        var sut = mocks.CreateInstance<CreateOrderCommandHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(command, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<NotFoundException>()
                 .WithMessage("Получатель не найден ни среди зарегистрированных, ни среди незарегистрированных пользователей");
    }
}
