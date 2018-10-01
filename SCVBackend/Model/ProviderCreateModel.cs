using System;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class ProviderCreateModel
    {
        /// <summary>
        ///     The provider's id.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        ///     The provider's name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     The provider's base API URL.
        /// </summary>
        [Required]
        public string BaseApiUrl { get; set; }
    }
}