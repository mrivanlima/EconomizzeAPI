﻿using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
