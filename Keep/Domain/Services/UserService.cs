using Keep.Domain.Entities;
using Keep.Domain.Exceptions;
using Keep.Driven.NpgsqlPersistence;

namespace Keep.Domain.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(string identityId, DateTime createdAt, CancellationToken ct = default);
}

public record UserService : IUserService
{
    private readonly IPersistenceCtx _persistenceCtx;

    public UserService(IPersistenceCtx persistenceCtx)
    {
        _persistenceCtx = persistenceCtx;
    }
    
    public async Task<User> CreateUserAsync(string identityId, DateTime createdAt, CancellationToken ct = default)
    {
        var userByIdentityId = await _persistenceCtx.UserRepo.GetByIdentityIdAsync(identityId, ct);
        if (userByIdentityId != null)
            throw new DuplicatedUserIdentityIdExc(nameof(identityId));

        var newUser = new User
        {
            IdentityId = identityId,
            CreatedAt = createdAt
        };
        
        await _persistenceCtx.UserRepo.AddAsync(newUser, ct);

        return newUser;
    }
}