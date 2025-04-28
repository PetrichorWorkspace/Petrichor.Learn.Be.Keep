using FluentValidation;

namespace Keep.Domain.NoteAggregate.Rules;

public static class NoteTitleRule
{
    public const int MaxLength = 1000;
    
    public static IRuleBuilderOptions<T, string> NoteTitleRuleValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .MaximumLength(MaxLength);
    }
}