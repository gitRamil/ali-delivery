using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Application.UseCases.CompletePassport;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using PassportType = Ali.Delivery.Order.Application.Dtos.Enums.PassportType;

namespace Ali.Delivery.Order.Application.Tests.UseCases.CompletePassport;

[Trait("Category", "Unit")]
public class CompletePassportCommandHandlerTest
{

    [Fact]
    public async Task HandlerShouldCompleteUsersPassport()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        var user = new User(fixture.Create<SequentialGuid>(), 
                            fixture.Create<UserLogin>(), 
                            fixture.Create<UserPassword>(), Role.BasicUser, fixture.Create<UserBirthDay>());
        var passportType = fixture.Create<PassportType>();
        var passportNumber = new PassportInfoPassportNumber("1234567890");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();
        var firstName =fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        
        mocks.CurrentUserSet(user.Id);
        mocks.MockDbSet(s => s.Users, user);
        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();
        var command = new CompletePassportCommand(passportType, passportNumber, (DateTime)regDate, issuedBy, firstName, lastName);
        var sut = mocks.CreateInstance<CompletePassportCommandHandler>();
        
        // Act.
        var result = await sut.Handle(command, default);
        
        // Assert.
        mocks.Verify();

        result.Should()
              .Be(user.Id);
        
        user.FirstName.Should().Be(firstName);
        user.LastName.Should().Be(lastName);
        
        user.PassportInfo?.PassportType.Should().Be(passportType.ToPassportType());
        user.PassportInfo?.PassportNumber.Should().Be(passportNumber);
        user.PassportInfo?.RegDate.Should().Be(regDate);
        user.PassportInfo?.IssuedBy.Should().Be(issuedBy);
    }
    
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;
        var currentUser = Mock.Of<ICurrentUser>();

        // Act.
        var act = () => new CompletePassportCommandHandler(context, currentUser);

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
        var act = () => new CompletePassportCommandHandler(context, currentUser);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(currentUser));
    }

    [Fact]
    public async Task HandlerShouldThrowExceptionWhenUserNotFound()
    {
        // Arrange.
        var mocker = new AutoMocker();
        mocker.MockDbSet(u => u.Users);
        mocker.CurrentUserSet(SequentialGuid.Create());

        var handler = mocker.CreateInstance<CompletePassportCommandHandler>();
        var command = new CompletePassportCommand(PassportType.International, "1234567890", DateTime.UtcNow, "Some Authority", "Ivan", "Ivanov");
        
        // Act.
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(command, CancellationToken.None));

        // Assert.
        Assert.Equal("Пользователь не найден.", exception.Message);

    }
}
    
