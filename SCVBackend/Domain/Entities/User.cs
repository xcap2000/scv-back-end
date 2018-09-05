using System;

namespace SCVBackend.Domain.Entities
{
    // TODO - Create user details table to save avatar image.
    public class User
    {
        public User
            (
            Guid id,
            UserType type,
            string name,
            string email,
            string password,
            string salt
            )
        {
            Id = id;
            Type = type;
            Name = name;
            Email = email;
            Password = password;
            Salt = salt;
        }

        public Guid Id { get; set; }
        public UserType Type { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}