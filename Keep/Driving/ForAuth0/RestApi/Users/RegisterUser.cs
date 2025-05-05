using FluentValidation;
using Keep.Domain.UserAggregate.Entities;
using Keep.Domain.UserAggregate.Exceptions;
using Keep.Domain.UserAggregate.Services;
using Keep.Driven.NpgsqlPersistence;
using Shared.Core.Domain.Rules.BaseEntityRules;
using Shared.Core.Driving.EndPoints;
using Shared.Core.Driving.Models;
using Shared.Core.Driving.RequestValidation.Http;

namespace Keep.Driving.ForAuth0.RestApi.Users;

public abstract record RegisterUser : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost(Router.Users, HandleAsync)
            .WithRequestValidation<RegisterUserRequest>()
            .RequireAuthorization(SecurityForAuth0.Policy);
    
    public record RegisterUserRequest(string IdentityId, DateTime? CreatedAt);

    public class RegisterUserRule : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRule()
        {
            RuleFor(req => req.IdentityId)
                .IdRuleValidator();
            
            RuleFor(req => req.CreatedAt)
                .NotNull();
            
            When(req => req.CreatedAt is not null, () =>
            {
                RuleFor(req => req.CreatedAt!.Value)
                    .CreatedAtRuleValidator();
            });
        }
    }
    
    public record RegisterUserResponse(string Id)
    {
        public static RegisterUserResponse ToResponse(User user) => new (user.Id);
    }
    
    static async Task<IResult> HandleAsync(
        RegisterUserRequest req, 
        IUserService userService,
        IPersistenceCtx persistenceCtx,
        CancellationToken ct = default)
    {
        try
        {
            var newUser = await userService.CreateUserAsync(req.IdentityId, req.CreatedAt!.Value, ct);
            await persistenceCtx.SaveChangesAsync(ct);
            
            return TypedResults.Created(newUser.Id, new SuccessResponse<RegisterUserResponse>
            {
                Message = "User registered successfully",
                Data = RegisterUserResponse.ToResponse(newUser)
            });
        }
        catch (DuplicatedUserIdentityIdExc e)
        {
            return TypedResults.BadRequest(ErrorResponse.ToErrorResponse(nameof(req.IdentityId), e));
        }
    }
}