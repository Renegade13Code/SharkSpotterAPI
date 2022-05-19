using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public interface IFlagRepository
    {
        public Task<Flag> AddFlagAsync(Flag flag);
        public Task<IEnumerable<Flag>> GetFlagsAsync();
        public Task<Flag?> GetFlagAsync(Guid id);
        public Task<Flag?> UpdateFlagAsync(Guid id, Flag flag);
        public Task<Flag?> DeleteFlagAsync(Guid id);
    }
}
