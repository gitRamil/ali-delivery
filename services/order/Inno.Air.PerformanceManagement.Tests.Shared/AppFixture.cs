using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using AutoFixture;

namespace Inno.Air.PerformanceManagement.Tests.Shared;

public class AppFixture : Fixture
{
    public AppFixture()
    {
        var passportInfo = new PassportInfo(this.Create<SequentialGuid>(),
                                            PassportType.Diplomatic,
                                            new PassportInfoPassportNumber("123"),
                                            this.Create<PassportInfoRegDate>(),
                                            this.Create<PassportInfoIssuedBy>());

        Customize<User>(c => c.FromFactory(() => new User(this.Create<SequentialGuid>(),
                                                          this.Create<UserLogin>(),
                                                          this.Create<UserPassword>(),
                                                          this.Create<Role>(),
                                                          this.Create<UserBirthDay>(),
                                                          this.Create<UserFirstName>(),
                                                          this.Create<UserLastName>(),
                                                          passportInfo))
                              .OmitAutoProperties());
    }
}
