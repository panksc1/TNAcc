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
    public class PayablesController: BaseApiController
    {
        private readonly IGenericRepository<Payable> _payablesRepo;
        private readonly IMapper _mapper;

        public PayablesController(
            IGenericRepository<Payable> payablesRepo,
            IMapper mapper)
        {
            this._payablesRepo = payablesRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<PayableDto>>> GetPayablesAsync(
            [FromQuery]PaymentSpecParams paymentParams)
        {
            var spec = new PayablesWithGigsAndEntitiesSpecification(paymentParams);
            var countSpec = new PayablesWithFiltersForCountSpecification(paymentParams);
            var totalItems = await this._payablesRepo.CountAsync(countSpec);
            var payables = await this._payablesRepo.ListAsync(spec);
            
            var data = this._mapper.Map<IReadOnlyList<Payable>, IReadOnlyList<PayableDto>>(payables);

            return Ok(new Pagination<PayableDto>(paymentParams.PageIndex, paymentParams.PageSize, totalItems, data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PayableDto>> GetPayableAsync(int id)
        {
            // Create a new instance of ProductsWithTypesAndBrandsSpecification
            var spec = new PayablesWithGigsAndEntitiesSpecification(id);
            // Then go to the Products Repository and get the entity with the returned
            // specification
            var payable = await this._payablesRepo.GetEntityWithSpec(spec);
            if(payable == null) return NotFound(new ApiResponse(404));

            return this._mapper.Map<Payable, PayableDto>(payable);
        }

        // [HttpPost]
        // public async Task<ActionResult> AddPayableAsync()
        // {

        // }

        
    }
}