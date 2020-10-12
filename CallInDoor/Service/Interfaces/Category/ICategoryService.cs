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
        //Task<CategoryTBL> Create(CreateCategoryDTO model);
        Task<CategoryTBL> Create(CreateCategoryDTO model);
        Task<bool> Update(CategoryTBL categoryFromDB, CreateCategoryDTO model);


        //Task<bool> Update(ServiceTBL model);
        //Task<bool> Update(ServiceTBL serviceFromDB, CreateServiceDTO model);








        //public Task<List<AreaTBL>> GetAreaById(int Id)

         Task<AreaTBL> GetAreaById(int Id);
        Task<AreaTBL> UpdateArea(AreaTBL areaFreomDB, CreateAreaDTO model);
        Task<AreaTBL> CreateArea(CreateAreaDTO model);
        Task<(bool succsseded, List<string> result)> ValidateArea(CreateAreaDTO model);
        Task<(bool succsseded, List<string> result)> ValidateAreaForEdit(CreateAreaDTO model, AreaTBL areaFromDB);


    }
}
