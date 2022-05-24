using Microsoft.AspNetCore.Mvc;
using SharkSpotterAPI.Models.DTO;
using AutoMapper;
using SharkSpotterAPI.Models;
using SharkSpotterAPI.Repository;
using Microsoft.AspNetCore.Authorization;

namespace SharkSpotterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeachController : ControllerBase
    {
        private readonly IBeachRepository beachRepo;
        private readonly IMapper mapper;

        public BeachController(IBeachRepository beachRepo, IMapper mapper)
        {
            this.beachRepo = beachRepo;
            this.mapper = mapper;
        }

        // GET: api/<BeachController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var BeachesDomain = await beachRepo.GetAllBeachAsync();
            var BeachesDTO = mapper.Map<List<BeachDTO>>(BeachesDomain);
            return Ok(BeachesDTO);
        }

        // GET api/<BeachController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var beachDomain = await beachRepo.GetBeachAsync(id);

            if(beachDomain == null)
            {
                return NotFound($"Beach with id {id} not found");
            }

            var beachDTO = mapper.Map<BeachDTO>(beachDomain);

            return Ok(beachDTO);
        }

        // POST api/<BeachController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] AddBeachRequest addBeachRequest)
        {
            var BeachDomain = new Beach()
            {
                Name = addBeachRequest.Name,
                Geolocation = addBeachRequest.Geolocation,
                Latitude = addBeachRequest.Latitude,
                Longitude = addBeachRequest.Longitude
            };

            BeachDomain = await beachRepo.AddBeachAsync(BeachDomain);

            var BeachDTO = mapper.Map<BeachDTO>(BeachDomain);

            return CreatedAtAction(nameof(Get), new {id = BeachDTO.Id }, BeachDTO);
        }

        // PUT api/<BeachController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateBeachRequest updateBeachRequest)
        {
            var beachDomain = new Beach()
            {
                Name = updateBeachRequest.Name,
                Geolocation = updateBeachRequest.Geolocation,
                Latitude = updateBeachRequest.Latitude,
                Longitude = updateBeachRequest.Longitude
            };
                
            beachDomain = await beachRepo.UpdateBeach(id, beachDomain);
            if(beachDomain == null)
            {
                return NotFound($"Beach with id {id} does not exist");
            }
            var BeachDTO = mapper.Map<BeachDTO>(beachDomain);
            return Ok(BeachDTO);

        }

        // DELETE api/<BeachController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var beachDomain = await beachRepo.DeleteBeach(id);
            if(beachDomain == null)
            {
                return NotFound($"Beach with id {id} does not exist");
            }
            var beachDTO = mapper.Map<BeachDTO>(beachDomain);
            return Ok(beachDTO);
        }
    }
}
