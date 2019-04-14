using dotnet_core_multi_tenancy_sample.Tenants;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_multi_tenancy_sample.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Tenant _tenant;
        public TestController(Tenant tenant)
        {
            _tenant = tenant;
        }

        // GET api/test/tenant
        [HttpGet("tenant")]
        public ActionResult<string> Get()
        {
            return _tenant.Name;
        }

    }
}
