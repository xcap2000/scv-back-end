using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SCVBackend.Domain;
using System;
using System.Diagnostics;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (environment.IsDevelopment())
            {
                services.AddDbContextPool<ScvContext>(
                    options => options.UseNpgsql(configuration.GetConnectionString("Default")));
            }
            else
            {
                Debug.WriteLine("DEBUG--:" + configuration.GetConnectionString("Default").Replace("SECRET_PASSWORD", configuration["SECRET_PASSWORD"]));
                Console.WriteLine("CONSOLE--:" + configuration.GetConnectionString("Default").Replace("SECRET_PASSWORD", configuration["SECRET_PASSWORD"]));

                services.AddDbContextPool<ScvContext>(
                    options => options.UseNpgsql(configuration.GetConnectionString("Default").Replace("SECRET_PASSWORD", configuration["SECRET_PASSWORD"])));
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            );

            app.MigrateDatabase();
            app.SeedDatabase();

            app.UseMvc();
        }
    }
}
