using dotnet_core_multi_tenancy_sample.Tenants;
using Microsoft.EntityFrameworkCore;

namespace dotnet_core_multi_tenancy_sample.DB
{
    public class TenantDbContext : DbContext
    {
        public DbSet<Cache> Caches { get; set; }

        private readonly Tenant _tenant;
        public TenantDbContext(Tenant tenant)
        {
            _tenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_tenant.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
