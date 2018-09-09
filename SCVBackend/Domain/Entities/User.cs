using System;
using System.Collections.Generic;

namespace SCVBackend.Domain.Entities
{
    public class User
    {
        public User
        (
            Guid id,
            UserType type,
            string name,
            string email,
            string password,
            string salt,
            byte[] photo
        )
        {
            Id = id;
            Type = type;
            Name = name;
            Email = email;
            Password = password;
            Salt = salt;
            Photo = photo;
        }

        public Guid Id { get; set; }
        public UserType Type { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public byte[] Photo { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}