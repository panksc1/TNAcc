using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BandsController: BaseApiController
    {
        private readonly IGenericRepository<Band> _bandsRepo;
        private readonly IMapper _mapper;

        public BandsController(
            IGenericRepository<Band> bandsRepo,
            IMapper mapper)
        {
            this._bandsRepo = bandsRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Band>>> GetBands(
            [FromQuery]BandSpecParams bandParams)
        {
            var spec = new BandsSpecification(bandParams);
            var totalItems = await this._bandsRepo.CountAsync(spec);

            // Wrap in Ok() to allow us to return and IReadOnlyList
            return Ok(await this._bandsRepo.ListAsync(spec));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Band>> GetBandAsync(int id)
        {
            // Create a new instance of ProductsWithTypesAndBrandsSpecification
            var spec = new BandsSpecification(id);
            // Then go to the Products Repository and get the entity with the returned
            // specification
            var band = await this._bandsRepo.GetEntityWithSpec(spec);
            if(band == null) return NotFound(new ApiResponse(404));

            return Ok(band);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Band>> UpdateBandAsync([FromBody]Band band)
        {
            if (string.IsNullOrWhiteSpace(band.Name))
            {
                return BadRequest(new ApiResponse(400));
            }

            var updated = await this._bandsRepo.UpdateEntityAsync(band);
            if (updated == null) return NotFound(new ApiResponse(404));
            return updated;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Band>> CreateBandAsync([FromBody]Band band)
        {
            if (string.IsNullOrWhiteSpace(band.Name))
            {
                return BadRequest(new ApiResponse(400));
            }

            var created = await this._bandsRepo.AddEntityAsync(band);
            if (created == null) return NotFound(new ApiResponse(404));
            return created;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Band>> DeleteBandAsync(int id)
        {
            var deleted = await this._bandsRepo.DeleteEntityAsync(id);
            if(deleted == null) return NotFound(new ApiResponse(404));
            return deleted;
        }
    }
}