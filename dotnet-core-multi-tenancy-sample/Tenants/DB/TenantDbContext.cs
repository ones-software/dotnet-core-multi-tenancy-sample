using Microsoft.EntityFrameworkCore;

namespace dotnet_core_multi_tenancy_sample.Tenants.DB
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
        }

        public void Initialize()
        {
            Database.Migrate();
        }
    }
}
