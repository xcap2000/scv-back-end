using System;

namespace SCVBackend.Domain.Entities
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string CreditCardNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}