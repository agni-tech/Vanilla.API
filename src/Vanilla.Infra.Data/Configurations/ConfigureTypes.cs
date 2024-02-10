using Microsoft.EntityFrameworkCore;

namespace Vanilla.Infra.Data.Configurations;

public static class ConfigureTypes
{
    public static ModelBuilder AddCustomTypes(this ModelBuilder modelBuilder)
    {

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.GetProperties()
                .ToList()
                .ForEach(p =>
                {
                    p.SetIsUnicode(false);

                    switch (p.ClrType)
                    {
                        case Type t when t == typeof(string):
                            p.SetColumnType("varchar");
                            p.SetMaxLength(255);
                            break;

                        case Type t when t == typeof(float) || t == typeof(float?):
                            p.SetColumnType("float");
                            break;

                        case Type t when t == typeof(decimal) || t == typeof(decimal?):
                            p.SetPrecision(12);
                            p.SetScale(8);
                            break;

                        case Type t when t == typeof(DateTime) || t == typeof(DateTime?):
                            p.SetColumnType("datetime");
                            break;

                    }
                });

            entityType.GetForeignKeys()
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                .ToList()
                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        }

        return modelBuilder;
    }
}
