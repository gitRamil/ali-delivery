using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.NotAuthUser;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;

namespace Ali.Delivery.Order.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class NotAuthUserTests
{
    [Fact]
    public void CreateNotAuthUserShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var creatorPassportInfo = new PassportInfo(SequentialGuid.Create(),
                                                   PassportType.Internal,
                                                   new PassportInfoPassportNumber("123456789"),
                                                   new PassportInfoRegDate(DateTime.Now),
                                                   new PassportInfoIssuedBy("MVD RF"));
        var creator = new User(fixture.Create<SequentialGuid>(),
                               fixture.Create<UserLogin>(),
                               fixture.Create<UserPassword>(),
                               fixture.Create<Role>(),
                               fixture.Create<UserBirthDay>(),
                               fixture.Create<UserFirstName>(),
                               fixture.Create<UserLastName>(),
                               creatorPassportInfo);
        var firstName = fixture.Create<NotAuthUserFirstName>();
        var lastName = fixture.Create<NotAuthUserLastName>();
        var phoneNumber = new NotAuthUserPhoneNumber("+77877777777");
        
        // Act.
        var act = () => new NotAuthUser(id, creator, firstName, lastName, phoneNumber);
       
        // Assert.
        act.Should().NotThrow();
    }
    [Fact]
    public void CreateNotAuthUserShouldSThrowArgumentNullExceptionWhenCreatorIsNull()
    {
        // Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        User creator = null!;
        var firstName = fixture.Create<NotAuthUserFirstName>();
        var lastName = fixture.Create<NotAuthUserLastName>();
        var phoneNumber = new NotAuthUserPhoneNumber("+77877777777");
        
        // Act.
        var act = () => new NotAuthUser(id, creator, firstName, lastName, phoneNumber);
       
        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(creator));
    }
}
