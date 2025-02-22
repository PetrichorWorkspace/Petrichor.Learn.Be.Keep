using FluentValidation;
using Keep.Domain.UserAggregate.Entities;
using Keep.Domain.UserAggregate.Exceptions;
using Keep.Domain.UserAggregate.Services;
using Keep.Driven.NpgsqlPersistence;
using Keep.Driving.Common.Security;
using Shared.Core.Domain.Rules.BaseEntityRules;
using Shared.Core.Driving.EndPoints;
using Shared.Core.Driving.EndPoints.Http.Extensions;
using Shared.Core.Driving.Models;

namespace Keep.Driving.Users.ForAuth0.RestApi;

public abstract record RegisterUserFromAuth0 : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost(Router.Users + "/register-user-from-auth0", HandleAsync)
            .WithRequestValidation<Request>()
            .RequireAuthorization(SecurityForAuth0.Policy);
    
    public record Request(string IdentityId, DateTime? CreatedAt);

    public class RequestRule : AbstractValidator<Request>
    {
        public RequestRule()
        {
            RuleFor(u => u.IdentityId)
                .IdRuleValidator();
            
            RuleFor(u => u.CreatedAt)
                .NotNull();
            
            When(u => u.CreatedAt != null, () =>
            {
                RuleFor(u => u.CreatedAt!.Value)
                    .CreatedAtRuleValidator();
            });
        }
    }
    
    public record Response(string Id)
    {
        public static Response ToResponse(User user) => new (user.Id);
    }
    
    static async Task<IResult> HandleAsync(
        Request req, 
        IUserService userService,
        IPersistenceCtx persistenceCtx,
        CancellationToken ct = default)
    {
        try
        {
            var newUser = await userService.CreateUserAsync(req.IdentityId, req.CreatedAt!.Value, ct);
            await persistenceCtx.SaveChangesAsync(ct);
            
            return TypedResults.Created(newUser.Id, new SuccessResponse<Response>
            {
                Message = "user registered successfully",
                Data = Response.ToResponse(newUser)
            });
        }
        catch (DuplicatedUserIdentityIdExc e)
        {
            return TypedResults.BadRequest(ErrorResponse.ToErrorResponse(nameof(req.IdentityId), e));
        }
    }
}