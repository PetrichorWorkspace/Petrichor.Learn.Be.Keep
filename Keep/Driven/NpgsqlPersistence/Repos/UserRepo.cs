using Keep.Domain.UserAggregate.Entities;
using Shared.Core.Driven.Persistence.Repositories;

namespace Keep.Driven.NpgsqlPersistence.Repos;

public interface IUserRepo : IBaseRepository<User>
{
}

public class UserRepo(PersistenceCtx persistenceCtx) : BaseRepo<User>(persistenceCtx), IUserRepo
{
}