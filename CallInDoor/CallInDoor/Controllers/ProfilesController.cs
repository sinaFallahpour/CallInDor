using System;
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

namespace CallInDoor.Controllers
{
    [Authorize]
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
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProfilesController(
                DataContext context,
                IHttpContextAccessor httpContextAccessor,
                IStringLocalizer<ProfilesController> localizer,
                IStringLocalizer<ShareResource> localizerShared,
                 IAccountService accountService,
                 ICommonService commonService,
                 IWebHostEnvironment hostingEnvironment
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _localizerShared = localizerShared;
            _accountService = accountService;
            _commonService = commonService;
            _hostingEnvironment = hostingEnvironment;
        }


        #endregion

        #region get Profile

        // /api/Profile/Profile?username=2Fsina
        [HttpGet("GetProfile")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetProfile()
        {
            try
            {
                var profile = await _accountService.ProfileGet();
                if (profile == null)
                {
                    List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                    return NotFound(new ApiBadRequestResponse(erros, 404));
                }
                return Ok(_commonService.OkResponse(profile, _localizerShared["SuccessMessage"].Value.ToString()));
            }
            catch
            {
                List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiBadRequestResponse(erros, 500));
            }
        }
        #endregion



        #region UpdateProfile

        // /api/Profile/UpdateProfile
        [HttpPut("UpdateProfile")]
        [Authorize]
        public async Task<ActionResult> UpdateProfile([FromForm] UpdateProfileDTO model)
        {
            //validate   === check kon bishtr az 3 ta nazashte bashe  madrak ra

            var currentSerialNumber = _accountService.GetcurrentSerialNumber();
            var currentUserName = _accountService.GetCurrentUserName();

            var res = await _accountService.ValidateUpdateProfile(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var user = await _context.Users
                .Where(x => x.SerialNumber == currentSerialNumber && x.UserName == currentUserName)
                .Include(c => c.Fields)
                //.ThenInclude(o => o.FieldTBL)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                return NotFound(new ApiBadRequestResponse(erros, 404));
            }
            if (!user.IsEditableProfile)
            {
                List<string> erros = new List<string> { _localizerShared["EditableProfileNotAllowed"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            user.Bio = model.Bio;
            user.Email = model.Email;
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.NationalCode = model.NationalCode;





            //upload immage
            if (model.Image != null && model.Image.Length > 0 && model.Image.IsImage())
            {
                var imageAddress = await _accountService.SvaeFileToHost("Upload/User", user.ImageAddress, model.Image);
                user.ImageAddress = imageAddress;
            }

            //upload video
            if (model.Video != null && model.Video.Length > 0 && model.Video.IsVideo())
            {
                var videoAddress = await _accountService.SvaeFileToHost("Upload/User", user.VideoAddress, model.Video);
                user.VideoAddress = videoAddress;
            }



            _context.FieldTBL.RemoveRange(user.Fields);

            if (model.Fields != null)
            {
                user.Fields = new List<FieldTBL>();
                foreach (var item in model.Fields)
                {
                    var newFiled = new FieldTBL()
                    {
                        Title = item.Title,
                        DegreeType = item.DegreeType
                    };
                    user.Fields.Add(newFiled);
                }
            }


            var certifications = new List<ProfileCertificateTBL>();
            if (model.RequiredFile != null)
            {
                foreach (var item in model.RequiredFile)
                {
                    if (item.RequiredCertificate != null)
                    {
                        foreach (var item2 in item?.RequiredCertificate)
                        {
                            var fileaddress = await _accountService.SvaeFileToHost("Upload/ProfileCertificate", null, item2.File);
                            _context.ProfileCertificateTBL.Add(
                                new ProfileCertificateTBL()
                                {
                                    ServiceId = item.ServiceId,
                                    UserName = currentUserName,
                                    ///uploadFile
                                    FileAddress =await _accountService.SvaeFileToHost("Upload/ProfileCertificate", null, item2.File)

                                });
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, _localizerShared["SuccessMessage"].Value.ToString()));
        }



        #endregion



    }
}
