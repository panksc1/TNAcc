using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ReceivablesController: BaseApiController
    {
        private readonly IGenericRepository<Receivable> _receivablesRepo;
        private readonly IMapper _mapper;

        public ReceivablesController(
            IGenericRepository<Receivable> receivablesRepo,
            IMapper mapper)
        {
            this._receivablesRepo = receivablesRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ReceivableDto>>> GetReceivablesAsync(
            [FromQuery]PaymentSpecParams paymentParams)
        {
            var spec = new ReceivablesWithGigsAndEntitiesSpecification(paymentParams);
            var countSpec = new ReceivablesWithFiltersForCountSpecification(paymentParams);
            var totalItems = await this._receivablesRepo.CountAsync(countSpec);
            var receivables = await this._receivablesRepo.ListAsync(spec);
            
            var data = this._mapper.Map<IReadOnlyList<Receivable>, IReadOnlyList<ReceivableDto>>(receivables);

            return Ok(new Pagination<ReceivableDto>(paymentParams.PageIndex, paymentParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReceivableDto>> GetReceivableAsync(int id)
        {
            // Create a new instance of ProductsWithTypesAndBrandsSpecification
            var spec = new ReceivablesWithGigsAndEntitiesSpecification(id);
            // Then go to the Products Repository and get the entity with the returned
            // specification
            var receivable = await this._receivablesRepo.GetEntityWithSpec(spec);
            if(receivable == null) return NotFound(new ApiResponse(404));

            return this._mapper.Map<Receivable, ReceivableDto>(receivable);
        }
    }
}