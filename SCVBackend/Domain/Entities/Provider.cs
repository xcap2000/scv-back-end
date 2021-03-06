﻿using System;
using System.Collections.Generic;

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
        public ICollection<Product> Products { get; set; }
    }
}