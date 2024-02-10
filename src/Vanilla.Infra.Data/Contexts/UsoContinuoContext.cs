using Vanilla.Domain.Options.Entities;
using Vanilla.Domain.UserGroupFeatures.Entities;
using Vanilla.Domain.UserGroups.Entities;
using Vanilla.Domain.Users.Entities;
using Vanilla.Infra.Data.Configurations;
using Vanilla.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace Vanilla.Infra.Data.Contexts;

public class VanillaContext : DbContext
{
    #region Construtor

    public VanillaContext(DbContextOptions<VanillaContext> options) : base(options)
    {
        Database.Migrate();
    }

    #endregion

    #region DBSets
    public DbSet<User> User { get; set; }
    public DbSet<UserGroup> UserGroup { get; set; }
    public DbSet<UserGroupFeature> UserGroupFeature { get; set; }
    public DbSet<Option> Option { get; set; }

    #endregion

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddMaps()
                    .AddCustomTypes();

        base.OnModelCreating(modelBuilder);

    }

    public override int SaveChanges()
    {
        CustomSaveChanges();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        CustomSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void CustomSaveChanges()
    {
        var entries = ChangeTracker.Entries()
                                   .Where(e => e.Entity is IEntity && (
                                          e.State == EntityState.Added ||
                                          e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Modified)
                ((IEntity)entityEntry.Entity).WithUpdatedAt(DateTime.Now);
        }
    }

    #endregion
}
