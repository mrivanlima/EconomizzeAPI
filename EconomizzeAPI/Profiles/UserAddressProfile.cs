using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class UserAddressProfile :Profile
    {
        public UserAddressProfile()
        {
            CreateMap<UserAddress, UserAddressViewModel>().ReverseMap();
        }
    }
}
