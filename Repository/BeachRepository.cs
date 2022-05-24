using SharkSpotterAPI.Data;
using SharkSpotterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SharkSpotterAPI.Repository
{
    public class BeachRepository : IBeachRepository
    {
        private readonly SharkSpotterDbContext dbContext;

        public BeachRepository(SharkSpotterDbContext DbContext)
        {
            dbContext = DbContext;
        }

        public async Task<Beach> AddBeachAsync(Beach beach)
        {
            beach.Id = Guid.NewGuid();
            await dbContext.Beaches.AddAsync(beach);
            await dbContext.SaveChangesAsync();
            return beach;
            
        }
        public async Task<IEnumerable<Beach>> GetAllBeachAsync()
        {
            return await dbContext.Beaches.ToListAsync(); 
        }

        public async Task<Beach?> GetBeachAsync(Guid id)
        {
            return await dbContext.Beaches.FindAsync(id);
        }

        public async Task<Beach?> UpdateBeach(Guid id, Beach beach)
        {
            var foundBeach = await dbContext.Beaches.FindAsync(id);
            if (foundBeach == null)
            {
                return null;
            }
            foundBeach.Name = beach.Name;
            foundBeach.Geolocation = beach.Geolocation;
            foundBeach.Latitude = beach.Latitude;
            foundBeach.Longitude = beach.Longitude;

            await dbContext.SaveChangesAsync();
            return foundBeach;
        }

        public async Task<Beach?> DeleteBeach(Guid id)
        {
            var beach = await dbContext.Beaches.FindAsync(id);
            
            if(beach == null)
            {
                return null;
            }
            dbContext.Beaches.Remove(beach);
            await dbContext.SaveChangesAsync();
            return beach;
        }
    }
}
