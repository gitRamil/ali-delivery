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
        var id = fixture.Create<SequentialGuid>();
        var user = fixture.Create<User>();
        var language = LanguageDictionary.English;

        //Act.
        var act = () => new UserConfig(id, language, user);

        //Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void AddUserConfigShouldThrowArgumentNullExceptionLanguageIsNull()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var user = fixture.Create<User>();
        LanguageDictionary language = null!;

        //Act.
        var act = () => new UserConfig(id, language, user);

        //Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(language));
    }

    [Fact]
    public void AddUserConfigShouldThrowArgumentNullExceptionWhenUserIsNull()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        User user = null!;
        var language = LanguageDictionary.English;

        //Act.
        var act = () => new UserConfig(id, language, user);

        //Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(user));
    }

    [Fact]
    public void ProtectedConstructorShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange
        var type = typeof(UserConfig);
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var userConfig = (UserConfig)constructor.Invoke(null);

        // Assert
        userConfig.Should()
                  .NotBeNull();

        userConfig.Id.Should()
                  .Be(SequentialGuid.Empty);

        userConfig.User.Should()
                  .BeNull();

        userConfig.Language.Should()
                  .BeNull();
    }

    [Fact]
    public void UpdateLanguageShouldSucceedWhenAllArgumentsArePasses()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var user = fixture.Create<User>();
        var language = LanguageDictionary.English;
        var newLanguage = LanguageDictionary.Russian;
        var userConfig = new UserConfig(id, language, user);

        //Act.
        var act = () => userConfig.UpdateLanguage(newLanguage);

        //Assert.
        act.Should()
           .NotThrow();

        userConfig.Language.Should()
                  .Be(newLanguage);

        userConfig.User.Should()
                  .Be(user);

        userConfig.Id.Should()
                  .Be(id);
    }

    [Fact]
    public void UpdateLanguageShouldThrowArgumentNullExceptionWhenNewLanguageIsNull()
    {
        //Arrange.
        var fixture = new Fixture();
        var id = fixture.Create<SequentialGuid>();
        var user = fixture.Create<User>();
        var language = LanguageDictionary.English;
        LanguageDictionary newLanguage = null!;
        var userConfig = new UserConfig(id, language, user);

        //Act.
        var act = () => userConfig.UpdateLanguage(newLanguage);

        //Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(newLanguage));
    }
}
