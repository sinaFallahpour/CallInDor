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

namespace CallInDoor.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {


        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private IStringLocalizer<ShareResource> _localizerShared;

        public CategoryController(DataContext context,
             IStringLocalizer<ShareResource> localizerShared,
             IAccountService accountService,
              ICategoryService categoryService
            )
        {
            _context = context;
            _accountService = accountService;
            _localizerShared = localizerShared;
            _categoryService = categoryService;
        }


        [HttpGet("GetByIdForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetById(int Id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var category = await _context.CategoryTBL.Where(c => c.Id == Id).Select(c => new
            {
                c.Id,
                c.Title,
                c.PersianTitle,
                c.ParentId,
                c.IsEnabled,
                serviceName = c.Service.Name,
            }).FirstOrDefaultAsync();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = category,
                Message = PubicMessages.SuccessMessage
            },
            PubicMessages.SuccessMessage
           ));

        }



        // GET: api/ServiceType
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllCategoryWithChildren()
        {
            //var checkToken = await _accountService.CheckTokenIsValid();
            //if (!checkToken)
            //    return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));
            var categoriesWithChild = await _categoryService.GetAllCateWithChildren();


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = categoriesWithChild,
                Message = PubicMessages.SuccessMessage
            },
               PubicMessages.SuccessMessage
              ));
        }





        [HttpGet("GetAllCateGoryForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetAllCateGoryForAdmin()
        {
            var result = await _accountService.CheckTokenIsValid();
            if (!result)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var categories = await _context.CategoryTBL
               .AsNoTracking()
               .Select(c => new
               {
                   c.Id,
                   c.IsEnabled,
                   c.Title,
                   c.PersianTitle,
                   serviceName = c.Service.Name,
                   parentName = c.Parent.Title,
               }).ToListAsync();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = categories,
                Message = PubicMessages.SuccessMessage
            },
               PubicMessages.SuccessMessage
              ));
        }





        /// <summary>
        /// ایجاد   دسته بندی
        /// </summary>
        /// <param name="CreateCategory"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDTO model)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            if (model.ServiceId == null)
            {
                var errors = new List<string>();
                errors.Add("service Is required");
                return BadRequest(new ApiBadRequestResponse(errors));
            }


            var result = await _categoryService.Create(model);
            if (result)
            {
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = new { },
                    Message = PubicMessages.SuccessMessage
                },
                   PubicMessages.SuccessMessage
                  ));
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

        }





        [HttpPut("Update")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<IActionResult> Update([FromBody] CreateCategoryDTO model)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));


            var service = _categoryService.GetById(model.Id);
            if (service == null)
            {
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));
            }

            var result = await _categoryService.Update(service, model);
            if (result)
            {
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = new { },
                    Message = PubicMessages.SuccessMessage
                },
                   PubicMessages.SuccessMessage
                ));
            }


            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

        }





    }
}
