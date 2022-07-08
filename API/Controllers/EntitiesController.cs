using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EntitiesController: BaseApiController
    {
        private readonly IGenericRepository<Entity> _entitiesRepo;
        private readonly IMapper _mapper;

        public EntitiesController(
            IGenericRepository<Entity> entitiesRepo,
            IMapper mapper)
        {
            this._entitiesRepo = entitiesRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Entity>>> GetEntities()
        {
            // Wrap in Ok() to allow us to return and IReadOnlyList
            return Ok(await this._entitiesRepo.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Entity>>> GetEntities(int id)
        {
            // Wrap in Ok() to allow us to return and IReadOnlyList
            return Ok(await this._entitiesRepo.GetByIdAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Entity>> CreateEntityAsync([FromBody]Entity entity)
        {
            if ((string.IsNullOrWhiteSpace(entity.Name) && string.IsNullOrWhiteSpace(entity.Company)) ||
            (string.IsNullOrWhiteSpace(entity.Type)))
            {
                return BadRequest(new ApiResponse(400));
            }

            var created = await this._entitiesRepo.AddEntityAsync(entity);
            if (created == null) return NotFound(new ApiResponse(404));
            return created;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Entity>> UpdateEntityAsync([FromBody]Entity entity)
        {
            if ((string.IsNullOrWhiteSpace(entity.Name) && string.IsNullOrWhiteSpace(entity.Company)) ||
            (string.IsNullOrWhiteSpace(entity.Type) || (entity.Type.ToUpper() != "RECEIVABLE" && entity.Type.ToUpper() != "PAYABLE")))
            {
                return BadRequest(new ApiResponse(400));
            }

            var updated = await this._entitiesRepo.UpdateEntityAsync(entity);
            if (updated == null) return NotFound(new ApiResponse(404));
            return updated;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Entity>> DeleteEntityAsync(int id)
        {
            var deleted = await this._entitiesRepo.DeleteEntityAsync(id);
            if(deleted == null) return NotFound(new ApiResponse(404));
            return deleted;
        }

    }
}