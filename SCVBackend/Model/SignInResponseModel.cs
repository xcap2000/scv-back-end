using SCVBackend.Domain.Entities;
using System;

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

        public string Token { get; }
        public Guid UserId { get; }
        public UserType UserType { get; }
        public string UserName { get; }
        public string Photo { get; }
    }
}