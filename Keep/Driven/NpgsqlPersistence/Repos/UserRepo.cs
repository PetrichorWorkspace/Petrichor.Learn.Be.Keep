using Keep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Driven.Persistence.Repositories;

namespace Keep.Driven.NpgsqlPersistence.Repos;

public interface IUserRepo : IBaseRepository<User>
{
    Task<User?> GetByIdentityIdAsync(string identityId, CancellationToken ct = default);
}

public class UserRepo(PersistenceCtx persistenceCtx) : BaseRepo<User>(persistenceCtx), IUserRepo
{
    public Task<User?> GetByIdentityIdAsync(string identityId, CancellationToken ct = default)
        => DbSet.FirstOrDefaultAsync(e => e.IdentityId == identityId, ct);
}