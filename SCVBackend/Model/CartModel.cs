using System;

namespace SCVBackend.Model
{
    public class CartModel
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public object CartItems { get; set; }
    }
}