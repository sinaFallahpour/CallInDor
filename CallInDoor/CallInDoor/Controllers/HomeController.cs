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

namespace CallInDoor.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : BaseControlle
    {

        #region
        private readonly DataContext _context;
        private readonly ICommonService _commonService;
        private IStringLocalizer<ShareResource> _localizerShared;

        public HomeController(DataContext context,
            ICommonService commonService,
            IStringLocalizer<ShareResource> localizerShared
            )
        {
            _context = context;
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
                .Select(c => new
                {
                    c.UserName
                }).Distinct().CountAsync();

            return Ok(_commonService.OkResponse(ProvidersCount, _localizerShared["SuccessMessage"].Value.ToString()));
        }


        #endregion



        #region AllProviderCount

        [HttpGet("GetAllServicesCount")]
        public async Task<ActionResult> GetAllServicesCount()
        {
            var ServicesCount = await _context.BaseMyServiceTBL
                  .Where(c => c.ConfirmedServiceType == ConfirmedServiceType.Confirmed)
                  .CountAsync();
            //.Distinct().CountAsync();

            return Ok(_commonService.OkResponse(ServicesCount, _localizerShared["SuccessMessage"].Value.ToString()));
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

            return Ok(_commonService.OkResponse(allServiceTypes, _localizerShared["SuccessMessage"].Value.ToString()));
        }


        #endregion




    }
}
