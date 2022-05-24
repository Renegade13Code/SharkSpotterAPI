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
        public async Task<List<T>?> GetUserRoles<T>(Guid userId)
        {
            //var roleIds = await dbContext.UserRoles
            //    .Where(x => x.UserId == id)
            //    .Select(x => x.RoleId)
            //    .ToListAsync();

            var foundUser = await dbContext.Users
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (foundUser == null)
            {
                return null;
            }

            var roleIds = foundUser.UserRoles
                .Select(x => x.RoleId)
                .ToList();

            //check this
            var roles = new List<T>();
            if (roleIds.Count() != 0 && roleIds != null)
            {
                if(typeof(T) == typeof(String))
                {
                    //var roles = new List<T>();
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
                    //var roles = new List<T>();
                    foreach (var roleId in roleIds)
                    {
                        var foundRole = await dbContext.Roles.FindAsync(roleId);
                        roles.Add((T)Convert.ChangeType(foundRole, typeof(T)));
                    }
                    return roles;
                }
            }
            return roles;
        }

        public async Task<List<Role>?> UpdateUserRoles(Guid id, IEnumerable<Role> roles)
        {
            
            var userRoles = new List<Role>();

            if(roles != null && roles.Count() > 0)
            {
                // Get role ids and create and add user roles list
                foreach(var role in roles)
                {
                    // find role 
                    var foundRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name.ToLower() == role.Name.ToLower());
                    if(foundRole != null)
                    {
                        userRoles.Add(foundRole);
                        await dbContext.UserRoles.AddAsync(new User_Role()
                        {
                            Id = Guid.NewGuid(),
                            RoleId = foundRole.Id,
                            UserId = id
                        });
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            await dbContext.SaveChangesAsync();
            return userRoles;

        }
    }
}
