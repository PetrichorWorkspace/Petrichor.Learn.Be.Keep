using Keep.Domain.NoteAggregate.Entities;
using Keep.Domain.NoteAggregate.Rules;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keep.Driven.NpgsqlPersistence.EntityConfigs;

public class NoteConfig : BaseEntityConfig<Note>
{
    public override void Configure(EntityTypeBuilder<Note> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId);
        
        builder
            .Property(n => n.Title)
            .HasMaxLength(NoteTitleRule.MaxLength)
            .IsRequired();
        
        builder
            .Property(n => n.Content)
            .IsRequired();
    }
}