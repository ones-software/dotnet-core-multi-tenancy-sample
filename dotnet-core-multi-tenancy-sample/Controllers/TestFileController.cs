using dotnet_core_multi_tenancy_sample.Tenants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace dotnet_core_multi_tenancy_sample.Controllers
{
    [Route("api/test-file")]
    [ApiController]
    public class TestFileController : ControllerBase
    {
        private readonly TenantFileService _tenantFileService;
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public TestFileController(TenantFileService tenantFileService
            , FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _tenantFileService = tenantFileService;
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        // POST api/test-file/
        [HttpPost]
        public bool Post([FromForm] IFormFile file)
        {
            _tenantFileService.Save(file);
            return true;
        }

        // GET api/test-file/{fileName}
        [HttpGet("{fileName}")]
        public IActionResult Post(string fileName)
        {
            var memoryStream = _tenantFileService.Load(fileName);
            return File(memoryStream, GetContentType(fileName));
        }

        public string GetContentType(string fileName)
        {
            if (!_fileExtensionContentTypeProvider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}