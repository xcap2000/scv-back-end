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
            if (!context.Brands.Any())
            {
                var orderNumber = 1;

                context.Orders.AddRange
                (
                    new Order
                    (
                        Guid.Parse("3685851e-ef23-4b64-a8bb-92deb07e24a0"),
                        orderNumber++,
                        OrderStatus.Closed,
                        Guid.Parse("b3cd53fa-f3c4-47ee-8397-579a932571e7"),
                        DateTime.Now
                    )
                    {
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem
                            (
                                Guid.Parse("3685851e-ef23-4b64-a8bb-92deb07e24a0"),
                                1,
                                5_000M * 1.25M,
                                Guid.Parse(""),
                                Guid.Parse("7a95fc88-b230-4167-abd3-b9237b8a845b")
                            )
                        }
                    }
                );

                context.SaveChanges();
            }
        }
    }
}