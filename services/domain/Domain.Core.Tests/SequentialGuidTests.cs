using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Core.Primitives;
using FluentAssertions;

namespace Domain.Core.Tests;

[Trait("Category", "Unit")]
public class SequentialGuidTests
{
    [Fact]
    public void CanExecuteTryFormatSuccessfully()
    {
        // Arrange.
        var sequentialGuid = SequentialGuid.Empty;
        Span<char> output = stackalloc char[36];

        // Act.
        var result = ((ISpanFormattable)sequentialGuid).TryFormat(output, out var charsWritten, "N", null);

        var str = output.Slice(0, charsWritten)
                        .ToString();

        // Assert.
        result.Should()
              .BeTrue();

        str.Should()
           .Be("00000000000000000000000000000000");
    }

    [Fact]
    public void CanExecuteTryParseSequentialGuidFromSpanSuccessfully()
    {
        // Arrange.
        var sequentialGuid = SequentialGuid.Create();

        // Act.
        var tryParseResult = SequentialGuid.TryParse(sequentialGuid.ToString()
                                                                   .AsSpan(),
                                                     null,
                                                     out var result);

        // Assert.
        tryParseResult.Should()
                      .BeTrue();

        sequentialGuid.Should()
                      .Be(result);
    }

    [Fact]
    public void CanExecuteTryParseSequentialGuidFromStringSuccessfully()
    {
        // Arrange.
        var sequentialGuid = SequentialGuid.Create();

        // Act.
        var tryParseResult = SequentialGuid.TryParse(sequentialGuid.ToString(), null, out var result);

        // Assert.
        tryParseResult.Should()
                      .BeTrue();

        sequentialGuid.Should()
                      .Be(result);
    }

    [Fact]
    public void CanImplicitConvertGuidToSequentialGuid()
    {
        // Arrange.
        var x = Guid.Empty;

        // Act.
        SequentialGuid sequentialGuid = x;

        // Assert.
        sequentialGuid.Should()
                      .Be(SequentialGuid.Empty);
    }

    [Fact]
    public void CanImplicitConvertSequentialGuidToGuid()
    {
        // Arrange.
        var x = SequentialGuid.Empty;

        // Act.
        Guid guid = x;

        // Assert.
        guid.Should()
            .Be(Guid.Empty);
    }

    [Fact]
    public void CanParseSequentialGuidFromSpan()
    {
        // Arrange.
        var sequentialGuid = SequentialGuid.Create();

        // Act.
        var result = SequentialGuid.Parse(sequentialGuid.ToString()
                                                        .AsSpan(),
                                          null);

        // Assert.
        result.Should()
              .Be(sequentialGuid);
    }

    [Fact]
    public void CanParseSequentialGuidFromString()
    {
        // Arrange.
        var sequentialGuid = SequentialGuid.Create();

        // Act.
        var result = SequentialGuid.Parse(sequentialGuid.ToString(), null);

        // Assert.
        result.Should()
              .Be(sequentialGuid);
    }

    [Fact]
    public void CompareToMustThrowExceptionIfArgsDoesNotMatchTheTargetType()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<SequentialGuid>();
        var y = fixture.Create<Guid>();

        // Act.
        var exception = Record.Exception(() => x.CompareTo((object)y));

        // Assert.
        exception.Should()
                 .NotBeNull()
                 .And.BeOfType<ArgumentException>()
                 .Subject.Message.Should()
                 .Contain(nameof(SequentialGuid));
    }

    [Theory]
    [AutoData]
    public void CurrentElementMustFollowArgsInSortOrderWhenComparedUsingNotStronglyTypedOverload(SequentialGuid first)
    {
        // Arrange.

        // Act.
        var compareTo = first.CompareTo(null);

        // Assert.
        compareTo.Should()
                 .BeGreaterThan(0);
    }

    [Fact]
    public void EqualsMethodMustBeReflective()
    {
        // Arrange.
        var fixture = new Fixture();
        var x = fixture.Create<SequentialGuid>();

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
        var x = fixture.Create<SequentialGuid>();
        var y = fixture.Create<SequentialGuid>();

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
        var x = fixture.Create<SequentialGuid>();
        var y = x;
        var z = x;

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
    public void EqualsMethodMustReturnFalseIfArgsIsDifferent(SequentialGuid first, SequentialGuid second)
    {
        // Arrange.

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsMethodMustReturnFalseIfArgsIsNull(SequentialGuid first)
    {
        // Arrange.
        SequentialGuid? second = null!;

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsOperatorMethodMustReturnFalseIfFirstArgsIsNull(SequentialGuid second)
    {
        // Arrange.
        SequentialGuid? first = null!;

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void EqualsOperatorMethodMustReturnFalseIfSecondArgsIsNull(SequentialGuid first)
    {
        // Arrange.
        SequentialGuid? second = null!;

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
        ValueObjectTests.Name first = null!;
        ValueObjectTests.Name second = null!;

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
        var x = fixture.Create<SequentialGuid>();

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
        var x = fixture.Create<SequentialGuid>();
        var y = fixture.Create<SequentialGuid>();

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
        var x = fixture.Create<SequentialGuid>();
        var y = x;
        var z = x;

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
    public void EqualsOperatorMustReturnFalseIfArgsIsDifferent(SequentialGuid first, SequentialGuid second)
    {
        // Arrange.

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Fact]
    public void NotEqualsOperatorMethodMustReturnFalseIfBothArgsIsNull()
    {
        // Arrange.
        SequentialGuid? first = null!;
        SequentialGuid? second = null!;

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeFalse();
    }

    [Theory]
    [AutoData]
    public void NotEqualsOperatorMethodMustReturnTrueIfFirstArgsIsNull(SequentialGuid second)
    {
        // Arrange.
        SequentialGuid? first = null!;

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Theory]
    [AutoData]
    public void NotEqualsOperatorMethodMustReturnTrueIfSecondArgsIsNull(SequentialGuid first)
    {
        // Arrange.
        SequentialGuid? second = null!;

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
        var x = fixture.Create<SequentialGuid>();

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
        var x = fixture.Create<SequentialGuid>();
        var y = fixture.Create<SequentialGuid>();

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
        var x = fixture.Create<SequentialGuid>();
        var y = fixture.Create<SequentialGuid>();
        var z = fixture.Create<SequentialGuid>();

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
    public void NotEqualsOperatorMustReturnTrueIfArgsIsDifferent(SequentialGuid first, SequentialGuid second)
    {
        // Arrange.

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Theory]
    [AutoData]
    public void TheFirstElementGeneratedMustPrecedeTheLastElementInTheSortOrderWhenComparedUsingNotStronglyTypedOverload(SequentialGuid first, SequentialGuid second)
    {
        // Arrange.

        // Act.
        var compareTo = second.CompareTo(first);

        // Assert.
        compareTo.Should()
                 .BeGreaterThan(0);
    }

    [Theory]
    [AutoData]
    public void TheFirstElementGeneratedMustPrecedeTheLastElementInTheSortOrderWhenComparedUsingStronglyTypedOverload(SequentialGuid first, SequentialGuid second)
    {
        // Arrange.

        // Act.
        var compareTo = second.CompareTo((object)first);

        // Assert.
        compareTo.Should()
                 .BeGreaterThan(0);
    }

    [Theory]
    [AutoData]
    public void TheLastGeneratedElementMustFollowThePreviousOneInSortOrderWhenComparedUsingNotStronglyTypedOverload(SequentialGuid first, SequentialGuid second)
    {
        // Arrange.

        // Act.
        var compareTo = first.CompareTo((object)second);

        // Assert.
        compareTo.Should()
                 .BeLessThan(0);
    }

    [Theory]
    [AutoData]
    public void TheLastGeneratedElementMustFollowThePreviousOneInSortOrderWhenComparedUsingStronglyTypedOverload(SequentialGuid first, SequentialGuid second)
    {
        // Arrange.

        // Act.
        var compareTo = first.CompareTo(second);

        // Assert.
        compareTo.Should()
                 .BeLessThan(0);
    }

    [Fact]
    public void ToByteArrayMustReturnAByteArrayOf16Elements()
    {
        // Arrange.
        var x = SequentialGuid.Empty;

        // Act.
        var array = x.ToByteArray();

        // Assert.
        array.Should()
             .BeEquivalentTo(new byte[16]);
    }

    [Fact]
    public void ToStringMustReturnStringRepresentation()
    {
        // Arrange.
        var x = SequentialGuid.Empty;

        // Act.
        var str = x.ToString();

        // Assert.
        str.Should()
           .Be("00000000-0000-0000-0000-000000000000");
    }

    [Fact]
    public void ToStringWithFormatParameterAndFormatProviderMustReturnStringRepresentation()
    {
        // Arrange.
        var x = SequentialGuid.Empty;

        // Act.
        var str = x.ToString("N", null);

        // Assert.
        str.Should()
           .Be("00000000000000000000000000000000");
    }

    [Fact]
    public void ToStringWithFormatParameterMustReturnStringRepresentation()
    {
        // Arrange.
        var x = SequentialGuid.Empty;

        // Act.
        var str = x.ToString("N");

        // Assert.
        str.Should()
           .Be("00000000000000000000000000000000");
    }

    [Fact]
    public void TwoTheDifferentSequentialGuidAreNotEqualWhenComparedUsingNotEqualsOperator()
    {
        // Arrange.
        var fixture = new Fixture();
        var first = fixture.Create<SequentialGuid>();
        var second = fixture.Create<SequentialGuid>();

        // Act.
        var result = first != second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void TwoTheSameElementsMustBeInTheSamePositionInTheSortOrderWhenComparedUsingNotStronglyTypedOverload()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<SequentialGuid>();
        var first = fixture.Create<SequentialGuid>();
        var second = fixture.Create<SequentialGuid>();

        // Act.
        var compareTo = first.CompareTo((object)second);

        // Assert.
        compareTo.Should()
                 .Be(0);
    }

    [Fact]
    public void TwoTheSameElementsMustBeInTheSamePositionInTheSortOrderWhenComparedUsingStronglyTypedOverload()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<SequentialGuid>();
        var first = fixture.Create<SequentialGuid>();
        var second = fixture.Create<SequentialGuid>();

        // Act.
        var compareTo = first.CompareTo(second);

        // Assert.
        compareTo.Should()
                 .Be(0);
    }

    [Fact]
    public void TwoTheSameSequentialGuidAreEqualWhenComparedUsingEqualsMethod()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<SequentialGuid>();
        var first = fixture.Create<SequentialGuid>();
        var second = fixture.Create<SequentialGuid>();

        // Act.
        var result = first.Equals(second);

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void TwoTheSameSequentialGuidAreEqualWhenComparedUsingEqualsOperator()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<SequentialGuid>();
        var first = fixture.Create<SequentialGuid>();
        var second = fixture.Create<SequentialGuid>();

        // Act.
        var result = first == second;

        // Assert.
        result.Should()
              .BeTrue();
    }

    [Fact]
    public void TwoTheSameValueObjectMustHaveTheSameHashCode()
    {
        // Arrange.
        var fixture = new Fixture();
        fixture.Freeze<SequentialGuid>();
        var first = fixture.Create<SequentialGuid>();
        var second = fixture.Create<SequentialGuid>();

        // Act.
        var result1 = first.GetHashCode();
        var result2 = second.GetHashCode();

        // Assert.
        result1.Should()
               .Be(result2);
    }
}
