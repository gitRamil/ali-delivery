using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.GetUser;
using Ali.Delivery.Order.Domain.Entities;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetUser;

[Trait("Category", "Unit")]
public class GetUserQueryHandlerTests
{
    [Fact]
    public async Task HandlerShouldReturnUser()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user = fixture.Create<User>();

        mocks.MockDbSet(u => u.Users, user);

        var sut = mocks.CreateInstance<GetUserQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetUserQuery(user.Id), default);

        // Assert.
        mocks.Verify();

        result.Id.Should()
              .Be(user.Id);
    }

    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new GetUserQueryHandler(context);

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

        var sut = mocks.CreateInstance<GetUserQueryHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'query')");
    }

    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenUserNotinBase()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user = fixture.Create<User>();

        mocks.MockDbSet(u => u.Users);

        var sut = mocks.CreateInstance<GetUserQueryHandler>();

        var command = new GetUserQuery(user.Id);

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));
    }
}
