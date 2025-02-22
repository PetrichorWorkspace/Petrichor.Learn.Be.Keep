using Keep.Domain.UserAggregate.Entities;

namespace UnitTest.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Validate_AllFieldsValid_ReturnsTrue()
    {
        const string identityIdToTest = "123456";
        var userToTest = new User { IdentityId = identityIdToTest };

        var userValidator = new UserRule();
        
        var actualIsValid = userValidator.Validate(userToTest).IsValid;
        Assert.True(actualIsValid);
    }

    [Fact]
    public void Validate_AllFieldsInvalid_ReturnsFalse()
    {
        const string identityIdToTest = "123";
        var userToTest = new User { IdentityId = identityIdToTest };

        var userValidator = new UserRule();
        
        var actualValidationResult = userValidator.Validate(userToTest);
        
        var actualIsValid = actualValidationResult.IsValid;
        Assert.False(actualIsValid);
        
        const int expectedNumberOfInvalidFields = 1;
        var actualNumberOfInvalidFields = actualValidationResult.Errors.GroupBy(failure => failure.PropertyName).Count();
        Assert.Equal(expectedNumberOfInvalidFields, actualNumberOfInvalidFields);
    }
}