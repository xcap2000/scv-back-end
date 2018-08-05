using System;

namespace SCVBackend.Model
{
    public class ProviderListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BaseApiUrl { get; set; }
    }
}