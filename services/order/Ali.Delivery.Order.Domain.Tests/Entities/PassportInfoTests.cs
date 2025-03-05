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
    public void UpdatePassportInfoShouldSucceedWhenAllValidArgumentsPassed()
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
    }

    [Fact]
    public void UpdatePassportInfoShouldThrowArgumentNullExceptionWhenPassportTypeIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        PassportType passportType1 = null!;
        var passportNumber1 = new PassportInfoPassportNumber("1234325678");
        var regDate1 = fixture.Create<PassportInfoRegDate>();
        var issuedBy1 = fixture.Create<PassportInfoIssuedBy>();

        var passportInfo = new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Act.

        var act = () => passportInfo.UpdatePassport(passportNumber1, regDate1, issuedBy1, passportType1);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(passportType1));
    }

    [Fact]
    public void UpdatePassportInfoShouldThrowArgumentNullExceptionWhenPassportNumberIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        var passportType1 = PassportType.International;
        PassportInfoPassportNumber passportNumber1 = null!;
        var regDate1 = fixture.Create<PassportInfoRegDate>();
        var issuedBy1 = fixture.Create<PassportInfoIssuedBy>();

        var passportInfo = new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Act.

        var act = () => passportInfo.UpdatePassport(passportNumber1, regDate1, issuedBy1, passportType1);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(passportNumber1));
    }

    [Fact]
    public void UpdatePassportInfoShouldThrowArgumentNullExceptionWhenRegDateIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        var passportType1 = PassportType.International;
        var passportNumber1 = new PassportInfoPassportNumber("1234235678");
        PassportInfoRegDate regDate1 = null!;
        var issuedBy1 = fixture.Create<PassportInfoIssuedBy>();

        var passportInfo = new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Act.

        var act = () => passportInfo.UpdatePassport(passportNumber1, regDate1, issuedBy1, passportType1);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(regDate1));
    }

    [Fact]
    public void UpdatePassportInfoShouldThrowArgumentNullExceptionWhenIssuedByIsNull()
    {
        // Arrange.
        var fixture = new Fixture();

        var id = fixture.Create<SequentialGuid>();
        var passportType = PassportType.Internal;
        var passportNumber = new PassportInfoPassportNumber("12345678");
        var regDate = fixture.Create<PassportInfoRegDate>();
        var issuedBy = fixture.Create<PassportInfoIssuedBy>();

        var passportType1 = PassportType.International;
        var passportNumber1 = new PassportInfoPassportNumber("12345628");
        var regDate1 = fixture.Create<PassportInfoRegDate>();
        PassportInfoIssuedBy issuedBy1 = null!;

        var passportInfo = new PassportInfo(id, passportType, passportNumber, regDate, issuedBy);

        // Act.

        var act = () => passportInfo.UpdatePassport(passportNumber1, regDate1, issuedBy1, passportType1);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>(nameof(issuedBy1));
    }
}
