using AutoMapper;
using Domain;
using Domain.DTO.Category;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {


        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryService(
            DataContext context,
             IMapper mapper
               )
        {
            _context = context;
            _mapper = mapper;

        }


        /// <summary>
        /// get by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CategoryTBL GetById(int Id)
        {
            return _context.CategoryTBL.Find(Id);
        }





        /// <summary>
        /// get   All
        /// </summary>
        public List<CategoryListDTO> GetAll()
        {
            return _context.CategoryTBL.Where(c => c.IsEnabled == true).Select(c => new CategoryListDTO
            {
                Id = c.Id,
                IsEnabled = c.IsEnabled,
                ParentId = c.ParentId,
                PersianTitle = c.PersianTitle,
                ServiceId = c.ServiceId,
                Title = c.Title,
            }).ToList();
        }





        /// <summary>
        ///get Al category with its Children
        /// </summary>
        public async Task<List<CategoryListDTO>> GetAllCateWithChildren()
        {
            var cats = await _context.CategoryTBL.Where(c => c.IsEnabled == true)
                .Include(c => c.Children)
                .AsNoTracking()
                .ToListAsync();
            var catList = _mapper.Map<List<CategoryTBL>, List<CategoryListDTO>>(cats);
            return catList;
        }





        /// <summary>
        /// Create
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<bool> Create(CreateCategoryDTO model)
        {
            if (model == null) return false;
            var Category = new CategoryTBL()
            {
                Title = model.Title,
                PersianTitle = model.PersianTitle,
                ServiceId = model.ServiceId,
                IsEnabled = model.IsEnabled,
                ParentId = model.ParentId,
            };
            try
            {
                await _context.CategoryTBL.AddAsync(Category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch  
            {
                return false;
            }
        }








        /// <summary>
        ///Update
        /// </summary>
        /// <param name="Service"></param>
        /// <returns></returns>
        public async Task<bool> Update(CategoryTBL categoryFromDB, CreateCategoryDTO model)
        {
            try
            {
                //var serviceFromDb = await _context.ServiceTBL.FindAsync(model.Id);
                if (model == null) return false;
                categoryFromDB.Title = model.Title;
                categoryFromDB.PersianTitle = model.PersianTitle;
                categoryFromDB.IsEnabled = model.IsEnabled;
                categoryFromDB.ParentId = model.ParentId;
                categoryFromDB.ServiceId = model.ServiceId;

                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }





    }
}
