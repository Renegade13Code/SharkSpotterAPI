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
    public class SharkStatusController : ControllerBase
    {
        private readonly ISharkStatusRepository sharkStatRepo;
        private readonly IBeachRepository beachRepo;
        private readonly IFlagRepository flagRepo;
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;

        public SharkStatusController(ISharkStatusRepository sharkStatRepo, IBeachRepository beachRepo,
            IFlagRepository flagRepo, IUserRepository userRepo, IMapper mapper)
        {
            this.sharkStatRepo = sharkStatRepo;
            this.beachRepo = beachRepo;
            this.flagRepo = flagRepo;
            this.userRepo = userRepo;
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
        [Authorize(Roles = "Admin, Spotter")]
        public async Task<IActionResult> Post([FromBody] AddSharkStatusRequest addSharkStatusRequest)
        {
            // Validate flag, beach and user id
            if (!(await ValidateBeachID(addSharkStatusRequest.BeachId)))
            {
                return BadRequest($"Beach ID {addSharkStatusRequest.BeachId} is not valid");
            }
            if (!(await ValidateFlagID(addSharkStatusRequest.FlagId)))
            {
                return BadRequest($"Flag ID {addSharkStatusRequest.FlagId} is not valid");
            }

            if(!(await ValidateUserID(addSharkStatusRequest.UserId)))
            {
                return BadRequest($"User ID {addSharkStatusRequest.UserId} is not valid");
            }

            var SharkStatDomain = new SharkStatus()
            {
                FlagId = addSharkStatusRequest.FlagId,
                BeachId = addSharkStatusRequest.BeachId,
                UserId = addSharkStatusRequest.UserId,
                Start = addSharkStatusRequest.Start,
                End = addSharkStatusRequest.End
            };

            SharkStatDomain = await sharkStatRepo.AddSharkStatusAsync(SharkStatDomain);
            var sharkStatDTO = mapper.Map<SharkStatusDTO>(SharkStatDomain);

            return CreatedAtAction(nameof(Get), new { id = sharkStatDTO.Id }, sharkStatDTO);
        }

        //PUT api/<SharkStatusController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Spotter")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateSharkStatusRequest updateSharkStatusRequest)
        {
            // Validate flag and beach id
            if (!(await ValidateBeachID(updateSharkStatusRequest.BeachId)))
            {
                return BadRequest($"Beach ID {updateSharkStatusRequest.BeachId} is not valid");
            }
            if (!(await ValidateFlagID(updateSharkStatusRequest.FlagId)))
            {
                return BadRequest($"Flag ID {updateSharkStatusRequest.FlagId} is not valid");
            }
            if (!(await ValidateUserID(updateSharkStatusRequest.UserId)))
            {
                return BadRequest($"User ID {updateSharkStatusRequest.UserId} is not valid");
            }

            var sharkStatDomain = new SharkStatus()
            {
                FlagId = updateSharkStatusRequest.FlagId,
                BeachId = updateSharkStatusRequest.BeachId,
                UserId = updateSharkStatusRequest.UserId,
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
        [Authorize(Roles = "Admin, Spotter")]
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

        #region private methods

        private async Task<bool> ValidateBeachID(Guid beachID)
        {
            var foundBeach = await beachRepo.GetBeachAsync(beachID);
            if(foundBeach == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateFlagID(Guid flagID)
        {
            var foundflag = await flagRepo.GetFlagAsync(flagID);
            if (foundflag == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateUserID(Guid userID)
        {
            var foundUser = await userRepo.GetUserAsync(userID);
            if (foundUser == null)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
