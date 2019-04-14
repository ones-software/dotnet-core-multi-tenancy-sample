using dotnet_core_multi_tenancy_sample.DB;
using dotnet_core_multi_tenancy_sample.Files;
using dotnet_core_multi_tenancy_sample.Tenants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<TenantContext>();
            services.AddScoped<HttpTenantResolveMiddleware>();        
            services.AddScoped(serviceProvider => serviceProvider.GetService<TenantContext>()?.Tenant);

            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<TenantDbContext>();


            services.AddScoped<TenantFileService>();
            services.AddScoped<FileExtensionContentTypeProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseMiddleware<HttpTenantResolveMiddleware>();
          
            app.UseMvc();

            foreach (var tenant in app.ApplicationServices.GetService<TenantResolver>().Tenants)
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var tenantContext = scope.ServiceProvider.GetService<TenantContext>();
                    tenantContext.Tenant = tenant;
                    var dbContext = scope.ServiceProvider.GetService<TenantDbContext>();
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
