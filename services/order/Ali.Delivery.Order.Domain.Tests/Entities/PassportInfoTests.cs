using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

namespace Ali.Delivery.Order.Domain.Tests.Entities;

[Trait("Category", "Unit")]
public class PassportInfoTests
{
    [Fact]
    public void CreatePassportInfoShouldSucceedWhenAllValidArgumentsPassed()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        // Act.

        var act = () => new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Assert.
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void CreatePassportInfoShouldThrowArgumentNullExceptionWhenIssuedByIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        var regDate = fixture.Create<PassportInfoRegDate>();
        PassportInfoIssuedBy issuedBy = null!;

        // Act.

        var act = () => new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(issuedBy));
    }

    [Fact]
    public void CreatePassportInfoShouldThrowArgumentNullExceptionWhenPassportNumberIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        PassportInfoPassportNumber passportNumber = null!;
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        // Act.

        var act = () => new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(passportNumber));
    }

    [Fact]
    public void CreatePassportInfoShouldThrowArgumentNullExceptionWhenPassportTypeIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        PassportType passportType = null!;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        // Act.

        var act = () => new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(passportType));
    }

    [Fact]
    public void CreatePassportInfoShouldThrowArgumentNullExceptionWhenRegDateIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        PassportInfoRegDate regDate = null!;
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        // Act.

        var act = () => new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(regDate));
    }

    [Fact]
    public void ProtectedConstructorShouldInitializePropertiesWithDefaultValues()
    {
        // Arrange
        var type = typeof(PassportInfo);
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!;

        // Act
        var passportInfo = (PassportInfo)constructor.Invoke(null);

        // Assert
        passportInfo.Should()
                    .NotBeNull();

        passportInfo.Id.Should()
                    .Be(SequentialGuid.Empty);

        passportInfo.PassportType.Should()
                    .BeNull();

        passportInfo.PassportNumber.Should()
                    .BeNull();

        passportInfo.RegDate.Should()
                    .BeNull();

        passportInfo.IssuedBy.Should()
                    .BeNull();
    }

    [Fact]
    public void UpdatePassportInfoShouldSucceedWhenUpdatePassportInfo()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        var passportType1 = PassportType.International;
        var passportNumber1 = new PassportInfoPassportNumber("1234523678");
        var regDate1 = fixture.Create<PassportInfoRegDate>();
        var issuedBy1 = fixture.Create<PassportInfoIssuedBy>();

        var passportInfo = new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Act.

        var act = () => passportInfo.UpdatePassport(passportNumber1, regDate1, issuedBy1, passportType1);

        // Assert.
        act.Should()
           .NotThrow();

        passportInfo.PassportType.Should()
                    .Be(passportType1);

        passportInfo.PassportNumber.Should()
                    .Be(passportNumber1);

        passportInfo.RegDate.Should()
                    .Be(regDate1);

        passportInfo.IssuedBy.Should()
                    .Be(issuedBy1);
    }
}
