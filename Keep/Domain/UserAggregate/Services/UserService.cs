using FluentValidation;
using Keep.Domain.UserAggregate.Entities;
using Keep.Domain.UserAggregate.Exceptions;
using Keep.Driven.NpgsqlPersistence;

namespace Keep.Domain.UserAggregate.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(string identityId, DateTime createdAt, CancellationToken ct = default);
}

public record UserService : IUserService
{
    private readonly IPersistenceCtx _persistenceCtx;
    private readonly IValidator<User> _validator;

    public UserService(IPersistenceCtx persistenceCtx, IValidator<User> validator)
    {
        _persistenceCtx = persistenceCtx;
        _validator = validator;
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
        
        await _validator.ValidateAndThrowAsync(newUser, ct);
        
        await _persistenceCtx.UserRepo.AddAsync(newUser, ct);

        return newUser;
    }
}