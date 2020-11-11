using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Response;
using Domain.DTO.Settings;
using Domain.Utilities;
using Helper.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Common;

namespace CallInDoor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICommonService _commonService;

        public SettingsController(DataContext context, ICommonService commonService)
        {
            _context = context;
            _commonService = commonService;
        }



        [HttpGet("GetSettings")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetSettings()
        {
            var settings = await _context.SettingsTBL.ToListAsync();
            var settingsFromDB = new SettingsGetDTO()
            {
                Aboutus = settings.Where(c => c.Key == PublicHelper.AboutUsKeyName).SingleOrDefault().Value,
                AboutusEnglish = settings.Where(c => c.Key == PublicHelper.AboutUsKeyName).SingleOrDefault().EnglishValue,

                Address = settings.Where(c => c.Key == PublicHelper.AddressKeyName).SingleOrDefault().Value,
                AddressEnglish = settings.Where(c => c.Key == PublicHelper.AddressKeyName).SingleOrDefault().EnglishValue,

                Email = settings.Where(c => c.Key == PublicHelper.EmailKeyName).SingleOrDefault().Value,
                EmailEnglish = settings.Where(c => c.Key == PublicHelper.EmailKeyName).SingleOrDefault().EnglishValue,

                PhoneNumber = settings.Where(c => c.Key == PublicHelper.PhoneNumberKeyName).SingleOrDefault().Value,
                PhoneNumberEnglish = settings.Where(c => c.Key == PublicHelper.PhoneNumberKeyName).SingleOrDefault().EnglishValue,
            };
            return Ok(_commonService.OkResponse(settingsFromDB, PubicMessages.SuccessMessage));
        }





        ///// <summary>
        ///// edit settings
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("CreateSettings")]
        ////[Authorize(Roles = PublicHelper.ADMINROLE)]
        ////[ClaimsAuthorize(IsAdmin = true)]
        //public async Task<ActionResult> CreateSettings()
        //{

        //    var settingsTBL = new List<SettingTBL>();


        //    var AboutUsSettings = new SettingTBL()
        //    {
        //        CreatedAt = DateTime.Now,
        //        Key = PublicHelper.AboutUsKeyName,
        //    };

        //    var AddressSettings = new SettingTBL()
        //    {
        //        CreatedAt = DateTime.Now,
        //        Key = PublicHelper.AddressKeyName,
        //    };

        //    var Emailsettings = new SettingTBL()
        //    {
        //        CreatedAt = DateTime.Now,
        //        Key = PublicHelper.EmailKeyName,
        //    };

        //    var PhoneNumberSettings = new SettingTBL()
        //    {
        //        CreatedAt = DateTime.Now,
        //        Key = PublicHelper.PhoneNumberKeyName,
        //    };

        //    settingsTBL.Add(AboutUsSettings);
        //    settingsTBL.Add(AddressSettings);
        //    settingsTBL.Add(Emailsettings);
        //    settingsTBL.Add(PhoneNumberSettings);
        //    await _context.SettingsTBL.AddRangeAsync(settingsTBL);
        //    await _context.SaveChangesAsync();

        //    return Ok("OK");
        //}






        /// <summary>
        /// edit settings
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UpdateSettings")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> UpdateSettings(SettingsGetDTO model)
        {

            var settingsFromDB = await _context.SettingsTBL.ToListAsync();

            var aboutUs = settingsFromDB.SingleOrDefault(c => c.Key == PublicHelper.AboutUsKeyName);
            if (aboutUs != null)
            {
                aboutUs.Value = model.Aboutus;
                aboutUs.EnglishValue = model.AboutusEnglish;
                aboutUs.UpdatedAt = DateTime.Now;
            }

            var address = settingsFromDB.SingleOrDefault(c => c.Key == PublicHelper.AddressKeyName);
            if (address != null)
            {
                address.Value = model.Address;
                address.EnglishValue = model.AddressEnglish;
                address.UpdatedAt = DateTime.Now;
            }

            var phoneNumber = settingsFromDB.SingleOrDefault(c => c.Key == PublicHelper.PhoneNumberKeyName);
            if (phoneNumber != null)
            {
                phoneNumber.Value = model.PhoneNumber;
                phoneNumber.EnglishValue = model.PhoneNumberEnglish;
                phoneNumber.UpdatedAt = DateTime.Now;
            }

            var email = settingsFromDB.SingleOrDefault(c => c.Key == PublicHelper.EmailKeyName);
            if (email != null)
            {
                email.Value = model.Email;
                email.EnglishValue = model.EmailEnglish;
                email.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return Ok(_commonService.OkResponse(settingsFromDB, PubicMessages.SuccessMessage));
        }





    }
}
