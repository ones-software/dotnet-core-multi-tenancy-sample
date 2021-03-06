﻿using dotnet_core_multi_tenancy_sample.DB;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_multi_tenancy_sample.Controllers
{
    [Route("api/test-db")]
    [ApiController]
    public class TestDbController : ControllerBase
    {

        private readonly TenantDbContext _tenantDbContext;
        public TestDbController(TenantDbContext tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
        }

        // GET api/test-db/{key}
        [HttpGet("{key}")]
        public ActionResult<string> Get(string key)
        {
            var cacheData = _tenantDbContext.Caches.Find(key);
            if (cacheData == null)
                return new NotFoundResult();

            return cacheData.Value;
        }

        // POST api/test/{key}
        [HttpPost("{key}")]
        public bool Post(string key, [FromForm] string value)
        {
            var cacheData = _tenantDbContext.Caches.Find(key);
            if (cacheData == null)
            {
                cacheData = new Cache {Key = key};
                _tenantDbContext.Add(cacheData);

            }

            cacheData.Value = value;
            _tenantDbContext.SaveChanges();

            return true;
        }
    }
}