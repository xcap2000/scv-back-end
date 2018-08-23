using SCVBackend.Domain.Entities;
using System;
using System.Linq;

namespace SCVBackend.Domain.Seed
{
    public static class ProviderSeed
    {
        public static void SeedProviders(this ScvContext context)
        {
            if (!context.Providers.Any())
            {
                for (int i = 1; i <= 100; i++)
                {
                    context.Providers.Add
                        (
                            new Provider(Guid.NewGuid(), $"Provider {i}", $"https://provider{i}.com/api")
                        );
                }

                context.SaveChanges();
            }
        }
    }
}