using System;
using System.Collections.Generic;

namespace SCVBackend.Domain.Entities
{
    public class Product
    {
        public Product
        (
            Guid id,
            string name,
            int quantity,
            decimal buyPrice,
            decimal sellPrice,
            Guid brandId,
            Guid providerId,
            byte[] photo
        )
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            BrandId = brandId;
            ProviderId = providerId;
            Photo = photo;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }
        public byte[] Photo { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}