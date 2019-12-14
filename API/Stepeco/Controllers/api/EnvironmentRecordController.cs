using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stepeco.Core.BLL.Interfaces;
using Stepeco.Core.DAL.Entities;
using Stepeco.Models;

namespace Stepeco.Controllers.api
{
    [Route("api/[controller]"), Produces("application/json")]
    [ApiController]
    public class EnvironmentRecordController : ControllerBase
    {
        private readonly IEnvironmentRecordEntityService _entityService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public EnvironmentRecordController(IEnvironmentRecordEntityService entityService, IMapper mapper, IConfiguration configuration)
        {
            _entityService = entityService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<EnvironmentRecordViewModel>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _entityService.AllAsQueryable.ToListAsync();
            var models = _mapper.Map<IEnumerable<EnvironmentRecord>, IEnumerable<EnvironmentRecordViewModel>>(entities);
            return Ok(models);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EnvironmentRecordViewModel), 200)]
        public IActionResult Post([FromBody]EnvironmentRecordPostModel model)
        {
            if(model.Keyword != _configuration["Settings:Keyword"])
            {
                return BadRequest("Wrong keyword");
            }

            try
            {
                var entity = _mapper.Map<EnvironmentRecord>(model);
                entity.CreatedDate = DateTime.Now;
                entity = _entityService.Create(entity, false);
                _entityService.Save();
                var result = _mapper.Map<EnvironmentRecord, EnvironmentRecordViewModel>(entity);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}