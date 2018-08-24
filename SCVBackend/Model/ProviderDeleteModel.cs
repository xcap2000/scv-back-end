using System;

namespace SCVBackend.Model
{
    public class ProviderDeleteModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BaseApiUrl { get; set; }
    }
}