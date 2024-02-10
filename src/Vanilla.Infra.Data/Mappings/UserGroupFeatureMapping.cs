using Vanilla.Domain.UserGroupFeatures.Entities;
using Vanilla.Shared.Configurations;
using Vanilla.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vanilla.Infra.Data.Mappings;

public class UserGroupFeatureMapping : EntityTypeConfiguration<UserGroupFeature>
{
    public override void Map(EntityTypeBuilder<UserGroupFeature> builder)
    {

        builder.Property(_ => _.Name)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(_ => _.Description)
               .HasMaxLength(255)
               .IsRequired();

        #region Relationships

        builder.HasOne(p => p.UserGroup)
               .WithMany(f => f.UserGroupFeatures)
               .HasForeignKey(fk => fk.UserGroupId)
               .HasPrincipalKey(pk => pk.Id);

        #endregion

        builder.ToTable(StringResource.UserGroupFeatureTableName);
    }
}