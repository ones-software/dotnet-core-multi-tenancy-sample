using System.IO;
using Microsoft.AspNetCore.Http;

namespace dotnet_core_multi_tenancy_sample.Tenants
{
    public class TenantFileService
    {
        private readonly Tenant _tenant;

        public TenantFileService(Tenant tenant)
        {
            _tenant = tenant;
        }


        public void Save(IFormFile file)
        {
            var path = Path.Combine(_tenant.FileDirectory, file.FileName);
            var directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }


        public MemoryStream Load(string fileName)
        {
            var path = Path.Combine(_tenant.FileDirectory,fileName);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return memory;
        }
    }
}
