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
                var profile = await _accountService.CheckIsCurrentUserName(username);

                if (profile == null)
                    return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

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





        //#region UpdateProfile

        ////  /api/Profile/UpdateProfile
        //[HttpPut("UpdateProfile")]
        //[Authorize()]
        //public async Task<ActionResult> UpdateProfile(UpdateProfileDTO model)
        //{



        //    var user = await _accountService.CheckIsCurrentUserName(model.Id);
        //    if (user == null)
        //        return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

        //    try
        //    {
        //        //Upload Photo

        //        if (await _context.Users.Where(x => x.Email == model.Email && x.Email != user.Email).AnyAsync())
        //            return new JsonResult(new { Status = 0, Message = "این ایمیل  موجود است" });

        //        //var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        //        user.Email = model.Email;
        //        user.Name = model.Name;
        //        user.LastName = model.LastName;
        //        user.Bio = model.Bio;
            



        //        //user.City = model.City;
        //        //user.Phone = model.Phone;
        //        //if (user.WorkExperience != null)
        //        //{
        //        //    user.WorkExperience.CompanyName = model.CompanyName;
        //        //    user.WorkExperience.Descriptions = model.Descriptions;
        //        //    user.WorkExperience.EnterDate = model.WorkEnterDate;
        //        //    user.WorkExperience.ExitDate = model.WorkExitDate;
        //        //    user.WorkExperience.Semat = model.Semat;
        //        //}
        //        //if (user.EducationHistry != null)
        //        //{
        //        //    user.EducationHistry.EnterDate = model.EduEnterDate;
        //        //    user.EducationHistry.ExitDate = model.EduExitDate;
        //        //    user.EducationHistry.MaghTa = model.MaghTa;
        //        //    user.EducationHistry.UnivercityName = model.UnivercityName;
        //        //}

        //        ////اطلاعات ديگر
        //        //user.Birthdate = model.Birthdate;
        //        //user.Gender = model.Gender;
        //        //user.MarriedType = model.MarriedType;
        //        //user.LanguageKnowing = model.LanguageKnowing;




        //        var SerialNumber = Guid.NewGuid().ToString().GetHash();
        //        user.SerialNumber = SerialNumber;

        //        await _context.SaveChangesAsync();

        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var role = userRoles.First();
        //        var currentUser = new UserVM
        //        {
        //            Id = user.Id,
        //            Email = user.Email,
        //            Nickname = user.Nickname,
        //            UserName = user.UserName,
        //            PhotoAddress = user.PhotoAddress,
        //            Token = _jwtGenerator.CreateToken(user, role),
        //        };
        //        var userInfo = _mapper.Map<ApplicationUser, ProfilesDTO>(user);
        //        userInfo.CurrentUser = currentUser;

        //        return new JsonResult(new { Status = 1, Message = "", Data = userInfo });

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { Status = 0, Message = "خطایی رخ داده است", });
        //    }
        //}

        //#endregion



    }
}
