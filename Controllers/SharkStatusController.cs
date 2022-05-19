using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SharkSpotterAPI.Models.Domain;
using SharkSpotterAPI.Models.DTO;
using SharkSpotterAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharkSpotterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharkStatusController : ControllerBase
    {
        private readonly ISharkStatusRepository sharkStatRepo;
        private readonly IMapper mapper;

        public SharkStatusController(ISharkStatusRepository sharkStatRepo, IMapper mapper)
        {
            this.sharkStatRepo = sharkStatRepo;
            this.mapper = mapper;
        }


        #region StandardCRUD    
        // GET: api/<SharkStatusController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sharkStats = await sharkStatRepo.GetSharkStatusAsync();
            var sharkStatsDTO = mapper.Map<List<SharkStatusDTO>>(sharkStats);
            return Ok(sharkStatsDTO);
        }

        // GET api/<SharkStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sharkStat = await sharkStatRepo.GetSharkStatusAsync(id);
            if(sharkStat == null)
            {
                return NotFound($"Shark Status with id {id} not found");
            }

            var sharkStatDTO = mapper.Map<SharkStatusDTO>(sharkStat);

            return Ok(sharkStatDTO);
        }

        //// POST api/<SharkStatusController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSharkStatusRequest addSharkStatusRequest)
        {
            var SharkStatDomain = new SharkStatus()
            {
                FlagId = addSharkStatusRequest.FlagId,
                BeachId = addSharkStatusRequest.BeachId,
                Start = addSharkStatusRequest.Start,
                End = addSharkStatusRequest.End
            };

            SharkStatDomain = await sharkStatRepo.AddSharkStatusAsync(SharkStatDomain);
            var sharkStatDTO = mapper.Map<SharkStatusDTO>(SharkStatDomain);

            return Ok(sharkStatDTO);
        }

        //PUT api/<SharkStatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateSharkStatusRequest updateSharkStatusRequest)
        {
            var sharkStatDomain = new SharkStatus()
            {
                FlagId = updateSharkStatusRequest.FlagId,
                BeachId = updateSharkStatusRequest.BeachId,
                Start = updateSharkStatusRequest.Start,
                End = updateSharkStatusRequest.End
            };
            
            sharkStatDomain = await sharkStatRepo.UpdateSharkStatusAsync(id, sharkStatDomain);
            if(sharkStatDomain == null)
            {
                return NotFound($"Shark Status with id {id} not found");
            }

            var sharkStatusDTO = mapper.Map<SharkStatusDTO>(sharkStatDomain);
            return Ok(sharkStatusDTO);


        }

        // DELETE api/<SharkStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sharkStatDomain = await sharkStatRepo.DeleteSharkStatusAsync(id);
            if(sharkStatDomain == null)
            {
                return NotFound($"Shark Status with id {id} not found");
            }

            var sharkStatDTO = mapper.Map<SharkStatusDTO>(sharkStatDomain);
            return Ok(sharkStatDTO);
        }
        #endregion

        #region GetByBeachId
        [HttpGet]
        [Route("filter/beach/{id}")]
        public async Task<IActionResult> GetAllByBeachId([FromRoute] Guid id)
        {
            var sharkStatsDomain = await sharkStatRepo.GetAllSharkStatByBeachAsync(id);
            var sharkStatsDTO = mapper.Map<List<SharkStatusDTO>>(sharkStatsDomain);
            return Ok(sharkStatsDTO);
        }

        [HttpGet]
        [Route("latest/beach/{id}")]
        public async Task<IActionResult> GetLatestByBeachId([FromRoute] Guid id)
        {
            var sharkStatsDomain = await sharkStatRepo.GetLatestSharkStatByBeachAsync(id);
            var sharkStatsDTO = mapper.Map<SharkStatusDTO>(sharkStatsDomain);
            return Ok(sharkStatsDTO);
        }
        #endregion
    }
}
