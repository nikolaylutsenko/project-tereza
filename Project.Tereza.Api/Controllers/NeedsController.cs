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
        private static readonly List<Need> _needs = new List<Need>
            {
                new("82d257a5-d72b-4f08-bcf2-76ebdc958b5f", "Laptop", "Need laptop for working needs.", 1, false),
                new("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82", "Royal Canin Sphyncx 2 kg", "I need food for my cat, please help!", 1, false),
                new("a961067e-c777-42c2-8fee-71180d750bd7", "Aspirin", "Please, I can't find this drug in retail", 3, false)
            };
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
        public async Task<IResult> AddNeedAsync(AddNeedRequest request)
        {
            // here must be validation
            var need = _mapper.Map<Need>(request);

            await _needService.AddAsync(need);

            return Results.Created($"/needs/{need.Id}", need);
        }

        [HttpPut("{id:guid}")]
        public async Task<IResult> UpdateNeedAsync(Guid id, UpdateNeedRequest request)
        {
            var oldNeed = await _needService.GetAsync(id);

            if (oldNeed is null)
            {
                return Results.NotFound();
            }

            // here must be validation

            var updatedNeed = _mapper.Map(request, oldNeed);

            await _needService.UpdateAsync(updatedNeed);

            return Results.NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IResult> DeleteNeedAsync(Guid id)
        {
            var need = await _needService.GetAsync(id);

            if (need is null)
            {
                return Results.NotFound();
            }

            await _needService.DeleteAsync(need);

            return Results.NoContent();
        }
    }
}