using System.Security.Claims;
using FluentValidation;
using Keep.Domain.NoteAggregate.Entities;
using Keep.Domain.NoteAggregate.Rules;
using Keep.Domain.NoteAggregate.Services;
using Keep.Domain.UserAggregate.Exceptions;
using Keep.Driven.NpgsqlPersistence;
using Shared.Core.Driven.Security;
using Shared.Core.Driving.EndPoints;
using Shared.Core.Driving.Models;
using Shared.Core.Driving.RequestValidation.Http;

namespace Keep.Driving.ForKeepFe.RestApi.Notes;

public abstract record CreateNote : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost(Router.Notes, HandleAsync)
            .WithRequestValidation<CreateNoteRequest>()
            .RequireAuthorization(SecurityForKeepFe.Policy);
    
    public record CreateNoteRequest(string Title, string Content);
    
    public class CreateNoteRequestRule : AbstractValidator<CreateNoteRequest>
    {
        public CreateNoteRequestRule()
        {
            RuleFor(req => req.Title)
                .NoteTitleRuleValidator();
            
            RuleFor(req => req.Content)
                .NoteContentRuleValidator();
        }
    }
    
    public record CreateNoteResponse(string Id)
    {
        public static CreateNoteResponse ToResponse(Note note) => new (note.Id);
    }

    static async Task<IResult> HandleAsync(
        CreateNoteRequest req, 
        ClaimsPrincipal claims,
        INoteService noteService, 
        IPersistenceCtx persistenceCtx, 
        CancellationToken ct = default)
    {
        try
        {
            var userId = claims.GetUserId()!;
            
            var note = await noteService.CreateNoteAsync(userId, req.Title, req.Content,  ct);
            await persistenceCtx.SaveChangesAsync(ct);
            
            return TypedResults.Created(note.Id, new SuccessResponse<CreateNoteResponse>
            {
                Message = "Note created successfully",
                Data = CreateNoteResponse.ToResponse(note)
            });
        }
        catch (UserIdDoesNotExistExc)
        {
            return TypedResults.Unauthorized();
        }
    }
}