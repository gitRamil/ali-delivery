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
        var chatId = fixture.Create<long>();
        var user = new User(id, login, chatId);
        var langRus = LanguageDictionary.Russian;


        user.UpsertUserLanguage(langRus);
        var initialConfig = user.UserConfig!;
        var initialId = initialConfig.Id;

        // Act
        user.UpsertUserLanguage(langRus);

        // Assert

        user.UserConfig!
            .Id.Should()
            .Be(initialId);

        user.UserConfig
            .Language.Should()
            .Be(LanguageDictionary.Russian);
    }

    [Fact]
    public void ChatId_ShouldBeSetCorrectly_FromConstructor()
    {
        // Arrange
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var expectedChatId = fixture.Create<long>();

        // Act
        var user = new User(id, login, expectedChatId);

        // Assert
        user.ChatId.Should().Be(expectedChatId);
    }

    [Fact]
    public void AddOrUpdateUserConfigShouldSucceedWhenAllValidArgumentsArePassed()
    {
        //Arrange.
        var langRus = LanguageDictionary.Russian;
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<long>();
        var user = new User(id, login, chatId);

        //Act.
        var act = () => user.UpsertUserLanguage(langRus);

        //Assert.
        act.Should()
            .NotThrow();

        user.UserConfig!
            .Language
            .Should()
            .Be(LanguageDictionary.Russian);
    }

    [Fact]
    public void AddOrUpdateUserConfigShouldUpdateExistingConfigWhenConfigAlreadyExists()
    {
        // Arrange
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var login = fixture.Create<string>();
        var chatId = fixture.Create<long>();
        var user = new User(id, login, chatId);
        var langRus = LanguageDictionary.Russian;
        var langEng = LanguageDictionary.English;
        user.UpsertUserLanguage(langRus);

        // Act 
        user.UpsertUserLanguage(langEng);

        // Assert
        user.UserConfig!
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
        var chatId = fixture.Create<long>();
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
        var chatId = fixture.Create<long>();

        //Act.
        var act = () => new User(id, login, chatId);

        //Assert.
        act.Should()
            .NotThrow();
    }


    [Fact]
    public void AddUserShouldThrowArgumentNullExceptionWhenLoginIsNull()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var chatId = fixture.Create<long>();
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
        var constructor =
            type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var user = (User)constructor.Invoke(null);

        // Assert
        user.Should()
            .NotBeNull();

        user.Id.Should()
            .Be(SequentialGuid.Empty);

        user.Login.Should()
            .BeNull();
    }
}