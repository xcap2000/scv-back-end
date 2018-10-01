using System;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class AddToCartModel
    {
        /// <summary>
        ///     The user's id.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        ///     The product's id.
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }
    }
}
