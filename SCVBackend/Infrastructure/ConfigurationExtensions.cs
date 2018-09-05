using Microsoft.Extensions.Configuration;

namespace SCVBackend.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static string WithSecretIfAvailable(this IConfiguration configuration, string configurationKey, string secretKey)
        {
            return configuration[configurationKey].Replace(secretKey, configuration[secretKey]);
        }
    }
}