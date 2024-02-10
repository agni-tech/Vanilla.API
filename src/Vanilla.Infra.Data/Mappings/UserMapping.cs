using Vanilla.Domain.Users.Entities;
using Vanilla.Shared.Configurations;
using Vanilla.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vanilla.Infra.Data.Mappings;

public class UserMapping : EntityTypeConfiguration<User>
{
    public override void Map(EntityTypeBuilder<User> builder)
    {

        builder.Property(_ => _.FirstName)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(_ => _.LastName)
               .HasMaxLength(200)
               .IsRequired(false);

        builder.Property(_ => _.Email)
               .HasMaxLength(200)
               .IsRequired(false);

        builder.Property(_ => _.Role)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(_ => _.UserGroupId)
               .IsRequired(false);

        builder.Property(_ => _.EnterpriseId)
                .IsRequired(false);

        builder.Property(_ => _.Password)
               .HasMaxLength(150)
               .IsRequired();

        #region Relationships

        builder.HasOne(p => p.UserGroup)
               .WithMany(f => f.Users)
               .HasForeignKey(fk => fk.UserGroupId)
               .HasPrincipalKey(pk => pk.Id);
        
        #endregion

        builder.ToTable(StringResource.UserTableName);
    }
}
