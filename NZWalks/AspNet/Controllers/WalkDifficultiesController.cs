using AspNet.Data;
using AspNet.Models.DTO;
using AspNet.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDomain = await walkDifficultyRepository.GetAllAsync();

            var walkDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDomain);

            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if(walkDifficulty == null)
            {
                return NotFound();
            }

            //Convert Domain to DTOs
            var walkDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Convert DTO to Domain
            var walkDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code,
            };

            //Call repository
            walkDomain = await walkDifficultyRepository.AddAsync(walkDomain);

            //Convert back to DTO
            var walkDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDomain);

            //Return Ok Response

            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //Convert DTO to Domain Model
            var walkDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code,
            };

            //Call repository to update
            walkDomain = await walkDifficultyRepository.UpdateAsync(id, walkDomain);

            if(walkDomain == null) 
            { 
                return NotFound(); 
            }

            //Convert Domain to DTO
            var walkDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDomain);

            //Return response
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            //Get walk from database
            var walk = await walkDifficultyRepository.DeleteAsync(id);

            //If Null then Not Found
            if(walk == null)
            {
                return NotFound();
            }

            //Convert response back to DTO
            var walkDTO = mapper.Map<Models.DTO.WalkDifficulty>(walk);

            //Return response
            return Ok(walkDTO);
        }

    }
}
