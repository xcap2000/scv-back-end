using System;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class UpdateCartItemModel
    {
        /// <summary>
        ///     The cart item's id.
        /// </summary>
        [Required]
        public Guid CartItemId { get; set; }

        /// <summary>
        ///     The new quantity.
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}