using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class SignInModel
    {
        /// <summary>
        ///     The user's email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///     The user's password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}