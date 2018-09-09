using System;
using System.Collections.Generic;

namespace SCVBackend.Domain.Entities
{
    public class Brand
    {
        public Brand
        (
            Guid id,
            string name,
            byte[] logo
        )
        {
            Id = id;
            Name = name;
            Logo = logo;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}