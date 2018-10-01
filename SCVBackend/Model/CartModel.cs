using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class CartModel
    {
        /// <summary>
        ///     The cart's id.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        ///     The cart's total.
        /// </summary>
        [Required]
        public decimal Total { get; set; }

        /// <summary>
        ///     The cart's items.
        /// </summary>
        [Required]
        public IList<CartItemModel> CartItems { get; set; }
    }
}