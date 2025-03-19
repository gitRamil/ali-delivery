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
        const string notAuthFirstName = "Ivan";
        const string notAuthLastName = "Ivanov";
        const string phoneNumber = "+77471234567";

        // Act.
        var act = () => user.AddNotAuthUser(notAuthFirstName, notAuthLastName, phoneNumber);

        // Assert.
        act.Should()
           .NotThrow();

        user.NotAuthUsers.First()
            .FirstName?.ToString()
            .Should()
            .Be(notAuthFirstName);
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
        const string notAuthFirstName = "Ivan";
        const string notAuthLastName = "Ivanov";
        const string phoneNumber = "+77471234567";

        // Act.
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

        // Act.
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

        // Act.
        var act = () => user.CreatePassportInfo(passportId, passportType, passportNumber, regDate, issuedBy);

        // Assert.
        act.Should()
           .Throw<InvalidOperationException>("Паспортные данные уже существуют.");
    }

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

        // Act.
        var act = () => new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        // Assert.
        act.Should()
           .NotThrow();
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

        // Act.
        var act = () => new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(birthDay));
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

        // Act.
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

        // Act.
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

        // Act.
        var act = () => new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(role));
    }

    [Fact]
    public void ProtectedConstructorShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange
        var type = typeof(User);
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var user = (User)constructor.Invoke(null);

        // Assert
        user.Should()
            .NotBeNull();

        user.Id.Should()
            .Be(SequentialGuid.Empty);

        user.Login.Should()
            .BeNull();

        user.FirstName.Should()
            .BeNull();

        user.LastName.Should()
            .BeNull();

        user.Password.Should()
            .BeNull();

        user.PassportInfo.Should()
            .BeNull();

        user.BirthDay.Should()
            .BeNull();

        user.Role.Should()
            .BeNull();
    }

    [Fact]
    public void UpdateBirthDayShouldSucceedWhenUpdateBirthDay()
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

        // Act.
        var act = () => user.UpdateBirthDay(newBirthDay);

        // Assert.
        act.Should()
           .NotThrow();

        user.BirthDay.Should()
            .Be(newBirthDay);
    }

    [Fact]
    public void UpdateLoginShouldSucceedWhenUpdateLogin()
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

        // Act.
        var act = () => user.UpdateLogin(newLogin);

        // Assert.
        act.Should()
           .NotThrow();

        user.Login.Should()
            .Be(newLogin);
    }

    [Fact]
    public void UpdateNameShouldSucceedWhenUpdateName()
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

        // Act.
        var act = () => user.UpdateName(newFirstName, newLastName);

        // Assert.
        act.Should()
           .NotThrow();

        user.FirstName.Should()
            .Be(newFirstName);

        user.LastName.Should()
            .Be(newLastName);
    }

    [Fact]
    public void UpdateRoleShouldSucceedWhenUpdateRole()
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

        // Act.
        var act = () => user.UpdateRole(newRole);

        // Assert.
        act.Should()
           .NotThrow();

        user.Role.Should()
            .Be(newRole);
        
        user.Login.ToString().Should().Be(login.ToString());
        user.Password.ToString().Should().Be(password.ToString());
        user.FirstName!.ToString().Should().Be(firstName.ToString());
        user.LastName!.ToString().Should().Be(lastName.ToString());

    }
}
