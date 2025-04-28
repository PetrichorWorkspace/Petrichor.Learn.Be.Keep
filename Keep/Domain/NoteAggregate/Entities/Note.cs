using Keep.Domain.NoteAggregate.Rules;
using Keep.Domain.UserAggregate.Entities;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Rules.BaseEntityRules;

namespace Keep.Domain.NoteAggregate.Entities;

public class Note : BaseEntity
{
    public required string UserId { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }

    #region Data Query Purpose Only

    public User? User { get; init; }

    #endregion
}

public class NoteRule : BaseEntityRule<Note>
{
    public NoteRule()
    {
        RuleFor(n => n.UserId)
            .IdRuleValidator();
        
        RuleFor(n => n.Title)
            .NoteTitleRuleValidator();
        
        RuleFor(n => n.Content)
            .NoteContentRuleValidator();
    }
}