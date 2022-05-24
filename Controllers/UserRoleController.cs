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
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository userRoleRepo;
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;
        private int roleLimit = 5;

        public UserRoleController(IUserRoleRepository userRoleRepo, IUserRepository userRepo, IMapper mapper)
        {
            this.userRoleRepo = userRoleRepo;
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        // GET api/<UserRoleController>/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get([FromRoute] Guid userId)
        {
            var roles = await userRoleRepo.GetUserRoles<Role>(userId);
            if(roles == null)
            {
                return NotFound($"User with id {userId} not found");
            }
            var rolesDTO = mapper.Map<List<RoleDTO>>(roles);
            return Ok(rolesDTO);
            
        }

        // PUT api/<UserRoleController>/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put([FromRoute] Guid userId, [FromBody] UpdateUserRolesRequest updateUserRolesRequest)
        {
            var userDomain = await userRepo.GetUserAsync(userId);
            if(userDomain == null)
            {
                return BadRequest($"User with id {userId} does not exist.");
            }

            if (updateUserRolesRequest.Roles.Count() > roleLimit)
            {
                return BadRequest($"Maximum role limit of {roleLimit} exceeded");
            }

            var roles = new List<Role>();
            if (updateUserRolesRequest.Roles != null && updateUserRolesRequest.Roles.Any())
            {
                foreach(var roleName in updateUserRolesRequest.Roles)
                {
                    roles.Add(new Role()
                    {
                        Name = roleName
                    });
                }

            }
            var rolesDomain = await userRoleRepo.UpdateUserRoles(userId, roles);
            if(rolesDomain == null)
            {
                return BadRequest("Invalid role name/s. Valid role names are: \"Admin\", \"Spotter\".");
            }
            var userDTO = mapper.Map<UserDTO>(userDomain);
            userDTO.Roles = mapper.Map<List<RoleDTO>>(rolesDomain);
            return Ok(userDTO);

        }
    }
}
