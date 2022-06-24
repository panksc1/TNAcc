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

    }
}