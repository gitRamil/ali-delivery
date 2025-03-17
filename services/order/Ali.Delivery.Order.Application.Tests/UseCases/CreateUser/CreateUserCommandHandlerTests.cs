using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.UseCases.CreateUser;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.CreateUser;

[Trait("Category", "Unit")]
public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task CreateUserShouldSucceed()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);

        mocks.MockDbSet(u => u.Users);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var role = RoleCode.BasicUser;
        var command = new CreateUserCommand(fixture.Create<UserLogin>(), fixture.Create<UserPassword>(), role, fixture.Create<UserBirthDay>());

        var sut = mocks.CreateInstance<CreateUserCommandHandler>();

        // Act.
        var result = await sut.Handle(command, CancellationToken.None);

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
        var act = () => new CreateUserCommandHandler(context);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(context));
    }

    [Fact]
    public async Task ConstructorShouldThrowsArgumentNullExceptionWhenCommandIsNull()
    {
        // Arrange.
        var mocks = new AutoMocker(MockBehavior.Strict);
        mocks.GetMock<IAppDbContext>();
        mocks.GetMock<ICurrentUser>();

        var sut = mocks.CreateInstance<CreateUserCommandHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'command')");

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
