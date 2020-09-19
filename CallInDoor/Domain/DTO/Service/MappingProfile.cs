using AutoMapper;
using Domain.DTO.Account;
using Domain.DTO.Category;
using Domain.Entities;

namespace Domain.DTO.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryTBL, CategoryListDTO>();
        }
    }
}
