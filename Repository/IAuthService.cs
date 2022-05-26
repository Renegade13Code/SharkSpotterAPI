using SharkSpotterAPI.Models.Domain;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Repository
{
    public interface IAuthService
    {
        Task<User?> AuthenticateUser(string username, string password);
        Task<User> RegisterUser(AddUserRequest addUserRequest);
        Task<string> CreateToken(User user);
    }
}
