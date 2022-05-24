using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharkSpotterAPI.Models.Domain;
using SharkSpotterAPI.Models.DTO;
using SharkSpotterAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharkSpotterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagController : ControllerBase
    {
        private readonly IFlagRepository flagRepo;
        private readonly IMapper mapper;

        public FlagController(IFlagRepository flagRepo, IMapper mapper)
        {
            this.flagRepo = flagRepo;
            this.mapper = mapper;
        }

        //GET: api/<FlagController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var flagsDomain = await flagRepo.GetFlagsAsync();
            var flagsDTO = mapper.Map<List<FlagDTO>>(flagsDomain);
            return Ok(flagsDTO);
        }

        // GET api/<FlagController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var flagDomain = await flagRepo.GetFlagAsync(id);
            if(flagDomain == null)
            {
                return NotFound($"Flag with id {id} not found");
            }
            var flagDTO = mapper.Map<FlagDTO>(flagDomain);
            return Ok(flagDTO);
        }

        // POST api/<FlagController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] AddFlagRequest addFlagRequest)
        {
            var flagDomain = new Flag()
            {
                Color = addFlagRequest.Color,
            };

            flagDomain = await flagRepo.AddFlagAsync(flagDomain);
            var flagDTO = mapper.Map<FlagDTO>(flagDomain);
            return CreatedAtAction(nameof(Get), new {id = flagDTO.Id}, flagDTO);
        }

        // PUT api/<FlagController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateFlagRequest updateFlagRequest)
        {
            var flagDomain = new Flag()
            {
                Color = updateFlagRequest.Color
            };

            flagDomain = await flagRepo.UpdateFlagAsync(id, flagDomain);
            if(flagDomain == null)
            {
                return NotFound($"Flag with id {id} not found");
            }

            var flagDTO = mapper.Map<FlagDTO>(flagDomain);
            return Ok(flagDTO);

        }

        //// DELETE api/<FlagController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var flagDomain = await flagRepo.DeleteFlagAsync(id);
            if (flagDomain == null)
            {
                return NotFound($"Flag with id {id} not found");
            }
            var flagDTO = mapper.Map<FlagDTO>(flagDomain);
            return Ok(flagDTO);
        }
    }
}
