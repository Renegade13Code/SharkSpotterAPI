using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public interface IUserRepository
    {
        public Task<User> AddUserAync(User user);
        public Task<IEnumerable<User>> GetAllUserAsync();
        public Task<User?> GetUserAsync(Guid id);
        public Task<User?> UpdateUserAsync(Guid id, User user);
        public Task<User?> DeleteUserAsync(Guid id);
        public Task<User?> AuthenticateUser(string userName, string password);
    }
}
