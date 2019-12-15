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
    public class StepController : ControllerBase
    {
        private readonly IStepEntityService _entityService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public StepController(IStepEntityService entityService, IMapper mapper, IConfiguration configuration)
        {
            _entityService = entityService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<StepViewModel>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _entityService.AllAsQueryable.ToListAsync();
            var models = _mapper.Map<IEnumerable<Step>, IEnumerable<StepViewModel>>(entities);
            return Ok(models);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StepViewModel), 200)]
        public IActionResult Post([FromBody]StepPostModel model)
        {
            if(model.Keyword != _configuration["Settings:Keyword"])
            {
                return BadRequest("Wrong keyword");
            }

            try
            {
                var entity = _mapper.Map<Step>(model);
                entity.CreatedDate = DateTime.Now;
                entity = _entityService.Create(entity, false);
                _entityService.Save();
                var result = _mapper.Map<Step, StepViewModel>(entity);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("AsJson")]
        public IActionResult DownloadAsJson()
        {
            var models = _mapper.Map<IEnumerable<Step>, List<StepViewModel>>(_entityService.All);
            var json = JsonConvert.SerializeObject(models);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            MemoryStream stream = new MemoryStream(byteArray);
            return File(stream, "application/json", "Steps.json");
        }

        [HttpGet("AsCSV")]
        public IActionResult DownloadAsCSV()
        {
            var models = _mapper.Map<IEnumerable<Step>, List<StepViewModel>>(_entityService.All);
            StringBuilder result = new StringBuilder();
            result.AppendLine("Latitude;Longitude;DateTime");
            foreach (var item in models)
            {
                result.AppendLine(String.Format("{0};{1};{2};", item.Latitude, item.Longitude, item.CreatedDate));
            }
            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(result.ToString());
            MemoryStream stream = new MemoryStream(byteArray);
            return File(stream, "application/vnd.ms-excel", "Steps.csv");
        }

        [HttpGet("AsXML")]
        public IActionResult DownloadAsXML()
        {
            var models = _mapper.Map<IEnumerable<Step>, List<StepViewModel>>(_entityService.All);
            string result;
            using (var stream = new MemoryStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var serializer = new XmlSerializer(models.GetType());
                    serializer.Serialize(stream, models);
                    stream.Position = 0;
                    result = reader.ReadToEnd();
                }
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(result);
            MemoryStream resultStream = new MemoryStream(byteArray);
            return File(resultStream, "application/xml", "Steps.xml");
        }
    }
}