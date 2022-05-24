using SharkSpotterAPI.Models;

namespace SharkSpotterAPI.Repository
{
    public interface IBeachRepository
    {
        public Task<Beach> AddBeachAsync(Beach beach);
        public Task<IEnumerable<Beach>> GetAllBeachAsync();
        public Task<Beach?> GetBeachAsync(Guid id);
        public Task<Beach?> UpdateBeach(Guid id, Beach beach);
        public Task<Beach?> DeleteBeach(Guid id);
    }
}
