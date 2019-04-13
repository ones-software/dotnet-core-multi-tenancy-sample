using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_core_multi_tenancy_sample.Tenants;
using dotnet_core_multi_tenancy_sample.Tenants.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dotnet_core_multi_tenancy_sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<TenantResolver>();
            services.AddScoped<TenantFileService>();
            services.AddScoped<TenantContext>();
            services.AddScoped<HttpTenantResolveMiddleware>();
            services.AddScoped<FileExtensionContentTypeProvider>();

            services.AddScoped(serviceProvider => serviceProvider.GetService<TenantContext>()?.Tenant);

            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<TenantDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseMiddleware<HttpTenantResolveMiddleware>();
          

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            foreach (var tenant in app.ApplicationServices.GetService<TenantResolver>().Tenants)
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var tenantContext = scope.ServiceProvider.GetService<TenantContext>();
                    tenantContext.Tenant = tenant;
                    var dbContext = scope.ServiceProvider.GetService<TenantDbContext>();
                    dbContext.Initialize();
                }
            }
        }
    }
}
