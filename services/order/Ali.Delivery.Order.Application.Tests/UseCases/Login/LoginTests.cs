// Тесты, которые не смог реализовать.

// using Ali.Delivery.Domain.Core.Primitives;
// using Ali.Delivery.Order.Application.Abstractions;
// using Ali.Delivery.Order.Application.Dtos.Enums;
// using Ali.Delivery.Order.Application.UseCases.Login;
// using Ali.Delivery.Order.Domain.Entities;
// using Ali.Delivery.Order.Domain.Entities.Dictionaries;
// using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
// using Ali.Delivery.Order.Domain.ValueObjects.User;
// using Inno.Air.PerformanceManagement.Tests.Shared;
// using Microsoft.Extensions.Options;
// using Moq;
// using Moq.AutoMock;
//
// namespace Ali.Delivery.Order.Application.Tests.UseCases.Login;
// [Trait("Category", "Unit")]
// public class LoginTests
// {
//     // Тесты, которые не смог реализовать.
//     [Fact]
//     public async Task Handle_ValidCredentials_ReturnsJwtToken()
//     {
//         // Arrange
//         var fixture = new Fixture();
//         var mocks = new AutoMocker(MockBehavior.Strict);
//         var id = fixture.Create<SequentialGuid>();
//         var login = fixture.Create<UserLogin>();
//         var password = fixture.Create<UserPassword>();
//         var role = Role.BasicUser;
//         var birthDay = fixture.Create<UserBirthDay>();
//         var firstName = fixture.Create<UserFirstName>();
//         var lastName = fixture.Create<UserLastName>();
//
//         var passportInfo = new PassportInfo(SequentialGuid.Create(),
//                                             PassportType.Internal,
//                                             new PassportInfoPassportNumber("123456789"),
//                                             new PassportInfoRegDate(DateTime.Now),
//                                             new PassportInfoIssuedBy("MVD RF"));
//         var user = new User(id, login, password, role, birthDay, firstName, lastName, passportInfo);
//         var query = new LoginUserQuery(login, password);
//         
//         
//         
//
//         
//         
//         
//         
//         mocks.MockDbSet(u => u.Users, user);
//         mocks.GetMock<IAppDbContext>()
//              .SetupDefaultSaveChangesAsync();
//         
//         var sut = mocks.CreateInstance<LoginQueryHandler>();
//         
//         var result = await sut.Handle(query, CancellationToken.None);
//         
//         mocks.Verify();
//         
//         result.Should().NotBeNull();
//         
//         
//
//         
//     }
//     
// }


