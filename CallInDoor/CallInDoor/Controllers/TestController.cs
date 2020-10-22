using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using CallInDoor.Config.Permissions;
using Domain;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;

namespace CallInDoor.Controllers
{
    [Route("api/Test")]
    public class TestController : BaseControlle
    {

        #region ctor

        private readonly DataContext _context;
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IAccountService _accountService;

        public TestController(
            DataContext context,
            IStringLocalizer<ShareResource> localizerShared,
               IAccountService accountService
            )
        {
            _context = context;
            _localizerShared = localizerShared;
            _accountService = accountService;
        }

        #endregion ctor

        #region List (Pagination)

        [HttpGet("Index")]
        [ClaimsAuthorize]
        [PermissionAuthorize(PublicPermissions.User.GetAll, PublicPermissions.User.userEdit)]
        public async Task<IActionResult> Index(int? page, int? perPage,
                   string searchedWord)
        {

            var requiredPermission = new List<string>() {
             PublicPermissions.User.GetAll,
             PublicPermissions.User.userEdit
            };

            var hasPermission = await _accountService.CheckHasPermission(requiredPermission);
            if (!hasPermission)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));


            var QueryAble = _context.Tests.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(searchedWord))
            {
                QueryAble = QueryAble.Where(c =>
                      (c.Name.ToLower().StartsWith(searchedWord.ToLower()) || c.Name.ToLower().Contains(searchedWord.ToLower())
                      ||
                     (c.Category.StartsWith(searchedWord.ToLower()) || c.Category.ToLower().Contains(searchedWord.ToLower()))
                     ||
                     (c.price.ToString().ToLower().StartsWith(searchedWord.ToLower()) || c.price.ToString().ToLower().Contains(searchedWord.ToLower()))
                     ||
                      (c.price.ToString().ToLower().StartsWith(searchedWord.ToLower()) || c.price.ToString().ToLower().Contains(searchedWord.ToLower()))
                     ||
                     (c.price.ToString().ToLower().StartsWith(searchedWord.ToLower()) || c.price.ToString().ToLower().Contains(searchedWord.ToLower()))
                     ));
            };


            if (perPage == 0)
                perPage = 1;
            page = page ?? 0;
            perPage = perPage ?? 10;

            var count = QueryAble.Count();
            double len = (double)count / (double)perPage;
            var totalPages = Math.Ceiling((double)len);


            var Tests = await QueryAble
                .Skip((int)page * (int)perPage)
                .Take((int)perPage)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Image,
                    c.order_status,
                    c.popularity,
                    c.price,
                    c.Category
                }).ToListAsync();

            var data = new { Tests, totalPages };


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = data,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
               _localizerShared["SuccessMessage"].Value.ToString()
            ));

        }



        #endregion

        #region GetById


        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {

            var testFromDB = _context.Tests.Find(id);
            if (testFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = testFromDB,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
               _localizerShared["SuccessMessage"].Value.ToString()
            ));

        }



        #endregion

        #region Create


        [HttpPost("Create")]
        //[Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> Create([FromBody] Test model)
        {

            _context.Tests.Add(model);
            await _context.SaveChangesAsync();


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = new { },
                Message = PubicMessages.SuccessMessage
            },
               PubicMessages.SuccessMessage
              ));

        }


        #endregion

        #region update

        [HttpPut("Update")]
        //[Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> Update([FromBody] Test model)
        {

            var testFromDb = _context.Tests.Find(model.Id);
            testFromDb.Name = model.Name;
            testFromDb.popularity = model.popularity;
            testFromDb.order_status = model.order_status;
            testFromDb.price = model.price;
            testFromDb.Image = model.Image;
            testFromDb.Category = model.Category;

            await _context.SaveChangesAsync();


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = new { },
                Message = PubicMessages.SuccessMessage
            },
               PubicMessages.SuccessMessage
              ));

        }


        #endregion

        #region Delete


        [HttpDelete("Delete")]
        //[Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> Delete([FromBody] Test model)
        {

            var testFromDb = _context.Tests.Find(model.Id);
            testFromDb.Name = model.Name;
            testFromDb.popularity = model.popularity;
            testFromDb.order_status = model.order_status;
            testFromDb.price = model.price;
            testFromDb.Image = model.Image;
            testFromDb.Category = model.Category;

            await _context.SaveChangesAsync();


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = new { },
                Message = PubicMessages.SuccessMessage
            },
               PubicMessages.SuccessMessage
              ));

        }



        #endregion

    }
}
