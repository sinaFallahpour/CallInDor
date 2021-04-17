using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.CheckDiscount;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Resource;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CheckDiscountController : BaseControlle
    {
        #region ctor

        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;

        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;

        public CheckDiscountController(
            DataContext context,
                   IAccountService accountService,
                   ICommonService commonService,
                IStringLocalizer<ShareResource> localizerShared,
                IResourceServices resourceServices
            )
        {
            _context = context;
            _accountService = accountService;
            _commonService = commonService;
            _localizerShared = localizerShared;
            _resourceServices = resourceServices;
        }

        #endregion ctor




        /// <summary>
        ///گرفتن   تخفیف های من با ایدی
        /// </sumظmary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetCheckDiscountByIdInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetCheckDiscountByIdInAdmin(int id)
        {
            var checkDiscount = await _context
               .CheckDiscountTBL
               .AsNoTracking()
               .Where(c => c.Id == id)
               .Select(c => new
               {
                   c.Id,
                   c.PersianTitle,
                   c.EnglishTitle,
                   c.ExpireTime,
                   c.Percent,
                   c.Code,
                   serviceName = c.Service != null ? c.Service.Name + $"({c.Service.PersianName})" : null,
                   ServiceId = c.Service.Id,
                   c.HourCount,
                   c.DayCount

               })
               .FirstOrDefaultAsync();

            if (checkDiscount == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                //return NotFound(_commonService.NotFoundErrorReponse(true));
            }
            return Ok(_commonService.OkResponse(checkDiscount, true));
        }





        /// <summary>
        ///گرفتن لیست تخفیف هاهای من
        /// </sumظmary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllCheckDiscountInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllCheckDiscountInAdmin()
        {
            var checkDiscounts = await _context
               .CheckDiscountTBL
               .AsNoTracking()
               .Select(c => new
               {
                   c.Id,
                   c.PersianTitle,
                   c.EnglishTitle,
                   c.ExpireTime,
                   c.Percent,
                   c.Code,
                   serviceId = c.Service.Id,
                   serviceName = c.Service != null ? c.Service.Name + $"({c.Service.PersianName})" : null,

               })
               .ToListAsync();
            return Ok(_commonService.OkResponse(checkDiscounts, true));
        }






        /// <summary>
        /// افزودن یک تخفیف 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddCheckDiscountInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> AddCheckDiscountInAdmin([FromBody] AddCheckDiscountDTO model)
        {
            var serviceFromDB = await _context.ServiceTBL.Where(c => c.Id == model.ServiceId).FirstOrDefaultAsync();
            if (serviceFromDB == null)
            {
                var err = new List<string>();
                err.Add($"invalid service");
                return BadRequest(new ApiBadRequestResponse(err));
            }

            //var isServiceExist = await _context.ServiceTBL.AnyAsync(c => c.Id == model.ServiceId);

            //if (!isServiceExist)
            //{
            //    var err = new List<string>();
            //    err.Add($"invalid service");
            //    return BadRequest(new ApiBadRequestResponse(err));
            //}


            var isExist = await _context.CheckDiscountTBL.AnyAsync(c =>
                   c.PersianTitle.ToLower() == model.PersianTitle.ToLower() ||
                   c.PersianTitle.ToLower() == model.PersianTitle.ToLower());

            if (isExist)
            {
                var err = new List<string>();
                err.Add($"discount with this persian title(or english title) already exist ");
                return BadRequest(new ApiBadRequestResponse(err));
            }

            if (model.DayCount == null && model.HourCount == null)
            {
                var err = new List<string>();
                err.Add($"please enter day or hour for discount");
                return BadRequest(new ApiBadRequestResponse(err));
            }


            DateTime expiretime = DateTime.Now;
            if (model.DayCount != null)
                expiretime = expiretime.AddDays((int)model.DayCount);
            if (model.HourCount != null)
                expiretime = expiretime.AddHours((int)model.HourCount);

            var checkDiscount = new CheckDiscountTBL()
            {
                EnglishTitle = model.EnglishTitle,
                PersianTitle = model.PersianTitle,
                Code = model.Code,
                ExpireTime = expiretime,
                Percent = model.Percent,

                DayCount = model.DayCount,
                HourCount = model.HourCount,
                ServiceId = model.ServiceId,
            };

            try
            {
                await _context.CheckDiscountTBL.AddAsync(checkDiscount);
                await _context.SaveChangesAsync();

                var response = new
                {
                    checkDiscount.Id,
                    serviceName = checkDiscount.Service.Name,
                    expiretime = checkDiscount.ExpireTime,
                    persianServiceName = checkDiscount.Service.PersianName
                };

                return Ok(_commonService.OkResponse(response, true));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                          new ApiResponse(500, PubicMessages.InternalServerMessage));
            }
        }





        #region Update CheckDiscount In Admin

        [HttpPut("UpdateCheckDiscountInAdmin")]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> UpdateCheckDiscountInAdmin([FromBody] AddCheckDiscountDTO model)
        {


            if (model.DayCount == null && model.HourCount == null)
            {
                var err = new List<string>();
                err.Add($"please enter day or hour for discount");
            }

            var isExist = await _context.CheckDiscountTBL.AnyAsync(c =>
                                    c.Id != model.Id && (c.PersianTitle.ToLower() == model.PersianTitle.ToLower()
                                    ||
                                    c.PersianTitle.ToLower() == model.PersianTitle.ToLower()));

            if (isExist)
            {
                var err = new List<string>();
                err.Add($"discount with this persian title(or english title) already exist ");
                return BadRequest(new ApiBadRequestResponse(err));
            }

            var serviceFromDB = await _context.ServiceTBL.Where(c => c.Id == model.ServiceId).FirstOrDefaultAsync();
            if (serviceFromDB == null)
            {
                var err = new List<string>();
                err.Add($"invalid service");
                return BadRequest(new ApiBadRequestResponse(err));
            }


            CheckDiscountTBL checkDiscountfFromDB = await _context.CheckDiscountTBL.Where(c => c.Id == model.Id)
                                                            .Include(c => c.Service)
                                                            .FirstOrDefaultAsync();

            if (checkDiscountfFromDB == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                ////////return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }
            DateTime expiretime = DateTime.Now;
            if (model.DayCount != null)
                expiretime = expiretime.AddDays((int)model.DayCount);
            if (model.DayCount != null)
                expiretime = expiretime.AddHours((int)model.HourCount);

            checkDiscountfFromDB.Percent = model.Percent;
            checkDiscountfFromDB.EnglishTitle = model.EnglishTitle;
            checkDiscountfFromDB.PersianTitle = model.PersianTitle;
            checkDiscountfFromDB.ExpireTime = expiretime;
            checkDiscountfFromDB.Code = model.Code;
            checkDiscountfFromDB.DayCount = model.DayCount;
            checkDiscountfFromDB.HourCount = model.HourCount;
            checkDiscountfFromDB.ServiceId = model.ServiceId;

            try
            {
                await _context.SaveChangesAsync();

                var response = new
                {
                    checkDiscountfFromDB.Id,
                    serviceName = checkDiscountfFromDB.Service.Name,
                    expiretime = checkDiscountfFromDB.ExpireTime,
                    persianServiceName = checkDiscountfFromDB.Service.PersianName
                };
                return Ok(_commonService.OkResponse(response, true));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                          new ApiResponse(500, PubicMessages.InternalServerMessage));
            }


        }


        #endregion





        #region Expire Check Discount In Admin

        /// <summary>
        /// اکسپایر کردن یک درصد تخفیف
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("ExpireCheckDiscountInAdmin")]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> ExpireCheckDiscountInAdmin([FromBody] AddCheckDiscountDTO model)
        {

            if (model.DayCount == null && model.HourCount == null)
            {


                List<string> erros = new List<string> { "please enter day or hour for discount" };
                return BadRequest(new ApiBadRequestResponse(erros, 400));


                //////////var err = new List<string>();
                //////////err.Add($"please enter day or hour for discount");

            }


            CheckDiscountTBL checkDiscountfFromDB = await _context.CheckDiscountTBL.FindAsync(model.Id);
            if (checkDiscountfFromDB == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                //////////return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

            }
            checkDiscountfFromDB.ExpireTime = DateTime.Now.AddYears(-100);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(checkDiscountfFromDB.Id, true));
            }
            catch
            {
                List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));

                //return StatusCode(StatusCodes.Status500InternalServerError,
                //          new ApiResponse(500, PubicMessages.InternalServerMessage));
            }
        }


        #endregion





        /// <summary>
        ///آیا کد تخفیف معتبر است یا خیر
        /// </summary>
        /// <param name="disCountCode"></param>
        /// <returns></returns>
        [HttpGet("IsDiscountCodeValid")]
        public async Task<ActionResult> IsDiscountCodeValid(string disCountCode, int requestId)
        {
            CheckDiscountTBL discountFromDB = await _context.CheckDiscountTBL.Where(c => c.Code.ToLower().Trim() == disCountCode.ToLower().Trim()).FirstOrDefaultAsync();
            if (discountFromDB == null || discountFromDB.ExpireTime < DateTime.Now)
            {
                List<string> erros = new List<string> { 
                    
                    //_localizerShared["InvalidDiscointCode"].Value.ToString()
                    _resourceServices.GetErrorMessageByKey("InvalidDiscointCode")
                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            string currentUserName = _accountService.GetCurrentUserName();
            var requestFromDB = await _context.ServiceRequestTBL
                .Where(c => c.Id == requestId)
                .Select(c => new
                {
                    ServiceId = c.BaseMyServiceTBL.ServiceTbl.Id
                })
                .FirstOrDefaultAsync();

            if (requestFromDB == null)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["InvalidDiscointCode"].Value.ToString()
                _resourceServices.GetErrorMessageByKey("InvalidDiscointCode")

                };
                return BadRequest(new ApiBadRequestResponse(erros));

            }

            if (discountFromDB.ServiceId != requestFromDB.ServiceId)
            {
                List<string> erros = new List<string> {
                    //_localizerShared["InvalidDiscointCode"].Value.ToString() 
                _resourceServices.GetErrorMessageByKey("InvalidDiscointCode")

                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }




            //return Ok(_commonService.OkResponse(discountFromDB.Percent, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(discountFromDB.Percent, false));
        }





    }
}
