using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class ProductVersionProfile : Profile
    {
        public ProductVersionProfile()
        {
            CreateMap<ProductVersion, ProductVersionViewModel>().ReverseMap();
        }
    }
}
