using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_core_multi_tenancy_sample.Tenants;
using dotnet_core_multi_tenancy_sample.Tenants.DB;
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
        public ActionResult<Tenant> Get()
        {
            return _tenant;
        }

    }
}
