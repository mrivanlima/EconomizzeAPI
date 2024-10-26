using AutoMapper;
using Economizze.Library;
using EconomizzeHybrid.Model;

namespace EconomizzeAPI.Profiles
{
    public class QuotePrescriptionProfile : Profile
    {
        public QuotePrescriptionProfile()
        {
            CreateMap<QuotePrescription, QuotePrescriptionViewModel>().ReverseMap();
        }
    }
}
