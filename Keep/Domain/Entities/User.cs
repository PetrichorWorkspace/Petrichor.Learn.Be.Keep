using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Rules.BaseEntityRules;

namespace Keep.Domain.Entities;

public class User : BaseEntity
{
    public required string IdentityId { get; init; }
}

public class UserRule : BaseEntityRule<User>
{
    public UserRule()
    {
        RuleFor(u => u.IdentityId)
            .IdRuleValidator();
    }
}