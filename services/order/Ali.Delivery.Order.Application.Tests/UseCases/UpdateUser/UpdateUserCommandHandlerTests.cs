using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.UseCases.UpdateUser;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;
using Shouldly;
using PassportType = Ali.Delivery.Order.Application.Dtos.Enums.PassportType;

namespace Ali.Delivery.Order.Application.Tests.UseCases.UpdateUser;

[Trait("Category","Unit")]
public class UpdateUserCommandHandlerTests
{
    [Fact]
    public async Task HandlerShouldUpdateUser()
    {
        // Arrange.
        var fixture = new Fixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            Domain.Entities.Dictionaries.PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        
        var newLogin = fixture.Create<UserLogin>();
        var newRole = RoleCode.BasicUser;
        var newBirthDay = fixture.Create<UserBirthDay>();
        var newFirstName = fixture.Create<UserFirstName>();
        var newLastName = fixture.Create<UserLastName>();
        var newPassportNumber = new PassportInfoPassportNumber("12312312333");
        var newRegDate = fixture.Create<DateTime>();
        var newIssuedBy = fixture.Create<PassportInfoIssuedBy>();
        var newPassportType = PassportType.Diplomatic;

        mocks.MockDbSet(u => u.Users, user);
        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var command = new UpdateUserCommand(user.Id, 
                                            newLogin, 
                                            newFirstName, 
                                            newLastName, 
                                            newPassportType, 
                                            newPassportNumber, 
                                            newRegDate, 
                                            newIssuedBy, 
                                            newRole, 
                                            newBirthDay);
        
        var sut = mocks.CreateInstance< UpdateUserCommandHandler>();

        // Act.
        var result = await sut.Handle(command, default);

        // Assert.
        mocks.GetMock<IAppDbContext>()
             .Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        result.Login.Should().Be(newLogin);

        mocks.GetMock<IAppDbContext>()
             .Verify(db => db.Users, Times.Once);


    }
    [Fact]
    public async Task ConstructorShouldThrowsArgumentNullExceptionWhenCommandIsNull()
    {
        // Arrange.
        var mocks = new AutoMocker(MockBehavior.Strict);
    
        mocks.GetMock<IAppDbContext>();
        
        var sut = mocks.CreateInstance<UpdateUserCommandHandler>();

        // Act.
        Func<Task> act = () => sut.Handle(null!, CancellationToken.None);

        // Assert.
        await act.Should()
                 .ThrowAsync<ArgumentNullException>()
                 .WithMessage("Value cannot be null. (Parameter 'command')");
        
        mocks.GetMock<IAppDbContext>().Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never
        );
    }
    
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new UpdateUserCommandHandler(context);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(context));
    }
    [Fact]
    public async Task HandlerShouldThrowNotFoundExceptionWhenUserNotinBase()
    {
        // Arrange.
        var fixture = new Fixture();
        var mocks = new AutoMocker(MockBehavior.Strict);
        
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            Domain.Entities.Dictionaries.PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        
        var newLogin = fixture.Create<UserLogin>();
        var newRole = RoleCode.BasicUser;
        var newBirthDay = fixture.Create<UserBirthDay>();
        var newFirstName = fixture.Create<UserFirstName>();
        var newLastName = fixture.Create<UserLastName>();
        var newPassportNumber = new PassportInfoPassportNumber("12312312333");
        var newRegDate = fixture.Create<DateTime>();
        var newIssuedBy = fixture.Create<PassportInfoIssuedBy>();
        var newPassportType = PassportType.Diplomatic;

        mocks.MockDbSet(u => u.Users);
        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();

        var command = new UpdateUserCommand(user.Id, 
                                            newLogin, 
                                            newFirstName, 
                                            newLastName, 
                                            newPassportType, 
                                            newPassportNumber, 
                                            newRegDate, 
                                            newIssuedBy, 
                                            newRole, 
                                            newBirthDay);
        
        var sut = mocks.CreateInstance< UpdateUserCommandHandler>();
        mocks.GetMock<IAppDbContext>()
             .SetupDefaultSaveChangesAsync();
        
        // Act & Assert.
        await Should.ThrowAsync<NotFoundException>(() => 
                                                       sut.Handle(command, CancellationToken.None)
        );
        
        mocks.GetMock<IAppDbContext>()
             .Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);

    }
    
}
