using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Location.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class UserConfigTest
{
    [Fact]
    public void AddUserConfigShouldSucceedWhenAllArgumentsArePasses()
    {
        //Arrange.
        var fixture = new Fixture();
        var user = fixture.Create<User>();
        var userId = user.Id;
        var language = LanguageDictionary.English;

        //Act.
        var act = () => new UserConfig(userId, language);

        //Assert.
        act.Should()
            .NotThrow();
    }

    [Fact]
    public void AddUserConfigShouldThrowArgumentNullExceptionLanguageIsNull()
    {
        //Arrange.
        var fixture = new Fixture();
        var user = fixture.Create<User>();
        var userId = user.Id;
        LanguageDictionary language = null!;

        //Act.
        var act = () => new UserConfig(userId, language);

        //Assert.
        act.Should()
            .Throw<ArgumentNullException>(nameof(language));
    }


    [Fact]
    public void ProtectedConstructorShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange
        var type = typeof(UserConfig);
        var constructor =
            type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var userConfig = (UserConfig)constructor.Invoke(null);

        // Assert
        userConfig.Should()
            .NotBeNull();

        userConfig.Id.Should()
            .Be(SequentialGuid.Empty);

        userConfig.Language.Should()
            .BeNull();
    }

    [Fact]
    public void UserShouldBeNullWhenUserConfigCreatedManually()
    {
        // Arrange
        var fixture = new Fixture();
        var language = LanguageDictionary.English;
        var user = fixture.Create<User>();

        var userConfig = new UserConfig(user.Id, language);

        // Act & Assert
        userConfig.User.Should().BeNull();
        userConfig.Id.Should().Be(user.Id);
    }


    [Fact]
    public void UpdateLanguageShouldSucceedWhenAllArgumentsArePasses()
    {
        //Arrange.
        var fixture = new Fixture();
        var user = fixture.Create<User>();
        var userId = user.Id;
        var language = LanguageDictionary.English;
        var newLanguage = LanguageDictionary.Russian;
        var userConfig = new UserConfig(userId, language);

        //Act.
        var act = () => userConfig.UpdateLanguage(newLanguage);

        //Assert.
        act.Should()
            .NotThrow();

        userConfig.Language.Should()
            .Be(newLanguage);

        userConfig.Id.Should()
            .Be(userId);
    }
}