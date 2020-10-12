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




        #region  Category

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
                IsSubCategory=c.IsSubCategory,
                IsForCourse=c.IsForCourse
                
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
        public async Task<CategoryTBL> Create(CreateCategoryDTO model)
        {
            if (model == null) return null;
            var Category = new CategoryTBL()
            {
                Title = model.Title,
                PersianTitle = model.PersianTitle,
                ServiceId = model.ServiceId,
                IsEnabled = model.IsEnabled,
                ParentId = model.ParentId,
                IsForCourse = model.IsForCourse,
                IsSubCategory = model.IsSubCategory
            };
            try
            {
                var category = _context.CategoryTBL.AddAsync(Category);
                await _context.SaveChangesAsync();
                return Category;
            }
            catch
            {
                return null;
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
                categoryFromDB.IsForCourse = model.IsForCourse;
                categoryFromDB.IsSubCategory = model.IsSubCategory;

                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }





        #endregion



        #region  Area




        /// <summary>
        /// get Area by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<AreaTBL> GetAreaById(int Id)
        {
            return _context.AreaTBL.Where(c => c.Id == Id).Include(c => c.Specialities).FirstOrDefaultAsync();
        }




        /// <summary>
        /// CreateArea
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<AreaTBL> CreateArea(CreateAreaDTO model)
        {
            if (model == null) return null;

            var Area = new AreaTBL()
            {
                Title = model.Title,
                PersianTitle = model.PersianTitle,
                IsProfessional = model.IsProfessional,
                //Specialities = model.Specialities,
                ServiceId = model.ServiceId,
                IsEnabled = model.IsEnabled,

            };

            Area.Specialities = new List<SpecialityTBL>();
            if (model.Specialities != null)
            {
                foreach (var item in model.Specialities)
                {
                    var speciality = new SpecialityTBL()
                    {
                        Area = Area,
                        PersianName = item.PersianName,
                        EnglishName = item.EnglishName,
                    };
                    Area.Specialities.Add(speciality);
                }
            }
            try
            {
                var area = await _context.AreaTBL.AddAsync(Area);
                await _context.SaveChangesAsync();
                return Area;
            }
            catch
            {
                return null;
            }
        }





        /// <summary>
        /// UpdateArea
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<AreaTBL> UpdateArea(AreaTBL areaFreomDB, CreateAreaDTO model)
        {

            try
            {
                if (model == null) return null;
                areaFreomDB.Title = model.Title;
                areaFreomDB.PersianTitle = model.PersianTitle;
                areaFreomDB.IsEnabled = model.IsEnabled;
                areaFreomDB.ServiceId = model.ServiceId;
                areaFreomDB.IsProfessional = model.IsProfessional;
                if (!areaFreomDB.IsProfessional)
                    areaFreomDB.Specialities = null;

                else
                {



                    var specilaities = new List<SpecialityTBL>();


                    //_context.SpecialityTBL.RemoveRange(areaFreomDB.Specialities);
                    //areaFreomDB.Specialities = new List<SpecialityTBL>();
                    if (model.Specialities != null)
                    {
                        if (areaFreomDB.Specialities != null)
                            _context.SpecialityTBL.RemoveRange(areaFreomDB.Specialities);

                        foreach (var item in model.Specialities)
                        {
                            var speciality = new SpecialityTBL()
                            {
                                Area = areaFreomDB,
                                EnglishName = item.EnglishName,
                                PersianName = item.PersianName,
                            };
                            specilaities.Add(speciality);
                            //areaFreomDB.Specialities.Add(speciality);
                        }
                        areaFreomDB.Specialities = specilaities;
                    }
                }

                await _context.SaveChangesAsync();
                return areaFreomDB;
            }
            catch (Exception ex)
            {
                return null;
            }

        }






        /// <summary>
        /// ولیدیت کردن آبجکت  چت سرویس
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateArea(CreateAreaDTO model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();



            var isTittleExist = await _context.AreaTBL.AnyAsync(c => c.Title == model.Title);
            if (isTittleExist)
            {
                IsValid = false;
                Errors.Add($"{model.Title} already exist.");
            }

            var isPersianTittleExist = await _context.AreaTBL.AnyAsync(c => c.PersianTitle == model.PersianTitle);
            if (isPersianTittleExist)
            {
                IsValid = false;
                Errors.Add($"{model.PersianTitle} already exist.");
            }

            if (model.IsProfessional)
                if (model.Specialities == null)
                {
                    IsValid = false;
                    Errors.Add("Specialities is Required");
                }

            if (model.ServiceId == null)
            {
                IsValid = false;
                var errors = new List<string>();
                Errors.Add("service Is required");
            }


            //validate serviceTypes
            var serviceFromDb = await _context
            .ServiceTBL
            .Where(c => c.Id == model.ServiceId)
            .FirstOrDefaultAsync();

            if (serviceFromDb == null)
            {
                IsValid = false;
                Errors.Add("service Not Exist");
            }

            return (IsValid, Errors);
        }






        /// <summary>
        /// ولیدیت کردن آبجکت  چت سرویس
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool succsseded, List<string> result)> ValidateAreaForEdit(CreateAreaDTO model, AreaTBL areaFromDB)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();



            var isTittleExist = await _context.AreaTBL.AnyAsync(c => c.Title == model.Title && c.Id != areaFromDB.Id);
            if (isTittleExist)
            {
                IsValid = false;
                Errors.Add($"{model.Title} already exist.");
            }

            var isPersianTittleExist = await _context.AreaTBL.AnyAsync(c => c.PersianTitle == model.PersianTitle && c.Id != model.Id);
            if (isPersianTittleExist)
            {
                IsValid = false;
                Errors.Add($"{model.PersianTitle} already exist.");
            }

            if (model.IsProfessional)
                if (model.Specialities == null)
                {
                    IsValid = false;
                    Errors.Add("Specialities is Required");
                }

            if (model.ServiceId == null)
            {
                IsValid = false;
                var errors = new List<string>();
                Errors.Add("service Is required");
            }


            //validate serviceTypes
            var serviceFromDb = await _context
            .ServiceTBL
            .Where(c => c.Id == model.ServiceId)
            .FirstOrDefaultAsync();

            if (serviceFromDb == null)
            {
                IsValid = false;
                Errors.Add("service Not Exist");
            }

            return (IsValid, Errors);
        }




        #endregion

    }
}
