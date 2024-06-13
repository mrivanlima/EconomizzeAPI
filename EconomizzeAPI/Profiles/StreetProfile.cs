using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class StreetProfile : Profile
    {
        public StreetProfile()
        {
            CreateMap<Street, StreetViewModel>().ReverseMap();
        }
    }
}
