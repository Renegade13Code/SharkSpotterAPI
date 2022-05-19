using Microsoft.EntityFrameworkCore;
using SharkSpotterAPI.Data;
using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public class FlagRepository : IFlagRepository
    {
        private readonly SharkSpotterDbContext dbContext;

        public FlagRepository(SharkSpotterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Flag> AddFlagAsync(Flag flag)
        {
            flag.Id = Guid.NewGuid();
            await dbContext.Flags.AddAsync(flag);
            await dbContext.SaveChangesAsync();
            return flag;

        }

        public async Task<IEnumerable<Flag>> GetFlagsAsync()
        {
            return await dbContext.Flags.ToListAsync();
        }

        public async Task<Flag?> GetFlagAsync(Guid id)
        {
            return await dbContext.Flags.FindAsync(id);
        }

        public async Task<Flag?> DeleteFlagAsync(Guid id)
        {
            var flag = await dbContext.Flags.FindAsync(id);
            if (flag == null)
            {
                return null;
            }
            dbContext.Flags.Remove(flag);
            await dbContext.SaveChangesAsync();
            return flag;
        }

        public async Task<Flag?> UpdateFlagAsync(Guid id, Flag flag)
        {
            var foundFlag = await dbContext.Flags.FindAsync(id);
            if(foundFlag == null)
            {
                return null;
            }
            foundFlag.Color = flag.Color;
            await dbContext.SaveChangesAsync();
            return foundFlag;
        }
    }
}
