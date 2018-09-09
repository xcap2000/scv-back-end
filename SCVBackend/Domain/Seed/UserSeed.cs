using SCVBackend.Domain.Entities;
using System;
using System.Linq;
using SCVBackend.Infrastructure;
using static SCVBackend.Domain.Entities.UserType;

namespace SCVBackend.Domain.Seed
{
    public static class UserSeed
    {
        public static void SeedUsers(this ScvContext context)
        {
            if (!context.Users.Any())
            {
                var (password1, salt1) = "123".Encrypt();
                var (password2, salt2) = "456".Encrypt();
                var (password3, salt3) = "789".Encrypt();

                context.Users.AddRange
                (
                    new User
                    (
                        Guid.Parse("b3cd53fa-f3c4-47ee-8397-579a932571e7"),
                        Customer,
                        "Fujika Maria Namoto",
                        "namoto@gmail.com",
                        password1,
                        salt1,
                        "namoto".Image("UserSeed")
                    ),
                    new User
                    (
                        Guid.Parse("5b00cc61-1e3a-41b6-8916-42b385265cf1"),
                        Seller,
                        "Fujika Maria Nakombi",
                        "nakombi@gmail.com",
                        password2,
                        salt2,
                        "nakombi".Image("UserSeed")
                    ),
                    new User
                    (
                        Guid.Parse("73334fc2-0338-4a7c-9d1b-1703cac26bf1"),
                        Admin,
                        "Pedro K. Beludo",
                        "kbeludo@gmail.com",
                        password3,
                        salt3,
                        "kbeludo".Image("UserSeed")
                    )
                );

                context.SaveChanges();
            }
        }
    }
}