using Microsoft.AspNetCore.Mvc;
using SharkSpotterAPI.Models.DTO;
using SharkSpotterAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharkSpotterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly IMyTokenHandler myTokenHandler;

        public AuthController(IUserRepository userRepo, IMyTokenHandler myTokenHandler)
        {
            this.userRepo = userRepo;
            this.myTokenHandler = myTokenHandler;
        }
        // GET: api/<AuthController>
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromQuery] UserLoginRequest userLoginRequest)
        {
            // Hash password before comparing
            var user = await userRepo.AuthenticateUser(userLoginRequest.UserName, userLoginRequest.Password);
            if (user != null)
            {
                var token = await myTokenHandler.CreateToken(user);
                return Ok(token);
            }
            return BadRequest("Username or password is incorrect");
        }
    }
}
