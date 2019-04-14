using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace dotnet_core_multi_tenancy_sample.Tenants
{
    public class HttpTenantResolveMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpTenantResolveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,
            TenantResolver tenantResolver,
            TenantContext tenantContext)
        {
            var tenant = tenantResolver.ResolveByHost(context.Request.Host.ToString());
            tenantContext.Tenant = tenant;
            await _next.Invoke(context);
        }

    }
}
