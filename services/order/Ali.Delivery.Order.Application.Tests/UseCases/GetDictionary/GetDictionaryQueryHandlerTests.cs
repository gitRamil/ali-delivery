using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.UseCases.GetDictionary;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetDictionary;

[Trait("Category", "Unit")]
public class GetDictionaryQueryHandlerTests
{
    [Fact]
    public async Task HandlerShouldReturnDictionaryValues()
    {
        // Arrange.
        var mocks = new AutoMocker(MockBehavior.Strict);
        var dictionaryCode = DictionaryCode.Role;

        var dictionaryValues = new[]
        {
            Role.SuperUser,
            Role.Courier,
            Role.BasicUser,
            Role.NotAuthUser
        };
        var dictionaryValuesProviderMock = new Mock<IDictionaryValuesProvider>();

        dictionaryValuesProviderMock.Setup(p => p.GetDictionaryValuesAsync(dictionaryCode, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(dictionaryValues)
                                    .Verifiable();

        mocks.Use(dictionaryValuesProviderMock.Object);

        var sut = mocks.CreateInstance<GetDictionaryQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetDictionaryQuery(dictionaryCode), CancellationToken.None);

        // Assert.
        dictionaryValuesProviderMock.Verify();

        result.Should()
              .NotBeNull()
              .And.HaveCount(3);
    }

    [Fact]
    public async Task HandlerShouldThrowInvalidOperationExceptionWhenCodeOrNameIsNull()
    {
        // Тесты, которые не смог реализовать.
    }
}
