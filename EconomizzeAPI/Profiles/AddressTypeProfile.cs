using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class AddressTypeProfile : Profile
    {
        public AddressTypeProfile()
        {
            CreateMap<AddressType, AddressTypeViewModel>().ReverseMap();
        }
    }
}
