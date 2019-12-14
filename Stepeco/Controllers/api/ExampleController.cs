using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Stepeco.Controllers.DAL.Entities;

namespace Stepeco.Controllers.api
{
    [Route("api/[controller]/[action]")]
    public class ExampleController : Controller
    {
        private static readonly List<Region> Regions = new List<Region>
        {
            new Region { Id = 1, Temperature = 10, LocationX = 41, LocationY = 69 },
            new Region { Id = 2, Temperature = 20, LocationX = 41, LocationY = 69 },
            new Region { Id = 3, Temperature = 30, LocationX = 41, LocationY = 69 }
        };
        [HttpGet]
        public IEnumerable<Region> GetRegions()
        {
            return Regions.ToArray();
        }

        [HttpPost]
        public ActionResult CreateRegion([FromBody] Region region)
        {
            Regions.Add(region);
            return Ok(region);
        }
    }
}