using System.ComponentModel.DataAnnotations;

namespace dotnet_core_multi_tenancy_sample.DB
{
    public class Cache
    {
        [Key, StringLength(255)]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
