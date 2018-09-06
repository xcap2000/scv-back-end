using System.IO;
using System.Reflection;

namespace SCVBackend.Infrastructure
{
    public static class StringExtensions
    {
        public static byte[] Photo(this string photoKey)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"SCVBackend.Domain.Seed.UserSeed.{photoKey}.jpg");

            var memoryStream = new MemoryStream((int)stream.Length);

            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}