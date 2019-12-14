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
    }
}