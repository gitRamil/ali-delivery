using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;

namespace Ali.Delivery.Domain.Core.Tests;

[Trait("Category", "Unit")]
public class ValueObjectTests
{
    [Fact]
    public void DerivedValueObjectsAreEqualWhenComparedUsingNotEqualsOperator()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<string>();
        var name = new Name(value);
        var userName = new DerivedName(value);

        // Act.
        var result1 = name != userName;

        // Assert.
        result1.Should()
               .BeTrue();
    }

    [Fact]
    public void DerivedValueObjectsAreNotEqualWhenComparedUsingEqualsMethod()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<string>();
        var name = new Name(value);
        var userName = new DerivedName(value);

        // Act.
        var result1 = name.Equals(userName);

        // Assert.
        result1.Should()
               .BeFalse();
    }

    [Fact]
    public void DerivedValueObjectsAreNotEqualWhenComparedUsingEqualsOperator()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<string>();
        var name = new Name(value);
        var userName = new DerivedName(value);

        // Act.
        var result1 = name == userName;

        // Assert.
        result1.Should()
               .BeFalse();
    }

    [Fact]
    public void EqualsMethodMustBeReflective()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<Name>();

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
        var value = fixture.Create<string>();
        var x = new Name(value);
        var y = new Name(value);

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
        var value = fixture.Create<string>();
        var x = new Name(value);
        var y = new Name(value);
        var z = new Name(value);

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
    public void EqualsMethodMustReturnFalseIfArgsHasDifferentContent(Name first, Name second)
    {
        // Arrange.

        // Act.
        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsMethodMustReturnFalseIfArgsIsDifferentType(Name first, FullName second)
    {
        // Arrange.

        // Act.
        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsMethodMustReturnFalseIfArgsIsNull(Name first)
    {
        // Arrange.
        Name second = null!;

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsOperatorMethodMustReturnFalseIfFirstArgsIsNull(Name second)
    {
        // Arrange.
        Name first = null!;

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsOperatorMethodMustReturnFalseIfSecondArgsIsNull(Name first)
    {
        // Arrange.
        Name second = null!;

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
        Name first = null!;
        Name second = null!;

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
        var x = fixture.Create<Name>();

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
        var value = fixture.Create<string>();
        var x = new Name(value);
        var y = new Name(value);

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
        var value = fixture.Create<string>();
        var x = new Name(value);
        var y = new Name(value);
        var z = new Name(value);

        // Act.
        var result1 = x == y;
        var result2 = y == z;
        var result3 = x == z;

        // Assert.
        result1.Should()
               .Be(result2)
               .And.Be(result3);
    }

    [Theory]
    [AutoData]
    public void EqualsOperatorMustReturnFalseIfArgsHasDifferentContent(Name first, Name second)
    {
        // Arrange.

        // Act.
        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsOperatorMustReturnFalseIfArgsIsDifferentType(Name first, FullName second)
    {
        // Arrange.

        // Act.
        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void NotEqualsOperatorMethodMustReturnFalseIfBothArgsIsNull()
    {
        // Arrange.
        Name first = null!;
        Name second = null!;

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void NotEqualsOperatorMethodMustReturnTrueIfFirstArgsIsNull(Name second)
    {
        // Arrange.
        Name first = null!;

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Theory]
    [AutoData]
    public void NotEqualsOperatorMethodMustReturnTrueIfSecondArgsIsNull(Name first)
    {
        // Arrange.
        Name second = null!;

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void NotEqualsOperatorMustBeReflective()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<Name>();

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
        var x = fixture.Create<Name>();
        var y = fixture.Create<Name>();

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
        var x = fixture.Create<Name>();
        var y = fixture.Create<Name>();
        var z = fixture.Create<Name>();

        // Act.
        var result1 = x != y;
        var result2 = y != z;
        var result3 = x != z;

        // Assert.
        result1.Should()
               .Be(result2)
               .And.Be(result3);
    }

    [Theory]
    [AutoData]
    public void NotEqualsOperatorMustReturnTrueIfArgsHasDifferentContent(Name first, Name second)
    {
        // Arrange.

        // Act.
        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void TwoTheSameValueObjectMustHaveTheSameHashCode()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<string>();
        var first = new Name(value);
        var second = new Name(value);

        // Act.
        var result1 = first.GetHashCode();
        var result2 = second.GetHashCode();

        // Assert.
        result1.Should()
               .Be(result2);
    }

    [Fact]
    public void TwoValueObjectOfTheSameContentAreEqualWhenComparedUsingEqualsMethod()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<string>();
        var first = new Name(value);
        var second = new Name(value);

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void TwoValueObjectOfTheSameContentAreEqualWhenComparedUsingEqualsOperator()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<string>();
        var first = new Name(value);
        var second = new Name(value);

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void TwoValueObjectOfTheSameContentAreEqualWhenComparedUsingNotEqualsOperator()
    {
        // Arrange.
        var fixture = new Fixture();
        var value = fixture.Create<string>();
        var first = new Name(value);
        var second = new Name(value);

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    public class FullName : ValueObject
    {
        private readonly string _name;

        public FullName(string name) => _name = name;

        /// <summary>
        /// Возвращает набор компонентов, участвующий в сравнении.
        /// </summary>
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return _name;
        }
    }

    public class Name : ValueObject
    {
        private readonly string _name;

        public Name(string name) => _name = name;

        /// <summary>
        /// Возвращает набор компонентов, участвующий в сравнении.
        /// </summary>
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return _name;
        }
    }

    private class DerivedName : Name
    {
        public DerivedName(string name)
            : base(name)
        {
        }
    }
}
