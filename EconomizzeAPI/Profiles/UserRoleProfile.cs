using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleViewModel>().ReverseMap();
        }
    }
}
