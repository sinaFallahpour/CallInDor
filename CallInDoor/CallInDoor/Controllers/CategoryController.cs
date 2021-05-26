using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Domain.Utilities;
using Domain.DTO.Category;
using Service.Interfaces.Category;
using CallInDoor.Config.Attributes;
using Service.Interfaces.Common;
using Service.Interfaces.Resource;

namespace CallInDoor.Controllers
{
    /// <summary>
    /// هم دسته بندی  
    /// و هم Area
    /// </summary>
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        #region ctor

        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly ICommonService _commonService;

        public CategoryController(DataContext context,
             IStringLocalizer<ShareResource> localizerShared,
             IAccountService accountService,
             ICommonService commonService,
              ICategoryService categoryService

            )
        {
            _context = context;
            _accountService = accountService;
            _commonService = commonService;
            _localizerShared = localizerShared;
            _categoryService = categoryService;
        }

        #endregion

        #region Category

        #region GetById

        [HttpGet("GetByIdForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetById(int Id)
        {
            var category = await _context.CategoryTBL.Where(c => c.Id == Id).Select(c => new
            {
                c.Id,
                c.Title,
                c.PersianTitle,
                c.ImageAddress,
                c.ParentId,
                c.IsEnabled,
                c.IsForCourse,
                c.IsSubCategory,
                serviceName = c.Service.Name,
                IsSupplier = c.IsSupplier,
                serviceId = c.Service.Id,
            }).FirstOrDefaultAsync();

            if (category == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
                //return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }
            return Ok(_commonService.OkResponse(category, PubicMessages.SuccessMessage));

        }

        #endregion


        #region  Get All Not Subcategory ByServiceId
        //گرفتن تمانپم دسته برندی هیه یک سرویس خاص(آن هایی مه ساب کتگوری نیستند) ت
        [HttpGet("GetAllNotSubcategoryByServiceId")]
        public async Task<ActionResult> GetAllNotSubcategoryByServiceId(int serviceId)
        {

            var topUsernames = await _context.BaseMyServiceTBL.AsNoTracking().Where(c => c.ServiceId == serviceId)
                      .OrderByDescending(c => c.StarCount)
                      .ThenBy(c => c.Under3StarCount)
                      .ThenByDescending(c => c.CreateDate)
                      .Take(6)
                      .Select(c => c.UserName)
                      .ToListAsync();


            var usres = await _context.Users.Where(c => topUsernames.Contains(c.UserName))
                .Select(c => new Top6UserOfCategoryDTO
                {
                    ImageAddress = c.ImageAddress,
                    Name = c.Name,
                    LastName = c.LastName,
                    IsOnline = c.IsOnline,
                    StarCount = c.StarCount,
                })
                .ToListAsync();


            var cats = await _context.CategoryTBL.Where(c => c.IsEnabled == true &&
             c.ServiceId == serviceId && c.IsSubCategory == false)
                .AsNoTracking()
             .Select(c => new
             {
                 c.Id,
                 //c.Title,
                 //c.PersianTitle,
                 Title = _commonService.GetNameByCulture(c),
                 c.ImageAddress,
                 c.IsEnabled,
                 c.ParentId,
                 c.ServiceId,
                 c.IsForCourse,
                 c.IsSubCategory,
                 c.IsSupplier,
                 users = usres
             })
                .ToListAsync();

            return Ok(_commonService.OkResponse(cats, PubicMessages.SuccessMessage));
        }
        #endregion




        #region Get All Subcategory ByServiceId
        //گرفتن تمام دسته بندی هایی یک سرویس خاص(آن هایی مه ساب کتگوری هستند)  ت
        [HttpGet("GetAllSubcategoryByServiceId")]
        public async Task<ActionResult> GetAllSubcategoryByServiceId(int serviceId)
        {

            var topUsernames = await _context.BaseMyServiceTBL.AsNoTracking().Where(c => c.ServiceId == serviceId)
                    .OrderByDescending(c => c.StarCount)
                    .ThenBy(c => c.Under3StarCount)
                    .ThenByDescending(c => c.CreateDate)
                    .Take(6)
                    .Select(c => c.UserName)
                    .ToListAsync();


            var usres = await _context.Users.Where(c => topUsernames.Contains(c.UserName))
                .Select(c => new Top6UserOfCategoryDTO
                {
                    ImageAddress = c.ImageAddress,
                    Name = c.Name,
                    LastName = c.LastName,
                    IsOnline = c.IsOnline,
                    StarCount = c.StarCount,
                })
                .ToListAsync();



            var cats = await _context.CategoryTBL.Where(c => c.IsEnabled == true &&
             c.ServiceId == serviceId && c.IsSubCategory == true)
                .AsNoTracking()
             .Select(c => new
             {
                 c.Id,
                 //c.Title,
                 //c.PersianTitle,
                 Title = _commonService.GetNameByCulture(c),

                 c.ImageAddress,
                 c.IsEnabled,
                 c.ParentId,
                 c.ServiceId,
                 c.IsForCourse,
                 c.IsSubCategory,
                 c.IsSupplier,
                 users = usres
             })
                .ToListAsync();
            return Ok(_commonService.OkResponse(cats, PubicMessages.SuccessMessage));
        }
        #endregion



        #region GetAllCategoryWithChildren
        // GET: api/ServiceType
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllCategoryWithChildren(int serviceId)
        {
            var categoriesWithChild = await _categoryService.GetAllCateWithChildren(serviceId);
            return Ok(_commonService.OkResponse(categoriesWithChild, PubicMessages.SuccessMessage));
        }


        #endregion

        #region GetAllParentCateGoryForAdmin
        [HttpGet("GetAllParentCateGoryForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllParentCateGoryForAdmin()
        {

            var categories = await _context
                .CategoryTBL
                .Where(c => c.ParentId == null)
               .AsNoTracking()
               .Select(c => new
               {
                   c.Id,
                   c.Title,
                   c.PersianTitle,
                   c.ImageAddress,
                   c.IsEnabled,
                   c.IsSubCategory,
                   c.IsForCourse,
                   c.IsSupplier,
                   serviceName = c.Service.Name,
                   parentName = c.Parent.Title,
               }).ToListAsync();

            return Ok(_commonService.OkResponse(categories, PubicMessages.SuccessMessage));

        }


        #endregion


        #region GetAllCateGoryForAdmin
        [HttpGet("GetAllCateGoryForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllCateGoryForAdmin()
        {

            var categories = await _context
                .CategoryTBL
               .AsNoTracking()
               .Select(c => new
               {
                   c.Id,
                   c.Title,
                   c.PersianTitle,
                   c.ImageAddress,
                   c.IsEnabled,
                   c.IsSubCategory,
                   c.IsForCourse,
                   c.IsSupplier,
                   serviceName = c.Service.Name,
                   parentName = c.Parent.Title,
               }).ToListAsync();

            return Ok(_commonService.OkResponse(categories, PubicMessages.SuccessMessage));

        }


        #endregion

        #region Create

        /// <summary>
        /// ایجاد   دسته بندی
        /// </summary>
        /// <param name="CreateCategory"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> Create([FromForm] CreateCategoryDTO model)
        {

            if (model.ServiceId == null)
            {
                var errors = new List<string>();
                errors.Add("service is required");
                return BadRequest(new ApiBadRequestResponse(errors));
            }
            if (model.Image == null || model.Image.Length <= 0)
            {
                var errors = new List<string>();
                errors.Add("image file is required ");
                return BadRequest(new ApiBadRequestResponse(errors));
            }
            if (!model.Image.IsImage())
            {
                var errors = new List<string>();
                errors.Add("invalid file format. please inter image format");
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            var category = await _categoryService.Create(model);
            if (category != null)
                return Ok(_commonService.OkResponse(category, PubicMessages.SuccessMessage));



            List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));
            //////return StatusCode(StatusCodes.Status500InternalServerError,
            //////  new ApiResponse(500, PubicMessages.InternalServerMessage)
            //////);

        }

        #endregion

        #region  Update

        [HttpPut("Update")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<IActionResult> Update([FromForm] CreateCategoryDTO model)
        {

            var service = _categoryService.GetById(model.Id);
            if (service == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
                //return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));
            }


            if (model.Image != null && !model.Image.IsImage())
            {
                var errors = new List<string>();
                errors.Add("invalid file format. please inter image format");
                return BadRequest(new ApiBadRequestResponse(errors));
            }


            var result = await _categoryService.Update(service, model);
            if (result)
                return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));


            List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));

            //////////return StatusCode(StatusCodes.Status500InternalServerError,
            //////////  new ApiResponse(500, PubicMessages.InternalServerMessage)
            //////////);

        }

        #endregion


        #endregion

        #region Area




        #region GetById

        [HttpGet("/api/Area/GetAreaByIdForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAreaByIdForAdmin(int Id)
        {
            var service = await _context.AreaTBL.Where(c => c.Id == Id).Select(c => new
            {
                c.Id,
                c.Title,
                c.PersianTitle,
                c.IsEnabled,
                c.IsProfessional,
                serviceName = c.Service.Name,
                serviceId = c.Service.Id,
                c.Specialities
                //serviceId = c.Service != null ? c.Service.Id : null
            }).FirstOrDefaultAsync();

            if (service == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                //return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }
            return Ok(_commonService.OkResponse(service, PubicMessages.SuccessMessage));

        }

        #endregion


        #region GetAllAreaForAdmin

        [HttpGet("/api/Area/GetAllAreaForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllAreaForAdmin()
        {
            var areas = await _context.AreaTBL
               .AsNoTracking()
               .Select(c => new
               {
                   c.Id,
                   c.Title,
                   c.PersianTitle,
                   c.IsEnabled,
                   c.IsProfessional,
                   serviceName = c.Service.Name,
               }).ToListAsync();

            return Ok(_commonService.OkResponse(areas, PubicMessages.SuccessMessage));

        }


        #endregion


        #region GetAllAreaWith Specialities in User

        [HttpGet("/api/Area/GetAllAreaWithSpeciality")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAllAreaForAdmin(int servcieId)
        {
            var areas = await _context.AreaTBL
                .AsNoTracking()
               .Where(C => C.ServiceId == servcieId)
               .Select(c => new
               {
                   c.Id,
                   //c.Title,
                   //c.PersianTitle,
                   Title = _commonService.GetNameByCulture(c),
                   c.IsEnabled,
                   c.IsProfessional,
                   serviceName = c.Service.Name,
                   specialities = c.Specialities.Select(c => new { c.Id, c.EnglishName, c.PersianName }).ToList()
               })
               .ToListAsync();
            return Ok(_commonService.OkResponse(areas, PubicMessages.SuccessMessage));

        }


        #endregion





        #region Create

        /// <summary>
        ///  ایجاد Area
        /// </summary>
        /// <param name="CreateCategory"></param>
        /// <returns></returns>
        [HttpPost("/api/Area/CreateArea")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> CreateArea([FromBody] CreateAreaDTO model)
        {

            //validate
            var res = await _categoryService.ValidateArea(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            if (!model.IsProfessional)
                model.Specialities = null;

            var Area = await _categoryService.CreateArea(model);
            var area = new
            {
                Area.Id,
                Area.Title,
                Area.PersianTitle,
                Area.ServiceId,
                serviceName = Area.Service?.Name,
                Area.IsEnabled,
                Area.IsProfessional,
            };

            if (Area != null)
                return Ok(_commonService.OkResponse(area, PubicMessages.SuccessMessage));


            List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));

            //////////return StatusCode(StatusCodes.Status500InternalServerError,
            //////////  new ApiResponse(500, PubicMessages.InternalServerMessage)
            //////////);
        }

        #endregion




        #region UpdateArea

        /// <summary>
        ///  ایجاد Area
        /// </summary>
        /// <param name="UpdateArea"></param>
        /// <returns></returns>
        [HttpPut("/api/Area/UpdateArea")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> UpdateArea([FromBody] CreateAreaDTO model)
        {


            var area = await _categoryService.GetAreaById(model.Id);
            if (area == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                //return NotFound(new ApiResponse(404, "area " + PubicMessages.NotFoundMessage));
            }
            //validate
            var res = await _categoryService.ValidateAreaForEdit(model, area);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            if (!model.IsProfessional)
                model.Specialities = null;

            var Area = await _categoryService.UpdateArea(area, model);
            var areResponse = new
            {
                Area.Id,
                Area.Title,
                Area.PersianTitle,
                Area.ServiceId,
                serviceName = Area.Service?.Name,
                Area.IsEnabled,
                Area.IsProfessional,
                //Specialities=area.Specialities.  Area.Specialities
            };

            if (Area != null)
                return Ok(_commonService.OkResponse(areResponse, PubicMessages.SuccessMessage));



            List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));

            ////////////return StatusCode(StatusCodes.Status500InternalServerError,
            ////////////  new ApiResponse(500, PubicMessages.InternalServerMessage)
            ////////////);
        }

        #endregion



        #endregion

    }
}
