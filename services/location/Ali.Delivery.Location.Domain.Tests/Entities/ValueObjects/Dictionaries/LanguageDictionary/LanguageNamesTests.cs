using Ali.Delivery.Location.Domain.ValueObjects.Dictionaries.LanguageDictionary;

namespace Ali.Delivery.Location.Domain.Tests.Entities.ValueObjects.Dictionaries.LanguageDictionary;

[Trait("Category", "Unit")]
public class LanguageNamesTests
{
    [Fact]
    public void ConstructorShouldSetNameWhenValid()
    {
        // Arrange
        var name = "English";

        // Act
        var languageName = new LanguageName(name);

        // Assert
        languageName.ToString().Should().Be("English");
    }

    [Fact]
    public void ConstructorShouldThrowArgumentExceptionWhenNullOrWhitespace()
    {
        // Arrange
        string name = null!;

        // Act
        var act = () => new LanguageName(name);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Наименование справочника языков не может быть null или пустой строкой.*");
    }

    [Fact]
    public void ConstructorShouldThrowArgumentExceptionWhenNameTooLong()
    {
        // Arrange
        var name = new string('a', LanguageName.MaxLength + 1);

        // Act
        var act = () => new LanguageName(name);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage($"Наименование справочника языков не может быть больше {LanguageName.MaxLength}.*");
    }

    [Fact]
    public void OperatorExplicitConversionFromStringReturnsLanguageName()
    {
        // Arrange
        var name = "French";

        // Act
        var languageName = (LanguageName)name;

        // Assert
        languageName.ToString().Should().Be("French");
    }

    [Fact]
    public void OperatorExplicitConversionFromNullStringReturnsNull()
    {
        // Arrange
        string name = null!;

        // Act
        var languageName = (LanguageName?)name;

        // Assert
        languageName.Should().BeNull();
    }

    [Fact]
    public void OperatorImplicitConversionToStringReturnsName()
    {
        // Arrange
        var languageName = new LanguageName("Spanish");

        // Act
        string name = languageName;

        // Assert
        name.Should().Be("Spanish");
    }

    [Fact]
    public void OperatorImplicitConversionNullLanguageNameReturnsNull()
    {
        // Arrange
        LanguageName languageName = null!;

        // Act
        string name = languageName;

        // Assert
        name.Should().BeNull();
    }

    [Fact]
    public void LanguageNamesWithSameNameShouldBeEqual()
    {
        // Arrange
        var name1 = new LanguageName("German");
        var name2 = new LanguageName("German");

        // Act & Assert
        name1.Should().Be(name2);
    }

    [Fact]
    public void LanguageNames_WithDifferentNames_ShouldNotBeEqual()
    {
        // Arrange
        var name1 = new LanguageName("German");
        var name2 = new LanguageName("Italian");

        // Act & Assert
        name1.Should().NotBe(name2);
    }
}