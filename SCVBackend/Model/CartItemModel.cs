using System;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class CartItemModel
    {
        /// <summary>
        ///     The cart's item id.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        ///     The cart's item photo.
        /// </summary>
        [Required]
        public string Photo { get; set; }

        /// <summary>
        ///     The cart's item product name.
        /// </summary>
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        ///     The cart's item product quantity.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        ///     The cart's item product price.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        ///     The cart's item product subtotal.
        /// </summary>
        [Required]
        public decimal Subtotal { get; set; }
    }
}