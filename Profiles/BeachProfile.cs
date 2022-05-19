using AutoMapper;
using SharkSpotterAPI.Models;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Profiles
{
    public class BeachProfile : Profile
    {
        public BeachProfile()
        {
            CreateMap<Beach, BeachDTO>()
                .ReverseMap();
        }
    }
}
