using System;
                
namespace SCVBackend.Model
{
    public class CheckoutModel
    {
        public Guid CartId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string CreditCardNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
