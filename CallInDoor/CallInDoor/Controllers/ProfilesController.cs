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

    public class ProfilesController : BaseControlle
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountService _accountService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IStringLocalizer<ProfilesController> _localizer;
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProfilesController(UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager,
                DataContext context,
                IHttpContextAccessor httpContextAccessor,
                IStringLocalizer<ProfilesController> localizer,
                IStringLocalizer<ShareResource> localizerShared,
                 IAccountService accountService,
                 IWebHostEnvironment hostingEnvironment
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _localizerShared = localizerShared;
            _accountService = accountService;
            _hostingEnvironment = hostingEnvironment;
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

            //////////////////var userDegress = new List<User_Degree_Field>();
            //////////////////foreach (var item in model.UsersDegrees)
            //////////////////{
            //////////////////    userDegress.Add(new User_Degree_Field()
            //////////////////    {
            //////////////////        DegreeId = item.DegreeId,
            //////////////////        DegreeName = item.DegreeName,
            //////////////////        DegreePersianName = item.DegreePersianName,

            //////////////////        FieldId = item.FieldId,
            //////////////////        FieldName = item.FieldName,
            //////////////////        FieldPersianName = item.FieldPersianName,
            //////////////////        //UserId = model.Id,
            //////////////////    });
            //////////////////}
            //////////////////user.UsersDegrees = null;
            //////////////////user.UsersDegrees = userDegress;

            ///upload File



            try
            {
                //Upload Photo


                //var slideFromDb = _context.TBL_Sliders.SingleOrDefault(c => c.Id == model.Id);
                //slideFromDb.Description = model.Description;
                //slideFromDb.Title = model.Title;
                //slideFromDb.IsActive = model.IsActive;
                //slideFromDb.LanguageType = model.LanguageType;
                var err = new List<string>();

                #region file validation
                if (model.File != null)
                {
                    string uniqueFileName = null;
                    if (!model.File.IsImage())
                    {
                        //context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(errors));
                        err.Add(_localizer["Bad Image Format"].Value.ToString());
                        return BadRequest(new ApiBadRequestResponse(err));
                    }
                    if (model.File.Length > 1000000)
                    {
                        err.Add(_localizer["File Is Too Large"].Value.ToString());
                    }
                    if (model.File.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Upload/User");
                        uniqueFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + model.File.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            model.File.CopyTo(stream);
                        }

                        if (!string.IsNullOrEmpty(user.ImageAddress))
                        {
                            var LastImagePath = user.ImageAddress.Substring(1);
                            LastImagePath = Path.Combine(_hostingEnvironment.WebRootPath, LastImagePath);
                            if (System.IO.File.Exists(LastImagePath))
                            {
                                System.IO.File.Delete(LastImagePath);
                            }

                            //System.IO.File.Delete(LastImagePath);
                        }

                        //update Newe Pic Address To database
                        user.ImageAddress = "Upload/User" + uniqueFileName;
                    }
                }
                #endregion file validation














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





        // GET: api/ServiceType
        [HttpGet("getAllDegeeWithFields")]
        public async Task<ActionResult> getAllDegeeWithFields()
        {

            var degress = await _context.DegreeTBL.Select(c => new
            {
                c.Id,
                c.PersianTitle,
                c.Title,
                Fields = c.Fields.Select(x => new
                {
                    x.PersianTitle,
                    x.Title,
                    x.Id
                })
            }).ToListAsync();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = degress,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
            _localizerShared["SuccessMessage"].Value.ToString()
            ));
        }


         

         
    }
}
