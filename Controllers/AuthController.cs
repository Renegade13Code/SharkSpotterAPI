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
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        // GET: api/<AuthController>
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromQuery] UserLoginRequest userLoginRequest)
        {
            var user = await authService.AuthenticateUser(userLoginRequest.UserName, userLoginRequest.Password);
            if (user != null)
            {
                var token = await authService.CreateToken(user);
                return Ok(token);
            }
            return BadRequest("Username or password is incorrect");
        }
    }
}
