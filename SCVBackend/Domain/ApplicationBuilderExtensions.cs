using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SCVBackend.Domain.Seed;

namespace SCVBackend.Domain
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (var serviceScope = serviceScopeFactory.CreateScope())
            using (var context = serviceScope.ServiceProvider.GetRequiredService<ScvContext>())
            {
                context.Database.Migrate();
            }
        }

        public static void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (var serviceScope = serviceScopeFactory.CreateScope())
            using (var context = serviceScope.ServiceProvider.GetRequiredService<ScvContext>())
            {
                context.SeedUsers();
                context.SeedBrands();
                context.SeedProviders();
                context.SeedProducts();
                context.SeedOrders();
            }
        }
    }
}