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

namespace CallInDoor.Controllers
{
    [Route("api/ServiceType")]
    //[ApiController]
    public class ServiceController : BaseControlle
    {
        #region ctor
        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IServiceService _servicetypeService;
        private readonly ICommonService _commonService;

        private IStringLocalizer<ShareResource> _localizerShared;
        private IStringLocalizer<ServiceController> _locaLizer;
        public ServiceController(
            DataContext context,
              IAccountService accountService,
              RoleManager<AppRole> roleManager,
              IServiceService servicetypeService,
              ICommonService commonService,
             IStringLocalizer<ShareResource> localizerShared,
              IStringLocalizer<ServiceController> locaLizer

            )
        {
            _context = context;
            _commonService = commonService;
            _roleManager = roleManager;
            _accountService = accountService;
            _localizerShared = localizerShared;
            _locaLizer = locaLizer;
            _servicetypeService = servicetypeService;
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
                   c.SitePercent,
                   c.IsEnabled,
                   c.MinPriceForService,
                   c.MinSessionTime,
                   c.AcceptedMinPriceForNative,
                   c.AcceptedMinPriceForNonNative,
                   c.Color,
                   RoleId = c.AppRole.Id,
                   RequiredCertificates = c.ServidceTypeRequiredCertificatesTBL.Where(c => c.Isdeleted == false).Select(c => new { c.Id, c.FileName, c.PersianFileName }),
                   tags = c.Tags.Where(p => p.IsEnglisTags && !string.IsNullOrEmpty(p.TagName)).Select(s => s.TagName).ToList(),
                   persinaTags = c.Tags.Where(p => p.IsEnglisTags == false && !string.IsNullOrEmpty(p.PersianTagName)).Select(s => s.PersianTagName).ToList()
               }).FirstOrDefaultAsync();

            if (Service == null)
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

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
                      c.SitePercent,
                      c.Color,
                      RoleName = c.AppRole.Name,
                      c.AcceptedMinPriceForNative,
                      c.AcceptedMinPriceForNonNative,
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
                List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                return NotFound(new ApiBadRequestResponse(erros, 404));
                //return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));
            }
            var tags = await _context.ServiceTags
                .AsNoTracking()
                .Where(c => c.ServiceId == Id)
                .Select(c => new
                {
                    c.Id,
                    c.IsEnglisTags,
                    c.PersianTagName,
                    c.TagName,
                }).ToListAsync();

            return Ok(_commonService.OkResponse(tags, _localizerShared["SuccessMessage"].Value.ToString()));

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
                List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                return NotFound(new ApiBadRequestResponse(erros, 404));
            }
            var serviceTimsandProice = await _context
                .ServiceTBL
                .AsNoTracking()
                .Where(c => c.Id == Id)
                .Select(c => new
                {
                    c.MinPriceForService,
                    c.MinSessionTime,
                }).ToListAsync();

            return Ok(_commonService.OkResponse(serviceTimsandProice, _localizerShared["SuccessMessage"].Value.ToString()));
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
            var serviceTimsandProice = await _context
                .ServiceTBL
                .AsNoTracking()
                .Select(c => new
                {
                    c.MinPriceForService,
                    c.MinSessionTime
                }).ToListAsync();
            return Ok(_commonService.OkResponse(serviceTimsandProice, _localizerShared["SuccessMessage"].Value.ToString()));
        }




        /// <summary>
        /// ایجاد  سرویس
        /// </summary>
        /// <param name="CreateServiceDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> CreateForAdmin([FromBody] CreateServiceDTO model)
        {

            #region validation
            var roleExist = await _context.Roles.AnyAsync(c => c.Id == model.RoleId);
            var errors = new List<string>();
            if (!roleExist)
            {
                errors.Add("invalid Roles");
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            if (model.RequiredFiles != null)
            {
                foreach (var item in model.RequiredFiles)
                {
                    if (string.IsNullOrEmpty(item.FileName))
                    {
                        errors.Add("file name is required");
                        return BadRequest(new ApiBadRequestResponse(errors));
                    }
                    if (string.IsNullOrEmpty(item.PersianFileName))
                    {
                        errors.Add("persian file name is required");
                        return BadRequest(new ApiBadRequestResponse(errors));
                    }
                }
            }

            var seviceExist = await _context.ServiceTBL.AnyAsync(c => c.Name == model.Name || c.PersianName == model.PersianName);
            if (seviceExist)
            {
                errors.Add($"name Or persian Name already exist");
                return BadRequest(new ApiBadRequestResponse(errors));
            }


            #endregion 
            var result = await _servicetypeService.Create(model);
            if (result)
                return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );
        }





        [HttpPut("UpdateServiceForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<IActionResult> UpdateServiceForAdmin([FromBody] CreateServiceDTO model)
        {
            #region validation
            var roleExist = await _context.Roles.AnyAsync(c => c.Id == model.RoleId);
            var errors = new List<string>();
            if (!roleExist)
            {
                //var errors = new List<string>();
                errors.Add("invalid Role.");
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            if (model.RequiredFiles != null)
            {
                foreach (var item in model.RequiredFiles)
                {
                    if (string.IsNullOrEmpty(item.FileName))
                    {
                        errors.Add("file name is required");
                        return BadRequest(new ApiBadRequestResponse(errors));
                    }
                    if (string.IsNullOrEmpty(item.PersianFileName))
                    {
                        errors.Add("persian file name is required");
                        return BadRequest(new ApiBadRequestResponse(errors));
                    }
                }
            }

            var seviceExist = await _context.ServiceTBL.AnyAsync(c => (c.Name == model.Name || c.PersianName == model.PersianName) && c.Id != model.Id);
            if (seviceExist)
            {
                errors.Add($"name Or persian Name already exist");
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            var service = await _servicetypeService.GetByIdWithJoin(model.Id);
            if (service == null)
                return NotFound(new ApiResponse(404, "service " + PubicMessages.NotFoundMessage));


            #endregion


            var result = await _servicetypeService.Update(service, model);
            if (result)
                return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );
        }



        #endregion
        #region  MyService





        /// <summary>
        /// گرفتن تمام سرویس های من  از نوع یک سرویس تایپ خاص
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyService")]
        [Authorize]
        [ClaimsAuthorize]
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
                   c.ServiceType,
                   //c.IsActive
               }).ToListAsync();

            if (Service == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));
            return Ok(_commonService.OkResponse(Service, _localizerShared["SuccessMessage"].Value.ToString()));

        }





        #region chatService


        /// <summary>
        /// ایجاد  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/userService/AddChatServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddChatServiceForUser([FromBody] AddChatServiceForUsersDTO model)
        {

            var res = await _servicetypeService.ValidateChatService(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            if (model.PackageType == PackageType.Free)
                model.Duration = null;
            else
                model.FreeMessageCount = null;

            var BaseMyService = new BaseMyServiceTBL()
            {
                ConfirmedServiceType = ConfirmedServiceType.Pending,
                CreateDate = DateTime.Now,
                ServiceName = model.ServiceName,
                ServiceType = (ServiceType)model.ServiceType,
                UserName = model.UserName,
                ServiceId = model.ServiceId,
                CatId = model.CatId,
                SubCatId = model.SubCatId,
                IsDeleted = false,
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
            };

            if (model.PackageType == PackageType.Free)
                MyChatService.FreeMessageCount = (int)model.FreeMessageCount;
            else
            {
                MyChatService.Duration = (int)model.Duration;
            }

            try
            {
                //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
                await _context.MyChatServiceTBL.AddAsync(MyChatService);

                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
                    Message = _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                },
                 _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }
        }





        /// <summary>
        ///گرفتن یک سرویس از نوع چت و وویس و.. برای یک کاربر
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("/api/userService/GetChatServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetChatServiceForUser(int Id)
        {

            var serviceFromDB = await _context
                .BaseMyServiceTBL.Where(c => c.Id == Id && c.IsDeleted == false)
                .Select(c => new
                {
                    c.Id,
                    c.MyChatsService.PackageType,
                    c.MyChatsService.BeTranslate,
                    c.MyChatsService.FreeMessageCount,
                    c.MyChatsService.IsServiceReverse,
                    c.MyChatsService.PriceForNativeCustomer,
                    c.MyChatsService.PriceForNonNativeCustomer,
                    c.MyChatsService.BaseMyChatTBL.CatId,
                    c.MyChatsService.BaseMyChatTBL.SubCatId,
                    c.MyChatsService.BaseMyChatTBL.ServiceName,
                    c.ServiceType,
                    c.UserName,
                    c.ConfirmedServiceType,
                    c.IsDeleted,
                }).FirstOrDefaultAsync();

            var currentUsername = _accountService.GetCurrentUserName();

            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            if (serviceFromDB.UserName != currentUsername)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = serviceFromDB,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
        _localizerShared["SuccessMessage"].Value.ToString()
        ));


        }






        /// <summary>
        /// ایجاد  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/api/userService/UpdateChatServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> UpdateChatServiceForUser([FromBody] AddChatServiceForUsersDTO model)
        {

            var res = await _servicetypeService.ValidateChatService(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var currentUsername = _accountService.GetCurrentUserName();

            var serviceFromDB = await _context
               .BaseMyServiceTBL
               .Where(c => c.Id == model.Id && c.IsDeleted == false)
               .Include(c => c.MyChatsService)
               .FirstOrDefaultAsync();


            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            if (serviceFromDB.UserName != currentUsername)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            if (serviceFromDB.ConfirmedServiceType != ConfirmedServiceType.Pending)
            {
                var errors = new List<string>() {
                      _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
               };
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            serviceFromDB.ServiceName = model.ServiceName;
            serviceFromDB.ServiceType = (ServiceType)model.ServiceType;
            serviceFromDB.CatId = model.CatId;
            serviceFromDB.SubCatId = model.SubCatId;
            //serviceFromDB.BaseMyChatTBL.IsActive = model.IsActive;

            serviceFromDB.MyChatsService.PackageType = model.PackageType;
            serviceFromDB.MyChatsService.BeTranslate = model.BeTranslate;
            serviceFromDB.MyChatsService.FreeMessageCount = (int)model.FreeMessageCount;
            serviceFromDB.MyChatsService.IsServiceReverse = model.IsServiceReverse;
            serviceFromDB.MyChatsService.PriceForNativeCustomer = (double)model.PriceForNativeCustomer;
            serviceFromDB.MyChatsService.PriceForNonNativeCustomer = (double)model.PriceForNonNativeCustomer;


            try
            {
                //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
                //await _context.MyChatServiceTBL.AddAsync(MyChatService);

                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
                    Message = _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                },
                 _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }


        }









        /// <summary>
        /// حذف  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("/api/userService/DeleteChatServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> DeleteChatServiceForUser(int id)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var serviceFromDB = await _context
               .BaseMyServiceTBL
               .Where(c => c.Id == id && c.IsDeleted == false)
               .FirstOrDefaultAsync();


            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            if (serviceFromDB.UserName != currentUsername)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            if (serviceFromDB.ConfirmedServiceType != ConfirmedServiceType.Pending)
            {
                var errors = new List<string>() {
                      _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
               };
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            serviceFromDB.IsDeleted = true;

            try
            {
                //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
                //await _context.MyChatServiceTBL.AddAsync(MyChatService);

                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
                    Message = _locaLizer["DeleteServiceMessage"].Value.ToString()
                },
                 _locaLizer["DeleteServiceMessage"].Value.ToString()
                ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }


        }



        #endregion

        #region ServiceService

        /// <summary>
        /// ایجاد سرویس service برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/userService/AddServiceServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddServiceServiceForUser([FromBody] AddServiceServiceForUsersDTO model)
        {

            var res = await _servicetypeService.ValidateServiceService(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));


            var BaseMyService = new BaseMyServiceTBL()
            {
                ConfirmedServiceType = ConfirmedServiceType.Pending,
                CreateDate = DateTime.Now,
                ServiceName = model.ServiceName,
                ServiceType = (ServiceType)model.ServiceType,
                UserName = model.UserName,
                ServiceId = model.ServiceId,
                CatId = model.CatId,
                SubCatId = model.SubCatId,
                IsDeleted = false
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

            try
            {
                //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
                await _context.MyServiceServiceTBL.AddAsync(MyChatService);

                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
                    Message = _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                },
                 _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }

        }



        /// <summary>
        ///گرفتن یک سرویس از نوع چت و وویس و.. برای یک کاربر
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
                   c.MyServicesService.AreaId,
                   c.MyServicesService.SpecialityId,
                   c.CatId,
                   c.SubCatId,
                   c.ServiceName,
                   c.ServiceType,
                   c.ServiceId,
                   c.UserName,
                   c.ConfirmedServiceType,

                   //c.BaseMyChatTBL.IsConfirmByAdmin
               }).FirstOrDefaultAsync();


            //public string Speciality { get; set; }
            //public string Area { get; set; }



            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            var currentUsername = _accountService.GetCurrentUserName();
            if (serviceFromDB.UserName != currentUsername)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = serviceFromDB,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
             _localizerShared["SuccessMessage"].Value.ToString()
            ));
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

            var res = await _servicetypeService.ValidateServiceService(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var serviceFromDB = await _context
                .BaseMyServiceTBL
                .Where(c => c.Id == model.Id && c.IsDeleted == false)
                .Include(c => c.MyServicesService)
                .FirstOrDefaultAsync();

            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            var currentUsername = _accountService.GetCurrentUserName();
            if (serviceFromDB.UserName != currentUsername)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            if (serviceFromDB.ConfirmedServiceType != ConfirmedServiceType.Pending)
            {
                var errors = new List<string>() {
                      _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
               };
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            serviceFromDB.ServiceName = model.ServiceName;
            serviceFromDB.ServiceType = (ServiceType)model.ServiceType;
            serviceFromDB.CatId = model.CatId;
            serviceFromDB.SubCatId = model.SubCatId;
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


            //public string Speciality { get; set; }
            //public string Area { get; set; }


            try
            {
                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
                    Message = _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                },
                 _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }

        }





        /// <summary>
        /// حذف  سرویس service برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("/api/userService/DeleteServiceServiceForUser")]
        [Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> DeleteServiceServiceForUser(int id)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var serviceFromDB = await _context
                .BaseMyServiceTBL
                .Where(c => c.Id == id && c.IsDeleted == false)
                .FirstOrDefaultAsync();


            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            if (serviceFromDB.UserName != currentUsername)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            if (serviceFromDB.ConfirmedServiceType != ConfirmedServiceType.Pending)
            {
                var errors = new List<string>() {
                      _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
               };
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            serviceFromDB.IsDeleted = true;

            try
            {
                //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
                //await _context.MyChatServiceTBL.AddAsync(MyChatService);

                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
                    Message = _locaLizer["DeleteServiceMessage"].Value.ToString()
                },
                 _locaLizer["DeleteServiceMessage"].Value.ToString()
                ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }


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
        public async Task<ActionResult> AddCourseServiceForUser([FromForm] AddCourseServiceForUsersDTO model)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            var res = await _servicetypeService.ValidateCourseService(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));


            var previewFilreAddress = _servicetypeService.SvaeFileToHost("Upload/CoursePreview/", model.PreviewFile);
            if (previewFilreAddress == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new ApiResponse(500, _localizerShared["FileUploadErrorMessage"].Value.ToString()));


            var BaseMyService = new BaseMyServiceTBL()
            {
                ConfirmedServiceType = ConfirmedServiceType.Pending,
                CreateDate = DateTime.Now,
                ServiceName = model.ServiceName,
                ServiceType = ServiceType.Course,
                UserName = model.UserName,
                ServiceId = model.ServiceId,
                CatId = model.CatId,
                IsDeleted = false
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

            try
            {
                //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
                await _context.MyCourseServiceTBL.AddAsync(MyCourseService);

                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
                    Message = _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                },
                 _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }

        }





        /////////////////////////////////////////////////////////////تمام نشده
        /// <summary>
        /// آپدیت سرویس course برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/api/userService/UpdateCourseServiceForUser")]
        [Authorize]
        public async Task<ActionResult> UpdateCourseServiceForUser([FromBody] AddCourseServiceForUsersDTO model)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            var res = await _servicetypeService.ValidateCourseService(model);
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
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            var currentUsername = _accountService.GetCurrentUserName();
            if (serviceFromDB.BaseMyChatTBL.UserName != currentUsername)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            if (serviceFromDB.BaseMyChatTBL.ConfirmedServiceType != ConfirmedServiceType.Pending)
            {
                var errors = new List<string>() {
                      _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
               };
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            serviceFromDB.BaseMyChatTBL.ServiceName = model.ServiceName;
            serviceFromDB.BaseMyChatTBL.CatId = model.CatId;

            serviceFromDB.Description = model.Description;
            serviceFromDB.NewCategory = model.NewCategory;
            serviceFromDB.Price = (double)model.Price;
            serviceFromDB.TotalLenght = model.TotalLenght;
            serviceFromDB.DisCountPercent = model.DisCountPercent;
            ////PreviewVideoAddress  
            ////topics


            try
            {
                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
                    Message = _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                },
                 _locaLizer["SuccesfullAddServiceMessage"].Value.ToString()
                ));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
            }

        }
        /////////////////////////////////////////////////////////تمام نشده


        #endregion



        #endregion











        #region  accept Service

        #endregion









        #region reject  and accept Provided Service
     

        [HttpPost("AcceptProvideServicesInAdmin")]
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

            if (string.IsNullOrEmpty(serviceFromDB.ServiceTbl.RoleId))
                return StatusCode(StatusCodes.Status500InternalServerError,
                                        new ApiResponse(500, PubicMessages.InternalServerMessage));

            var roleFromDB = await _roleManager.FindByIdAsync(serviceFromDB.ServiceTbl.RoleId);

            if (cuurentRole.ToLower() != roleFromDB.Name.ToLower())
                return Unauthorized(new ApiResponse(403, PubicMessages.ForbidenMessage));

            serviceFromDB.ConfirmedServiceType = ConfirmedServiceType.Confirmed;

            await _context.SaveChangesAsync();
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

            if (string.IsNullOrEmpty(serviceFromDB.ServiceTbl.RoleId))
                return StatusCode(StatusCodes.Status500InternalServerError,
                                        new ApiResponse(500, PubicMessages.InternalServerMessage));

            var roleFromDB = await _roleManager.FindByIdAsync(serviceFromDB.ServiceTbl.RoleId);

            if (cuurentRole.ToLower() != roleFromDB.Name.ToLower())
                return Unauthorized(new ApiResponse(403, PubicMessages.ForbidenMessage));

            serviceFromDB.ConfirmedServiceType = ConfirmedServiceType.Rejected;
            serviceFromDB.RejectReason = model.RejectReason;

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
                   string searchedWord, DateTime createDate, ServiceType? serviceType, ConfirmedServiceType? confirmedServiceType)
        {
            var currentRole = _accountService.GetCurrentRole();
            if (currentRole != PublicHelper.ADMINROLE)
            {
                var res = await _servicetypeService.GetAllProvideServicesForNotAdmin(page, perPage, searchedWord, createDate, serviceType, confirmedServiceType);
                return Ok(_commonService.OkResponse(res, PubicMessages.SuccessMessage));
            }
            var result = await _servicetypeService.GetAllProvideServicesForAdmin(page, perPage, searchedWord, createDate, serviceType, confirmedServiceType);
            return Ok(_commonService.OkResponse(result, PubicMessages.SuccessMessage));
        }

        #endregion








    }
}



