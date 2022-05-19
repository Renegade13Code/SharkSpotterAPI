using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public interface ISharkStatusRepository
    {
        public Task<SharkStatus> AddSharkStatusAsync(SharkStatus sharkStatus);
        public Task<IEnumerable<SharkStatus>> GetSharkStatusAsync();
        public Task<SharkStatus?> GetSharkStatusAsync(Guid id);

        // Get current shark status by beach id
        public Task<SharkStatus> GetLatestSharkStatByBeachAsync(Guid beachId);

        // Get all shark status by beach id
        public Task<IEnumerable<SharkStatus>> GetAllSharkStatByBeachAsync(Guid beachId);

        public Task<SharkStatus?> UpdateSharkStatusAsync(Guid id, SharkStatus sharkStatus);

        // Update current shark status by beach id

        public Task<SharkStatus?> DeleteSharkStatusAsync(Guid id);
    }
}
