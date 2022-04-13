using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Tereza.Core;
using Project.Tereza.Requests;
using Project.Tereza.Responses;

namespace Project.Tereza.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeedsController : ControllerBase
    {
        private static readonly List<Need> _needs = new List<Need>
            {
                new("82d257a5-d72b-4f08-bcf2-76ebdc958b5f", "Laptop", "Need laptop for working needs.", 1, false),
                new("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82", "Royal Canin Sphyncx 2 kg", "I need food for my cat, please help!", 1, false),
                new("a961067e-c777-42c2-8fee-71180d750bd7", "Aspirin", "Please, I can't find this drug in retail", 3, false)
            };
        private readonly IMapper _mapper;

        public NeedsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeedResponse>>> GetAllNeedsAsync()
        {
            return await Task.Factory.StartNew(() => _mapper.Map<List<NeedResponse>>(_needs));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NeedResponse>> GetNeedByIdAsync(Guid id)
        {
            return await Task.Factory.StartNew(() => _mapper.Map<NeedResponse>(_needs.First(x => x.Id == id)));
        }

        [HttpPost]
        public async Task<IResult> AddNeedAsync(AddNeedRequest request)
        {
            // here must be validation
            var need = _mapper.Map<Need>(request);

            await Task.Factory.StartNew(() => _needs.Add(need));

            return Results.Created($"/needs/{need.Id}", need);
        }

        [HttpPut("{id:guid}")]
        public async Task<IResult> UpdateNeedAsync(Guid id, UpdateNeedRequest request)
        {
            var oldNeed = _needs.FirstOrDefault(x => x.Id == id);

            if (oldNeed is null)
            {
                return Results.NotFound();
            }

            // here must be validation

            var updatedNeed = _mapper.Map(request, oldNeed);

            await Task.Factory.StartNew(() => (_needs[_needs.IndexOf(oldNeed)] = updatedNeed));

            return Results.NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IResult> DeleteNeedAsync(Guid id)
        {
            var need = _needs.FirstOrDefault(x => x.Id == id);

            if (need is null)
            {
                return Results.NotFound();
            }

            await Task.Factory.StartNew(() => _needs.ToList().Remove(need));

            return Results.NoContent();
        }
    }
}