using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class QuoteProductProfile : Profile
    {
        public QuoteProductProfile()
        {
            CreateMap<QuoteProduct, QuoteProductViewModel>().ReverseMap();
        }
    }
}
