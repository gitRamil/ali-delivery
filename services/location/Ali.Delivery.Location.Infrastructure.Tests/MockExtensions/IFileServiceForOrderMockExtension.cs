// using System.Net;
// using Ali.Delivery.Location.Application.Abstractions;
// using Ali.Delivery.Location.Application.Models;
// using Moq;
// using Refit;
//
// namespace Ali.Delivery.Location.Infrastructure.Tests.MockExtensions;
//
// public static class IFileServiceForOrderMockExtension
// {
//      public static void SetupLoginSuccess(this Mock<IFileServiceForOrder> mock, string token = "validToken")
//     {
//         mock.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>()))
//             .ReturnsAsync(token);
//     }
//
//     public static void SetupLoginReturnsNull(this Mock<IFileServiceForOrder> mock)
//     {
//         mock.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>()))
//             .ReturnsAsync((string?)null);
//     }
//
//     public static void SetupLoginReturnsEmptyString(this Mock<IFileServiceForOrder> mock)
//     {
//         mock.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>()))
//             .ReturnsAsync(string.Empty);
//     }
//
//     public static void SetupLoginReturnsWhitespace(this Mock<IFileServiceForOrder> mock)
//     {
//         mock.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>()))
//             .ReturnsAsync("   ");
//     }
//
//     public static void SetupGetCurrentUserSuccess(this Mock<IFileServiceForOrder> mock, Guid userId, string token = "validToken")
//     {
//         mock.Setup(s => s.GetCurrentUserAsync($"Bearer {token}"))
//             .ReturnsAsync(new UserInfo(userId, "someLogin"));
//     }
//
//     public static void SetupGetCurrentUserSuccess(this Mock<IFileServiceForOrder> mock, UserInfo userInfo, string token = "validToken")
//     {
//         mock.Setup(s => s.GetCurrentUserAsync($"Bearer {token}"))
//             .ReturnsAsync(userInfo);
//     }
//
//     public static void SetupGetCurrentUserReturnsNull(this Mock<IFileServiceForOrder> mock, string token = "validToken")
//     {
//         mock.Setup(s => s.GetCurrentUserAsync($"Bearer {token}"))
//             .ReturnsAsync((UserInfo?)null);
//     }
//
//     public static void SetupLoginThrowsApiUnauthorized(this Mock<IFileServiceForOrder> mock)
//     {
//         var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
//         var apiException = ApiException.Create(
//             new HttpRequestMessage(HttpMethod.Post, "http://test.com/login"),
//             HttpMethod.Post,
//             response,
//             new RefitSettings()).Result;
//             
//         mock.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>()))
//             .ThrowsAsync(apiException);
//     }
//
//     public static void SetupGetCurrentUserThrowsApiUnauthorized(this Mock<IFileServiceForOrder> mock, string token = "validToken")
//     {
//         var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
//         var apiException = ApiException.Create(
//             new HttpRequestMessage(HttpMethod.Get, "http://test.com/user"),
//             HttpMethod.Get,
//             response,
//             new RefitSettings()).Result;
//             
//         mock.Setup(s => s.GetCurrentUserAsync($"Bearer {token}"))
//             .ThrowsAsync(apiException);
//     }
//
//     public static void SetupLoginThrowsGenericException(this Mock<IFileServiceForOrder> mock, string message = "Some error")
//     {
//         mock.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>()))
//             .ThrowsAsync(new Exception(message));
//     }
//
//     public static void SetupGetCurrentUserThrowsGenericException(this Mock<IFileServiceForOrder> mock, string token = "validToken", string message = "Some error")
//     {
//         mock.Setup(s => s.GetCurrentUserAsync($"Bearer {token}"))
//             .ThrowsAsync(new Exception(message));
//     }
//     
//     public static void SetupFullSuccessFlow(this Mock<IFileServiceForOrder> mock, Guid userId, string token = "validToken", string login = "testUser")
//     {
//         mock.SetupLoginSuccess(token);
//         mock.SetupGetCurrentUserSuccess(new UserInfo(userId, login), token);
//     }
// }
