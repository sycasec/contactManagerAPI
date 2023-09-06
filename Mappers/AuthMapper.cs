using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.AuthModels;

namespace contactManagerAPI.Mappers
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<User, RegisterModel>().ReverseMap();
            CreateMap<User, LoginModel>().ReverseMap();
        }
    }
}
