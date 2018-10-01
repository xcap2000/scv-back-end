using System;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class CheckoutModel
    {
        /// <summary>
        ///     The cart's id.
        /// </summary>
        [Required]
        public Guid CartId { get; set; }

        /// <summary>
        ///     The address street.
        /// </summary>
        [Required]
        public string Street { get; set; }

        /// <summary>
        ///     The address city.
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        ///     The address state.
        /// </summary>
        [Required]
        public string State { get; set; }

        /// <summary>
        ///     The address country.
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        ///     The address postal code.
        /// </summary>
        [Required]
        public string PostalCode { get; set; }

        /// <summary>
        ///     The payment credit card number.
        /// </summary>
        [Required]
        public string CreditCardNumber { get; set; }

        /// <summary>
        ///     The payment credit card verification code.
        /// </summary>
        [Required]
        public string VerificationCode { get; set; }
    }
}
