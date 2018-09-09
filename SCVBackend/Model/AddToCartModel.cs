using System;

namespace SCVBackend.Model
{
    public class AddToCartModel
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
