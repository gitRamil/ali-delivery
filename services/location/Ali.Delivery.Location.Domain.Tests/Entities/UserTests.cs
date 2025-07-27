using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Location.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class UserTests
{
    [Fact]
    public void AddOrUpdateUserConfigShouldNotMakeChangesWhenSameLanguageProvided()
    {
        // Arrange
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<string>();
        var user = new User(id, login, chatId);

        user.UpsertUserLanguage("ru");
        var initialConfig = user.UserConfigs.First();
        var initialId = initialConfig.Id;

        // Act
        user.UpsertUserLanguage("ru");

        // Assert
        user.UserConfigs.Should()
            .HaveCount(1);

        user.UserConfigs.First()
            .Id.Should()
            .Be(initialId);

        user.UserConfigs.First()
            .Language.Should()
            .Be(LanguageDictionary.Russian);
    }

    [Fact]
    public void AddOrUpdateUserConfigShouldSetCorrectLanguageWhenValidLanguageCodeProvided()
    {
        // Arrange
        const string languageCode = "en";
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<string>();
        var user = new User(id, login, chatId);

        // Act
        user.UpsertUserLanguage(languageCode);

        // Assert
        user.UserConfigs.Should()
            .HaveCount(1);

        user.UserConfigs.First()
            .Language.Should()
            .Be(LanguageDictionary.English);

        user.UserConfigs.First()
            .Language.Code.ToString()
            .Should()
            .Be("EN");
    }

    [Fact]
    public void AddOrUpdateUserConfigShouldSetDefaultLanguageWhenLanguageCodeIsNull()
    {
        // Arrange
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<string>();
        var user = new User(id, login, chatId);

        // Act
        user.UpsertUserLanguage(null);

        // Assert
        user.UserConfigs.Should()
            .HaveCount(1);

        user.UserConfigs.First()
            .Language.Code.ToString()
            .Should()
            .Be("RU");

        user.UserConfigs.First()
            .Language.Should()
            .Be(LanguageDictionary.Russian);
    }

    [Fact]
    public void AddOrUpdateUserConfigShouldSucceedWhenAllValidArgumentsArePassed()
    {
        //Arrange.
        const string languageCode = "ru";
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<string>();
        var user = new User(id, login, chatId);

        //Act.
        var act = () => user.UpsertUserLanguage(languageCode);

        //Assert.
        act.Should()
           .NotThrow();

        user.UserConfigs.Should()
            .HaveCount(1);

        user.UserConfigs.First()
            .Language.Code.ToString()
            .Should()
            .Be("RU");
    }

    [Fact]
    public void AddOrUpdateUserConfigShouldThrowArgumentExceptionWhenLanguageCodeIsInvalid()
    {
        // Arrange
        const string invalidLanguageCode = "gr";
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<string>();
        var user = new User(id, login, chatId);

        // Act
        var act = () => user.UpsertUserLanguage(invalidLanguageCode);

        // Assert
        act.Should()
           .Throw<ArgumentException>()
           .WithMessage("*gr*")
           .And.ParamName.Should()
           .Be("code");
    }

    [Fact]
    public void AddOrUpdateUserConfigShouldUpdateExistingConfigWhenConfigAlreadyExists()
    {
        // Arrange
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<string>();
        var user = new User(id, login, chatId);
        user.UpsertUserLanguage("ru");

        // Act 
        user.UpsertUserLanguage("en");

        // Assert
        user.UserConfigs.Should()
            .HaveCount(1);

        user.UserConfigs.First()
            .Language.Should()
            .Be(LanguageDictionary.English);
    }

    [Fact]
    public void AddUserLocation_ShouldAddLocationToCollection_WhenValidCoordinatesProvided()
    {
        // Arrange
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<string>();
        var user = new User(id, login, chatId);

        const double longitude = 37.6173;
        const double latitude = 55.7558;

        // Act
        user.AddUserLocation(longitude, latitude);

        // Assert
        user.UserLocations.Should()
            .HaveCount(1);
        var addedLocation = user.UserLocations.First();

        addedLocation.Longitude.Should()
                     .Be(longitude);

        addedLocation.Latitude.Should()
                     .Be(latitude);

        addedLocation.User.Should()
                     .Be(user);

        addedLocation.Id.Should()
                     .NotBe(SequentialGuid.Empty);
    }

    [Fact]
    public void AddUserShouldSucceedWhenAllValidArgumentsArePassed()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<string>();

        //Act.
        var act = () => new User(id, login, chatId);

        //Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void AddUserShouldThrowArgumentNullExceptionWhenChatIdIsNull()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        string chatId = null!;
        var login = fixture.Create<string>();

        //Act.
        var act = () => new User(id, login, chatId);

        //Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(chatId));
    }

    [Fact]
    public void AddUserShouldThrowArgumentNullExceptionWhenLoginIsNull()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var chatId = fixture.Create<string>();
        string login = null!;

        //Act.
        var act = () => new User(id, login, chatId);

        //Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(login));
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

        user.ChatId.Should()
            .BeNull();
    }
}
