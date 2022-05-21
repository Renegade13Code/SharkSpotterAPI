using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public interface IUserRoleRepository
    {
        public Task<List<T>?> GetUserRoles<T>(Guid id);
        public Task<User_Role?> UpdateUserRoles(Guid id);
    }
}
