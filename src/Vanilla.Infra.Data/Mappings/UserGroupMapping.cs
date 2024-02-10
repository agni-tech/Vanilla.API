using Vanilla.Domain.UserGroups.Entities;
using Vanilla.Shared.Configurations;
using Vanilla.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vanilla.Infra.Data.Mappings;

public class UserGroupMapping : EntityTypeConfiguration<UserGroup>
{
    public override void Map(EntityTypeBuilder<UserGroup> builder)
    {
        builder.Property(_ => _.Name)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(_ => _.Description)
               .HasMaxLength(200)
               .IsRequired(false);
        #region Relationships

        #endregion

        builder.ToTable(StringResource.UserGroupTableName);
    }
}
