using AutoMapper;
using SharkSpotterAPI.Models.Domain;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Profiles
{
    public class SharkStatusProfile : Profile
    {
        public SharkStatusProfile()
        {
            CreateMap<SharkStatus, SharkStatusDTO>()
                .ReverseMap();
        }
    }
}
