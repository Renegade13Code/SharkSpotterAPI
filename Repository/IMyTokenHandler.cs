using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Repository
{
    public interface IMyTokenHandler
    {
        Task<string> CreateToken(User user);
    }
}
