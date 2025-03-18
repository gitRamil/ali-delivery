using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.UseCases.GetAllUsers;
using Ali.Delivery.Order.Domain.Entities;
using Inno.Air.PerformanceManagement.Tests.Shared;
using Moq;
using Moq.AutoMock;

namespace Ali.Delivery.Order.Application.Tests.UseCases.GetAllUsers;

[Trait("Category", "Unit")]
public class GetAllUsersQueryHandlerTests
{
    [Fact]
    public void ConstructorShouldFailWhenNullArgumentAppDbContextPassed()
    {
        // Arrange.
        IAppDbContext context = null!;

        // Act.
        var act = () => new GetAllUsersQueryHandler(context);

        // Assert.
        act.Should()
           .Throw<ArgumentNullException>()
           .WithParameterName(nameof(context));
    }

    [Fact]
    public async Task HandlerShouldGetAllUsers()
    {
        // Arrange.
        var fixture = new AppFixture();
        var mocks = new AutoMocker(MockBehavior.Strict);

        var users = new[]
        {
            fixture.Create<User>(),
            fixture.Create<User>(),
            fixture.Create<User>(),
            fixture.Create<User>(),
            fixture.Create<User>(),
            fixture.Create<User>()
        };

        mocks.MockDbSet(u => u.Users, users);
        var sut = mocks.CreateInstance<GetAllUsersQueryHandler>();

        // Act.
        var result = await sut.Handle(new GetAllUsersQuery(), CancellationToken.None);

        // Assert.
        mocks.Verify();

        result.Should()
              .NotBeNull()
              .And.HaveCount(6);
    }
}
