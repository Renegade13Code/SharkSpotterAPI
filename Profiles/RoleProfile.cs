using AutoMapper;
using SharkSpotterAPI.Models.Domain;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDTO>()
                .ReverseMap();
        }
    }
}
