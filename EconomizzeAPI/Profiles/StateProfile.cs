using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
	public class StateProfile : Profile
	{
		public StateProfile()
		{
			CreateMap<State, StateViewModel>().ReverseMap();
		}
	}
}
