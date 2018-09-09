using SCVBackend.Domain.Entities;
using System;
using System.Linq;
using SCVBackend.Infrastructure;

namespace SCVBackend.Domain.Seed
{
    public static class BrandSeed
    {
        public static void SeedBrands(this ScvContext context)
        {
            if (!context.Brands.Any())
            {
                context.Brands.AddRange
                (
                    new Brand
                    (
                        Guid.Parse("fd1c21b9-57c0-4c9c-9d63-7a775638ec4b"),
                        "Mont Blanc",
                        "montblanc".Image("BrandSeed")
                    ),
                    new Brand
                    (
                        Guid.Parse("c8d7ec20-96cd-456f-b11e-b3ce7c1dbdc8"),
                        "Parker",
                        "parker".Image("BrandSeed")
                    ),
                    new Brand
                    (
                        Guid.Parse("e5f62aae-5b27-41e4-a6b7-e5c62d734aae"),
                        "Crown",
                        "crown".Image("BrandSeed")
                    )
                );

                context.SaveChanges();
            }
        }
    }
}