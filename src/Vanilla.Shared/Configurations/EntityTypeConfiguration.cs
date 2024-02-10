using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vanilla.Shared.Configurations
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
