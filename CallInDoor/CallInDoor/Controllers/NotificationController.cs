using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class NotificationController : BaseControlle
    {

        #region ctor
        private readonly DataContext _context;
        private readonly ICommonService _commonService;
        private readonly IAccountService _accountService;


        private IStringLocalizer<NotificationController> _localizer;
        private IStringLocalizer<ShareResource> _localizerShared;

        public NotificationController(
            DataContext context,
                   ICommonService commonService,
                   IAccountService accountService,
               IStringLocalizer<NotificationController> localizer,
                IStringLocalizer<ShareResource> localizerShared
            )
        {
            _context = context;
            _commonService = commonService;
            _accountService = accountService;
            _localizer = localizer;
            _localizerShared = localizerShared;
        }

        #endregion ctor




        /// <summary>
        ///گرفتن نوتیفیکیشن های من
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyNotification")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAllMyNotification()
        {
            var currentusername = _accountService.GetCurrentUserName();
            var isPersian = _commonService.IsPersianLanguage();

            var notifs = await _context
               .NotificationTBL
               .AsNoTracking()
               .Where(C => C.UserName == currentusername)
               .Select(c => new
               {
                   c.SenderUserName,
                   c.Id,
                   Text = isPersian ? c.TextPersian : c.EnglishText,
                   c.CreateDate,
                   c.IsReaded,
               }).ToListAsync();

            //return Ok(_commonService.OkResponse(notifs, _localizerShared["SuccessMessage"].Value.ToString()));

            return Ok(_commonService.OkResponse(notifs, false));

        }




        /// <summary>
        ///گرفتن تعداد نوتیفیکیشن های نخوانده من
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetUnReadedNotificationCount")]
        //[Authorize]
        //[ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetUnReadedNotificationCount()
        {
            var currentusername = _accountService.GetCurrentUserName();

            var notisCount = await _context
               .NotificationTBL
               .Where(C => C.UserName == currentusername && !C.IsReaded)
               .AsNoTracking()
               .CountAsync();

            return Ok(_commonService.OkResponse(notisCount, false));

            //return Ok(_commonService.OkResponse(notisCount, _localizerShared["SuccessMessage"].Value.ToString()));
        }






        /// <summary>
        ///گرفتن نوتیفیکیشن های خوانده نشده ی من
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyUnReadedNotification")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAllMyUnReadedNotification()
        {
            var currentusername = _accountService.GetCurrentUserName();
            var isPersian = _commonService.IsPersianLanguage();

            var notifs = await _context
               .NotificationTBL
               .AsNoTracking()
               .Where(C => C.UserName == currentusername && C.IsReaded == false)
               .Select(c => new
               {
                   c.SenderUserName,
                   c.Id,
                   //Text = isPersian ? c.TextPersian : c.EnglishText,
                   Text = _commonService.GetNameByCulture(c),
                   c.CreateDate,
                   c.IsReaded,
               }).ToListAsync();

            //return Ok(_commonService.OkResponse(notifs, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(notifs, false));
        }





        /// <summary>
        ///گرفتن نوتیفیکیشن های خوانده نشده ی من
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyNotificationById")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetAllMyNotificationById(int Id)
        {
            var currentusername = _accountService.GetCurrentUserName();
            var isPersian = _commonService.IsPersianLanguage();

            var notif = await _context
               .NotificationTBL
               .AsNoTracking()
               .Where(C => C.UserName == currentusername && C.Id == Id)
               .Select(c => new
               {
                   c.SenderUserName,
                   c.Id,
                   c.UserName,
                   //Text = isPersian ? c.TextPersian : c.EnglishText,
                   Text = _commonService.GetNameByCulture(c),
                   c.CreateDate,
                   c.IsReaded,
               }).FirstOrDefaultAsync();

            if (notif == null)
                return BadRequest(_commonService.NotFoundErrorReponse(false));


            if (notif.UserName != currentusername)
            {
                //List<string> erros = new List<string> { _localizerShared["UnauthorizedMessage"].Value.ToString() };
                //////////List<string> erros = new List<string> { _commonService.UnAuthorizeErrorReponse(, false) };
                //////////return Unauthorized(new ApiBadRequestResponse(erros, 401));
                return Unauthorized(_commonService.UnAuthorizeErrorReponse(false));
            }


            //return Ok(_commonService.OkResponse(notif, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(notif, false));

        }





        /// <summary>
        ///خوانده شده کردنیک نوتیف
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut("ReadNotif")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        [ProducesResponseType(200)]
        public async Task<ActionResult> ReadNotif(int Id)
        {
            var currentusername = _accountService.GetCurrentUserName();
            var isPersian = _commonService.IsPersianLanguage();

            var notif = await _context
               .NotificationTBL
               .Where(C => C.UserName == currentusername && C.Id == Id)
              .FirstOrDefaultAsync();

            if (notif == null)
                return NotFound(_commonService.NotFoundErrorReponse(false));

            notif.IsReaded = true;
            await _context.SaveChangesAsync();

            return Ok(_commonService.OkResponse(null, false));
        }



    }
}
