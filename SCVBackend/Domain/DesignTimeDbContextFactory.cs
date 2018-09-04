using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SCVBackend.Domain
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ScvContext>
    {
        public ScvContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            var configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString("Default");

            var builder = new DbContextOptionsBuilder<ScvContext>();
            builder.UseNpgsql(connectionString);

            return new ScvContext(builder.Options);
        }
    }
}