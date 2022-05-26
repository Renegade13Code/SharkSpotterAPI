using Microsoft.EntityFrameworkCore;
using SharkSpotterAPI.Data;
using SharkSpotterAPI.Models.Domain;
/*
 * This class violates single responsibility principle!
 * CRUD operations on User_Roles should have its own class
 */
namespace SharkSpotterAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SharkSpotterDbContext dbContext;

        public UserRepository(SharkSpotterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> AddUserAync(User user)
        {
            user.Id = Guid.NewGuid();
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            // Use include user roles here?
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            return await dbContext.Users
                .Include(x => x.UserRoles) 
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        }
        //public async Task<User?> UpdateUserAsync(Guid id, User user)
        //{
        //    var foundUser = await dbContext.Users.FindAsync(id);
        //    if (foundUser == null)
        //    {
        //        return null;
        //    }

        //    foundUser.Username = user.Username;
        //    foundUser.Email = user.Email;
        //    foundUser.Password = user.Password;
        //    foundUser.Firstname = user.Firstname;
        //    foundUser.Lastname = user.Lastname;

        //    await dbContext.SaveChangesAsync();
        //    return foundUser;
        //}
        public async Task<User?> DeleteUserAsync(Guid id)
        {
            var foundUser = await dbContext.Users.FindAsync(id);
            if(foundUser == null)
            {
                return null;
            }
            dbContext.Users.Remove(foundUser);
            await dbContext.SaveChangesAsync();
            return foundUser;

        }
    }
}
