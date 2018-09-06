using System;

namespace SCVBackend.Infrastructure
{
    public static class ByteExtensions
    {
        public static string ToBase64(this byte[] image)
        {
            return Convert.ToBase64String(image);
        }
    }
}