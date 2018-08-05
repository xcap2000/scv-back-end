using Microsoft.EntityFrameworkCore;

namespace SCVBackend.Domain
{
    public class ScvContext : DbContext
    {
        public ScvContext(DbContextOptions<ScvContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
        }

        public DbSet<Provider> Providers { get; set; }
    }
}
