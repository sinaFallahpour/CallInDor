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
                c.ParentId,
                c.IsEnabled,
                c.IsForCourse,
                c.IsSubCategory,
                serviceName = c.Service.Name,
                serviceId = c.Service.Id,
            }).FirstOrDefaultAsync();

            if (category == null)
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

            return Ok(_commonService.OkResponse(category, PubicMessages.SuccessMessage));

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
                   c.IsEnabled,
                   c.IsSubCategory,
                   c.IsForCourse,
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
                   c.IsEnabled,
                   c.IsSubCategory,
                   c.IsForCourse,
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
        public async Task<ActionResult> Create([FromBody] CreateCategoryDTO model)
        {

            if (model.ServiceId == null)
            {
                var errors = new List<string>();
                errors.Add("service is required");
                return BadRequest(new ApiBadRequestResponse(errors));
            }


            var category = await _categoryService.Create(model);
            if (category != null)
                return Ok(_commonService.OkResponse(category, PubicMessages.SuccessMessage));

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

        }

        #endregion

        #region  Update

        [HttpPut("Update")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<IActionResult> Update([FromBody] CreateCategoryDTO model)
        {

            var service = _categoryService.GetById(model.Id);
            if (service == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            var result = await _categoryService.Update(service, model);
            if (result)
                return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));


            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

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
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

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

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );
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
                return NotFound(new ApiResponse(404, "area " + PubicMessages.NotFoundMessage));

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

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );
        }

        #endregion



        #endregion

    }
}
