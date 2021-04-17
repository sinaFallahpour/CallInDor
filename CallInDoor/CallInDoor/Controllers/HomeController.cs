using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CallInDoor.Models;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Common;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CallInDoor.Config.Attributes;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CallInDoor.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : BaseControlle
    {

        #region
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommonService _commonService;
        private IStringLocalizer<ShareResource> _localizerShared;

        public HomeController(DataContext context,
            UserManager<AppUser> userManager,
            ICommonService commonService,
            IStringLocalizer<ShareResource> localizerShared
            )
        {
            _context = context;
            _userManager = userManager;
            _commonService = commonService;
            _localizerShared = localizerShared;
        }



        #endregion


        //public IActionResult Index()
        //{
        //    //return View();
        //}






        #region AllProviderCount

        [HttpGet("GetAllProviderCount")]
        public async Task<ActionResult> GetAllProviderCount()
        {
            var ProvidersCount = await _context
                .BaseMyServiceTBL
                .Where(c => c.ConfirmedServiceType == ConfirmedServiceType.Confirmed)
                .AsNoTracking()
                .Select(c => new
                {
                    c.UserName
                }).Distinct().CountAsync();

            //return Ok(_commonService.OkResponse(ProvidersCount, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(ProvidersCount, false));
        }


        #endregion



        #region AllProviderCount

        [HttpGet("GetAllServicesCount")]
        public async Task<ActionResult> GetAllServicesCount()
        {
            var ServicesCount = await _context.BaseMyServiceTBL
                  .Where(c => c.ConfirmedServiceType == ConfirmedServiceType.Confirmed)
                  .AsNoTracking()
                  .CountAsync();
            //.Distinct().CountAsync();

            //return Ok(_commonService.OkResponse(ServicesCount, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(ServicesCount, false));
        }


        #endregion





        #region  get AllSite users


        [HttpGet("GetAllUsersCount")]
        public async Task<ActionResult> GetAllUsersCount()
        {
            int usersCount = await _userManager.Users.AsNoTracking().CountAsync();
            //return Ok(_commonService.OkResponse(usersCount, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(usersCount, false));

        }


        #endregion




        #region AllProviderCount

        [HttpGet("DashBoardForAdmin")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> DashBoardForAdmin()
        {
            var UsersCount = await _context.Users.CountAsync();
            var ActiveUserCount = await _context.Users.Where(c => (c.LockoutEnd == null || c.LockoutEnd < DateTime.Now)).CountAsync();

            var publishedServiceProvider = await _context.BaseMyServiceTBL
                .Where(c => c.IsDeleted == false && c.ConfirmedServiceType == ConfirmedServiceType.Confirmed
                     && c.ProfileConfirmType == ProfileConfirmType.Confirmed)
                .CountAsync();


            //var query = _context.BaseMyServiceTBL.AsQueryable();
            //var sdd = query.ToList().GroupBy(c => c.ConfirmedServiceType).Select(c => new
            //{
            //   count =(double) (c.Count() *100) / query.Count(),
            //   c.Key
            //});


            var baseServicveCount = await _context.BaseMyServiceTBL.CountAsync();
            var ChatVoiceCount = await _context.BaseMyServiceTBL.Where(c => c.ServiceType == ServiceType.ChatVoice).CountAsync();
            var VideoCalCount = await _context.BaseMyServiceTBL.Where(c => c.ServiceType == ServiceType.VideoCal).CountAsync();
            var VoiceCallCount = await _context.BaseMyServiceTBL.Where(c => c.ServiceType == ServiceType.VoiceCall).CountAsync();
            var ServiceCount = await _context.BaseMyServiceTBL.Where(c => c.ServiceType == ServiceType.Service).CountAsync();
            var CourseCount = await _context.BaseMyServiceTBL.Where(c => c.ServiceType == ServiceType.Course).CountAsync();

            var baseServiceAnalyst = new
            {
                baseServicveCount,
                ChatVoiceCount = (double)(ChatVoiceCount * 100 / baseServicveCount),
                VideoCalCount = (double)(VideoCalCount * 100 / baseServicveCount),
                VoiceCallCount = (double)(VoiceCallCount * 100 / baseServicveCount),
                ServiceCount = (double)(ServiceCount * 100 / baseServicveCount),
                CourseCount = (double)(CourseCount * 100 / baseServicveCount),
            };


            var accepted = await _context.BaseMyServiceTBL.Where(c => c.ConfirmedServiceType == ConfirmedServiceType.Confirmed).CountAsync();
            var Pendding = await _context.BaseMyServiceTBL.Where(c => c.ConfirmedServiceType == ConfirmedServiceType.Pending).CountAsync();
            var Rejected = await _context.BaseMyServiceTBL.Where(c => c.ConfirmedServiceType == ConfirmedServiceType.Rejected).CountAsync();


            var serviceStatus = new
            {
                Accepted = (double)(accepted * 100 / baseServicveCount),
                Rejected = (double)(Rejected * 100 / baseServicveCount),
                Pendding = (double)(Pendding * 100 / baseServicveCount),
            };





            var acceptedProfile = await _context.BaseMyServiceTBL.Where(c => c.ProfileConfirmType == ProfileConfirmType.Confirmed).CountAsync();
            var penddingProfile = await _context.BaseMyServiceTBL.Where(c => c.ProfileConfirmType == ProfileConfirmType.Pending).CountAsync();
            var rejectedProfile = await _context.BaseMyServiceTBL.Where(c => c.ProfileConfirmType == ProfileConfirmType.Rejected).CountAsync();


            var profileStatus = new
            {
                acceptedProfile = (double)(acceptedProfile * 100 / baseServicveCount),
                penddingProfile = (double)(penddingProfile * 100 / baseServicveCount),
                rejectedProfile = (double)(rejectedProfile * 100 / baseServicveCount),
            };








            var response = new
            {
                UsersCount,
                ActiveUserCount,
                PublishedServiceProvider = publishedServiceProvider,
                BaseServiceAnalyst = baseServiceAnalyst,
                ServiceStatus = serviceStatus,
                ProfileStatus = profileStatus

            };
            //var ServicesCount = await _context.BaseMyServiceTBL
            //      .Where(c => c.ConfirmedServiceType == ConfirmedServiceType.Confirmed)
            //      .CountAsync();
            //.Distinct().CountAsync();


            return Ok(response);
            //return Ok(_commonService.OkResponse(ServicesCount, _localizerShared["SuccessMessage"].Value.ToString()));
        }


        #endregion










        //#region AllProviderCount

        //[HttpGet("AllClientsCount")]
        //public async Task<ActionResult> AllClientsCount()
        //{
        //    var ClientsCount = await _context.BaseMyServiceTBL
        //          .Where(c => c.ConfirmedServiceType == ConfirmedServiceType.Confirmed)
        //          .CountAsync();
        //           //.Distinct().CountAsync();

        //    return Ok(_commonService.OkResponse(ClientsCount, _localizerShared["SuccessMessage"].Value.ToString()));
        //}


        //#endregion





        #region 

        // GET: api/GetAllServiceForAdmin
        [HttpGet("GetAllServiceTypes")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllServiceTypes()
        {
            var allServiceTypes = await _context.ServiceTBL.Where(c => c.IsEnabled)
                .Select(c => new
                {
                    c.Id,
                    c.Color,
                    c.Name,
                    c.PersianName,
                }).ToListAsync();

            //return Ok(_commonService.OkResponse(allServiceTypes, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(allServiceTypes, false));

        }


        #endregion




    }
}
