using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class ProfessionProfile : Profile
    {
        public ProfessionProfile()
        {
            CreateMap<Profession, ProfessionViewModel>().ReverseMap();
        }
    }
}
