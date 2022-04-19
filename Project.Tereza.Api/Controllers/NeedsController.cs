using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MDEvents.Services.Specification;
using MDEvents.Services.Specification.SpecificationParameters;
using Microsoft.AspNetCore.Mvc;
using Project.Tereza.Core;
using Project.Tereza.Core.Interfaces;
using Project.Tereza.Requests.Requests;
using Project.Tereza.Requests.Requests.Specifications;
using Project.Tereza.Requests.Validators;
using Project.Tereza.Requests.Validators.Specifications;
using Project.Tereza.Responses;
using Project.Tereza.Responses.Responses;
using Project.Tereza.Responses.Responses.Paged;

namespace Project.Tereza.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeedsController : ControllerBase
    {
        private readonly IService<Need> _needService;
        private readonly AddNeedRequestValidator _addNeedRequestValidator;
        private readonly UpdateNeedRequestValidator _updateNeedRequestValidator;
        private readonly NeedSpecificationRequestValidator _needSpecificationRequestValidator;
        private readonly Serilog.ILogger _logger;
        private readonly IMapper _mapper;

        public NeedsController(IMapper mapper, Serilog.ILogger logger, IService<Need> needService,
             AddNeedRequestValidator addNeedRequestValidator, UpdateNeedRequestValidator updateNeedRequestValidator,
             NeedSpecificationRequestValidator needSpecificationRequestValidator)
        {
            _mapper = mapper;
            _logger = logger;
            _needService = needService;
            _addNeedRequestValidator = addNeedRequestValidator;
            _updateNeedRequestValidator = updateNeedRequestValidator;
            _needSpecificationRequestValidator = needSpecificationRequestValidator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<Need>>> GetAllNeedsAsync([FromQuery] NeedSpecificationRequest specificationRequest)
        {

            var validationResults = await _needSpecificationRequestValidator.ValidateAsync(specificationRequest);

            if (!validationResults.IsValid)
            {
                return BadRequest(_mapper.Map<IEnumerable<ErrorResponse>>(validationResults.Errors));
            }

            var specificationParameters = _mapper.Map<SpecificationParameters<Need>>(specificationRequest);

            var specification = new NeedSpecification(specificationParameters);

            var fluentResult = await _needService.FindAsync(specification);

            if (fluentResult.IsFailed)
            {
                var errors = new List<ErrorResponse>();
                foreach (var error in fluentResult.Errors)
                {
                    errors.Add(new ErrorResponse(error.Message));
                }

                return BadRequest(errors); // todo: is it valid?
            }

            var (items, count) = fluentResult.Value;

            return Ok(new PagedResponse<NeedResponse> { Skip = specificationRequest.Skip, Take = specificationRequest.Take, TotalItems = count, Data = _mapper.Map<IEnumerable<NeedResponse>>(items) });
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