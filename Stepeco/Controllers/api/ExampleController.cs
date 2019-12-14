using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Stepeco.Core.DAL.Entities;

namespace Stepeco.Controllers.api
{
    [Route("api/[controller]/[action]")]
    public class ExampleController : Controller
    {
        private static readonly List<EnvironmentRecord> Records = new List<EnvironmentRecord>
        {
            new EnvironmentRecord { Id = 1, Temperature = 10, LocationX = 41, LocationY = 69 },
            new EnvironmentRecord { Id = 2, Temperature = 20, LocationX = 41, LocationY = 69 },
            new EnvironmentRecord { Id = 3, Temperature = 30, LocationX = 41, LocationY = 69 }
        };

        [HttpGet]
        public IEnumerable<EnvironmentRecord> GetAll()
        {
            return Records.ToArray();
        }

        [HttpPost]
        public ActionResult Post([FromBody] EnvironmentRecord region)
        {
            Records.Add(region);
            return Ok(region);
        }
    }
}