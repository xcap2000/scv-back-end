using Microsoft.EntityFrameworkCore;
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
    }
}