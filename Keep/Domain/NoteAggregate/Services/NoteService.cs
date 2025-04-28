using FluentValidation;
using Keep.Domain.NoteAggregate.Entities;
using Keep.Driven.NpgsqlPersistence;

namespace Keep.Domain.NoteAggregate.Services;

public interface INoteService
{
    Task<Note> CreateNoteAsync(string userId, string title, string content, CancellationToken ct = default);
}

public record NoteService : INoteService
{
    private readonly IPersistenceCtx _persistenceCtx;
    private readonly IValidator<Note> _validator;

    public NoteService(IValidator<Note> validator, IPersistenceCtx persistenceCtx)
    {
        _validator = validator;
        _persistenceCtx = persistenceCtx;
    }

    public async Task<Note> CreateNoteAsync(string userId, string title, string content, CancellationToken ct = default)
    {
        var note = new Note
        {
            UserId = userId,
            Title = title,
            Content = content
        };
        
        await _validator.ValidateAndThrowAsync(note, ct);
        
        await _persistenceCtx.NoteRepo.AddAsync(note, ct);
        
        return note;
    }
}