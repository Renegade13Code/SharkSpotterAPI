using Microsoft.EntityFrameworkCore;
using SharkSpotterAPI.Data;
using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly SharkSpotterDbContext dbContext;
        public UserRoleRepository(SharkSpotterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // This is a generic method so that you can return List<Roles> or List<String>
        // Notice the `<T> after method name, so that the compiler understands where the T 
        // come from.
        public async Task<List<T>?> GetUserRoles<T>(Guid id)
        {
            var roleIds = await dbContext.UserRoles
                .Where(x => x.UserId == id)
                .Select(x => x.RoleId)
                .ToListAsync();

            //check this
            if (roleIds.Count() != 0 && roleIds != null)
            {
                if(typeof(T) == typeof(String))
                {
                    var roles = new List<T>();
                    foreach (var roleId in roleIds)
                    {
                        var roleName = dbContext.Roles
                            .Where(x => x.Id == roleId)
                            .Select(x => x.Name)
                            .FirstOrDefault();
                        roles.Add((T)Convert.ChangeType(roleName, typeof(T)));
                    }
                    return roles;
                }
                else if(typeof(T) == typeof(Role))
                {
                    var roles = new List<T>();
                    foreach (var roleId in roleIds)
                    {
                        var foundRole = await dbContext.Roles.FindAsync(roleId);
                        roles.Add((T)Convert.ChangeType(foundRole, typeof(T)));
                    }
                    return roles;
                }
            }
            return null;
        }

        public Task<User_Role?> UpdateUserRoles(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
