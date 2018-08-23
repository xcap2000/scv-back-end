using Microsoft.AspNetCore.Builder;

namespace SCVBackend.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseETagger(this IApplicationBuilder app)
        {
            app.UseMiddleware<ETagMiddleware>();
        }
    }
}