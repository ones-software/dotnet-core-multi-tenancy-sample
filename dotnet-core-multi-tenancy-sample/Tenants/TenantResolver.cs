using System.Collections.Generic;
using System.Linq;

namespace dotnet_core_multi_tenancy_sample.Tenants
{
    public class TenantResolver
    {
        public IEnumerable<Tenant> Tenants = new List<Tenant>
        {
            new Tenant
            {
                Name = "Tenant A",
                ConnectionString = "Server=.;Database=TenantADatabase;Integrated Security=True;",
                FileDirectory = "uploads\\tenantA",
                HostName = new List<string>{ "localhost:5000"}
            },
            new Tenant
            {
                Name = "Tenant B",
                ConnectionString = "Server=.;Database=TenantBDatabase;Integrated Security=True;",
                FileDirectory = "uploads\\tenantB",
                HostName = new List<string>{ "localhost:5001"}
            }

        };

        public Tenant ResolveByHost(string host)
        {
            return Tenants.FirstOrDefault(tenant => tenant.HostName.Contains(host));
        }
    }
}
