using AutoMapper;
using SharkSpotterAPI.Models.Domain;
using SharkSpotterAPI.Models.DTO;
using SharkSpotterAPI.Repository;

namespace SharkSpotterAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ReverseMap();
            
        }
    }
}
