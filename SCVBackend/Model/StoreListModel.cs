using System;

namespace SCVBackend.Model
{
    public class StoreListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool CanSell { get; set; }
        public string Photo { get; set; }
        public bool InCart { get; set; }
    }
}