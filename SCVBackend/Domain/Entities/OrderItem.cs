﻿using System;

namespace SCVBackend.Domain.Entities
{
    public class OrderItem
    {
        public OrderItem
        (
            Guid id,
            int quantity,
            decimal price,
            Guid orderId,
            Guid productId
        )
        {
            Id = id;
            Quantity = quantity;
            Price = price;
            OrderId = orderId;
            ProductId = productId;
        }

        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}