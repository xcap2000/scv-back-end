using System;
using System.Collections.Generic;

namespace SCVBackend.Model
{
    public class CartModel
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public IList<CartItemModel> CartItems { get; set; }
    }
}