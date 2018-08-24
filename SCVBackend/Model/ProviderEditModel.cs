using System;

namespace SCVBackend.Model
{
    public class ProviderEditModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string BaseApiUrl { get; set; }
    }
}