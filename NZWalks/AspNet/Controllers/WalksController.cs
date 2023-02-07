using AspNet.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //Fetch data from database - domain walks
            var walksDomain =  await walkRepository.GetAllAsync();

            //Convert domain walks to DTO walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);

            //return response
            return Ok(walksDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //Get Walk Domain object from database
            var walkDomain = await walkRepository.GetAsync(id);

            //Convert Domain object to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            //Return response
            return Ok(walkDTO);
        }

        [HttpPost]

        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //Convert DTO To Domain Object
            var walkDomain = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            //Pass Domain object to Repository to persist this
            walkDomain = await walkRepository.AddAsync(walkDomain);

            //Convert the Domain object back to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
            };

            //Send DTO response back to Client
            return CreatedAtAction(nameof(GetWalkAsync),new { id= walkDTO.Id },walkDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //Convert DTO to Domain object
            var walkDomain = new Models.Domain.Walk()
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };

            //Pass Details to Repository - Get Domain object in response (or null)
            walkDomain = await walkRepository.UpdateAsync(id,walkDomain);

            // Handle Null
            if(walkDomain == null)
            {
                return NotFound();
            }
            
             //Convert back Domain to DTO
             var walkDTO = new Models.DTO.Walk()
             {
                 Length = walkDomain.Length,
                 Name = walkDomain.Name,
                 RegionId = walkDomain.RegionId,
                 WalkDifficultyId = walkDomain.WalkDifficultyId,
             };

            //Return Response
            return Ok(walkDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Get walk from database
            var walk = await walkRepository.DeleteAsync(id);

            //If Null not found
            if(walk == null)
            {
                return NotFound();
            }

            //Convert response back to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            //return Ok response
            return Ok(walkDTO);
        }
    }
}
