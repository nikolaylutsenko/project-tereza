using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Tereza.Core;
using Project.Tereza.Responses;

namespace Project.Tereza.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeedsController : ControllerBase
    {
        private readonly IEnumerable<Need> _needs;
        private readonly IMapper _mapper;
        public NeedsController(IMapper mapper)
        {
            _mapper = mapper;
            _needs = new List<Need>
            {
                new("Laptop", "Need laptop for working needs.", 1, false),
                new("Royal Canin Sphyncx 2 kg", "I need food for my cat, please help!", 1, false),
                new("Aspirin", "Please, I can't find this drug in retail", 3, false)
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeedResponse>>> GetAllNeeds()
        {
            return await Task.Factory.StartNew(() => _mapper.Map<List<NeedResponse>>(_needs));
        }
    }
}