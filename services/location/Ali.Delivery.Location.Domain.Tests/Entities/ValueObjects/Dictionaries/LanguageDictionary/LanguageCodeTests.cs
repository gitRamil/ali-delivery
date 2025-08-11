using Ali.Delivery.Location.Domain.ValueObjects.Dictionaries.LanguageDictionary;

namespace Ali.Delivery.Location.Domain.Tests.Entities.ValueObjects.Dictionaries.LanguageDictionary;

public class LanguageCodeTests
{
    [Fact]
    public void ConstructorShouldSetCodeWhenValid()
    {
        // Arrange
        var code = "eng";

        // Act
        var languageCode = new LanguageCode(code);

        // Assert
        languageCode.ToString().Should().Be("eng");
    }

    [Fact]
    public void ConstructorShouldThrowArgumentExceptionWhenNullOrWhitespace()
    {
        // Arrange
        string code = null!;

        // Act
        var act = () => new LanguageCode(code);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Код справочника языков не может быть null или пустой строкой.*");
    }

    [Fact]
    public void ConstructorShouldTrimCode()
    {
        // Arrange
        var code = " ru ";

        // Act
        var languageCode = new LanguageCode(code);

        // Assert
        languageCode.ToString().Should().Be("ru");
    }

    [Fact]
    public void ConstructorShouldThrowArgumentExceptionWhenCodeTooLong()
    {
        // Arrange
        var code = "russian";

        // Act
        var act = () => new LanguageCode(code);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Код справочника языков не может быть больше 3.*");
    }

    [Fact]
    public void OperatorStringConversionReturnsCode()
    {
        // Arrange
        var languageCode = new LanguageCode("en");

        // Act
        string code = languageCode;

        // Assert
        code.Should().Be("en");
    }

    [Fact]
    public void OperatorStringConversionReturnsNullWhenLanguageCodeNull()
    {
        // Arrange
        LanguageCode code = null!;

        // Act
        string result = code;

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void LanguageCodesWithSameCodeShouldBeEqual()
    {
        // Arrange
        var code1 = new LanguageCode("fr");
        var code2 = new LanguageCode("fr");

        // Act & Assert
        code1.Should().Be(code2);
    }

    [Fact]
    public void LanguageCodesWithDifferentCodesShouldNotBeEqual()
    {
        // Arrange
        var code1 = new LanguageCode("fr");
        var code2 = new LanguageCode("en");

        // Act & Assert
        code1.Should().NotBe(code2);
    }
}