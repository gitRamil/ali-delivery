using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;

namespace Ali.Delivery.Order.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class UserTests
{
    [Fact]
    public void CreateUserShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));

        //Act.
        var act = () => new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void CreateUserShouldThrowArgumentNullExceptionWhenLoginIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        UserLogin login = null!;
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));

        //Act.
        var act = () => new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(login));
    }

    [Fact]
    public void CreateUserShouldThrowArgumentNullExceptionWhenPasswordIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        UserPassword password = null!;
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));

        //Act.
        var act = () => new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(password));
    }

    [Fact]
    public void CreateUserShouldThrowArgumentNullExceptionWhenRoleIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        Role role = null!;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));

        //Act.
        var act = () => new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(role));
    }

    [Fact]
    public void CreateUserShouldThrowArgumentNullExceptionWhenBirthdayIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        UserBirthDay birthDay = null!;
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));

        //Act.
        var act = () => new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(birthDay));
    }

    [Fact]
    public void AddNotAuthUserShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        var notAuthFirstName = "Ivan";
        var notAuthLastName = "Ivanov";
        var phoneNumber = "+77471234567";

        //Act.
        var act = () => user.AddNotAuthUser(notAuthFirstName, notAuthLastName, phoneNumber);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void AddNotAuthUserShouldThrowInvalidOperationExceptionWhenPassportInfoIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();
        var user = new User(id, login, password, role, birthDay, firstName, lastName);

        var notAuthFirstName = "Ivan";
        var notAuthLastName = "Ivanov";
        var phoneNumber = "+77471234567";

        //Act.
        var act = () => user.AddNotAuthUser(notAuthFirstName, notAuthLastName, phoneNumber);

        // Assert.
        act.Should()
           .Throw<InvalidOperationException>("Для продолжения пожалуйста заполните паспортные данные");
    }

    [Fact]
    public void CreatePassportInfoShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();
        var passportId = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("123456789");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();
        var user = new User(id, login, password, role, birthDay, firstName, lastName);

        //Act.
        var act = () => user.CreatePassportInfo(passportId, passportType, passportNumber, regDate, issuedBy);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void CreatePassportInfoShouldThrowInvalidOperationExceptionWhenPassportInfoIsAlreadyExist()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var passportId = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("123456789");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        //Act.
        var act = () => user.CreatePassportInfo(passportId, passportType, passportNumber, regDate, issuedBy);

        // Assert.
        act.Should()
           .Throw<InvalidOperationException>("Паспортные данные уже существуют.");
    }

    [Fact]
    public void UpdateBirthDayShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var newBirthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateBirthDay(newBirthDay);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void UpdateBirthDayShouldThrowArgumentNullExceptionWhenNewBirthDayIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        UserBirthDay newBirthDay = null!;
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateBirthDay(newBirthDay);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(birthDay));
    }

    [Fact]
    public void UpdateLoginShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var newLogin = fixture.Create<UserLogin>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateLogin(newLogin);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void UpdateLoginShouldThrowArgumentNullExceptionWhenNewLoginIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        UserLogin newLogin = null!;
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateLogin(newLogin);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(newLogin));
    }

    [Fact]
    public void UpdateNameShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var newFirstName = fixture.Create<UserFirstName>();
        var newLastName = fixture.Create<UserLastName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateName(newFirstName, newLastName);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void UpdateNameShouldThrowArgumentNullExceptionWhenNewFirstNameIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        UserFirstName newFirstName = null!;
        var newLastName = fixture.Create<UserLastName>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateName(newFirstName, newLastName);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(newFirstName));
    }

    [Fact]
    public void UpdateNameShouldThrowArgumentNullExceptionWhenNewLastNameIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        UserLastName newLastName = null!;
        var newFirstName = fixture.Create<UserFirstName>();
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateName(newFirstName, newLastName);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(newLastName));
    }

    [Fact]
    public void UpdateRoleShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        var firstName = fixture.Create<UserFirstName>();
        var newRole = Role.Courier;
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateRole(newRole);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void UpdateRoleShouldThrowArgumentNullExceptionWhenNewRoleIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<UserLogin>();
        var password = fixture.Create<UserPassword>();
        var role = Role.BasicUser;
        var birthDay = fixture.Create<UserBirthDay>();
        Role newRole = null!;
        var firstName = fixture.Create<UserFirstName>();
        var lastName = fixture.Create<UserLastName>();

        var passportInfo = new PassportInfo(SequentialGuid.Create(),
                                            PassportType.Internal,
                                            new PassportInfoPassportNumber("123456789"),
                                            new PassportInfoRegDate(DateTime.Now),
                                            new PassportInfoIssuedBy("MVD RF"));
        var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
        //Act.
        var act = () => user.UpdateRole(newRole);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(newRole));
    }
}
