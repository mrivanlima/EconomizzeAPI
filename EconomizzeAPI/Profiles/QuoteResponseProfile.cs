using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class QuoteResponseProfile : Profile
    {
        public QuoteResponseProfile()
        {
            CreateMap<QuoteResponse, QuoteResponseViewModel>().ReverseMap();
        }
    }
}
