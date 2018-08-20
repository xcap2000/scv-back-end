using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
            var options = new DbContextOptionsBuilder<ScvContext>()
                .UseSqlite(sqliteConnection)
                .Options;

            var context = new ScvContext(options);
            context.Database.EnsureCreated();
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

        public void Dispose()
        {
            sqliteConnection.Dispose();
        }
    }
}