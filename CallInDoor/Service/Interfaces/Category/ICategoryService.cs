using Domain.DTO.Category;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Category
{
    public interface ICategoryService
    {

        CategoryTBL GetById(int Id);

        //Task<List<ServiceTBL>> GetAll();

        List<CategoryListDTO> GetAll();
        Task<List<CategoryListDTO>> GetAllCateWithChildren();
        Task<bool> Create(CreateCategoryDTO model);

        Task<bool> Update(CategoryTBL categoryFromDB, CreateCategoryDTO model);


        //Task<bool> Update(ServiceTBL model);
        //Task<bool> Update(ServiceTBL serviceFromDB, CreateServiceDTO model);


    }
}
