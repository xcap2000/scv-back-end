using System;

namespace SCVBackend.Model
{
    public class UpdateCartItemModel
    {
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}