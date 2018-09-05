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
                        new User(Guid.NewGuid(), Customer, "Fujika Maria Namoto", "namoto@gmail.com", password1, salt1),
                        new User(Guid.NewGuid(), Seller, "Fujika Maria Nakombi", "nakombi@gmail.com", password2, salt2),
                        new User(Guid.NewGuid(), Admin, "Pedro K. Beludo", "kbeludo@gmail.com", password3, salt3)
                    );

                context.SaveChanges();
            }
        }
    }
}