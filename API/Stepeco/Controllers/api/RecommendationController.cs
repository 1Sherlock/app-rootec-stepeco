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
using Stepeco.Core.Enums;
using Stepeco.Models;

namespace Stepeco.Controllers.api
{
    [Route("api/[controller]"), Produces("application/json")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationEntityService _entityService;
        private readonly IEnvironmentRecordEntityService _environmentRecordEntityService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public RecommendationController(IRecommendationEntityService entityService, IEnvironmentRecordEntityService environmentRecordEntityService, IMapper mapper, IConfiguration configuration)
        {
            _entityService = entityService;
            _environmentRecordEntityService = environmentRecordEntityService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<RecommendationModel>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _entityService.AllAsQueryable.ToListAsync();
            var models = _mapper.Map<IEnumerable<Recommendation>, IEnumerable<RecommendationModel>>(entities);
            return Ok(models);
        }

        [HttpGet("Report")]
        [ProducesResponseType(typeof(ICollection<RecommendationViewModel>), 200)]
        public async Task<IActionResult> GetReport()
        {
            var recommendations = await _entityService.AllAsQueryable.ToListAsync();
            var models = new List<RecommendationViewModel>();
            foreach (var item in recommendations)
            {
                // implementation
            }
            return Ok(models);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RecommendationModel), 200)]
        public IActionResult Post([FromBody]RecommendationPostModel model)
        {
            if (model.Keyword != _configuration["Settings:Keyword"])
            {
                return BadRequest("Wrong keyword");
            }

            try
            {
                var entity = _mapper.Map<Recommendation>(model);
                entity.CreatedDate = DateTime.Now;
                entity = _entityService.Create(entity, false);
                _entityService.Save();
                var result = _mapper.Map<Recommendation, RecommendationModel>(entity);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(RecommendationModel), 200)]
        public async Task<IActionResult> Put([FromBody]RecommendationEditModel model)
        {
            if (model.Keyword != _configuration["Settings:Keyword"])
            {
                return BadRequest("Wrong keyword");
            }

            try
            {
                var entity = await _entityService.AllAsQueryable.FirstOrDefaultAsync(p => p.Id == model.Id);
                if (entity == null)
                    return NotFound();

                entity.Description = model.Description;
                entity.Minimum = model.Minimum;
                entity.Maximum = model.Maximum;
                entity.Type = model.Type;

                entity = _entityService.Update(entity, false);
                _entityService.Save();
                var result = _mapper.Map<Recommendation, RecommendationModel>(entity);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]RecommendationDeleteModel model)
        {
            if (model.Keyword != _configuration["Settings:Keyword"])
            {
                return BadRequest("Wrong keyword");
            }

            try
            {
                var entity = await _entityService.AllAsQueryable.FirstOrDefaultAsync(p => p.Id == model.Id);
                if (entity == null)
                    return NotFound();

                _entityService.Delete(entity.Id, false);
                _entityService.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}