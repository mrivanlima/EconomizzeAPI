using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
namespace EconomizzeAPI.Profiles
{
    public class AddressDetailProfile : Profile
    {
        public AddressDetailProfile()
        {
            CreateMap<AddressDetail, AddressDetailViewModel>().ReverseMap();
        }
    }
}
