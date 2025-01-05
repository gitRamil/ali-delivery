namespace Ali.Delivery.Order.Application.Tests;

[Trait("Category", "Unit")]
public class FakeTests
{
    [Fact]
    public void FakeTest()
    {
        // Arrange.
        var value = 2;

        // Act.
        var result = value + 1;

        // Assert.
        result.Should()
              .Be(2);
    }
}
