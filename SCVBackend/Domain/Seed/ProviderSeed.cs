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
                context.Providers.AddRange
                (
                    new Provider
                    (
                        Guid.Parse("3d461f70-24a8-4796-bb5a-768521bda2ee"),
                        "Mont Blanc Provider",
                        "https://montblanc-provider.com/api"
                    ),
                    new Provider
                    (
                        Guid.Parse("e3f2d6d9-daa0-4ea2-8a20-9af6896bdda8"),
                        "Parker Provider",
                        "https://parker-provider.com/api"
                    ),
                    new Provider
                    (
                        Guid.Parse("1406bbf9-92c2-41be-96ce-3a2b67123486"),
                        "Crown Provider",
                        "https://crown-provider.com/api"
                    )
                );

                context.SaveChanges();
            }
        }
    }
}