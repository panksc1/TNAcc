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
    public class GigsController : BaseApiController
    {
        private readonly IGenericRepository<Gig> _gigsRepo;
        private readonly IGenericRepository<Venue> _venuesRepo;
        private readonly IGenericRepository<Entity> _entitiesRepo;
        private readonly IGenericRepository<Payable> _payablesRepo;
        private readonly IGenericRepository<Receivable> _receivablesRepo;
        private readonly IMapper _mapper;

        public GigsController(
            IGenericRepository<Gig> gigsRepo,
            IGenericRepository<Venue> venuesRepo,
            IGenericRepository<Entity> entitiesRepo,
            IGenericRepository<Payable> payablesRepo,
            IGenericRepository<Receivable> receivablesRepo,
            IMapper mapper)
        {
            this._gigsRepo = gigsRepo;
            this._venuesRepo = venuesRepo;
            this._entitiesRepo = entitiesRepo;
            this._payablesRepo = payablesRepo;
            this._receivablesRepo = receivablesRepo;
            this._mapper = mapper;
        }

        [HttpGet("payables")]
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


        [HttpGet("/payables/{id}")]
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

        [HttpGet("receivables")]
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


        [HttpGet("/receivables/{id}")]
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

        [HttpGet("entities")]
        public async Task<ActionResult<IReadOnlyList<Entity>>> GetEntities()
        {
            // Wrap in Ok() to allow us to return and IReadOnlyList
            return Ok(await this._entitiesRepo.ListAllAsync());
        }

        [HttpGet("venues")]
        public async Task<ActionResult<IReadOnlyList<Entity>>> GetVenues()
        {
            // Wrap in Ok() to allow us to return and IReadOnlyList
            return Ok(await this._venuesRepo.ListAllAsync());
        }

        [HttpGet("gigs")]
        public async Task<ActionResult<Pagination<GigDto>>> GetGigsAsync(
            [FromQuery]GigSpecParams gigParams)
        {
            var spec = new GigsWithVenuesSpecification(gigParams);
            var countSpec = new GigsWithFiltersForCountSpecification(gigParams);
            var totalItems = await this._gigsRepo.CountAsync(countSpec);
            var gigs = await this._gigsRepo.ListAsync(spec);
            
            var data = this._mapper.Map<IReadOnlyList<Gig>, IReadOnlyList<GigDto>>(gigs);

            return Ok(new Pagination<GigDto>(gigParams.PageIndex, gigParams.PageSize, totalItems, data));
        }


        [HttpGet("/gigs/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GigDto>> GetGigAsync(int id)
        {
            // Create a new instance of ProductsWithTypesAndBrandsSpecification
            var spec = new GigsWithVenuesSpecification(id);
            // Then go to the Products Repository and get the entity with the returned
            // specification
            var gig = await this._gigsRepo.GetEntityWithSpec(spec);
            if(gig == null) return NotFound(new ApiResponse(404));

            return this._mapper.Map<Gig, GigDto>(gig);
        }
    }
}