using Keep.Domain.NoteAggregate.Entities;
using Shared.Core.Driven.Persistence.Repositories;

namespace Keep.Driven.NpgsqlPersistence.Repos;

public interface INoteRepo : IBaseRepository<Note>
{
    
}

public class NoteRepo(PersistenceCtx persistenceCtx) : BaseRepo<Note>(persistenceCtx), INoteRepo
{
    
}