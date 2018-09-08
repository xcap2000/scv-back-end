using SCVBackend.Domain.Entities;
using System;
using System.Linq;
using SCVBackend.Infrastructure;

namespace SCVBackend.Domain.Seed
{
    public static class ProductSeed
    {
        public static void SeedProducts(this ScvContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange
                (
                    // Mont Blanc
                    new Product
                    (
                        Guid.Parse("7a95fc88-b230-4167-abd3-b9237b8a845b"),
                        "MEISTERSTUCK MOZ-SOLIT-VER",
                        25,
                        5_000M,
                        5_000M * 1.25M,
                        Guid.Parse("fd1c21b9-57c0-4c9c-9d63-7a775638ec4b"),
                        Guid.Parse("3d461f70-24a8-4796-bb5a-768521bda2ee"),
                        "meisterstuck-moz-solit-ver".Image("ProductSeed")
                    ),
                    new Product
                    (
                        Guid.Parse("7a95fc88-b230-4167-abd3-b9237b8a845b"),
                        "MEISTERSTUCK CLASSIC",
                        25,
                        2_000M,
                        2_000M * 1.25M,
                        Guid.Parse("fd1c21b9-57c0-4c9c-9d63-7a775638ec4b"),
                        Guid.Parse("3d461f70-24a8-4796-bb5a-768521bda2ee"),
                        "meisterstuck-classic".Image("ProductSeed")
                    ),
                    new Product
                    (
                        Guid.Parse("7a95fc88-b230-4167-abd3-b9237b8a845b"),
                        "MEISTERSTUCK MOZART",
                        25,
                        2_100M,
                        2_100M * 1.25M,
                        Guid.Parse("fd1c21b9-57c0-4c9c-9d63-7a775638ec4b"),
                        Guid.Parse("3d461f70-24a8-4796-bb5a-768521bda2ee"),
                        "meisterstuck-mozart".Image("ProductSeed")
                    ),
                    // Parker
                    new Product
                    (
                        Guid.Parse("c9df536f-dd5d-41ff-b8d2-63b794a7a888"),
                        "INFLECTION",
                        50,
                        1_000M,
                        1_000M * 1.25M,
                        Guid.Parse("c8d7ec20-96cd-456f-b11e-b3ce7c1dbdc8"),
                        Guid.Parse("e3f2d6d9-daa0-4ea2-8a20-9af6896bdda8"),
                        "inflection".Image("ProductSeed")
                    ),
                    new Product
                    (
                        Guid.Parse("bf7ece8a-de36-4ad6-aabe-20abdde5996c"),
                        "DUOFOLD-3 CENTENNIAL",
                        50,
                        6_500M,
                        6_500M * 1.25M,
                        Guid.Parse("c8d7ec20-96cd-456f-b11e-b3ce7c1dbdc8"),
                        Guid.Parse("e3f2d6d9-daa0-4ea2-8a20-9af6896bdda8"),
                        "duofold-3-centennial".Image("ProductSeed")
                    ),
                    new Product
                    (
                        Guid.Parse("e2e93749-6caf-49ae-9e79-f9d8a61b20cf"),
                        "SONNET-4 CHISSELED-PRATA-CT",
                        50,
                        3_000M,
                        3_000M * 1.25M,
                        Guid.Parse("c8d7ec20-96cd-456f-b11e-b3ce7c1dbdc8"),
                        Guid.Parse("e3f2d6d9-daa0-4ea2-8a20-9af6896bdda8"),
                        "sonnet-4-chisseled".Image("ProductSeed")
                    ),
                    // Crown
                    new Product
                    (
                        Guid.Parse("5514920c-4fb8-44b8-aed6-83eb29e2635a"),
                        "CLASSIC BARCELONA",
                        100,
                        75M,
                        75M * 1.25M,
                        Guid.Parse("e5f62aae-5b27-41e4-a6b7-e5c62d734aae"),
                        Guid.Parse("1406BBF9-92C2-41BE-96CE-3A2B67123486"),
                        "classic-barcelona".Image("ProductSeed")
                    ),
                    new Product
                    (
                        Guid.Parse("6c67dbb1-05a3-4ea8-a2f0-35a3966c9138"),
                        "FASHION CRISTAL",
                        100,
                        50M,
                        50M * 1.25M,
                        Guid.Parse("e5f62aae-5b27-41e4-a6b7-e5c62d734aae"),
                        Guid.Parse("1406BBF9-92C2-41BE-96CE-3A2B67123486"),
                        "fashion-cristal".Image("ProductSeed")
                    ),
                    new Product
                    (
                        Guid.Parse("6c67dbb1-05a3-4ea8-a2f0-35a3966c9138"),
                        "ROYAL COLLECT MARFIM LACQ",
                        100,
                        350M,
                        350M * 1.25M,
                        Guid.Parse("e5f62aae-5b27-41e4-a6b7-e5c62d734aae"),
                        Guid.Parse("1406BBF9-92C2-41BE-96CE-3A2B67123486"),
                        "royal-collect-marfim-lacq".Image("ProductSeed")
                    )
                );

                context.SaveChanges();
            }
        }
    }
}