using Microsoft.IdentityModel.Tokens;
using SharkSpotterAPI.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharkSpotterAPI.Repository
{
    public class MyTokenHandler : IMyTokenHandler
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository user;
        private readonly IUserRoleRepository userRoleRepo;

        public MyTokenHandler(IConfiguration configuration, IUserRepository user, IUserRoleRepository userRoleRepo)
        {
            this.configuration = configuration;
            this.user = user;
            this.userRoleRepo = userRoleRepo;
        }
        public async Task<string> CreateToken(User user)
        {
            // Create Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Firstname));
            claims.Add(new Claim(ClaimTypes.Surname, user.Lastname));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var roles = await userRoleRepo.GetUserRoles<string>(user.Id);

            if(roles != null)
            {
                roles.ForEach(role =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                });
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration.GetValue<string>("Jwt:Issuer"),
                configuration.GetValue<string>("Jwt:Audience"),
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
