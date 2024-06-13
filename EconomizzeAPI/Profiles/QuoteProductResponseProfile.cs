using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class QuoteProductResponseProfile : Profile
    {
        public QuoteProductResponseProfile()
        {
            CreateMap<QuoteProductResponse, QuoteProductResponseViewModel>().ReverseMap();
        }
    }
}
