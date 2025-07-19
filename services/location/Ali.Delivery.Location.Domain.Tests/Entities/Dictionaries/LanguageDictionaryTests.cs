using Ali.Delivery.Location.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Location.Domain.Tests.Entities.Dictionaries;

[Trait("Category", "Unit")]
public class LanguageDictionaryTests
{
    [Fact]
    public void GetAllValuesShouldReturnAllValues()
    {
        //Arrange. 

        //Act.
        var values = LanguageDictionary.GetAllValues()
                                       .ToList();

        //Assert.
        values[0]
            .Id.Should()
            .Be(LanguageDictionary.Russian.Id);

        values[0]
            .Name.Should()
            .Be(LanguageDictionary.Russian.Name);

        values[1]
            .Id.Should()
            .Be(LanguageDictionary.English.Id);

        values[1]
            .Name.Should()
            .Be(LanguageDictionary.English.Name);
    }

    [Fact]
    public void LanguageDictionaryFromCodeShouldReturnLanguage()
    {
        //Arrange.
        const string languageCode = "ru";

        //Act.
        var act = () => LanguageDictionary.FromCode(languageCode);

        //Assert
        act.Should()
           .NotThrow();

        act.Should()
           .NotBeNull();
    }

    [Fact]
    public void LanguageDictionaryFromCodeShouldThrowArgumentExceptionWhenCodeIsInvalid()
    {
        //Arrange.
        const string languageCode = "gr";

        //Act.
        var act = () => LanguageDictionary.FromCode(languageCode);

        //Assert
        act.Should()
           .Throw<ArgumentException>($"Неизвестный код языка: {languageCode}");
    }

    [Fact]
    public void LanguageDictionaryFromCodeShouldThrowArgumentExceptionWhenCodeIsNull()
    {
        //Arrange.
        const string languageCode = null!;

        //Act.
        var act = () => LanguageDictionary.FromCode(languageCode!);

        //Assert
        act.Should()
           .Throw<ArgumentException>("Код языка не может быть пустым.");
    }
}
