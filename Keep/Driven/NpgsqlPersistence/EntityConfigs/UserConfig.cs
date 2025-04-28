using Keep.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Domain.Rules.BaseEntityRules;

namespace Keep.Driven.NpgsqlPersistence.EntityConfigs;

public class UserConfig : BaseEntityConfig<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(u => u.IdentityId)
            .IsUnique();

        builder
            .Property(u => u.IdentityId)
            .HasMaxLength(IdRule.MaxLength)
            .IsRequired();
    }
}