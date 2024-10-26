using AutoMapper;
using Economizze.Library;
using EconomizzeHybrid.Model;

namespace EconomizzeAPI.Profiles
{
    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {
            CreateMap<Prescription, PrescriptionViewModel>().ReverseMap();
        }
    }
}
