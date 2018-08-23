using EFSecondLevelCache.Core;
using EFSecondLevelCache.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SCVBackend.Domain.Configurations;
using SCVBackend.Domain.Entities;

namespace SCVBackend.Domain
{
    public class ScvContext : DbContext
    {
        public ScvContext(DbContextOptions<ScvContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
        }

        public DbSet<Provider> Providers { get; set; }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var changedEntityNames = this.GetChangedEntityNames();

            var result = base.SaveChanges();
            this.GetService<IEFCacheServiceProvider>().InvalidateCacheDependencies(changedEntityNames);

            return result;
        }
    }
}