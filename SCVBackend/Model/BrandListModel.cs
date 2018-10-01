using System;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class BrandListModel
    {
        /// <summary>
        ///     The brand's id.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        ///     The brand's name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     The brand's logo.
        /// </summary>
        [Required]
        public string Logo { get; set; }
    }
}