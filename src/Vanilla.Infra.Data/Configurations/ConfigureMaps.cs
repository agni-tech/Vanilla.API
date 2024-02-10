using Vanilla.Infra.Data.Mappings;
using Vanilla.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using MySaviors.Helpers.Extensions;

namespace Vanilla.Infra.Data.Configurations;

public static class MapConfig
{
    private static ModelBuilder AddIgnoreProperties(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var Ignoreds = entity.GetProperties()
                                 .Where(x => x.PropertyInfo.PropertyType.FullName.Contains("FluentValidation"))
                                 .Select(q => q.Name);

            Ignoreds.SForEach(fieldName => modelBuilder.Entity(entity.Name).Ignore(fieldName));
        }

        return modelBuilder;
    }

    public static ModelBuilder AddMaps(this ModelBuilder modelBuilder)
    {
       
        modelBuilder.AddConfiguration(new UserMapping());
        modelBuilder.AddConfiguration(new UserGroupMapping());
        modelBuilder.AddConfiguration(new UserGroupFeatureMapping());
        modelBuilder.AddConfiguration(new OptionMapping());
        modelBuilder.AddIgnoreProperties();

        return modelBuilder;
    }
}
