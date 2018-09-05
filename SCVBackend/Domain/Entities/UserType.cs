using System;

namespace SCVBackend.Domain.Entities
{
    [Flags]
    public enum UserType : byte
    {
        Customer = 1,
        Seller = 2,
        Admin = 4
    }
}