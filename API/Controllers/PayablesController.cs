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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PayableDto>> UpdatePayableAsync([FromBody]PayableDto payableDto)
        {
            if (!payableDto.IsValid())
            {
                return BadRequest(new ApiResponse(400));
            }

            var payable = new Payable
            {
                Id = payableDto.Id,
                AmountDue = payableDto.AmountDue,
                AmountPaid = payableDto.AmountPaid,
                DatePaid = payableDto.DatePaid,
                Method = payableDto.Method,
                Notes = payableDto.Notes,
                EntityId = payableDto.EntityId,
                GigId = payableDto.Gig.Id
            };

            var updated = await this._payablesRepo.UpdateEntityAsync(payable);
            if (updated == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Payable, PayableDto>(updated);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PayableDto>> CreatePayableAsync([FromBody]PayableDto payableDto)
        {
            if (!payableDto.IsValid())
            {
                return BadRequest(new ApiResponse(400));
            }

            var payable = new Payable
            {
                AmountDue = payableDto.AmountDue,
                AmountPaid = payableDto.AmountPaid,
                DatePaid = payableDto.DatePaid,
                Method = payableDto.Method,
                Notes = payableDto.Notes,
                EntityId = payableDto.EntityId,
                GigId = payableDto.Gig.Id
            };

            var created = await this._payablesRepo.AddEntityAsync(payable);
            if (created == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Payable, PayableDto>(created);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PayableDto>> DeletePayableAsync(int id)
        {
            var deleted = await this._payablesRepo.DeleteEntityAsync(id);
            if(deleted == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Payable, PayableDto>(deleted);
        }
    }
}