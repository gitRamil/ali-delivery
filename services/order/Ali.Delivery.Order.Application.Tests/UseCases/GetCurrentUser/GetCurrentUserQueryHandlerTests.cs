using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.GetCurrentUser;
using Ali.Delivery.Order.Domain.Entities;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetCurrentUser;

[Trait("Category","Unit")]
public class GetCurrentUserQueryHandlerTests
{
    [Fact]
    public async Task HandlerShouldGetCurrentUser()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var currentUser = fixture.Create<User>();
        
        mocks.MockDbSet(u => u.Users, currentUser);
        mocks.CurrentUserSet(currentUser.Id);
        
        var sut = mocks.CreateInstance<GetCurrentUserQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetCurrentUserQuery(), default);

        // Assert.
        mocks.Verify();

        result.Should()
              .NotBeNull();
        
        result.Id.Should().Be(currentUser.Id);
    }
    
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;
        var currentUser = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new GetCurrentUserQueryHandler(context, currentUser);

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
        var act = () => new GetCurrentUserQueryHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(currentUser));
    }
    [Fact]
    public async Task ConstructorShouldThrowsArgumentNullExceptionWhenRequestIsNull()
    {
        // Arrange.
        var mocks = new AutoMocker(MockBehavior.Strict);
    
        mocks.GetMock<IAppDbContext>();
        mocks.GetMock<ICurrentUser>();
    
        var sut = mocks.CreateInstance<GetCurrentUserQueryHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'request')");
        
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
    
        var command = new GetCurrentUserQuery();
        var sut = mocks.CreateInstance<GetCurrentUserQueryHandler>();

        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => 
                                                       sut.Handle(command, CancellationToken.None)
        );
    }
        
}
