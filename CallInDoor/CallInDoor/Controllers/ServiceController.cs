
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities;
using Domain.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Domain.Utilities;
using Service.Interfaces.Account;
using Domain.DTO.Response;
using Microsoft.Extensions.Localization;
using Service.Interfaces.ServiceType;
using Domain.DTO.Service;
using Domain.Enums;
using CallInDoor.Config.Attributes;
using Service.Interfaces.Common;
using CallInDoor.Config.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using CallInDoor.Hubs;
using Service.Interfaces.SmsService;
using Service.Interfaces.Resource;

namespace CallInDoor.Controllers
{
    [Route("api/ServiceType")]
    //[ApiController]
    public class ServiceController : BaseControlle
    {
        #region ctor

        private readonly IHubContext<NotificationHub> _hubContext;

        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IServiceService _servicetypeService;
        private readonly ICommonService _commonService;

        private readonly IResourceServices _resourceServices;


        private readonly ISmsService _smsService;

        //private IStringLocalizer<ShareResource> _localizerShared;
        //private IStringLocalizer<ServiceController> _locaLizer;
        public ServiceController(
        IHubContext<NotificationHub> hubContext,

            DataContext context,
              IAccountService accountService,
              RoleManager<AppRole> roleManager,
              IServiceService servicetypeService,
              ICommonService commonService,
              ISmsService smsService,
              //IStringLocalizer<ShareResource> localizerShared,
              //IStringLocalizer<ServiceController> locaLizer,
              IResourceServices resourceServices

            )
        {
            _hubContext = hubContext;
            _context = context;
            _commonService = commonService;
            _smsService = smsService;
            _roleManager = roleManager;
            _accountService = accountService;
            //_localizerShared = localizerShared;
            //_locaLizer = locaLizer;
            _servicetypeService = servicetypeService;
            _resourceServices = resourceServices;
        }

        #endregion
        #region ServiceType

        /// <summary>
        /// گرفتن یک سرویس تایپ مثل Translate
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetServiceByIdForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetServiceByIdForAdmin(int Id)
        {

            var Service = await _context.ServiceTBL
               .AsNoTracking()
               .Where(c => c.Id == Id)
               .Select(c => new
               {
                   c.Id,
                   c.Name,
                   c.PersianName,
                   c.ImageAddress,
                   c.SitePercent,
                   c.IsEnabled,
                   c.MinPriceForService,
                   c.MinSessionTime,
                   c.AcceptedMinPriceForNative,
                   c.AcceptedMinPriceForNonNative,

                   topTenPackagePrice = c.TopTenPackageTBL.FirstOrDefault().Price,
                   usersCount = c.TopTenPackageTBL.FirstOrDefault().Count,
                   dayCount = c.TopTenPackageTBL.FirstOrDefault().DayCount,
                   hourCount = c.TopTenPackageTBL.FirstOrDefault().HourCount,

                   c.Color,
                   c.IsProfileOptional,
                   RoleId = c.AppRole.Id,
                   RequiredCertificates = c.ServidceTypeRequiredCertificatesTBL.Where(c => c.Isdeleted == false).Select(c => new { c.Id, c.FileName, c.PersianFileName }),
                   tags = c.Tags.Where(p => p.IsEnglisTags && !string.IsNullOrEmpty(p.TagName)).Select(s => s.TagName).ToList(),
                   persinaTags = c.Tags.Where(p => p.IsEnglisTags == false && !string.IsNullOrEmpty(p.PersianTagName)).Select(s => s.PersianTagName).ToList()

               }).FirstOrDefaultAsync();

            if (Service == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                //return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }

            return Ok(_commonService.OkResponse(Service, PubicMessages.SuccessMessage));


        }






        // GET: api/GetAllServiceForAdmin
        [HttpGet("GetAllServiceForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllServiceForAdmin()
        {
            var AllServices = await _context
                  .ServiceTBL
                  .AsNoTracking()
                  .Select(c => new
                  {
                      c.Id,
                      c.IsEnabled,
                      c.Name,
                      c.PersianName,
                      c.ImageAddress,
                      c.SitePercent,
                      c.Color,
                      RoleName = c.AppRole.Name,
                      c.AcceptedMinPriceForNative,
                      c.AcceptedMinPriceForNonNative,
                      c.IsProfileOptional,
                      c.MinSessionTime,
                      c.MinPriceForService
                  }).ToListAsync();
            return Ok(_commonService.OkResponse(AllServices, PubicMessages.SuccessMessage));
        }





        // GET: api/GetAllService
        [HttpGet("GetAllActiveService")]
        [Authorize]
        public async Task<ActionResult> GetAllActiveService()
        {
            var services = await _servicetypeService.GetAllActiveService();
            return Ok(_commonService.OkResponse(services, PubicMessages.SuccessMessage));
        }





        [HttpGet("GetTagsForService")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetTagsForService(int? Id)
        {
            if (Id == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            var IsPersian = _commonService.IsPersianLanguage();

            var tags = await _context.ServiceTags.AsNoTracking().Where(c => c.ServiceId == Id).Select(c => new GetTagsDTO
            {
                Id = c.Id,
                TagName = _commonService.GetNameByCulture(c)
            })
            .ToListAsync();
            return Ok(_commonService.OkResponse(tags, false));
            //return Ok(_commonService.OkResponse(tags, _localizerShared["SuccessMessage"].Value.ToString()));
        }




        /// <summary>
        /// قیمت هر حوضه وزمان جلسه
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetTimeAndPriceForService/{Id}")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetTimeAndPriceForService(int? Id)
        {
            if (Id == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            var serviceTimsandProice = await _context
                .ServiceTBL
                .AsNoTracking()
                .Where(c => c.Id == Id)
                .Select(c => new
                {
                    c.AcceptedMinPriceForNative,
                    c.AcceptedMinPriceForNonNative,
                    c.MinPriceForService,
                    c.MinSessionTime,
                }).ToListAsync();
            return Ok(_commonService.OkResponse(serviceTimsandProice, false));

            //return Ok(_commonService.OkResponse(serviceTimsandProice, _localizerShared["SuccessMessage"].Value.ToString()));
        }



        /// <summary>
        /// قیمت هر حوضه وزمان جلسه
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetTimeAndPriceForService")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetTimeAndPriceForService()
        {
            var serviceTimsandProice = await _context.ServiceTBL.AsNoTracking()
                .Select(c => new
                {
                    c.AcceptedMinPriceForNative,
                    c.AcceptedMinPriceForNonNative,
                    c.MinPriceForService,
                    c.MinSessionTime
                }).ToListAsync();

            return Ok(_commonService.OkResponse(serviceTimsandProice, false));

            //return Ok(_commonService.OkResponse(serviceTimsandProice, _localizerShared["SuccessMessage"].Value.ToString()));
        }




        /// <summary>
        /// ایجاد  سرویس
        /// </summary>
        /// <param name="CreateServiceDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> CreateForAdmin([FromForm] CreateServiceDTO model)
        {
            var res = await validateCreateServiceForAdmin(model, false);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var result = await _servicetypeService.Create(model);
            if (result)
                return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));


            List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));


            //////////return StatusCode(StatusCodes.Status500InternalServerError,
            //////////  new ApiResponse(500, PubicMessages.InternalServerMessage)
            //////////);
        }





        [NonAction]
        public (bool succsseded, List<string> result) ValidateImageInEditMode(IFormFile image)
        {
            bool IsValid = true;
            var errors = new List<string>();
            if (image != null && !image.IsImage())
            {
                IsValid = false;
                errors.Add("invalid file format. please enter image format");
                return (IsValid, errors);
            }
            return (IsValid, errors);
        }


        [NonAction]
        public (bool succsseded, List<string> result) ValidateImageCreateMode(IFormFile image)
        {
            bool IsValid = true;
            var errors = new List<string>();

            if (image == null || image.Length <= 0)
            {
                IsValid = false;
                errors.Add("image file is required ");
                return (IsValid, errors);
            }
            if (!image.IsImage())
            {
                IsValid = false;
                errors.Add("invalid file format. please enter image format");
                return (IsValid, errors);
            }
            return (IsValid, errors);

        }


        #region Validate  ko30Sher
        [NonAction]
        public async Task<(bool succsseded, List<string> result)> validateCreateServiceForAdmin(CreateServiceDTO model, bool isEditMode)
        {
            bool IsValid = true;
            var errors = new List<string>();


            if (isEditMode)
            {
                var res = ValidateImageInEditMode(model.Image);
                if (!res.succsseded)
                    return res;
            }
            else
            {
                var res = ValidateImageCreateMode(model.Image);
                if (!res.succsseded)
                    return res;
            }

            //else
            //{
            //    if (model.Image == null || model.Image.Length <= 0)
            //    {
            //        IsValid = false;
            //        errors.Add("image file is required ");
            //        return (IsValid, errors);
            //    }
            //    if (!model.Image.IsImage())
            //    {
            //        IsValid = false;
            //        errors.Add("invalid file format. please enter image format");
            //        return (IsValid, errors);
            //    }
            //}




            if (model.DayCount == null && model.HourCount == null)
            {
                IsValid = false;
                errors.Add("please enter day or hour for discount");
                return (IsValid, errors);
            }


            if ((model.RequiredFiles == null || model.RequiredFiles.Count == 0) && !model.IsProfileOptional)
            {
                IsValid = false;
                errors.Add("please enter required files");
                return (IsValid, errors);
            }


            var roleExist = await _context.Roles.AsTracking().AnyAsync(c => c.Id == model.RoleId);
            if (!roleExist)
            {
                IsValid = false;
                errors.Add("invalid Roles");
                return (IsValid, errors);
            }

            if (model.RequiredFiles != null)
            {
                foreach (var item in model.RequiredFiles)
                {
                    if (string.IsNullOrEmpty(item.FileName))
                    {
                        IsValid = false;
                        errors.Add("file name is required");
                        return (IsValid, errors);
                    }
                    if (string.IsNullOrEmpty(item.PersianFileName))
                    {
                        IsValid = false;
                        errors.Add("persian file name is required");
                        return (IsValid, errors);
                    }
                }
            }

            var seviceExist = isEditMode ?
                                  await _context.ServiceTBL.AnyAsync(c => (c.Name == model.Name || c.PersianName == model.PersianName) && c.Id != model.Id)
                               :
                                   await _context.ServiceTBL.AsNoTracking().AnyAsync(c => c.Name == model.Name || c.PersianName == model.PersianName);
            if (seviceExist)
            {
                IsValid = false;
                errors.Add($"name Or persian Name already exist");
                return (IsValid, errors);
                //return BadRequest(new ApiBadRequestResponse(errors));
            }
            return (IsValid, errors);
        }
        #endregion



        [HttpPut("UpdateServiceForAdmin")]
        //[Authorize(Roles = PublicHelper.ADMINROLE)]
        //[ClaimsAuthorize(IsAdmin = true)]
        public async Task<IActionResult> UpdateServiceForAdmin([FromForm] CreateServiceDTO model)
        {
            #region validation
            var res = await validateCreateServiceForAdmin(model, true);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));
            var service = await _servicetypeService.GetByIdWithJoin(model.Id);
            if (service == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
                //////////return NotFound(new ApiResponse(404, "service " + PubicMessages.NotFoundMessage));
            }

            #endregion

            var result = await _servicetypeService.Update(service, model);
            if (result)
                return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));

            List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));


            //////////////return StatusCode(StatusCodes.Status500InternalServerError,
            //////////////                  new ApiResponse(500, PubicMessages.InternalServerMessage));

        }



        #endregion


        #region  MyService




        /// <summary>
        ///گرفتن سرویس تایپ های من
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyServiceCategory")]
        //[Authorize]
        [ClaimsAuthorize]
        public async Task<ActionResult> GetAllMyServiceCategory()
        {
            var currentusername = _accountService.GetCurrentUserName();
            var isPersian = _commonService.IsPersianLanguage();

            var query = (from bs in _context.BaseMyServiceTBL.Where(c => c.UserName == currentusername && c.IsDeleted == false && c.ServiceId != null)
                         join s in _context.ServiceTBL
                         on bs.ServiceId equals s.Id
                         select new ServiceCategoryDTO
                         {
                             ServiceId = s.Id,
                             //ServiceName = isPersian ? s.PersianName : s.Name,
                             ServiceName = _commonService.GetNameByCulture(s),
                             Color = s.Color,
                             IsDisabledByCompany = bs.IsDisabledByCompany
                         }).Distinct()
                          .AsQueryable();

            var myServiceTypes = await query.ToListAsync();
            return Ok(_commonService.OkResponse(myServiceTypes, false));
            //return Ok(_commonService.OkResponse(MyServiceTypes, _localizerShared["SuccessMessage"].Value.ToString()));
        }




        /// <summary>
        /// گرفتن تمام سرویس های من  از نوع یک سرویس تایپ خاص
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyService")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAllMyService(int ServiecTypeId)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var Service = await _context.BaseMyServiceTBL
               .AsNoTracking()
               .Where(c => c.ServiceId == ServiecTypeId && c.UserName == currentUsername && c.IsDeleted == false)
               .Select(c => new
               {
                   c.Id,
                   c.ServiceName,
                   c.ServiceTypes,
                   //c.ServiceType,
                   c.IsDisabledByCompany,
                   //c.IsActive
               }).ToListAsync();

            if (Service == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            return Ok(_commonService.OkResponse(Service, false));
        }


        #region chatService





        /// <summary>
        ///گرفتن یک سرویس از نوع چت و وویس و.. برای یک کاربر
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("/api/userService/GetChatServiceForUser")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetChatServiceForUser(int Id)
        {

            var serviceFromDB = await _context
                .BaseMyServiceTBL.Where(c => c.Id == Id && c.IsDeleted == false && c.IsDisabledByCompany == false)
                .Select(c => new
                {
                    c.MyChatsService.PackageType,
                    c.MyChatsService.BeTranslate,
                    c.MyChatsService.MessageCount,
                    c.MyChatsService.FreeMessageCount,
                    c.MyChatsService.Duration,
                    c.MyChatsService.IsServiceReverse,
                    c.MyChatsService.PriceForNativeCustomer,
                    c.MyChatsService.PriceForNonNativeCustomer,
                    c.Id,
                    c.CatId,
                    c.SubCatId,
                    c.ServiceName,
                    c.ServiceTypes,
                    ////////////c.ServiceType,
                    c.UserName,
                    c.ConfirmedServiceType,
                    c.IsDeleted,

                    c.CreateDate,
                    c.IsEditableService,
                    c.RejectReason,

                }).FirstOrDefaultAsync();

            var currentUsername = _accountService.GetCurrentUserName();

            if (serviceFromDB == null)
            {

                return BadRequest(_commonService.NotFoundErrorReponse(false));
                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                //return NotFound(new ApiBadRequestResponse(erros, 404));
            }

            if (serviceFromDB.UserName != currentUsername)
            {
                List<string> erros = new List<string> {
                    //_localizerShared["UnauthorizedMessage"].Value.ToString() 
                _resourceServices.GetErrorMessageByKey("UnauthorizedMessage")
                };
                return BadRequest(new ApiBadRequestResponse(erros, 401));
            }
            return Ok(_commonService.OkResponse(serviceFromDB, false));
        }





        /// <summary>
        /// chat or voice or video گرفتن اطلاعات برای یک سرویس   
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("/api/userService/GetChatServiceInfoForUpdate")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetChatServiceInfoForUpdate(int id)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var serviceFromDB = await _context
               .BaseMyServiceTBL
               .Where(c => c.Id == id && c.IsDeleted == false && c.IsDisabledByCompany == false)
               .Include(c => c.MyChatsService)
               .FirstOrDefaultAsync();

            #region  validation
            if (serviceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            if (serviceFromDB.UserName != currentUsername)
            {

                List<string> erros = new List<string> { _resourceServices.GetErrorMessageByKey("UnauthorizedMessage") };

                return BadRequest(new ApiBadRequestResponse(erros, 401));
            }
            //////if (serviceFromDB.ServiceType == ServiceType.Service || serviceFromDB.ServiceType == ServiceType.Course)
             if (serviceFromDB.ServiceTypes.Contains("3") || serviceFromDB.ServiceTypes.Contains("4") )
            {
                List<string> erros = new List<string> { _resourceServices.GetErrorMessageByKey("InValidServiceType") };
                return BadRequest(new ApiBadRequestResponse(erros));
            }
            #endregion

            var response = new
            {

                serviceFromDB.ServiceTypes,
                //////////serviceFromDB.ServiceType,
                serviceFromDB.ServiceName,
                serviceFromDB.MyChatsService.PackageType,
                serviceFromDB.MyChatsService.BeTranslate,
                serviceFromDB.MyChatsService.IsServiceReverse,
                serviceFromDB.MyChatsService.PriceForNativeCustomer,
                serviceFromDB.MyChatsService.PriceForNonNativeCustomer,

                serviceFromDB.MyChatsService.FreeMessageCount,
                serviceFromDB.MyChatsService.Duration,
                serviceFromDB.MyChatsService.MessageCount,
                serviceFromDB.IsDisabledByCompany

                //serviceFromDB.ServiceTbl.Name,
                //serviceFromDB.ServiceTbl.Id

            };


            //return Ok(_commonService.OkResponse(response, _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));

        }





        /// <summary>
        /// ایجاد  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/userService/AddChatServiceForUser")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddChatServiceForUser([FromBody] AddChatServiceForUsersDTO model)
        {

            var serviceFromDb = await _context
            .ServiceTBL
            .AsNoTracking()
            .Where(c => c.Id == model.ServiceId)
            .Select(c => new ServiceTBLVM
            {
                IsProfileOptional = c.IsProfileOptional,
                Id = c.Id,
                AcceptedMinPriceForNative = c.AcceptedMinPriceForNative,
                AcceptedMinPriceForNonNative = c.AcceptedMinPriceForNonNative,
                Name = c.Name
            })
              .FirstOrDefaultAsync();


            var res = await _servicetypeService.ValidateChatService(model, serviceFromDb);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));




            ////////////////if (model.ServiceType == ServiceType.ChatVoice)
            ////////////////{
            ////////////////    if (model.PackageType == PackageType.Free)
            ////////////////        model.MessageCount = null;
            ////////////////    else
            ////////////////        model.FreeMessageCount = null;
            ////////////////    //model.Duration = null;
            ////////////////}
            ////////////////else
            ////////////////{
            ////////////////    model.MessageCount = null;
            ////////////////    model.FreeMessageCount = null;
            ////////////////}


            var currentUsername = _accountService.GetCurrentUserName();

            var profiles = await _context.ProfileCertificateTBL.Where(c => c.ServiceId == model.ServiceId && c.UserName == currentUsername)
                                                            .ToListAsync();


            var res1 = profiles.Any(c => c.ProfileConfirmType == ProfileConfirmType.Confirmed);
            var res2 = profiles.Any(c => c.ProfileConfirmType == ProfileConfirmType.Rejected);

            var profileStatus = (res1 == false && res2 == false);

            if (res2)
            {
                var err = new List<string>();
                err.Add(_resourceServices.GetErrorMessageByKey("ProfileRejectedMessage"));
                return BadRequest(new ApiBadRequestResponse(err));
            }

            var BaseMyService = new BaseMyServiceTBL()
            {
                IsProfileOptional = serviceFromDb.IsProfileOptional,
                ConfirmedServiceType = ConfirmedServiceType.Pending,
                IsEditableService = false,
                //Latitude = model.Latitude,
                //Longitude = model.Longitude,
                StarCount = 0,
                Under3StarCount = 0,
                CreateDate = DateTime.Now,
                ServiceName = model.ServiceName,
                ServiceTypes = model.ServiceTypes,
                //////////ServiceType = (ServiceType)model.ServiceType,
                UserName = currentUsername,
                ServiceId = model.ServiceId,
                CatId = model.CatId,
                SubCatId = model.SubCatId,
                IsDeleted = false,
                IsDisabledByCompany = false,
                ProfileConfirmType = profileStatus ? ProfileConfirmType.Pending : res1 == true ? ProfileConfirmType.Confirmed : ProfileConfirmType.Rejected,
                //IsActive = false
            };

            var MyChatService = new MyChatServiceTBL()
            {

                PackageType = model.PackageType,
                BeTranslate = model.BeTranslate,
                //FreeMessageCount = (int)model.FreeMessageCount,
                IsServiceReverse = model.IsServiceReverse,
                PriceForNativeCustomer = (double)model.PriceForNativeCustomer,
                PriceForNonNativeCustomer = (double)model.PriceForNonNativeCustomer,
                BaseMyChatTBL = BaseMyService,
                FreeMessageCount = model.FreeMessageCount,
                MessageCount = model.MessageCount,
                Duration = model.Duration,

            };


            //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
            await _context.MyChatServiceTBL.AddAsync(MyChatService);

            await _context.SaveChangesAsync();
            //return Ok(_commonService.OkResponse(null, _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));
        }






        /// <summary>
        /// آپدیت  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/api/userService/UpdateChatServiceForUser")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> UpdateChatServiceForUser([FromBody] EditChatServiceForUsersDTO model)
        {


            var currentUsername = _accountService.GetCurrentUserName();

            var serviceFromDB = await _context
               .BaseMyServiceTBL
               .Where(c => c.Id == model.Id && c.IsDeleted == false)
               .Include(c => c.MyChatsService)
               .FirstOrDefaultAsync();

            if (serviceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            if (serviceFromDB.UserName != currentUsername)
            {
                return BadRequest(_resourceServices.GetErrorMessageByKey("UnauthorizedMessage"));
            }

            //if (serviceFromDB.ConfirmedServiceType != ConfirmedServiceType.Pending)
            //{
            //    var errors = new List<string>() {
            //          _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
            //   };
            //    return BadRequest(new ApiBadRequestResponse(errors));
            //}



            var res = await _servicetypeService.ValidateEditChatService(model, serviceFromDB);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            //if (serviceFromDB.ServiceType == ServiceType.ChatVoice)
            if (serviceFromDB.ServiceTypes.Contains("0"))
            {
                if (serviceFromDB.MyChatsService.PackageType == PackageType.Free)
                    serviceFromDB.MyChatsService.FreeMessageCount = model.FreeMessageCount;
                else
                    serviceFromDB.MyChatsService.MessageCount = model.MessageCount;
            }

            //if (serviceFromDB.ServiceType == ServiceType.VideoCal || serviceFromDB.ServiceType == ServiceType.VoiceCall)
            if (serviceFromDB.ServiceTypes.Contains("1") || serviceFromDB.ServiceTypes.Contains("2"))
            {
                //model.MessageCount = null;
                //model.FreeMessageCount = null;
                serviceFromDB.MyChatsService.Duration = model.Duration;
            }



            //if (model.PackageType == PackageType.Free)
            //    model.MessageCount = null;
            ////model.Duration = null;
            //else
            //    model.FreeMessageCount = null;


            serviceFromDB.ServiceName = model.ServiceName;
            //////////////serviceFromDB.Latitude = model.Latitude;
            //////////////serviceFromDB.Longitude = model.Longitude;


            //serviceFromDB.ServiceType = (ServiceType)model.ServiceType;
            //serviceFromDB.CatId = model.CatId;
            //serviceFromDB.SubCatId = model.SubCatId;
            //serviceFromDB.BaseMyChatTBL.IsActive = model.IsActive;

            //serviceFromDB.MyChatsService.MessageCount = model.MessageCount;
            //serviceFromDB.MyChatsService.FreeMessageCount = model.FreeMessageCount;
            //serviceFromDB.MyChatsService.Duration = model.Duration;

            //serviceFromDB.MyChatsService.PackageType = model.PackageType;
            serviceFromDB.MyChatsService.BeTranslate = model.BeTranslate;

            serviceFromDB.MyChatsService.IsServiceReverse = model.IsServiceReverse;
            serviceFromDB.MyChatsService.PriceForNativeCustomer = (double)model.PriceForNativeCustomer;
            serviceFromDB.MyChatsService.PriceForNonNativeCustomer = (double)model.PriceForNonNativeCustomer;


            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));

        }








        /// <summary>
        /// حذف  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("/api/userService/DeleteChatServiceForUser")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> DeleteChatServiceForUser(int id)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var serviceFromDB = await _context
               .BaseMyServiceTBL
               .Where(c => c.Id == id && c.IsDeleted == false)
               .FirstOrDefaultAsync();

            if (serviceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            if (serviceFromDB.UserName != currentUsername)
            {
                List<string> erros = new List<string> {
                    _resourceServices.GetErrorMessageByKey("UnauthorizedMessage")
                };
                return Unauthorized(new ApiBadRequestResponse(erros, 401));
            }

            //if (serviceFromDB.ConfirmedServiceType != ConfirmedServiceType.Pending)
            //{
            //    var errors = new List<string>() {
            //          _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
            //   };
            //    return BadRequest(new ApiBadRequestResponse(errors));
            //}

            serviceFromDB.IsDeleted = true;



            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));


        }



        #endregion

        #region ServiceService

        /// <summary>
        /// ایجاد سرویس service برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/userService/AddServiceServiceForUser")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddServiceServiceForUser([FromBody] AddServiceServiceForUsersDTO model)
        {
            var serviceFromDb = await _context
            .ServiceTBL
            .AsNoTracking()
            .Where(c => c.Id == model.ServiceId)
            .Select(c => new ServiceTBLVM
            {
                IsProfileOptional = c.IsProfileOptional,
                MinPriceForService = c.MinPriceForService,
                Id = c.Id,
                Name = c.Name
            })
            .FirstOrDefaultAsync();


            var res = await _servicetypeService.ValidateServiceService(model, serviceFromDb);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            if (model.FileNeeded == false)
                model.FileDescription = null;

            var curentUsername = _accountService.GetCurrentUserName();

            var profiles = await _context.ProfileCertificateTBL
                         .Where(c => c.ServiceId == model.ServiceId && c.UserName == curentUsername).ToListAsync();


            var res1 = profiles.Any(c => c.ProfileConfirmType == ProfileConfirmType.Confirmed);
            var res2 = profiles.Any(c => c.ProfileConfirmType == ProfileConfirmType.Rejected);

            var profileStatus = (res1 == false && res2 == false);
            var BaseMyService = new BaseMyServiceTBL()
            {
                IsProfileOptional = serviceFromDb.IsProfileOptional,
                ConfirmedServiceType = ConfirmedServiceType.Pending,
                ////////Latitude = model.Latitude,
                ////////Longitude = model.Longitude,
                CreateDate = DateTime.Now,
                ServiceName = model.ServiceName,
                ServiceTypes = "3",
                //ServiceType = (ServiceType)model.ServiceType,
                UserName = curentUsername,
                ServiceId = model.ServiceId,
                CatId = model.CatId,
                SubCatId = model.SubCatId,
                IsDeleted = false,
                IsDisabledByCompany = false,
                ProfileConfirmType = profileStatus ? ProfileConfirmType.Pending : res1 == true ? ProfileConfirmType.Confirmed : ProfileConfirmType.Rejected,

                //IsActive = false
            };

            var tags = model.Tags + "," + model.CustomTags;

            var MyChatService = new MyServiceServiceTBL()
            {

                Description = model.Description,
                DeliveryItems = model.DeliveryItems,
                FileNeeded = model.FileNeeded,
                FileDescription = model.FileDescription,
                HowWorkConducts = model.HowWorkConducts,
                BeTranslate = model.BeTranslate,
                Price = (double)model.Price,
                WorkDeliveryTimeEstimation = model.WorkDeliveryTimeEstimation,
                Tags = tags,
                AreaId = model.AreaId,
                SpecialityId = model.SpecialityId,
                BaseMyChatTBL = BaseMyService,
            };


            //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
            await _context.MyServiceServiceTBL.AddAsync(MyChatService);
            await _context.SaveChangesAsync();
            //return Ok(_commonService.OkResponse(null, _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));

        }



        /// <summary>
        ///گرفتن یک سرویس از نوع چت و وویس و.. برای یک کاربر
        ///این  ای پی ای برای کاربران بیرونی ایی است که مسخواهند سرویس من را ببینند
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("/api/userService/GetServiceServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetServiceServiceForUser(int Id)
        {

            var serviceFromDB = await _context
                .BaseMyServiceTBL
                .AsNoTracking()
               .Where(c => c.Id == Id && c.IsDeleted == false && c.IsDisabledByCompany == false)
               .Select(c => new
               {
                   c.MyServicesService.Id,
                   c.MyServicesService.Description,
                   c.MyServicesService.BeTranslate,
                   c.MyServicesService.FileNeeded,
                   c.MyServicesService.FileDescription,
                   c.MyServicesService.Price,
                   c.MyServicesService.WorkDeliveryTimeEstimation,
                   c.MyServicesService.HowWorkConducts,
                   c.MyServicesService.DeliveryItems,
                   c.MyServicesService.Tags,
                   c.MyServicesService.AreaId,
                   c.MyServicesService.SpecialityId,
                   c.CatId,
                   c.SubCatId,
                   c.ServiceName,
                   c.ServiceTypes,
                   //c.ServiceType,
                   c.ServiceId,
                   c.UserName,
                   c.ConfirmedServiceType,
                   c.CreateDate,
                   c.IsDeleted,
                   c.IsEditableService,
                   c.RejectReason,
                   //c.BaseMyChatTBL.IsConfirmByAdmin
               }).FirstOrDefaultAsync();





            if (serviceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            var currentUsername = _accountService.GetCurrentUserName();
            if (serviceFromDB.UserName != currentUsername)
            {
                List<string> erros = new List<string> {
                    _resourceServices.GetErrorMessageByKey("UnauthorizedMessage")
                };
                return Unauthorized(new ApiBadRequestResponse(erros, 401));
            }

            return Ok(_commonService.OkResponse(serviceFromDB, false));


        }



        /// <summary>
        /// آپدیت  سرویس service برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/api/userService/UpdateServiceServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> UpdateServiceServiceForUser([FromBody] AddServiceServiceForUsersDTO model)
        {
            var serviceFromDb = await _context
          .ServiceTBL
          .AsNoTracking()
          .Where(c => c.Id == model.ServiceId)
          .Select(c => new ServiceTBLVM
          {
              IsProfileOptional = c.IsProfileOptional,
              MinPriceForService = c.MinPriceForService,
              Id = c.Id,
              Name = c.Name
          })
          .FirstOrDefaultAsync();


            var res = await _servicetypeService.ValidateServiceService(model, serviceFromDb);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var serviceFromDB = await _context
                .BaseMyServiceTBL
                .Where(c => c.Id == model.Id && c.IsDeleted == false)
                .Include(c => c.MyServicesService)
                .FirstOrDefaultAsync();

            if (serviceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            var currentUsername = _accountService.GetCurrentUserName();
            if (serviceFromDB.UserName != currentUsername)
            {
                List<string> erros = new List<string> { _resourceServices.GetErrorMessageByKey("UnauthorizedMessage") };
                return Unauthorized(new ApiBadRequestResponse(erros, 401));
            }

            //if (serviceFromDB.ConfirmedServiceType != ConfirmedServiceType.Pending)
            //{
            //    var errors = new List<string>() {
            //          _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
            //   };
            //    return BadRequest(new ApiBadRequestResponse(errors));
            //}

            serviceFromDB.ServiceName = model.ServiceName;
            ////////serviceFromDB.Latitude = model.Latitude;
            ////////serviceFromDB.Longitude = model.Longitude;
            //serviceFromDB.ServiceType = (ServiceType)model.ServiceType;
            //serviceFromDB.CatId = model.CatId;
            //serviceFromDB.SubCatId = model.SubCatId;

            //serviceFromDB.BaseMyChatTBL.IsActive = model.IsActive;


            serviceFromDB.MyServicesService.Description = model.Description;
            serviceFromDB.MyServicesService.BeTranslate = model.BeTranslate;
            serviceFromDB.MyServicesService.FileNeeded = model.FileNeeded;
            serviceFromDB.MyServicesService.FileDescription = model.FileDescription;
            serviceFromDB.MyServicesService.Price = (double)model.Price;
            serviceFromDB.MyServicesService.WorkDeliveryTimeEstimation = model.WorkDeliveryTimeEstimation;
            serviceFromDB.MyServicesService.HowWorkConducts = model.HowWorkConducts;
            serviceFromDB.MyServicesService.DeliveryItems = model.DeliveryItems;
            serviceFromDB.MyServicesService.Tags = model.Tags + "," + model.CustomTags;


            await _context.SaveChangesAsync();
            //return Ok(_commonService.OkResponse(null, _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));


        }





        /// <summary>
        /// حذف  سرویس service برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("/api/userService/DeleteServiceServiceForUser")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> DeleteServiceServiceForUser(int id)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var serviceFromDB = await _context
                .BaseMyServiceTBL
                .Where(c => c.Id == id && c.IsDeleted == false)
                .FirstOrDefaultAsync();


            if (serviceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            if (serviceFromDB.UserName != currentUsername)
            {
                List<string> erros = new List<string> { _resourceServices.GetErrorMessageByKey("UnauthorizedMessage") };
                return BadRequest(new ApiBadRequestResponse(erros, 401));
            }

            //if (serviceFromDB.ConfirmedServiceType != ConfirmedServiceType.Pending)
            //{
            //    var errors = new List<string>() {
            //          _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
            //   };
            //    return BadRequest(new ApiBadRequestResponse(errors));
            //}

            serviceFromDB.IsDeleted = true;


            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));


        }

        #endregion


        #region CourseService

        /// <summary>
        /// ایجاد  سرویس course برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/userService/AddCourseServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddCourseServiceForUser([FromForm] AddCourseServiceForUsersDTO model)
        {

            var serviceFromDb = await _context
             .ServiceTBL
             .AsNoTracking()
             .Where(c => c.Id == model.ServiceId)
             .Select(c => new ServiceTBLVM
             {
                 IsProfileOptional = c.IsProfileOptional,
                 Id = c.Id,
                 MinPriceForService = c.MinPriceForService,
                 Name = c.Name
             })
             .FirstOrDefaultAsync();


            var res = await _servicetypeService.ValidateCourseService(model, serviceFromDb);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));


            var previewFilreAddress = _servicetypeService.SvaeFileToHost("Upload/CoursePreview/", model.PreviewFile);
            if (previewFilreAddress == null)
            {
                List<string> erros = new List<string> {
                    _resourceServices.GetErrorMessageByKey("FileUploadErrorMessage")
                };
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiBadRequestResponse(erros, 500));
            }
            //return StatusCode(StatusCodes.Status500InternalServerError,
            //        new ApiResponse(500, _localizerShared["FileUploadErrorMessage"].Value.ToString()));


            var curentUsername = _accountService.GetCurrentUserName();

            var profiles = await _context.ProfileCertificateTBL
                        .Where(c => c.ServiceId == model.ServiceId && c.UserName == curentUsername).ToListAsync();


            var res1 = profiles.Any(c => c.ProfileConfirmType == ProfileConfirmType.Confirmed);
            var res2 = profiles.Any(c => c.ProfileConfirmType == ProfileConfirmType.Rejected);

            var profileStatus = (res1 == false && res2 == false);


            var currentUsername = _accountService.GetCurrentUserName();
            var BaseMyService = new BaseMyServiceTBL()
            {
                IsProfileOptional = serviceFromDb.IsProfileOptional,
                //////////////Latitude = model.Latitude,
                //////////////Longitude = model.Longitude,
                ConfirmedServiceType = ConfirmedServiceType.Pending,
                CreateDate = DateTime.Now,
                ServiceName = model.ServiceName,
                //////ServiceType = ServiceType.Course,
                ServiceTypes = "4",
                UserName = currentUsername,
                ServiceId = model.ServiceId,
                CatId = model.CatId,
                IsDeleted = false,
                //ProfileConfirmType = ProfileConfirmType.Pending
                ProfileConfirmType = profileStatus ? ProfileConfirmType.Pending : res1 == true ? ProfileConfirmType.Confirmed : ProfileConfirmType.Rejected,

                //IsConfirmByAdmin = false,
            };


            var MyCourseService = new MyCourseServiceTBL()
            {
                Description = model.Description,
                TotalLenght = model.TotalLenght,
                DisCountPercent = model.DisCountPercent,
                NewCategory = model.NewCategory,
                Price = (double)model.Price,
                ////////////////TopicsTBLs  
                PreviewVideoAddress = previewFilreAddress,

                BaseMyChatTBL = BaseMyService,
            };


            //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
            await _context.MyCourseServiceTBL.AddAsync(MyCourseService);

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));
        }




        //////////////////تمام نشده
        /// <summary>
        /// آپدیت سرویس course برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/api/userService/UpdateCourseServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> UpdateCourseServiceForUser([FromBody] AddCourseServiceForUsersDTO model)
        {
            var serviceFromDb = await _context
             .ServiceTBL
             .AsNoTracking()
             .Where(c => c.Id == model.ServiceId)
             .Select(c => new ServiceTBLVM
             {
                 IsProfileOptional = c.IsProfileOptional,
                 Id = c.Id,
                 MinPriceForService = c.MinPriceForService,
                 Name = c.Name
             })
             .FirstOrDefaultAsync();


            var res = await _servicetypeService.ValidateCourseService(model, serviceFromDb);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var serviceFromDB = await _context
                .MyCourseServiceTBL
                .Where(c => c.Id == model.Id)
                .Include(c => c.BaseMyChatTBL)
                .Where(c => c.BaseMyChatTBL.IsDeleted == false)
                .Include(c => c.TopicsTBLs)
                .FirstOrDefaultAsync();

            //await _context.BaseMyServiceTBL.Where(c => c.Id == serviceFromDB.BaseId).LoadAsync();
            //await _context.MyCourseTopicsTBL.Where(c => c.MyCourseId == serviceFromDB.Id).LoadAsync();

            if (serviceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }


            var currentUsername = _accountService.GetCurrentUserName();
            if (serviceFromDB.BaseMyChatTBL.UserName != currentUsername)
            {
                List<string> erros = new List<string> { _resourceServices.GetErrorMessageByKey("UnauthorizedMessage") };
                return BadRequest(new ApiBadRequestResponse(erros, 401));
            }

            //if (serviceFromDB.BaseMyChatTBL.ConfirmedServiceType != ConfirmedServiceType.Pending)
            //{
            //    var errors = new List<string>() {
            //          _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
            //   };
            //    return BadRequest(new ApiBadRequestResponse(errors));
            //}

            serviceFromDB.BaseMyChatTBL.ServiceName = model.ServiceName;
            serviceFromDB.BaseMyChatTBL.CatId = model.CatId;
            //////////////serviceFromDB.BaseMyChatTBL.Latitude = model.Latitude;
            //////////////serviceFromDB.BaseMyChatTBL.Longitude = model.Longitude;

            serviceFromDB.Description = model.Description;
            serviceFromDB.NewCategory = model.NewCategory;
            serviceFromDB.Price = (double)model.Price;
            serviceFromDB.TotalLenght = model.TotalLenght;
            serviceFromDB.DisCountPercent = model.DisCountPercent;
            ////PreviewVideoAddress  
            ////topics

            await _context.SaveChangesAsync();
            //return Ok(_commonService.OkResponse(null, _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));


        }
        /////////////////////////////////////////////////////////تمام نشده


        #endregion



        #endregion

        #region other 
        #region  get Users serviceBy userId and CategoryId

        [HttpGet("GetUsersServiceById")]
        //////////[ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetUsersServiceById(string UserId, int serviceCategoryId)
        {

            var username = await _context.Users.Where(c => c.Id == UserId).Select(c => c.UserName).FirstOrDefaultAsync();
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }


            var isPersian = _commonService.IsPersianLanguage();

            var userFromDB = await _context.BaseMyServiceTBL
                .Where(c => c.UserName == username &&
                c.ServiceId == serviceCategoryId &&
                c.IsDeleted == false &&
                c.IsDisabledByCompany == false &&
                c.ConfirmedServiceType == ConfirmedServiceType.Confirmed && c.ProfileConfirmType == ProfileConfirmType.Confirmed)
                .Select(c => new
                {
                    c.Id,
                    c.ServiceName,
                    c.ServiceTypes,
                    //////c.ServiceType,
                    //CategoryName = isPersian ? c.CategoryTBL.PersianTitle : c.CategoryTBL.Title,
                    CategoryName = _commonService.GetNameByCulture(c.CategoryTBL),
                    //CategoryPersianName = c.CategoryTBL.PersianTitle,

                    //SubCategoryName = isPersian ? c.SubCategoryTBL.PersianTitle : c.SubCategoryTBL.Title,
                    SubCategoryName = _commonService.GetNameByCulture(c.SubCategoryTBL),
                    //SubCategoryPersianName = c.SubCategoryTBL.PersianTitle,

                    ChatService = new
                    {
                        PackageType = c.MyChatsService != null ? c.MyChatsService.PackageType : null,
                        Price = c.MyChatsService != null ? c.MyChatsService.PriceForNativeCustomer : null,
                        //PriceForNonNativeCustomer = c.MyChatsService != null ? c.MyChatsService?.PriceForNonNativeCustomer : null,
                        Duration = c.MyChatsService != null ? c.MyChatsService.Duration : null,
                        FreeMessageCount = c.MyChatsService != null ? c.MyChatsService.FreeMessageCount : null,
                    },

                    ServiceService = new
                    {
                        Price = c.MyServicesService != null ? c.MyServicesService.Price : null,
                    },

                    CourseService = new
                    {
                        Price = c.MyCourseService != null ? c.MyCourseService.Price : null,
                    },
                }).ToListAsync();

            //var profile = await _accountService.ProfileGet();
            if (userFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            return Ok(_commonService.OkResponse(userFromDB, false));
            //return Ok(_commonService.OkResponse(userFromDB, _localizerShared["SuccessMessage"].Value.ToString()));
        }

        #endregion





        /// <summary>
        /// این اگر کاربرانی بخواهند سرویس های من را ببینند جزییاتش را  این ای پی ای کال ود
        /// </summary>
        /// <param name="baseServiceId"></param>
        /// <returns></returns>
        [HttpGet("GetUsersServiceDetailsById")]
        public async Task<ActionResult> GetUsersServiceDetailsById(int baseServiceId)
        {

            var isPersian = _commonService.IsPersianLanguage();

            var userFromDB = await _context.BaseMyServiceTBL
                .Where(c =>
                c.Id == baseServiceId &&
                c.IsDeleted == false &&
                c.IsDisabledByCompany == false &&
                c.ConfirmedServiceType == ConfirmedServiceType.Confirmed &&
                c.ProfileConfirmType == ProfileConfirmType.Confirmed
                )
                .Join(_context.Users, a => a.UserName, us => us.UserName, (c, user) => new
                {

                    UserId = user.Id,
                    UsersStar = user.StarCount,
                    Name = user.Name,
                    LastName = user.LastName,
                    ImageAddress = user.ImageAddress,

                    c.ServiceName,
                    c.ServiceTypes,
                    //////c.ServiceType,
                    //CategoryName =  isPersian ? c.CategoryTBL.PersianTitle : c.CategoryTBL.Title,
                    CategoryName = _commonService.GetNameByCulture(c.CategoryTBL),
                    //CategoryPersianName = c.CategoryTBL.PersianTitle,
                    ServiceStar = c.StarCount,
                    ServiceUnder3Start = c.Under3StarCount,
                    //c.ServiceName

                    SubCategoryName = isPersian ? c.SubCategoryTBL.PersianTitle : c.SubCategoryTBL.Title,
                    //SubCategoryPersianName = c.SubCategoryTBL.PersianTitle,


                    ChatService = c.MyChatsService == null ? null :
                        new
                        {
                            PackageType = c.MyChatsService != null ? c.MyChatsService.PackageType : null,
                            //Price = c.MyChatsService != null ? c.MyChatsService.PriceForNativeCustomer : null,
                            PriceForNativeCustomer = c.MyChatsService != null ? c.MyChatsService.PriceForNativeCustomer : null,
                            //PriceForNonNativeCustomer = c.MyChatsService != null ? c.MyChatsService.PriceForNonNativeCustomer : null,
                            Duration = c.MyChatsService != null ? c.MyChatsService.Duration : null,
                            FreeMessageCount = c.MyChatsService != null ? c.MyChatsService.FreeMessageCount : null,
                            MessageCount = c.MyChatsService != null ? c.MyChatsService.MessageCount : null,
                            BeTranslate = c.MyChatsService != null ? c.MyChatsService.BeTranslate : false,
                            IsServiceReverse = c.MyChatsService != null ? c.MyChatsService.IsServiceReverse : false,
                        },

                    ServiceService = c.MyServicesService == null ? null : new
                    {
                        Price = c.MyServicesService != null ? c.MyServicesService.Price : null,
                        WorkDeliveryTimeEstimation = c.MyServicesService != null ? c.MyServicesService.WorkDeliveryTimeEstimation : null,
                        Tags = c.MyServicesService != null ? c.MyServicesService.Tags : null,
                        HowWorkConducts = c.MyServicesService != null ? c.MyServicesService.HowWorkConducts : null,
                        FileDescription = c.MyServicesService != null ? c.MyServicesService.FileDescription : null,
                        Description = c.MyServicesService != null ? c.MyServicesService.Description : null,
                        DeliveryItems = c.MyServicesService != null ? c.MyServicesService.DeliveryItems : null,
                        //Area = c.MyServicesService != null ? isPersian ? c.MyServicesService.AreaTBL.PersianTitle : c.MyServicesService.AreaTBL!.Title : null,
                        //Speciality = c.MyServicesService != null ? isPersian ?  c.MyServicesService.SpecialityTBL.PersianName : c.MyServicesService.SpecialityTBL.EnglishName :   null,
                    },

                    CourseService = c.MyCourseService == null ? null : new
                    {
                        Price = c.MyCourseService != null ? c.MyCourseService.Price : null,
                        Description = c.MyCourseService != null ? c.MyCourseService.Description : null,
                        TotalLenght = c.MyCourseService != null ? c.MyCourseService.TotalLenght : null,
                        DisCountPercent = c.MyCourseService != null ? c.MyCourseService.DisCountPercent : null,
                        PreviewVideoAddress = c.MyCourseService != null ? c.MyCourseService.PreviewVideoAddress : null,
                    },

                }).FirstOrDefaultAsync();


            if (userFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            return Ok(_commonService.OkResponse(userFromDB, false));
        }






        /// <summary>
        /// این اگر کاربرانی بخواهند سرویس های خودش را ببینند  جزییاتش را  این ای پی ای کال ود
        /// </summary>
        /// فرقش بالای در اینه که این سرویس هاییه که هنوز تو سایت پابلیش نظه رو هم میاره
        /// <param name="baseServiceId"></param>
        /// <returns></returns>
        [HttpGet("GetServiceDetailsById")]
        public async Task<ActionResult> GetServiceDetailsById(int baseServiceId)
        {
            var isPersian = _commonService.IsPersianLanguage();
            var currentUserName = _accountService.GetCurrentUserName();

            var userFromDB = await _context.BaseMyServiceTBL
                .Where(c =>
                c.Id == baseServiceId &&
                c.IsDeleted == false &&
                c.UserName.ToLower() == currentUserName.ToLower() /*&&*/
                )
                .Join(_context.Users, a => a.UserName, us => us.UserName, (c, user) => new
                {

                    UserId = user.Id,
                    UsersStar = user.StarCount,
                    Name = user.Name,
                    LastName = user.LastName,
                    ImageAddress = user.ImageAddress,

                    c.ServiceName,
                    c.ServiceTypes,
                    //c.ServiceType,
                    CategoryName = isPersian ? c.CategoryTBL.PersianTitle : c.CategoryTBL.Title,
                    //CategoryPersianName = c.CategoryTBL.PersianTitle,
                    ServiceStar = c.StarCount,
                    ServiceUnder3Start = c.Under3StarCount,

                    serviceCategoryName = isPersian ? c.ServiceTbl.PersianName : c.ServiceTbl.Name,
                    serviceCategoryId = c.ServiceTbl.Id,
                    c.IsDisabledByCompany,

                    SubCategoryName = isPersian ? c.SubCategoryTBL.PersianTitle : c.SubCategoryTBL.Title,
                    //SubCategoryPersianName = c.SubCategoryTBL.PersianTitle,


                    ChatService = c.MyChatsService == null ? null :
                        new
                        {
                            PackageType = c.MyChatsService != null ? c.MyChatsService.PackageType : null,
                            //Price = c.MyChatsService != null ? c.MyChatsService.PriceForNativeCustomer : null,
                            PriceForNativeCustomer = c.MyChatsService != null ? c.MyChatsService.PriceForNativeCustomer : null,
                            //PriceForNonNativeCustomer = c.MyChatsService != null ? c.MyChatsService.PriceForNonNativeCustomer : null,
                            Duration = c.MyChatsService != null ? c.MyChatsService.Duration : null,
                            FreeMessageCount = c.MyChatsService != null ? c.MyChatsService.FreeMessageCount : null,
                            MessageCount = c.MyChatsService.MessageCount,
                            BeTranslate = c.MyChatsService != null ? c.MyChatsService.BeTranslate : false,
                            IsServiceReverse = c.MyChatsService != null ? c.MyChatsService.IsServiceReverse : false,
                        },

                    ServiceService = c.MyServicesService == null ? null : new
                    {
                        Price = c.MyServicesService != null ? c.MyServicesService.Price : null,
                        WorkDeliveryTimeEstimation = c.MyServicesService != null ? c.MyServicesService.WorkDeliveryTimeEstimation : null,
                        Tags = c.MyServicesService != null ? c.MyServicesService.Tags : null,
                        HowWorkConducts = c.MyServicesService != null ? c.MyServicesService.HowWorkConducts : null,
                        FileDescription = c.MyServicesService != null ? c.MyServicesService.FileDescription : null,
                        Description = c.MyServicesService != null ? c.MyServicesService.Description : null,
                        DeliveryItems = c.MyServicesService != null ? c.MyServicesService.DeliveryItems : null,
                        //Area = c.MyServicesService != null ? isPersian ? c.MyServicesService.AreaTBL.PersianTitle : c.MyServicesService.AreaTBL!.Title : null,
                        //Speciality = c.MyServicesService != null ? isPersian ?  c.MyServicesService.SpecialityTBL.PersianName : c.MyServicesService.SpecialityTBL.EnglishName :   null,
                    },

                    CourseService = c.MyCourseService == null ? null : new
                    {
                        Price = c.MyCourseService != null ? c.MyCourseService.Price : null,
                        Description = c.MyCourseService != null ? c.MyCourseService.Description : null,
                        TotalLenght = c.MyCourseService != null ? c.MyCourseService.TotalLenght : null,
                        DisCountPercent = c.MyCourseService != null ? c.MyCourseService.DisCountPercent : null,
                        PreviewVideoAddress = c.MyCourseService != null ? c.MyCourseService.PreviewVideoAddress : null,
                    },

                }).FirstOrDefaultAsync();


            if (userFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            return Ok(_commonService.OkResponse(userFromDB, false));


        }






        #region  get users serviceCategory By Id

        [HttpGet("GetUsersCategoryServiceById")]
        ////////////[ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetUsersCategoryServiceById(string UserId)
        {

            var username = await _context.Users.Where(c => c.Id == UserId).AsNoTracking().Select(c => c.UserName).FirstOrDefaultAsync();
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                //return BadRequest(new ApiBadRequestResponse(erros, 404));
            }

            var isPersian = _commonService.IsPersianLanguage();
            var categOryServciceNames = await _context.BaseMyServiceTBL
                .Where(c => c.UserName == username &&
                c.IsDeleted == false &&
                c.IsDisabledByCompany == false &&
                c.ConfirmedServiceType == ConfirmedServiceType.Confirmed
                && c.ProfileConfirmType == ProfileConfirmType.Confirmed)
                 .Select(X => new
                 {
                     ServiceId = X.ServiceTbl.Id,
                     ServiceName = isPersian ? X.ServiceTbl.PersianName : X.ServiceTbl.Name,
                     Color = X.ServiceTbl.Color
                 }).Distinct()
                 .AsQueryable()
                 .ToListAsync();

            //return Ok(_commonService.OkResponse(categOryServciceNames, _localizerShared["SuccessMessage"].Value.ToString()));


            return Ok(_commonService.OkResponse(null, false));

        }
        #endregion

        #region  Searech Service

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpPost("SearchService")]
        ////////////////[Authorize]
        //[ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> SearchService([FromBody] SearchDTO model)
        {
            int serviceType;
            var result = int.TryParse(model.ServiceTypes, out serviceType);
            model.ServiceTypes = result ? serviceType.ToString() : 0.ToString();

            ResponseDTO res = await _servicetypeService.SearchService(model);
            return Ok(_commonService.OkResponse(res, false));
        }

        #endregion







        #region  Searech Service

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet("PopularService")]
        ////////////////[Authorize]
        //[ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> PopularService()
        {
            var currentusername = _accountService.GetCurrentUserName();
            var queryAble = _context.BaseMyServiceTBL
                                .Where(c => c.IsDeleted == false && c.IsDisabledByCompany == false
                                && c.ConfirmedServiceType == ConfirmedServiceType.Confirmed
                                &&
                                c.ProfileConfirmType == ProfileConfirmType.Confirmed
                                )
                                .AsNoTracking()
                                .OrderByDescending(c => c.StarCount)
                                .ThenBy(c => c.Under3StarCount)
                                .ThenByDescending(c => c.CreateDate)
                                .AsQueryable();


            var users = _context.Users.AsNoTracking().AsQueryable();
            var list = await (from u in users
                              join q in queryAble
                              on u.UserName equals q.UserName
                              select new
                              {
                                  Username = u.UserName,
                                  UserId = u.Id,
                                  Name = u.Name,
                                  LastName = u.LastName,
                                  Bio = u.Bio,
                                  ImageAddress = u.ImageAddress,
                                  StarCount = u.StarCount,

                                  //CategoryName =   q.CategoryTBL.Title /*s.CategoryTBL.Title*/,
                                  CategoryName = _commonService.GetNameByCulture(q.CategoryTBL)   /*s.CategoryTBL.Title*/,
                                  ////CategoryPersianName = q.CategoryTBL.PersianTitle /*s.CategoryTBL.PersianTitle */,
                                  //SubCategoryName = q.SubCategoryTBL.Title /*s.CategoryTBL.Title*/,
                                  SubCategoryName = _commonService.GetNameByCulture(q.SubCategoryTBL),

                                  //SubCategoryPersianName = q.SubCategoryTBL.PersianTitle /*s.CategoryTBL.PersianTitle*/,
                                  ServiceTypes = q.ServiceTypes  /*q.Select(y => y.ServiceType).Distinct().ToList()*/ /*s.ServiceType*/
                              })
                              .ToListAsync();


            //GroupBy
            var items = list.GroupBy(x => x.Username)
                .Select(x => new SearchResponseDTO
                {
                    UserId = x.FirstOrDefault().UserId,
                    Username = x.FirstOrDefault().Username,
                    Name = x.FirstOrDefault().Name,
                    LastName = x.FirstOrDefault().LastName,
                    Bio = x.FirstOrDefault().Bio,
                    ImageAddress = x.FirstOrDefault().ImageAddress,

                    CategoryName = x.FirstOrDefault().CategoryName,
                    //CategoryPersianName = x.FirstOrDefault().CategoryPersianName,

                    SubCategoryName = x.FirstOrDefault().SubCategoryName,
                    //SubCategoryPersianName = x.FirstOrDefault().SubCategoryPersianName,


                    //ServiceTypes = x.Select(y => y.ServiceTypes).Distinct().ToList(),
                    ServiceTypes = x.FirstOrDefault().ServiceTypes,
                    StarCount = x.FirstOrDefault().StarCount

                }).Take(4)
                 .ToList();


            var response = new ResponseDTO
            {
                Users = items,
                TotalPages = 0,
            };

            return Ok(_commonService.OkResponse(response, false));
        }

        #endregion





        #region  Get Top Ten of every servic
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTopTen")]
        ////////////////[Authorize]
        //[ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetTopTen(int serviceCategoryId)
        {
            var QueryAble = _context.User_TopTenPackageTBL.Where(c => c.ServiceId == serviceCategoryId).AsNoTracking().AsQueryable();
            var users = _context.Users.AsNoTracking().AsQueryable();
            var res = await (from u in users
                             join q in QueryAble
                             on u.UserName equals q.UserName
                             select new
                             {
                                 q.CreateDate,
                                 u.UserName,
                                 u.ImageAddress,
                                 u.Id,
                             }).OrderBy(c => c.CreateDate).ToListAsync();

            return Ok(_commonService.OkResponse(res, false));
        }


        #endregion








        /// <summary>
        ///گرفتن کامنت خهایس یک سرویس در ادمین
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetServiceCommentsForAdmin")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetServiceCommentsForAdmin(int Id)
        {

            var comments = await _context.ServiceCommentsTBL.Where(c => c.BaseMyServiceId == Id)
            .Select(c => new
            {
                c.Id,
                c.CreateDate,
                c.Comment,
                c.UserName,
                c.IsConfirmed,
            }).ToListAsync();

            return Ok(_commonService.OkResponse(comments, true));
        }






        /// <summary>
        ///گرفتن کامنت خهایس یک سرویس در ادمین
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("ConfirmComment")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> ConfirmComment(int Id)
        {

            var comment = await _context.ServiceCommentsTBL.Where(c => c.Id == Id).FirstOrDefaultAsync();

            if (comment == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                ////////////return NotFound(_commonService.NotFoundErrorReponse(true));

            }



            if (comment.IsConfirmed)
                comment.IsConfirmed = false;
            else
                comment.IsConfirmed = true;

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(comment.IsConfirmed, true));

        }







        #region add comment or Star
        /// <summary>
        /// جدول کامنت وستارههای بک سرویس 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddCommentAndStarToService")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddCommentAndStarToService([FromBody] AddCommentServiceDTO model)
        {
            //some validation     ------------------------  comming soon
            //1-  this user have request to this service or not 
            //if  iin request  tamam shde nabashe

            //_context.ServiceCommentsTBL
            //    .Where(c => c.UserName == currentUsername && c.ServiceId ==model.ServiceId)

            var serviceFromDB = await _context.BaseMyServiceTBL
                        .Where(c => c.ServiceId == model.ServiceId && c.IsDeleted == false && c.IsDisabledByCompany == false).FirstOrDefaultAsync();
            if (serviceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            if (model.StarCount >= 3)
                model.ResonForUnder3Star = null;

            var currentUsername = _accountService.GetCurrentUserName();

            var comment = new ServiceCommentsTBL()
            {
                Comment = model.Comment,
                CreateDate = DateTime.Now,
                ResonForUnder3Star = model.ResonForUnder3Star,
                StarCount = model.StarCount,
                UserName = currentUsername,
                BaseMyService = serviceFromDB,
                ////////BaseMyServiceId = model.ServiceId,                ////////BaseMyServiceId = model.ServiceId,
            };

            AppUser curentUser = await _accountService.GetUserByUserName(currentUsername);
            curentUser.StarCount++;


            serviceFromDB.StarCount += model.StarCount;
            if (model.StarCount < 3)
            {
                serviceFromDB.Under3StarCount++;
                curentUser.Under3StarCount++;
                //Under3StarCount
            }




            //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
            await _context.ServiceCommentsTBL.AddAsync(comment);
            await _context.SaveChangesAsync();
            //return Ok(_commonService.OkResponse(null, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));

        }

        #endregion


        #region add Service Survey
        /// <summary>
        /// جدول نظر سنجی یک سرویس 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddSurveyToService")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddSurveyToService([FromBody] AddSurveyToServiceDTO model)
        {
            //some validation     ------------------------  comming soon
            //1-  this user have request to this service or not 
            //if  iin request  tamam shde nabashe

            //_context.ServiceCommentsTBL
            //    .Where(c => c.UserName == currentUsername && c.ServiceId ==model.ServiceId)

            var questions = await _context.QuestionPullTBL.Where(c => c.ServiceId == model.ServiceId).Select(c => new
            {
                questionId = c.Id,
                answerIds = c.AnswersTBLs.Select(c => c.Id).ToList()
            }).ToListAsync();

            if (questions.Count == 0)
            {
                List<string> erros = new List<string> {
                    //_localizerShared["Noquestoin"].Value.ToString() 
                        _resourceServices.GetErrorMessageByKey("Noquestoin")

                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            //validation
            if (model.Questoins != null)
            {
                foreach (var item in model.Questoins)
                {
                    var ques = questions.Where(c => c.questionId == item.QuestionId).FirstOrDefault();
                    if (ques == null)
                    {

                        List<string> erros = new List<string> { 
                            //_localizerShared["InvalidQuestion"].Value.ToString() 
                        _resourceServices.GetErrorMessageByKey("InvalidQuestion")
                        };
                        return BadRequest(new ApiBadRequestResponse(erros));
                    }
                    else if (ques.answerIds.Any(c => c == item.AnswerId) == false)
                    {
                        List<string> erros = new List<string> {
                            //_localizerShared["InvalidAnswer"].Value.ToString()
                        _resourceServices.GetErrorMessageByKey("Noquestoin")
                        };
                        return BadRequest(new ApiBadRequestResponse(erros));
                    }
                }
            }


            if (model.Questoins != null)
            {
                var currentUsername = _accountService.GetCurrentUserName();
                var SurveyTBLs = new List<ServiceSurveyTBL>();
                foreach (var item in model.Questoins)
                {
                    new ServiceSurveyTBL()
                    {
                        UserName = currentUsername,
                        QuestionId = item.QuestionId,
                        AnswerId = item.AnswerId,
                    };
                }
                await _context.ServiceSurveyTBL.AddRangeAsync(SurveyTBLs);
            }


            //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
            await _context.SaveChangesAsync();
            //return Ok(_commonService.OkResponse(null, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));


        }

        #endregion


        #region Admin


        #region reject and accept Provided Service


        [HttpGet("AcceptProvideServicesInAdmin")]
        [Authorize]
        [PermissionAuthorize(PublicPermissions.Service.RejectProvideServices)]
        [PermissionDBCheck(IsAdmin = true, requiredPermission = new string[] { PublicPermissions.Service.RejectProvideServices })]
        public async Task<ActionResult> AcceptProvideServicesInAdmin(int serviceId)
        {
            var currentUserName = _accountService.GetCurrentUserName();
            var cuurentRole = _accountService.GetCurrentRole();

            var serviceFromDB = await _context.BaseMyServiceTBL
                .Where(c => c.Id == serviceId)
                .Include(c => c.ServiceTbl)
                 .FirstOrDefaultAsync();

            if (serviceFromDB == null)
            {

                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                ////////return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }
            if (string.IsNullOrEmpty(serviceFromDB.ServiceTbl.RoleId))
            {
                List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));


                //return StatusCode(StatusCodes.Status500InternalServerError,
                //                        new ApiResponse(500, PubicMessages.InternalServerMessage));

            }


            var roleFromDB = await _roleManager.FindByIdAsync(serviceFromDB.ServiceTbl.RoleId);

            if (cuurentRole != PublicHelper.ADMINROLE)
            {
                if (cuurentRole.ToLower() != roleFromDB.Name.ToLower())
                {
                    List<string> erros = new List<string> { PubicMessages.ForbiddenMessage };
                    return Unauthorized(new ApiBadRequestResponse(erros, 403));
                    //return Unauthorized(new ApiResponse(403, PubicMessages.ForbidenMessage));
                }
            }
            serviceFromDB.ConfirmedServiceType = ConfirmedServiceType.Confirmed;


            var persianConfirmMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceConfimNotificationKeyName).SingleOrDefault()?.Value;
            var englishConfirmMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceConfimNotificationKeyName).SingleOrDefault()?.EnglishValue;

            //var userFromDB = await _context.Users.Where(c => c.UserName == currentUserName).Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();
            var userFromDB = await _context.Users.Where(c => c.UserName == serviceFromDB.UserName).Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();


            var notification = new NotificationTBL()
            {
                CreateDate = DateTime.Now,
                EnglishText = englishConfirmMessage,
                TextPersian = persianConfirmMessage,

                IsReaded = false,
                NotificationStatus = NotificationStatus.ServiceConfirmation,
                SenderUserName = currentUserName,
                UserName = serviceFromDB.UserName,
            };

            _context.NotificationTBL.Add(notification);


            bool isPersian = _commonService.IsPersianLanguage();

            string confirmMessage = isPersian ? persianConfirmMessage : englishConfirmMessage;

            if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
                await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("Notifis", confirmMessage);

            await _context.SaveChangesAsync();

            //ارسال اس ام اس
            await _smsService.ConfirmServiceByAdmin(serviceFromDB.ServiceName, serviceFromDB.UserName);

            return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));
        }




        /// <summary>
        ///reject provided service        
        /// </summary>
        /// <returns></returns>
        [HttpPost("RejectProvideServicesInAdmin")]
        [Authorize]
        [PermissionAuthorize(PublicPermissions.Service.RejectProvideServices)]
        [PermissionDBCheck(IsAdmin = true, requiredPermission = new string[] { PublicPermissions.Service.RejectProvideServices })]
        public async Task<ActionResult> RejectProvideServicesInAdmin([FromBody] RejectProvideServicesInAdminDTO model)
        {
            var currentUserName = _accountService.GetCurrentUserName();
            var cuurentRole = _accountService.GetCurrentRole();

            var serviceFromDB = await _context.BaseMyServiceTBL
                .Where(c => c.Id == model.ServiceId)
                .Include(c => c.ServiceTbl)
                 .FirstOrDefaultAsync();

            if (serviceFromDB == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                //////////////return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }
            if (string.IsNullOrEmpty(serviceFromDB.ServiceTbl.RoleId))
            {
                List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));


                //////////////return StatusCode(StatusCodes.Status500InternalServerError,
                //////////////                        new ApiResponse(500, PubicMessages.InternalServerMessage));

            }

            var roleFromDB = await _roleManager.FindByIdAsync(serviceFromDB.ServiceTbl.RoleId);
            if (cuurentRole != PublicHelper.ADMINROLE)
            {
                if (cuurentRole.ToLower() != roleFromDB.Name.ToLower())
                {
                    List<string> erros = new List<string> { PubicMessages.ForbiddenMessage };
                    return BadRequest(new ApiBadRequestResponse(erros, 403));

                    ////////return Unauthorized(new ApiResponse(403, PubicMessages.ForbidenMessage));
                }
            }
            serviceFromDB.ConfirmedServiceType = ConfirmedServiceType.Rejected;
            serviceFromDB.RejectReason = model.RejectReason;



            var persinaRejectMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceRejectionKeyName).SingleOrDefault()?.Value;
            var englishRejectMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceRejectionKeyName).SingleOrDefault()?.EnglishValue;

            //var userFromDB = await _context.Users.Where(c => c.UserName == currentUserName).Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();


            var userFromDB = await _context.Users.Where(c => c.UserName == serviceFromDB.UserName).Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();

            var notification = new NotificationTBL()
            {
                CreateDate = DateTime.Now,
                EnglishText = englishRejectMessage,
                TextPersian = persinaRejectMessage,

                IsReaded = false,
                NotificationStatus = NotificationStatus.ServiceRejection,
                SenderUserName = currentUserName,
                UserName = serviceFromDB.UserName,
            };

            _context.NotificationTBL.Add(notification);

            bool isPersian = _commonService.IsPersianLanguage();

            string rejectMessage = isPersian ? persinaRejectMessage : englishRejectMessage;

            if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
                await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("Notifis", rejectMessage);

            await _context.SaveChangesAsync();

            return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));
        }







        #endregion

        #region   GetAllProvideServicesInAdmin
        /// <summary>
        /// رفتن تمامی سرویس های ثبت شده  (سرویس های گیرنده)
        /// سرویس هایی که حذف نشده ند
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProvideServicesInAdmin")]
        [Authorize]
        [PermissionAuthorize(PublicPermissions.Service.GetAllProvidedService)]
        [PermissionDBCheck(IsAdmin = true, requiredPermission = new string[] { PublicPermissions.Service.GetAllProvidedService })]
        public async Task<ActionResult> GetAllProvideServicesInAdmin(int? page, int? perPage,
                   string searchedWord, DateTime createDate, ServiceType? serviceType, string serviceTypes, ConfirmedServiceType? confirmedServiceType)
        {
            var currentRole = _accountService.GetCurrentRole();
            if (currentRole != PublicHelper.ADMINROLE)
            {
                var res = await _servicetypeService.GetAllProvideServicesForNotAdmin(page, perPage, searchedWord, createDate, serviceType, serviceTypes, confirmedServiceType);
                return Ok(_commonService.OkResponse(res, PubicMessages.SuccessMessage));
            }
            var result = await _servicetypeService.GetAllProvideServicesForAdmin(page, perPage, searchedWord, createDate, serviceType, serviceTypes, confirmedServiceType);
            return Ok(_commonService.OkResponse(result, PubicMessages.SuccessMessage));
        }

        #endregion




        #region  getChatServerviceDetails


        /// <summary>
        ///گرفتن یک   از نوع چت و وویس و.. برای یک ادمین
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetChatServiceDetailsInAdmin")]
        [Authorize]
        [PermissionAuthorize(PublicPermissions.Service.RejectProvideServices)]
        [PermissionDBCheck(IsAdmin = true, requiredPermission = new string[] { PublicPermissions.Service.RejectProvideServices })]
        public async Task<ActionResult> GetChatServiceDetailsInAdmin(int Id)
        {

            var serviceFromDB = await _context
                .BaseMyServiceTBL.Where(c => c.Id == Id && c.IsDeleted == false)
                .Select(c => new
                {
                    c.MyChatsService.PackageType,
                    c.MyChatsService.BeTranslate,
                    c.MyChatsService.FreeMessageCount,
                    c.MyChatsService.IsServiceReverse,
                    c.MyChatsService.PriceForNativeCustomer,
                    c.MyChatsService.PriceForNonNativeCustomer,
                    c.RejectReason,
                    c.CreateDate,
                    c.Id,
                    c.ServiceName,
                    c.ServiceTypes,
                    ////c.ServiceType,
                    c.CatId,
                    c.SubCatId,
                    c.UserName,
                    c.ConfirmedServiceType,
                    //c.IsDeleted,
                    c.IsEditableService,
                    c.ServiceTbl.RoleId,
                    c.IsDisabledByCompany
                }).FirstOrDefaultAsync();

            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

            var currentUsername = _accountService.GetCurrentUserName();
            var currentRole = _accountService.GetCurrentRole();

            if (currentRole != PublicHelper.ADMINROLE)
            {
                var roleFromDB = await _roleManager.FindByNameAsync(currentRole);
                var roleId = roleFromDB.Id;

                if (serviceFromDB.RoleId != roleId)
                {
                    List<string> erros = new List<string> { PubicMessages.UnAuthorizeMessage };
                    return Unauthorized(new ApiBadRequestResponse(erros, 401));
                    //////////////return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));
                }
            }
            //return Ok(_commonService.OkResponse(serviceFromDB, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(null, false));

        }

        #endregion





        /// <summary>
        ///گرفتن یک سرویس از نوع سرویس و.. برای یک ادمین
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetServiceServiceDetailsInAdmin")]
        [Authorize]
        [PermissionAuthorize(PublicPermissions.Service.GetAllProvidedService)]
        [PermissionDBCheck(IsAdmin = true, requiredPermission = new string[] { PublicPermissions.Service.GetAllProvidedService })]

        public async Task<ActionResult> GetServiceServiceDetailsInAdmin(int Id)
        {
            var serviceFromDB = await _context
                           .BaseMyServiceTBL
                           .AsNoTracking()
                          .Where(c => c.Id == Id && c.IsDeleted == false)
                          .Select(c => new
                          {
                              c.MyServicesService.Id,
                              c.MyServicesService.Description,
                              c.MyServicesService.BeTranslate,
                              c.MyServicesService.FileNeeded,
                              c.MyServicesService.FileDescription,
                              c.MyServicesService.Price,
                              c.MyServicesService.WorkDeliveryTimeEstimation,
                              c.MyServicesService.HowWorkConducts,
                              c.MyServicesService.DeliveryItems,
                              c.MyServicesService.Tags,
                              AreaTitle = c.MyServicesService.AreaTBL.Title,
                              SpecialityTitle = c.MyServicesService.SpecialityTBL.EnglishName,
                              CategoryTitile = c.CategoryTBL.Title,
                              SubcategoryTitile = c.SubCategoryTBL.Title,
                              c.ServiceName,
                              c.ServiceTypes,
                              ////c.ServiceType,
                              c.UserName,
                              confirmedServiceType = c.ConfirmedServiceType,
                              c.CreateDate,
                              //c.IsDeleted,
                              c.IsEditableService,
                              c.RejectReason,
                              c.ServiceTbl.RoleId
                              //c.BaseMyChatTBL.IsConfirmByAdmin
                          }).FirstOrDefaultAsync();


            if (serviceFromDB == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                //////////////return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }
            var currentUsername = _accountService.GetCurrentUserName();
            var currentRole = _accountService.GetCurrentRole();

            if (currentRole != PublicHelper.ADMINROLE)
            {
                var roleFromDB = await _roleManager.FindByNameAsync(currentRole);
                var roleId = roleFromDB.Id;

                if (serviceFromDB.RoleId != roleId)
                {
                    List<string> erros = new List<string> { PubicMessages.UnAuthorizeMessage };
                    return Unauthorized(new ApiBadRequestResponse(erros, 401));
                    //////////return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));
                }
            }


            return Ok(_commonService.OkResponse(serviceFromDB, false));
            //return Ok(_commonService.OkResponse(serviceFromDB, _localizerShared["SuccessMessage"].Value.ToString()));

        }






        #endregion


        #endregion

    }
}



