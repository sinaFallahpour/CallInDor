﻿using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Account
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<AppUser, ProfileGetDTO>();
            CreateMap<User_Degree_Field, UserDegreeDTO>();
        }
    }
}
