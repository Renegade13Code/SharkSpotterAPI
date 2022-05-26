using Microsoft.IdentityModel.Tokens;
using SharkSpotterAPI.Models.Domain;
using SharkSpotterAPI.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SharkSpotterAPI.Repository
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepo;
        private readonly IConfiguration configuration;
        private readonly IUserRoleRepository userRoleRepo;

        public AuthService(IUserRepository userRepo, IConfiguration configuration, IUserRoleRepository userRoleRepo)
        {
            this.userRepo = userRepo;
            this.configuration = configuration;
            this.userRoleRepo = userRoleRepo;
        }
        public async Task<User?> AuthenticateUser(string username, string password)
        {
            var user = await userRepo.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }

            if (!ValidatePassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<User> RegisterUser(AddUserRequest addUserRequest)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHash(addUserRequest.Password, out passwordHash, out passwordSalt);
            var user = new User()
            {
                Username = addUserRequest.Username,
                Email = addUserRequest.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Firstname = addUserRequest.Firstname,
                Lastname = addUserRequest.Lastname
            };

            user = await userRepo.AddUserAync(user);
            return user;

        }
        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Firstname));
            claims.Add(new Claim(ClaimTypes.Surname, user.Lastname));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var roles = await userRoleRepo.GetUserRoles<string>(user.Id);

            if (roles != null)
            {
                roles.ForEach(role =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                });
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
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

        #region private methods
        private bool ValidatePassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        #endregion
    }
}
