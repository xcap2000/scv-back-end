using System;
using System.Collections.Generic;

namespace SCVBackend.Domain.Entities
{
    public class Order
    {
        public Order
        (
            Guid id,
            long orderNumber,
            OrderStatus orderStatus,
            Guid userId,
            DateTime? closeDate = null
        )
        {
            Id = id;
            OrderNumber = orderNumber;
            OrderStatus = orderStatus;
            UserId = userId;
            CloseDate = closeDate;
        }

        public Guid Id { get; set; }
        public long OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime? CloseDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}