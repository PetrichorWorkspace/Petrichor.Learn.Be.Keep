using FluentValidation;

namespace Keep.Domain.NoteAggregate.Rules;

public static class NoteContentRule
{
    public static IRuleBuilderOptions<T, string> NoteContentRuleValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty();
    }
}