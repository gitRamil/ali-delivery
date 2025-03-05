using System.Linq.Expressions;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Moq.AutoMock;

namespace Inno.Air.PerformanceManagement.Tests.Shared;

public static class AppDbContextMockExtensions
{
    public static void SetupDefaultSaveChangesAsync(this Mock<IAppDbContext> mock) =>
        mock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0)
            .Verifiable();
    public static Mock<DbSet<T>> MockDbSet<T>(this AutoMocker mocks, Expression<Func<IAppDbContext, DbSet<T>>> expression, params T[] items) where T : class
    {
        var dbSetMock = items.BuildMock()
                             .BuildMockDbSet();

        mocks.GetMock<IAppDbContext>()
             .SetupGet(expression)
             .Returns(dbSetMock.Object)
             .Verifiable();
        return dbSetMock;
    }
    
    public static void CurrentUserSet(this AutoMocker mocks, SequentialGuid userId)
    {
        mocks.GetMock<ICurrentUser>()
             .Setup(u => u.Id)
             .Returns(userId)
             .Verifiable();
    }
}
