using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.DeleteUser;
using Ali.Delivery.Order.Domain.Entities;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace Ali.Delivery.Order.Application.Tests.UseCases.DeleteUser;

[Trait("Category", "Unit")]
public class DeleteUserCommandHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new DeleteUserCommandHandler(context);

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

        var sut = mocks.CreateInstance<DeleteUserCommandHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'command')");
    }

    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenUserNotinBase()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user = fixture.Create<User>();

        mocks.MockDbSet(u => u.Users);
        mocks.CurrentUserSet(user.Id);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var sut = mocks.CreateInstance<DeleteUserCommandHandler>();

        var command = new DeleteUserCommand(user.Id);

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));

        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task UserShouldBeDeleted()
    {
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user = fixture.Create<User>();

        mocks.MockDbSet(u => u.Users, user);

        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var command = new DeleteUserCommand(user.Id);

        var sut = mocks.CreateInstance<DeleteUserCommandHandler>();

        // Act.
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert.
        mocks.Verify();

        mocks.GetMock<IAppDbContext>()
             .Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotEqual(Guid.Empty, result);
    }
}
