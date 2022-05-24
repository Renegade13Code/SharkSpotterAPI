using Microsoft.EntityFrameworkCore;
using SharkSpotterAPI.Data;
using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public class SharkStatusRepository : ISharkStatusRepository
    {
        private readonly SharkSpotterDbContext dbContext;
        public SharkStatusRepository(SharkSpotterDbContext DbContext)
        {
            dbContext = DbContext;
        }

        // CRUD operations
        public async Task<SharkStatus> AddSharkStatusAsync(SharkStatus sharkStatus)
        {
            sharkStatus.Id = Guid.NewGuid();
            await dbContext.SharkStatuses.AddAsync(sharkStatus);
            await dbContext.SaveChangesAsync();
            return sharkStatus;
        }

        public async Task<IEnumerable<SharkStatus>> GetSharkStatusAsync()
        {
            return await dbContext.SharkStatuses
                .Include(x => x.Beach)
                .Include(x => x.Flag)
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<SharkStatus?> GetSharkStatusAsync(Guid id)
        {
            return await dbContext.SharkStatuses
                .Include(x => x.Beach)
                .Include(x => x.Flag)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<SharkStatus?> UpdateSharkStatusAsync(Guid id, SharkStatus sharkStatus)
        {
            var foundSharkStat = await dbContext.SharkStatuses.FindAsync(id);
            if(foundSharkStat == null)
            {
                return null;
            }
            foundSharkStat.FlagId = sharkStatus.FlagId;
            foundSharkStat.BeachId = sharkStatus.BeachId;
            foundSharkStat.Start = sharkStatus.Start;
            foundSharkStat.End = sharkStatus.End;
            foundSharkStat.UserId = sharkStatus.UserId;

            await dbContext.SaveChangesAsync();
            return foundSharkStat;
        }

        public async Task<SharkStatus?> DeleteSharkStatusAsync(Guid id)
        {
            var sharkStatus = await dbContext.SharkStatuses.FindAsync(id);
            if(sharkStatus == null)
            {
                return null;
            }
            dbContext.SharkStatuses.Remove(sharkStatus);
            await dbContext.SaveChangesAsync();
            return sharkStatus;
        }

        // Get all shark status by beach id
        public async Task<IEnumerable<SharkStatus>> GetAllSharkStatByBeachAsync(Guid beachId)
        {
            return await dbContext.SharkStatuses
                .Include(x => x.Beach)
                .Include(x => x.Flag)
                .Include(x => x.User)
                .Where(x => x.BeachId == beachId)
                .OrderByDescending(x => x.Start)
                .ToListAsync();
        }

        // Get current shark status by beach id
        public async Task<SharkStatus> GetLatestSharkStatByBeachAsync(Guid beachId)
        {
            return await dbContext.SharkStatuses
                .Include(x => x.Beach)
                .Include(x => x.Flag)
                .Include(x => x.User)
                .Where(x => x.BeachId == beachId)
                .OrderByDescending(x => x.Start)
                .FirstAsync();
        }
    }
}
