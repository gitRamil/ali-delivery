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
}
