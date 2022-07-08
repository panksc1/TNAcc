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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReceivableDto>> UpdateReceivableAsync([FromBody]ReceivableDto receivableDto)
        {
            if (!receivableDto.IsValid())
            {
                return BadRequest(new ApiResponse(400));
            }

            var receivable = new Receivable
            {
                Id = receivableDto.Id,
                AmountDue = receivableDto.AmountDue,
                AmountPaid = receivableDto.AmountPaid,
                DateReceived = receivableDto.DateReceived,
                InvoiceNumber = receivableDto.InvoiceNumber,
                Method = receivableDto.Method,
                Notes = receivableDto.Notes,
                EntityId = receivableDto.EntityId,
                GigId = receivableDto.Gig.Id
            };

            var updated = await this._receivablesRepo.UpdateEntityAsync(receivable);
            if (updated == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Receivable, ReceivableDto>(updated);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReceivableDto>> CreateReceivableAsync([FromBody]ReceivableDto receivableDto)
        {
            if (!receivableDto.IsValid())
            {
                return BadRequest(new ApiResponse(400));
            }

            var receivable = new Receivable
            {
                AmountDue = receivableDto.AmountDue,
                AmountPaid = receivableDto.AmountPaid,
                DateReceived = receivableDto.DateReceived,
                Method = receivableDto.Method,
                InvoiceNumber = receivableDto.InvoiceNumber,
                Notes = receivableDto.Notes,
                EntityId = receivableDto.EntityId,
                GigId = receivableDto.Gig.Id
            };

            var created = await this._receivablesRepo.AddEntityAsync(receivable);
            if (created == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Receivable, ReceivableDto>(created);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReceivableDto>> DeleteReceivableAsync(int id)
        {
            var deleted = await this._receivablesRepo.DeleteEntityAsync(id);
            if(deleted == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Receivable, ReceivableDto>(deleted);
        }
    }
}