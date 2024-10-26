using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class ProfessionalProfile : Profile
    {
        public ProfessionalProfile()
        {
            CreateMap<Professional, ProfessionalViewModel>().ReverseMap();
        }
    }
}
