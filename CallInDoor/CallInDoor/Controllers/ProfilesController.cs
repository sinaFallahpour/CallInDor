using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DTO.Account;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;

namespace CallInDoor.Controllers
{

    public class ProfilesController : BaseControlle
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountService _accountService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IStringLocalizer<ProfilesController> _localizer;
        private IStringLocalizer<ShareResource> _localizerShared;

        public ProfilesController(UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager,
                DataContext context,
                IHttpContextAccessor httpContextAccessor,
                IStringLocalizer<ProfilesController> localizer,
                IStringLocalizer<ShareResource> localizerShared,
                 IAccountService accountService
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _localizerShared = localizerShared;
            _accountService = accountService;
        }





        #region get Profile


        //  /api/Profile/Profile?username=2Fsina
        [HttpGet("Profile")]
        public async Task<ActionResult> Profile(string username)
        {
            //var result  = await _accountService.CheckTokenIsValid();
            //if (!result)
            //    return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            try
            {
                var profile = await _accountService.ProfileGet(username);
                if (profile == null)
                    return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

                //نیاز به چک نیست
                //var profile = await _accountService.CheckIsCurrentUserName(username);

                //if (profile == null)
                //    return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = profile,
                    Message = _localizerShared["SuccessMessage"].Value.ToString()
                },
                  _localizerShared["SuccessMessage"].Value.ToString()
                 ));
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                             new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }
        }
        #endregion





        #region UpdateProfile

        //  /api/Profile/UpdateProfile
        [HttpPut("UpdateProfile")]
        [Authorize()]
        public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileDTO model)
        {

            var currentSerialNumber = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == PublicHelper.SerialNumberClaim)?.Value;

            var user = await _context.Users.Where(x => x.SerialNumber == currentSerialNumber && x.Id == model.Id)
                .Include(c => c.UsersDegrees)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));




            user.Bio = model.Bio;
            user.Email = model.Email;
            user.Name = model.Name;
            user.LastName = model.LastName;

            var userDegress = new List<User_Degree_Field>();
            foreach (var item in model.UsersDegrees)
            {
                userDegress.Add(new User_Degree_Field()
                {
                    DegreeId = item.DegreeId,
                    DegreeName = item.DegreeName,
                    DegreePersianName = item.DegreePersianName,

                    FieldId = item.FieldId,
                    FieldName = item.FieldName,
                    FieldPersianName = item.FieldPersianName,
                    //UserId = model.Id,
                });
            }
            user.UsersDegrees = null;
            user.UsersDegrees = userDegress;

            ///upload File




            try
            {
                //Upload Photo



                //var SerialNumber = Guid.NewGuid().ToString().GetHash();
                //user.SerialNumber = SerialNumber;

                await _context.SaveChangesAsync();

                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = user,
                    Message = _localizerShared["SuccessMessage"].Value.ToString()
                },
                 _localizerShared["SuccessMessage"].Value.ToString()
                ));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }
        }

        #endregion



    }
}
