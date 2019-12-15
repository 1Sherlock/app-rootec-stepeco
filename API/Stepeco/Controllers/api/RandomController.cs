using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stepeco.Core.BLL.Interfaces;
using Stepeco.Core.DAL.Entities;
using Stepeco.Models;

namespace Stepeco.Controllers.api
{
    [Route("api/[controller]"), Produces("application/json")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly IStepEntityService _stepEntityService;
        private readonly IEnvironmentRecordEntityService _environmentRecordEntityService;
        private readonly IMapper _mapper;
        public RandomController(IStepEntityService stepEntityService, IEnvironmentRecordEntityService environmentRecordEntityService, IMapper mapper)
        {
            _stepEntityService = stepEntityService;
            _environmentRecordEntityService = environmentRecordEntityService;
            _mapper = mapper;
        }

        [HttpPost("Steps")]
        [ProducesResponseType(typeof(ICollection<StepViewModel>), 200)]
        public IActionResult Steps()
        {
            Random random = new Random();
            List<Step> entities = new List<Step>();
            for (int i = 1; i < 1000; i++)
            {
                var step = new Step();
                var rLatitudeD = random.NextDouble();
                var rLongitudeD = random.NextDouble();
                step.Longitude = 69 + rLongitudeD;
                step.Latitude = 41 + rLatitudeD;
                step.CreatedDate = DateTime.Now;
                step = _stepEntityService.Create(step);
                entities.Add(step);
            }
            _stepEntityService.Save();
            var models = _mapper.Map<IEnumerable<Step>, IEnumerable<StepViewModel>>(entities);
            return Ok(models);
        }

        [HttpPost("EnvironmentRecords")]
        [ProducesResponseType(typeof(ICollection<EnvironmentRecordViewModel>), 200)]
        public IActionResult EnvironmentRecords()
        {
            Random random = new Random();
            List<EnvironmentRecord> entities = new List<EnvironmentRecord>();
            for (int i = 1; i < 1000; i++)
            {
                var record = new EnvironmentRecord();
                var rLatitudeD = random.NextDouble();
                var rLongitudeD = random.NextDouble();
                record.Longitude = 69 + rLongitudeD;
                record.Latitude = 41 + rLatitudeD;
                record.Quality = random.Next(0, 300);
                record.Temperature = random.Next(-20, 50);
                record.NoiseLevel = random.Next(0, 100);
                record.Humidity = random.Next(0, 50);
                record.Pressure = random.Next(0, 50);
                record.CreatedDate = DateTime.Now;
                record = _environmentRecordEntityService.Create(record);
                entities.Add(record);
            }
            _environmentRecordEntityService.Save();
            var models = _mapper.Map<IEnumerable<EnvironmentRecord>, IEnumerable<EnvironmentRecordViewModel>>(entities);
            return Ok(models);
        }
    }
}