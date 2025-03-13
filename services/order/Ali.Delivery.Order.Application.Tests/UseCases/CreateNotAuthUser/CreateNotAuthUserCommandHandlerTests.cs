using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.CreateNotAuthUser;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.NotAuthUser;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace Ali.Delivery.Order.Application.Tests.UseCases.CreateNotAuthUser;

[Trait("Category", "Unit")]
public class CreateNotAuthUserCommandHandlerTests
{
    [Fact]

    public async Task HandlerShouldCreateNotAuthUser()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user = fixture.Create<User>();
        var notAuthUserFirstName = fixture.Create<NotAuthUserFirstName>();
        var notAuthUserLastName = fixture.Create<NotAuthUserLastName>();
        var notAuthUserPhoneNumber = new NotAuthUserPhoneNumber("+78777777777");
        
        mocks.CurrentUserSet(user.Id);
        
        mocks.MockDbSet(u => u.Users, user);
        
        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();
        
        var command = new CreateNotAuthUserCommand(notAuthUserFirstName, notAuthUserLastName, notAuthUserPhoneNumber);
        
        var sut = mocks.CreateInstance<CreateNotAuthUserCommandHandler>();

        // Act.
        var result = await sut.Handle(command, default);

        // Assert.
        mocks.GetMock<IAppDbContext>()
             .Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.NotEqual(Guid.Empty, result);

       mocks.GetMock<IAppDbContext>()
             .Verify(db => db.Users, Times.Once);
    }


    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;
        var currentUser = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new CreateNotAuthUserCommandHandler(context, currentUser);

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
        var act = () => new CreateNotAuthUserCommandHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(currentUser));
    }

    [Fact]
    public async Task HandleWhenUserNotFoundThrowsNotFoundException()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user  = fixture.Create<User>();
        
        mocks.CurrentUserSet(user.Id);
        mocks.MockDbSet(u => u.Users);
    
        var command = new CreateNotAuthUserCommand(
            fixture.Create<NotAuthUserFirstName>(),
            fixture.Create<NotAuthUserLastName>(),
            new NotAuthUserPhoneNumber("+79999999999")
        );
    
        var sut = mocks.CreateInstance<CreateNotAuthUserCommandHandler>();

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => 
                                                       sut.Handle(command, CancellationToken.None)
        );
        
        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
