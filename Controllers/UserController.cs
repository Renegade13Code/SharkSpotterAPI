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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;
        private readonly IUserRoleRepository userRoleRepo;

        public UserController(IUserRepository userRepo, IMapper mapper, IUserRoleRepository userRoleRepo)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
            this.userRoleRepo = userRoleRepo;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var usersDomain = await userRepo.GetAllUserAsync();
            var userDTO = mapper.Map<List<UserDTO>>(usersDomain);
            // Loop over users and retrive roles?
            return Ok(userDTO);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(Guid id)
        {
           var userDomain = await userRepo.GetUserAsync(id);
            if(userDomain == null)
            {
                return NotFound($"User with id {id} not found.");
            }

            var userDTO = mapper.Map<UserDTO>(userDomain);
            //return Ok(userDTO);           
            var roles = await userRoleRepo.GetUserRoles<Role>(id);
            userDTO.Roles = mapper.Map<List<RoleDTO>>(roles);
            return Ok(userDTO);           
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserRequest addUserRequest)
        {
            var userDomain = new User()
            {
                Username = addUserRequest.Username,
                Email = addUserRequest.Email,
                // Hash password here
                Password = addUserRequest.Password,
                Firstname = addUserRequest.Firstname,
                Lastname = addUserRequest.Lastname,
            };
            userDomain = await userRepo.AddUserAync(userDomain);

            var userDTO = mapper.Map<UserDTO>(userDomain);
            var roles = await userRoleRepo.GetUserRoles<Role>(userDTO.Id);
            userDTO.Roles = mapper.Map<List<RoleDTO>>(roles);
            return CreatedAtAction(nameof(Get), new {id = userDTO.Id} , userDTO);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest updateUserRequest)
        {
            var userDomain = new User()
            {
                Username = updateUserRequest.Username,
                Email = updateUserRequest.Email,
                Password = updateUserRequest.Password,
                Firstname = updateUserRequest.Firstname,
                Lastname = updateUserRequest.Lastname
            };
            userDomain = await userRepo.UpdateUserAsync(id, userDomain);
            if(userDomain == null)
            {
                return NotFound($"User with id {id} does not exist");
            }

            var userDTO = mapper.Map<UserDTO>(userDomain);
            var roles = await userRoleRepo.GetUserRoles<Role>(id);
            userDTO.Roles = mapper.Map<List<RoleDTO>>(roles);
            return Ok(userDTO);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userDomain = await userRepo.DeleteUserAsync(id);
            if (userDomain == null)
            {
                return NotFound($"User with id {id} does not exist.");
            }

            var userDTO = mapper.Map<UserDTO>(userDomain);
            var roles = await userRoleRepo.GetUserRoles<Role>(id);
            userDTO.Roles = mapper.Map<List<RoleDTO>>(roles);
            return Ok(userDTO);
        }
    }
}
