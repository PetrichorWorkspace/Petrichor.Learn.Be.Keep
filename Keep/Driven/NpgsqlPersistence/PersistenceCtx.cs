﻿using Keep.Domain.NoteAggregate.Entities;
using Keep.Domain.UserAggregate.Entities;
using Keep.Driven.NpgsqlPersistence.Repos;
using Microsoft.EntityFrameworkCore;

namespace Keep.Driven.NpgsqlPersistence;

public interface IPersistenceCtx
{
    IUserRepo UserRepo { get; }
    INoteRepo NoteRepo { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class PersistenceCtx : DbContext, IPersistenceCtx
{
    static PersistenceCtx()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    public required DbSet<User> Users { get; init; }
    public IUserRepo UserRepo { get; }
    
    public required DbSet<Note> Notes { get; init; }
    public INoteRepo NoteRepo { get; }

    public PersistenceCtx(DbContextOptions<PersistenceCtx> options) : base(options)
    {
        UserRepo = new UserRepo(this);
        NoteRepo = new NoteRepo(this);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceCtx).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}