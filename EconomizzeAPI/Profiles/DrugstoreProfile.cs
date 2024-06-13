using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class DrugstoreProfile : Profile
    {
        public DrugstoreProfile()
        {
            CreateMap<Drugstore, DrugstoreViewModel>().ReverseMap();
        }
    }
}
