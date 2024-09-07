using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;

namespace Domain.Core.Tests;

[Trait("Category", "Unit")]
public class EntityTests
{
    [Fact]
    public void EqualsMethodMustBeReflective()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<User>();

        // Act.
        // ReSharper disable once EqualExpressionComparison
        var result = x.Equals(x);

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void EqualsMethodMustBeSymmetrical()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<long>();
        var x = new User(value);
        var y = new User(value);

        // Act.
        var result1 = x.Equals(y);
        var result2 = y.Equals(x);

        // Assert.
        result1.Should()
               .Be(result2);
    }

    [Fact]
    public void EqualsMethodMustBeTransitive()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<long>();
        var x = new User(value);
        var y = new User(value);
        var z = new User(value);

        // Act.
        var result1 = x.Equals(y);
        var result2 = y.Equals(z);
        var result3 = x.Equals(z);

        // Assert.
        result1.Should()
               .Be(result2)
               .And.Be(result3);
    }

    [Theory]
    [AutoData]
    public void EqualsMethodMustReturnFalseIfArgsIsNull(User first)
    {
        // Arrange.
        User second = null!;

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void EqualsMethodMustReturnFalseIfEntitiesHasDifferentId()
    {
        // Arrange.
        var fixture = new Fixture();

        var first = fixture.Build<User>()
                           .OmitAutoProperties()
                           .Create();

        var second = fixture.Build<User>()
                            .OmitAutoProperties()
                            .Create();

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void EqualsMethodMustReturnFalseIfIdPropertyHasDefaultValue()
    {
        // Arrange.
        var id = 0;
        var first = new User(id);
        var second = new User(id);

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void EqualsMethodMustReturnFalseWhenCompareTwoTheEntityWithDifferentIdAndTheSameContent()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<string>();
        var first = fixture.Create<User>();
        var second = fixture.Create<User>();

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void EqualsMethodMustReturnTrueWhenCompareTwoTheEntityWithSameIdAndTheDifferentContent()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<long>();
        var first = fixture.Create<User>();
        var second = fixture.Create<User>();

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Theory]
    [AutoData]
    public void EqualsOperatorMethodMustReturnFalseIfFirstArgsIsNull(User second)
    {
        // Arrange.
        User first = null!;

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsOperatorMethodMustReturnFalseIfSecondArgsIsNull(User first)
    {
        // Arrange.
        User second = null!;

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void EqualsOperatorMethodMustReturnTrueIfBothArgsIsNull()
    {
        // Arrange.
        User first = null!;
        User second = null!;

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void EqualsOperatorMustBeReflective()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<User>();

        // Act.
        // ReSharper disable once EqualExpressionComparison
        var result = x == x;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void EqualsOperatorMustBeSymmetrical()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<long>();
        var x = new User(value);
        var y = new User(value);

        // Act.
        var result1 = x == y;
        var result2 = y == x;

        // Assert.
        result1.Should()
               .Be(result2);
    }

    [Fact]
    public void EqualsOperatorMustBeTransitive()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<long>();
        var x = new User(value);
        var y = new User(value);
        var z = new User(value);

        // Act.
        var result1 = x == y;
        var result2 = y == z;
        var result3 = x == z;

        // Assert.
        result1.Should()
               .Be(result2)
               .And.Be(result3);
    }

    [Fact]
    public void EqualsOperatorMustReturnFalseIfEntitiesHasDifferentId()
    {
        // Arrange.
        var fixture = new Fixture();

        var first = fixture.Build<User>()
                           .OmitAutoProperties()
                           .Create();

        var second = fixture.Build<User>()
                            .OmitAutoProperties()
                            .Create();

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void EqualsOperatorMustReturnFalseIfIdPropertyHasDefaultValue()
    {
        // Arrange.
        var id = 0;
        var first = new User(id);
        var second = new User(id);

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void EqualsOperatorMustReturnFalseWhenCompareTwoTheEntityWithDifferentIdAndTheSameContent()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<string>();
        var first = fixture.Create<User>();
        var second = fixture.Create<User>();

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void EqualsOperatorMustReturnTrueWhenCompareTwoTheEntityWithSameIdAndTheDifferentContent()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<long>();
        var first = fixture.Create<User>();
        var second = fixture.Create<User>();

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void NotEqualsOperatorMustReturnFalseWhenCompareTwoTheEntityWithSameIdAndTheDifferentContent()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<long>();
        var first = fixture.Create<User>();
        var second = fixture.Create<User>();

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void NotEqualsOperatorMustBeReflective()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<User>();

        // Act.
        // ReSharper disable once EqualExpressionComparison
        var result = x != x;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void NotEqualsOperatorMustBeSymmetrical()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<User>();
        var y = fixture.Create<User>();

        // Act.
        var result1 = x != y;
        var result2 = y != x;

        // Assert.
        result1.Should()
               .Be(result2);
    }

    [Fact]
    public void NotEqualsOperatorMustBeTransitive()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<User>();
        var y = fixture.Create<User>();
        var z = fixture.Create<User>();

        // Act.
        var result1 = x != y;
        var result2 = y != z;
        var result3 = x != z;

        // Assert.
        result1.Should()
               .Be(result2)
               .And.Be(result3);
    }

    [Fact]
    public void NotEqualsOperatorMustReturnFalseWhenCompareTwoTheEntityWithDifferentIdAndTheSameContent()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<string>();
        var first = fixture.Create<User>();
        var second = fixture.Create<User>();

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void NotEqualsOperatorMustReturnTrueIfEntitiesHasDifferentId()
    {
        // Arrange.
        var fixture = new Fixture();

        var first = fixture.Build<User>()
                           .OmitAutoProperties()
                           .Create();

        var second = fixture.Build<User>()
                            .OmitAutoProperties()
                            .Create();

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void NotEqualsOperatorMustReturnTrueIfIdPropertyHasDefaultValue()
    {
        // Arrange.
        var id = 0;
        var first = new User(id);
        var second = new User(id);

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void TwoTheSameEntityMustHaveTheSameHashCode()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<long>();
        var first = new User(value);
        var second = new User(value);

        // Act.
        var result1 = first.GetHashCode();
        var result2 = second.GetHashCode();

        // Assert.
        result1.Should()
               .Be(result2);
    }

    public class User : Entity<long>
    {
        public User(long id)
            : base(id)
        {
        }

        public string Value { get; set; } = default!;
    }
}
