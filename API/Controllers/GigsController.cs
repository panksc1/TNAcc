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
        private readonly IMapper _mapper;

        public GigsController(
            IGenericRepository<Gig> gigsRepo,
            IMapper mapper)
        {
            this._gigsRepo = gigsRepo;
            this._mapper = mapper;
        }

        [HttpGet]
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


        [HttpGet("{id}")]
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GigDto>> UpdateGigAsync([FromBody]GigDto gigDto)
        {
            if (!gigDto.IsValid())
            {
                return BadRequest(new ApiResponse(400));
            }

            var gig = new Gig
            {
                Id = gigDto.Id,
                Date = gigDto.Date,
                Pay = gigDto.Pay,
                VenueId = gigDto.VenueId,
                BandId = gigDto.BandId,
                Notes = gigDto.Notes
            };

            var updated = await this._gigsRepo.UpdateEntityAsync(gig);
            if (updated == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Gig, GigDto>(updated);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GigDto>> CreateGigAsync([FromBody]GigDto gigDto)
        {
            if (!gigDto.IsValid())
            {
                return BadRequest(new ApiResponse(400));
            }

            var gig = new Gig
            {
                Date = gigDto.Date,
                Pay = gigDto.Pay,
                VenueId = gigDto.VenueId,
                BandId = gigDto.BandId,
                Notes = gigDto.Notes
            };

            var created = await this._gigsRepo.AddEntityAsync(gig);
            if (created == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Gig, GigDto>(created);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GigDto>> DeleteGigAsync(int id)
        {
            var deleted = await this._gigsRepo.DeleteEntityAsync(id);
            if(deleted == null) return NotFound(new ApiResponse(404));
            return this._mapper.Map<Gig, GigDto>(deleted);
        }
    }

}