using Shared.Core.Domain.Exceptions;

namespace Keep.Domain.UserAggregate.Exceptions;

public class UserIdDoesNotExistExc(string targetPropertyName) 
    : Exception("does not exist"), IExcHasErrorCode
{
    public string Code => nameof(UserIdDoesNotExistExc);
    public string? TargetPropertyName { get; init; } = targetPropertyName;
}