using CacheManager.Core;
using EFSecondLevelCache.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SCVBackend.Domain;
using SCVBackend.Infrastructure;
using System;
using System.IO.Compression;
using System.Text;

namespace SCVBackend
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        private readonly IHostingEnvironment environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ScvContext>(
                options => options.UseNpgsql(configuration.WithSecretIfAvailable("ConnectionStrings:Default", "SECRET_PASSWORD")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options => options.EnableForHttps = true);

            services.AddEFSecondLevelCache();

            services.AddSingleton(typeof(ICacheManager<>), typeof(BaseCacheManager<>));
            services.AddSingleton(typeof(ICacheManagerConfiguration),
                new CacheManager.Core.ConfigurationBuilder()
                        .WithJsonSerializer()
                        .WithMicrosoftMemoryCacheHandle()
                        .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromMinutes(10))
                        .Build());

            services
              .AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(options =>
              {
                  options.RequireHttpsMetadata = true;
                  options.SaveToken = true;

                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidIssuer = configuration.WithSecretIfAvailable("Tokens:Issuer", "SECRET_ISSUER"),
                      ValidAudience = configuration.WithSecretIfAvailable("Tokens:Audience", "SECRET_AUDIENCE"),
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.WithSecretIfAvailable("Tokens:Key", "SECRET_TOKEN")))
                  };
              });

            /* TODO - Verify whether it will be possible to re-enable.
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "XSRF-TOKEN";
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.SuppressXFrameOptionsHeader = false;
            });
            */
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseETagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder =>
                builder.WithOrigins(configuration["CorsOrigins"])
                    .SetPreflightMaxAge(TimeSpan.FromHours(1D))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            );

            app.UseEFSecondLevelCache();
            app.MigrateDatabase();
            app.SeedDatabase();

            app.UseResponseCompression();
            app.UseMvc();
        }
    }
}