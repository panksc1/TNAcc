using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class VenuesController : BaseApiController
    {
        private readonly IGenericRepository<Venue> _venuesRepo;
        private readonly IMapper _mapper;

        public VenuesController(
            IGenericRepository<Venue> venuesRepo,
            IMapper mapper)
        {
            this._venuesRepo = venuesRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Venue>>> GetVenues(
            [FromQuery]VenueSpecParams venueParams)
        {
            var spec = new VenuesSpecification(venueParams);
            var totalItems = await this._venuesRepo.CountAsync(spec);

            // Wrap in Ok() to allow us to return and IReadOnlyList
            return Ok(await this._venuesRepo.ListAsync(spec));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Venue>> GetVenueAsync(int id)
        {
            // Create a new instance of ProductsWithTypesAndBrandsSpecification
            var spec = new VenuesSpecification(id);
            // Then go to the Products Repository and get the entity with the returned
            // specification
            var venue = await this._venuesRepo.GetEntityWithSpec(spec);
            if(venue == null) return NotFound(new ApiResponse(404));

            return Ok(venue);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Venue>> UpdateVenueAsync([FromBody]Venue venue)
        {
            if (string.IsNullOrWhiteSpace(venue.Name))
            {
                return BadRequest(new ApiResponse(400));
            }

            var updated = await this._venuesRepo.UpdateEntityAsync(venue);
            if (updated == null) return NotFound(new ApiResponse(404));
            return updated;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Venue>> CreateVenueAsync([FromBody]Venue venue)
        {
            if (string.IsNullOrWhiteSpace(venue.Name))
            {
                return BadRequest(new ApiResponse(400));
            }

            var created = await this._venuesRepo.AddEntityAsync(venue);
            if (created == null) return NotFound(new ApiResponse(404));
            return created;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Venue>> DeleteVenueAsync(int id)
        {
            var deleted = await this._venuesRepo.DeleteEntityAsync(id);
            if(deleted == null) return NotFound(new ApiResponse(404));
            return deleted;
        }
    }
}