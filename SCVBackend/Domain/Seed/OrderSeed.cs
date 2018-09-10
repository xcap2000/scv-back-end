using SCVBackend.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SCVBackend.Domain.Seed
{
    public static class OrderSeed
    {
        public static void SeedOrders(this ScvContext context)
        {
            if (!context.Orders.Any())
            {
                var orderNumber = 1;

                context.Orders.AddRange
                (
                    new Order
                    (
                        Guid.Parse("3685851e-ef23-4b64-a8bb-92deb07e24a0"),
                        OrderStatus.Closed,
                        Guid.Parse("b3cd53fa-f3c4-47ee-8397-579a932571e7"),
                        orderNumber++,
                        DateTime.Now
                    )
                    {
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem
                            (
                                Guid.Parse("6a1842f1-7466-474d-94cb-6e254d6f1724"),
                                1,
                                5_000M * 1.25M,
                                Guid.Parse("3685851e-ef23-4b64-a8bb-92deb07e24a0"),
                                Guid.Parse("7a95fc88-b230-4167-abd3-b9237b8a845b")
                            )
                        },
                        OrderDetails = new OrderDetails
                        {
                            Street = "Rua Monteiro Lobato 2100",
                            City = "Caçapava",
                            State = "São Paulo",
                            Country = "Brasil",
                            PostalCode = "12280-018",
                            CreditCardNumber = "845651231687651321",
                            VerificationCode = "222"
                        }
                    }
                );



                context.SaveChanges();
            }
        }
    }
}