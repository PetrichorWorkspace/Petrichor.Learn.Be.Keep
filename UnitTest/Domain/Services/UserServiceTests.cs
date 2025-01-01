using Keep.Domain.Entities;
using Keep.Domain.Exceptions;
using Keep.Domain.Services;
using Keep.Driven.NpgsqlPersistence;
using Moq;

namespace UnitTest.Domain.Services;

public class UserServiceTests
{
    private readonly Mock<IPersistenceCtx> _persistenceCtxMock = new();
    
    [Fact]
    public async Task CreateUserAsync_ValidInputs_CreatesSuccessfully()
    {
        _persistenceCtxMock.Setup(p => p.UserRepo.GetByIdentityIdAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(null as User);

        var userServiceToTest = new UserService(_persistenceCtxMock.Object);
        
        var actualUser = await userServiceToTest.CreateUserAsync("123456", DateTime.Now.AddHours(-1));
        Assert.NotNull(actualUser);
    }
    
    [Fact]
    public async Task CreateUserAsync_DuplicatedIdentityId_ThrowsException()
    {
        var identityId = "123456";
        _persistenceCtxMock.Setup(p => p.UserRepo.GetByIdentityIdAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(new User{ IdentityId = identityId });
        
        var userServiceToTest = new UserService(_persistenceCtxMock.Object);
        
        await Assert.ThrowsAsync<DuplicatedUserIdentityIdExc>(
            async () => await userServiceToTest.CreateUserAsync(identityId, DateTime.Now.AddHours(-1)));
    }
}