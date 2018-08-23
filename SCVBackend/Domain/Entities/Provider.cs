using System;

namespace SCVBackend.Domain.Entities
{
    public class Provider
    {
        public Provider
            (
            Guid id,
            string name,
            string baseApiUrl
            )
        {
            Id = id;
            Name = name;
            BaseApiUrl = baseApiUrl;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BaseApiUrl { get; set; }
    }
}