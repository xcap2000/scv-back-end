using System.IO;
using System.Reflection;

namespace SCVBackend.Infrastructure
{
    public static class StringExtensions
    {
        public static byte[] Image(this string photoKey, string seedKey)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"SCVBackend.Domain.Seed.{seedKey}.{photoKey}.gif");

            var memoryStream = new MemoryStream((int)stream.Length);

            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}