using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Rules.BaseEntityRules;

namespace Keep.Driven.NpgsqlPersistence.EntityConfigs;

public abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasMaxLength(IdRule.MaxLength);

        builder
            .Property(e => e.CreatedAt)
            .IsRequired();
        
        builder
            .Property(e => e.LastModifiedAt)
            .IsRequired(false);
    }
}