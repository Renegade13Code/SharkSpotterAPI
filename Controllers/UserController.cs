﻿using AutoMapper;
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
        public async Task<IActionResult> Get()
        {
            var usersDomain = await userRepo.GetAllUserAsync();
            var userDTO = mapper.Map<List<UserDTO>>(usersDomain);

            return Ok(userDTO);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
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
            var rolesDTO = mapper.Map<List<RoleDTO>>(roles);
            return Ok(rolesDTO);           
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserRequest addUserRequest)
        {
            var userDomain = new User()
            {
                Username = addUserRequest.Username,
                Email = addUserRequest.Email,
                Password = addUserRequest.Password,
                Firstname = addUserRequest.Firstname,
                Lastname = addUserRequest.Lastname,
            };
            userDomain = await userRepo.AddUserAync(userDomain);

            var userDTO = mapper.Map<UserDTO>(userDomain);
            return CreatedAtAction(nameof(Get), new {id = userDTO.Id} , userDTO);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
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
            return Ok(userDTO);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userDomain = await userRepo.DeleteUserAsync(id);
            if (userDomain == null)
            {
                return NotFound($"User with id {id} does not exist.");
            }

            var userDTO = mapper.Map<UserDTO>(userDomain);
            return Ok(userDTO);
        }
    }
}
