using SCVBackend.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SCVBackend.Model
{
    public class SignInResponseModel
    {
        public SignInResponseModel(string token, Guid userId, UserType userType, string userName, string photo)
        {
            Token = token;
            UserId = userId;
            UserType = userType;
            UserName = userName;
            Photo = photo;
        }

        /// <summary>
        ///     The authentication token.
        /// </summary>
        [Required]
        public string Token { get; }

        /// <summary>
        ///     The user's id.
        /// </summary>
        [Required]
        public Guid UserId { get; }

        /// <summary>
        ///     The user's type.
        /// </summary>
        [Required]
        public UserType UserType { get; }

        /// <summary>
        ///     The user's name.
        /// </summary>
        [Required]
        public string UserName { get; }

        /// <summary>
        ///     The user's photo.
        /// </summary>
        [Required]
        public string Photo { get; }
    }
}