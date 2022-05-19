using AutoMapper;
using SharkSpotterAPI.Models.Domain;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Profiles
{
    public class FlagProfile : Profile
    {
        public FlagProfile()
        {
            CreateMap<Flag, FlagDTO>()
                .ReverseMap();
        }
    }
}
