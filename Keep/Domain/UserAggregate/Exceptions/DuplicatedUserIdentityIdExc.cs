using Shared.Core.Domain.Exceptions;

namespace Keep.Domain.UserAggregate.Exceptions;

public class DuplicatedUserIdentityIdExc(string targetPropertyName) 
    : Exception("already in used"), IExcHasErrorCode
{
    public string Code => nameof(DuplicatedUserIdentityIdExc);
    public string? TargetPropertyName { get; init; } = targetPropertyName;
}