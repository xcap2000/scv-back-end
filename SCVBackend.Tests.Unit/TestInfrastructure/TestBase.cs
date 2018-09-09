using CacheManager.Core;
using EFSecondLevelCache.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SCVBackend.Domain;
using System;

namespace SCVBackend.Tests.Unit.TestInfrastructure
{
    public abstract class TestBase : IDisposable
    {
        private readonly SqliteConnection sqliteConnection;

        public TestBase()
        {
            sqliteConnection = new SqliteConnection("DataSource=:memory:");
            sqliteConnection.Open();

            using (var context = CreateContext())
            {
                context.Database.EnsureCreated();
            }
        }

        protected ScvContext CreateContext()
        {
            var services = new ServiceCollection();
            services.AddEFSecondLevelCache();

            services.AddScoped(typeof(ICacheManager<>), typeof(BaseCacheManager<>));
            services.AddSingleton(typeof(ICacheManagerConfiguration),
                new ConfigurationBuilder()
                        .WithJsonSerializer()
                        .WithMicrosoftMemoryCacheHandle()
                        .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromMilliseconds(1))
                        .Build());

            var serviceProvider = GetServiceProvider(services);

            var options = new DbContextOptionsBuilder<ScvContext>()
                .UseSqlite(sqliteConnection)
                .UseApplicationServiceProvider(serviceProvider)
                .Options;

            var context = new ScvContext(options);
            return context;
        }

        protected void UsingContext(Action<ScvContext> doWithContextAction)
        {
            using (var context = CreateContext())
            {
                doWithContextAction(context);
                context.SaveChanges();
            }
        }
        
        public IServiceProvider GetServiceProvider(ServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            EFServiceProvider.ApplicationServices = serviceProvider;

            return serviceProvider;
        }
        
        public void Dispose()
        {
            sqliteConnection.Dispose();
        }
    }
}