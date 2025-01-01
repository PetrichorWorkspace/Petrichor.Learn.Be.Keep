using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Core.Domain.Entities;
using Shared.Core.Driven.Persistence.Repositories;

namespace Keep.Driven.NpgsqlPersistence.Repos;

public abstract class BaseRepo<TEntity>(PersistenceCtx persistenceCtx) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected DbSet<TEntity> DbSet { get; } = persistenceCtx.Set<TEntity>();
    
    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken ct = default)
        => await DbSet.FirstOrDefaultAsync(e => e.Id == id, ct);

    // TODO require pagination
    public Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken ct = default)
        => DbSet.AddAsync(entity, ct);
}