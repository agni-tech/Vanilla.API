using Vanilla.Domain.Options.Entities;
using Vanilla.Shared.Configurations;
using Vanilla.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vanilla.Infra.Data.Mappings;

public class OptionMapping : EntityTypeConfiguration<Option>
{
    public override void Map(EntityTypeBuilder<Option> builder)
    {

        builder.Property(_ => _.Config)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(_ => _.Value)
               .HasMaxLength(255)
               .IsRequired();

        #region Relationships
        #endregion

        builder.ToTable(StringResource.BuildingTableName);
    }
}