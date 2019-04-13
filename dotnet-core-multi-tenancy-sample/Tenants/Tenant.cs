using System.Collections.Generic;

namespace dotnet_core_multi_tenancy_sample.Tenants
{
    public class Tenant
    {
        public string Name { get; set; }
        public string FileDirectory { get; set; }
        public string ConnectionString { get; set; }
        public IEnumerable<string> HostName { get; set; }
    }
}
