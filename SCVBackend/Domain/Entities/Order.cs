using System;
using System.Collections.Generic;
using System.Linq;

namespace SCVBackend.Domain.Entities
{
    public class Order
    {
        public Order
        (
            Guid id,
            OrderStatus orderStatus,
            Guid userId,
            long? orderNumber = null,
            DateTime? closeDate = null
        )
        {
            Id = id;
            OrderStatus = orderStatus;
            UserId = userId;
            OrderNumber = orderNumber;
            CloseDate = closeDate;
        }

        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public long? OrderNumber { get; set; }
        public DateTime? CloseDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal Total
        {
            get
            {
                return OrderItems?.Sum(o => o.Subtotal) ?? 0M;
            }
        }
    }
}