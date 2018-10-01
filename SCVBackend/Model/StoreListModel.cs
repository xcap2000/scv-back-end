using System;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class StoreListModel
    {
        /// <summary>
        ///     The product's id.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        ///     The product's name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     The product's price.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        ///     Wehther the product's is available.
        /// </summary>
        [Required]
        public bool CanSell { get; set; }

        /// <summary>
        ///     The product's photo.
        /// </summary>
        [Required]
        public string Photo { get; set; }

        /// <summary>
        ///     Wehther the product's is already in user's cart.
        /// </summary>
        [Required]
        public bool InCart { get; set; }
    }
}