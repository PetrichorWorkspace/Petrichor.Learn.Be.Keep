using FluentValidation;
using Keep.Domain.NoteAggregate.Entities;
using Keep.Domain.NoteAggregate.Rules;
using Keep.Domain.NoteAggregate.Services;
using Keep.Driven.NpgsqlPersistence;
using Shared.Core.Driving.EndPoints;
using Shared.Core.Driving.EndPoints.Http.Extensions;

namespace Keep.Driving.ForKeepFe.RestApi.Notes;

public abstract record CreateNote : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost(Router.Tasks, HandleAsync)
            .WithRequestValidation<Request>();
    
    public record Request(string Title, string Content);
    
    public class RequestRule : AbstractValidator<Request>
    {
        public RequestRule()
        {
            RuleFor(req => req.Title)
                .NoteTitleRuleValidator();
            
            RuleFor(req => req.Content)
                .NoteContentRuleValidator();
        }
    }
    
    public record Response(string Id)
    {
        public static Response ToResponse(Note note) => new (note.Id);
    }

    static async Task<IResult> HandleAsync(
        Request req, 
        INoteService noteService, 
        IPersistenceCtx persistenceCtx, 
        CancellationToken ct = default)
    {
        try
        {
            // var note = await noteService.CreateNoteAsync(req.Title, req.Content,  ct);
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}