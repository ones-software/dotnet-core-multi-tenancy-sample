using dotnet_core_multi_tenancy_sample.Tenants;
using Microsoft.EntityFrameworkCore.Design;

namespace dotnet_core_multi_tenancy_sample.DB
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            return new TenantDbContext(
                new Tenant
                {
                    ConnectionString = "Server=.;Database=TenantDb.ForMigration;Integrated Security=True"
                });
        }
    }
}