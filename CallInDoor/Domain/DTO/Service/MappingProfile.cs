using AutoMapper;
using Domain.DTO.Category;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryTBL, CategoryListDTO>();
        }
        //CategoryListDTO     //CategoryTBL
    }
}
