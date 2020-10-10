using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                 IWebHostEnvironment hostingEnvironment
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _localizerShared = localizerShared;
            _accountService = accountService;
            _hostingEnvironment = hostingEnvironment;
        }


        #endregion
        #region get Profile


        // /api/Profile/Profile?username=2Fsina
        [HttpGet("GetProfile")]
        public async Task<ActionResult> GetProfile(string username)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            try
            {
                var profile = await _accountService.ProfileGet();
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

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                             new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }
        }
        #endregion
        #region UpdateProfile

        // /api/Profile/UpdateProfile
        [HttpPut("UpdateProfile")]
        [Authorize()]
        public async Task<ActionResult> UpdateProfile([FromForm] UpdateProfileDTO model)
        {

            var currentSerialNumber = _accountService.GetcurrentSerialNumber();
            var res = await _accountService.ValidateUpdateProfile(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var user = await _context.Users.Where(x => x.SerialNumber == currentSerialNumber && x.UserName == model.Username)
                .Include(c => c.UsersFields)
                //.ThenInclude(o => o.FieldTBL)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));


            user.Bio = model.Bio;
            user.Email = model.Email;
            user.Name = model.Name;
            user.LastName = model.LastName;

            #region  upload Image
            string uniqueFileName = null;
            if (model.File != null && model.File.Length > 0 && model.File.IsImage())
            {
                if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
                {
                    _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Upload/User");
                uniqueFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + model.File.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                //Delete LastImage Image
                if (!string.IsNullOrEmpty(user.ImageAddress))
                {
                    var LastImagePath = user?.ImageAddress?.Substring(1);
                    LastImagePath = Path.Combine(_hostingEnvironment.WebRootPath, LastImagePath);
                    if (System.IO.File.Exists(LastImagePath))
                    {
                        System.IO.File.Delete(LastImagePath);
                    }
                }
                //update Newe Pic Address To database
                user.ImageAddress = "/Upload/User/" + uniqueFileName;
            }
            #endregion

            _context.UserField.RemoveRange(user.UsersFields);
            if (model.FieldsId != null)
            {
                foreach (var item in model.FieldsId)
                {
                    user.UsersFields.Add(new User_FieldTBL()
                    {
                        UserId = user.Id,
                        FieldId = item
                    });
                }
            }


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





        #endregion
        #region GetAllFields


        // GET: api/ServiceType
        [HttpGet("getAllFields")]
        public async Task<ActionResult> getAllFields()
        {

            var fields = await _context.FieldTBL.Select(c => new
            {
                c.Id,
                c.PersianTitle,
                c.Title,
                c.DegreeType,
            }).ToListAsync();


            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = fields,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
            _localizerShared["SuccessMessage"].Value.ToString()
            ));

        }

        #endregion 



    }
}
