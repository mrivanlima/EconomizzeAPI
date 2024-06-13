using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class DrugstoreNeighborhoodSubscriptionProfile : Profile
    {
        public DrugstoreNeighborhoodSubscriptionProfile()
        {
            CreateMap<DrugstoreNeighborhoodSubscription, DrugstoreNeighborhoodSubscriptionViewModel>().ReverseMap();
        }
    }
}
