using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class ContactTypeProfile : Profile
    {
        public ContactTypeProfile()
        {
            CreateMap<ContactType, ContactTypeViewModel>().ReverseMap();
        }
    }
}
