using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.UseCases.GetIsUserExist;
using Ali.Delivery.Order.Domain.Entities;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetIsUserExist;

[Trait("Category", "Unit")]
public class GetIsUserExistQueryHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new GetIsUserExistQueryHandler(context);

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

        var sut = mocks.CreateInstance<GetIsUserExistQueryHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'query')");
    }

    [Fact]
    public async Task HandlerShouldReturnFalse_WhenUserNotExists()
    {
        // Arrange
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user = fixture.Create<User>();

        mocks.MockDbSet(u => u.Users);

        var sut = mocks.CreateInstance<GetIsUserExistQueryHandler>();

        // Act
        var result = await sut.Handle(new GetIsUserExistQuery(user.Id), CancellationToken.None);

        // Assert
        result.Should()
              .BeFalse();
    }

    [Fact]
    public async Task HandlerShouldReturnIsUserExist()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user = fixture.Create<User>();

        mocks.MockDbSet(u => u.Users, user);

        var sut = mocks.CreateInstance<GetIsUserExistQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetIsUserExistQuery(user.Id), CancellationToken.None);

        // Assert.
        mocks.Verify();

        result.Should()
              .BeTrue();
    }
}
