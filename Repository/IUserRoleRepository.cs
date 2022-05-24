using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public interface IUserRoleRepository
    {
        public Task<List<T>?> GetUserRoles<T>(Guid id);
        public Task<List<Role>?> UpdateUserRoles(Guid id, IEnumerable<Role> roles);
    }
}
