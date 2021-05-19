﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Account;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Resource;

namespace CallInDoor.Controllers
{
    public class ProfilesController : BaseControlle
    {

        #region CTOR

        private readonly DataContext _context;
        //private readonly UserManager<AppUser> _userManager;
        //private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IStringLocalizer<ProfilesController> _localizer;
        //private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IResourceServices _resourceServices;

        public ProfilesController(
                DataContext context,
                IHttpContextAccessor httpContextAccessor,
                IStringLocalizer<ProfilesController> localizer,
                 //IStringLocalizer<ShareResource> localizerShared,
                 IAccountService accountService,
                 ICommonService commonService,
                 IWebHostEnvironment hostingEnvironment,
                 IResourceServices resourceServices
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            //_localizerShared = localizerShared;
            _accountService = accountService;
            _commonService = commonService;
            _hostingEnvironment = hostingEnvironment;
            _resourceServices = resourceServices;
        }


        #endregion

        #region  get User ById

        // /api/Profile/Profile?username=2Fsina
        [HttpGet("GetUserById")]
        //[ClaimsAuthorize(IsAdmin = false)]
        [AllowAnonymous]
        public async Task<ActionResult> GetUserById(string UserId)
        {

            var userFromDB = await _context.Users.Where(c => c.Id == UserId)
             .Select(c => new ProfileGetDTO
             {
                 //Username = c.UserName,
                 Name = c.Name,
                 Email = c.Email,
                 LastName = c.LastName,
                 Bio = c.Bio,
                 ImageAddress = c.ImageAddress,
                 VideoAddress = c.VideoAddress,
                 BirthDate = c.BirthDate,
                 IsCompany = c.IsCompany,
                 //IsEditableProfile = c.IsEditableProfile,
                 NationalCode = c.NationalCode,
                 Gender = c.Gender,

                 ProfileCertificate = _context.ProfileCertificateTBL
                                .Where(c => c.UserName == c.UserName)
                                    .Select(c => new ProfileCertificateDTO
                                    {
                                        //Id = c.Id,
                                        //////////////Id = c.RequiredCertificatesId,
                                        FileAdress = c.FileAddress,
                                        ServiceId = c.ServiceId,
                                        Username = c.UserName
                                    }).ToList(),
                 ProfileConfirmType = c.ProfileConfirmType,
                 Fields = c.Fields.Select(r => new FiledsDTO { DegreeType = r.DegreeType, Title = r.Title }).ToList()


                 //ProfileCertificate = _context.ProfileCertificateTBL
                 //            .Where(c => c.UserName == currentusername)
                 //                .Select(c => new ProfileCertificateDTO
                 //                {
                 //                       //Id = c.Id,
                 //                       //////////////Id = c.RequiredCertificatesId,
                 //                       FileAdress = c.FileAddress,
                 //                    ServiceId = c.ServiceId,
                 //                    Username = c.UserName
                 //                }).ToList(),
                 //ProfileConfirmType = c.ProfileConfirmType,
                 //Fields = c.Fields.Select(r => new FiledsDTO { DegreeType = r.DegreeType, Title = r.Title }).ToList()
             }).FirstOrDefaultAsync();



            //var profile = await _accountService.ProfileGet();
            if (userFromDB == null)
            {
                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                //return NotFound(new ApiBadRequestResponse(erros, 404));
                return BadRequest(_commonService.NotFoundErrorReponse(false));

            }
            //return Ok(_commonService.OkResponse(userFromDB, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(userFromDB, false));

        }
        #endregion

        #region get Profile

        // /api/Profile/Profile?username=2Fsina
        [HttpGet("GetProfile")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetProfile()
        {
            var profile = await _accountService.ProfileGet();
            if (profile == null)
            {
                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                //return NotFound(new ApiBadRequestResponse(erros, 404));
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            //return Ok(_commonService.OkResponse(profile, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(profile, false));
        }
        #endregion

        #region UpdateProfile

        [HttpPut("UpdateProfile")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> UpdateProfile([FromForm] UpdateProfileDTO model)
        {
            var currentSerialNumber = _accountService.GetcurrentSerialNumber();
            var currentUserName = _accountService.GetCurrentUserName();
            var res = await _accountService.ValidateUpdateProfile(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var user = await _context.Users
                .Where(x => x.SerialNumber == currentSerialNumber && x.UserName == currentUserName)
                .Include(c => c.Fields)
                .FirstOrDefaultAsync();

            var certificationFromDB = await _context.ProfileCertificateTBL.Where(c => c.UserName == currentUserName).ToListAsync();

            if (user == null)
            {
                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                //return BadRequest(new ApiBadRequestResponse(erros, 404));
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            if (user.IsCompany)
            {
                List<string> erros = new List<string> {
                //"InvalidAttamptYouAreCompany"
                    _resourceServices.GetErrorMessageByKey("InvalidAttamptYouAreCompany")
            };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
            }

            if (!user.IsEditableProfile)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["EditableProfileNotAllowed"].Value.ToString() 
               _resourceServices.GetErrorMessageByKey("EditableProfileNotAllowed")
                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }


            var result = await _accountService.UpdateProfile(user, certificationFromDB, model);
            if (result)
                return Ok(_commonService.OkResponse(null, false));

            //List<string> erroses = new List<string> {  _localizerShared["InternalServerMessage"].Value.ToString() };
            List<string> erroses = new List<string> { _resourceServices.GetErrorMessageByKey("InternalServerMessage") };
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ApiBadRequestResponse(erroses, 500));

        }


        #endregion

        #region get RequiredFile For Profile

        [HttpGet("GetUsersRequiredFile")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetUsersRequiredFile()
        {
            var currentusername = _accountService.GetCurrentUserName();

            var query = (from bs in _context.BaseMyServiceTBL.Where(c => c.UserName == currentusername && c.IsDeleted == false && c.ServiceId != null)
                         join s in _context.ServiceTBL
                         on bs.ServiceId equals s.Id
                         join req in _context.ServidceTypeRequiredCertificatesTBL.Where(c => c.Isdeleted == false)
                          on s.Id equals req.ServiceId
                         select new
                         {
                             serviceId = s.Id,

                             //serviceName = s.Name,
                             //ServicePersianName = s.PersianName,

                             serviceName = _commonService.GetNameByCulture(s),
                             //ServicePersianName = s.PersianName,
                             ServidceTypeRequiredCertificatesTBL = s.ServidceTypeRequiredCertificatesTBL.Select(c => new { c.Id, c.FileName, c.PersianFileName, c.ServiceId })
                         }).Distinct()
                          .AsQueryable();

            var requiredServiceForUser = await query.ToListAsync();
            //return Ok(_commonService.OkResponse(requiredServiceForUser, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(requiredServiceForUser, false));

        }
        #endregion







        #region  Firm
        [HttpGet("GetFirmProfile")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetFirmProfile()
        {
            var profile = await _accountService.ProfileFirmGet();
            if (profile == null)
            {
                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                //return BadRequest(new ApiBadRequestResponse(erros, 404));
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            //return Ok(_commonService.OkResponse(profile, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(profile, false));
        }




        [HttpPut("UpdatefirmProfile")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> UpdatefirmProfile([FromForm] UpdateFirmProfileDTO model)
        {
            var currentUserName = _accountService.GetCurrentUserName();

            var servicesIds = await _context.ServiceTBL.Select(c => c.Id).ToListAsync();
            var res = await _accountService.ValidateUpdateFirmProfile(model, servicesIds);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var user = await _context.Users
                                 .Where(x => x.UserName == currentUserName)
                                          .Include(c => c.FirmProfile)
                                              .ThenInclude(c => c.FirmServiceCategoryTBLs)
                                                   .FirstOrDefaultAsync();

            var certificationFromDB = await _context.ProfileCertificateTBL.Where(c => c.UserName == currentUserName).ToListAsync();


            #region validation
            if (user == null)
            {
                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                //return BadRequest(new ApiBadRequestResponse(erros, 404));
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            if (!user.IsCompany)
            {
                List<string> erros = new List<string> {
                    //"invalid attampt. you are not a company"
                    _resourceServices.GetErrorMessageByKey("InvalidAttamptYouAreCompany")
                };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
            }
            #endregion

            var result = await _accountService.UpdateFirmProfile(user, servicesIds, certificationFromDB, model);
            if (!result)
            {
                List<string> erroses = new List<string> { 
                    //_localizerShared["InternalServerMessage"].Value.ToString() 
               _resourceServices.GetErrorMessageByKey("InternalServerMessage")
                };
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses, 500));
            }

            //return Ok(_commonService.OkResponse(null, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));
        }




        #endregion
    }
}
