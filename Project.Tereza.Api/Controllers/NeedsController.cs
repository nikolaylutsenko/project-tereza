using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Tereza.Core;
using Project.Tereza.Core.Interfaces;
using Project.Tereza.Requests.Requests;
using Project.Tereza.Requests.Validators;
using Project.Tereza.Responses;
using Project.Tereza.Responses.Responses;

namespace Project.Tereza.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeedsController : ControllerBase
    {
        private readonly IService<Need> _needService;
        private readonly AddNeedRequestValidator _addNeedRequestValidator;
        private readonly UpdateNeedRequestValidator _updateNeedRequestValidator;
        private readonly Serilog.ILogger _logger;
        private readonly IMapper _mapper;

        public NeedsController(IMapper mapper, Serilog.ILogger logger, IService<Need> needService,
             AddNeedRequestValidator addNeedRequestValidator, UpdateNeedRequestValidator updateNeedRequestValidator)
        {
            _mapper = mapper;
            _logger = logger;
            _needService = needService;
            _addNeedRequestValidator = addNeedRequestValidator;
            _updateNeedRequestValidator = updateNeedRequestValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeedResponse>>> GetAllNeedsAsync()
        {
            var fluentResult = await _needService.GetAllAsync();

            if (fluentResult.IsFailed)
            {
                var errors = new List<ErrorResponse>();
                foreach (var error in fluentResult.Errors)
                {
                    errors.Add(new ErrorResponse(error.Message));
                }

                return BadRequest(errors); // todo: is it valid?
            }

            var needs = fluentResult.Value;

            return Ok(_mapper.Map<IEnumerable<NeedResponse>>(needs));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NeedResponse>> GetNeedByIdAsync(Guid id)
        {
            var fluentResult = await _needService.GetAsync(id);

            if (fluentResult.IsFailed)
            {
                var errors = new List<ErrorResponse>();
                foreach (var error in fluentResult.Errors)
                {
                    errors.Add(new ErrorResponse(error.Message));
                }

                return BadRequest(errors);
            }

            var need = fluentResult.Value;

            return Ok(_mapper.Map<NeedResponse>(need));
        }

        [HttpPost]
        public async Task<IActionResult> AddNeedAsync(AddNeedRequest request)
        {
            var validationResults = await _addNeedRequestValidator.ValidateAsync(request);

            if (!validationResults.IsValid)
            {
                var errors = _mapper.Map<List<ErrorResponse>>(validationResults.Errors);
                return BadRequest(errors);
            }

            var need = _mapper.Map<Need>(request);

            await _needService.AddAsync(need);

            return Created($"/needs/{need.Id}", need);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateNeedAsync(Guid id, UpdateNeedRequest request)
        {
            var fluentResult = await _needService.GetAsync(id);

            if (fluentResult.IsFailed)
            {
                var errors = new List<ErrorResponse>();

                foreach (var error in fluentResult.Errors)
                {
                    errors.Add(new ErrorResponse(error.Message));
                }

                return NotFound(errors);
            }

            var oldNeed = fluentResult.Value;

            var validationResults = await _updateNeedRequestValidator.ValidateAsync(request);

            if (!validationResults.IsValid)
            {
                var errors = _mapper.Map<List<ErrorResponse>>(validationResults.Errors);
                return BadRequest(errors);
            }

            var updatedNeed = _mapper.Map(request, oldNeed);

            await _needService.UpdateAsync(updatedNeed);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNeedAsync(Guid id)
        {
            var fluentResult = await _needService.GetAsync(id);

            if (fluentResult.IsFailed)
            {
                var errors = new List<ErrorResponse>();
                foreach (var error in fluentResult.Errors)
                {
                    errors.Add(new ErrorResponse(error.Message));
                }

                return NotFound(errors);
            }

            var need = fluentResult.Value;

            await _needService.DeleteAsync(need);

            return NoContent();
        }
    }
}