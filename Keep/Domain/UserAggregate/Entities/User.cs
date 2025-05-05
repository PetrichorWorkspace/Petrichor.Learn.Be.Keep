using Shared.Core.Domain.Entities;

namespace Keep.Domain.UserAggregate.Entities;

public class User : BaseEntity
{
}

public class UserRule : BaseEntityRule<User>
{
}