using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Tereza.Core;
using Project.Tereza.Core.Interfaces;
using Project.Tereza.Requests;
using Project.Tereza.Responses;

namespace Project.Tereza.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeedsController : ControllerBase
    {
        private readonly IService<Need> _needService;
        private readonly Serilog.ILogger _logger;
        private readonly IMapper _mapper;

        public NeedsController(IMapper mapper, Serilog.ILogger logger, IService<Need> needService)
        {
            _mapper = mapper;
            _logger = logger;
            _needService = needService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeedResponse>>> GetAllNeedsAsync()
        {
            var needs = await _needService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<NeedResponse>>(needs));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NeedResponse>> GetNeedByIdAsync(Guid id)
        {
            var need = await _needService.GetAsync(id);

            return Ok(_mapper.Map<NeedResponse>(need));
        }

        [HttpPost]
        public async Task<IActionResult> AddNeedAsync(AddNeedRequest request)
        {
            // here must be validation
            var need = _mapper.Map<Need>(request);

            await _needService.AddAsync(need);

            return Created($"/needs/{need.Id}", need);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateNeedAsync(Guid id, UpdateNeedRequest request)
        {
            var oldNeed = await _needService.GetAsync(id);

            if (oldNeed is null)
            {
                return NotFound();
            }

            // here must be validation

            var updatedNeed = _mapper.Map(request, oldNeed);

            await _needService.UpdateAsync(updatedNeed);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNeedAsync(Guid id)
        {
            var need = await _needService.GetAsync(id);

            if (need is null)
            {
                return NotFound();
            }

            await _needService.DeleteAsync(need);

            return NoContent();
        }
    }
}