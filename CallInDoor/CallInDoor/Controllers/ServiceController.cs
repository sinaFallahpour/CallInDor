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

namespace CallInDoor.Controllers
{
    [Route("api/ServiceType")]
    //[ApiController]
    public class ServiceController : BaseControlle
    {
        #region ctor
        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly IServiceService _servicetypeService;

        private IStringLocalizer<ShareResource> _localizerShared;
        private IStringLocalizer<ServiceController> _locaLizer;
        public ServiceController(DataContext context,
             IStringLocalizer<ShareResource> localizerShared,
              IStringLocalizer<ServiceController> locaLizer,
             IAccountService accountService,
              IServiceService servicetypeService
            )
        {
            _context = context;
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
        public async Task<ActionResult> GetServiceByIdForAdmin(int Id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var Service = await _context.ServiceTBL
               .AsNoTracking()
               .Where(c => c.Id == Id)
               .Select(c => new
               {
                   c.Id,
                   c.Name,
                   c.PersianName,
                   c.IsEnabled,
                   c.Color,
                   c.MinPriceForService,
                   c.MinSessionTime,
                   c.AcceptedMinPriceForNative,
                   c.AcceptedMinPriceForNonNative,
                   tags = c.Tags.Where(p => p.IsEnglisTags && !string.IsNullOrEmpty(p.TagName)).Select(s => s.TagName).ToList(),
                   persinaTags = c.Tags.Where(p => p.IsEnglisTags == false && !string.IsNullOrEmpty(p.PersianTagName)).Select(s => s.PersianTagName).ToList()
               }).FirstOrDefaultAsync();

            if (Service == null)
                return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = Service,
                Message = PubicMessages.SuccessMessage
            },
            PubicMessages.SuccessMessage
           ));

        }






        // GET: api/GetAllServiceForAdmin
        [HttpGet("GetAllServiceForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> GetAllServiceForAdmin()
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var AllServices = await _context
                  .ServiceTBL
                  .AsNoTracking()
                  .Select(c => new
                  {
                      c.Id,
                      c.IsEnabled,
                      c.Name,
                      c.PersianName,
                      c.Color,
                      c.AcceptedMinPriceForNative,
                      c.AcceptedMinPriceForNonNative,
                      c.MinSessionTime,
                      c.MinPriceForService
                  }).ToListAsync();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = AllServices,
                Message = PubicMessages.SuccessMessage
            },
          PubicMessages.SuccessMessage
         ));

        }





        // GET: api/GetAllService
        [HttpGet("GetAllActiveService")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllActiveService()
        {
            var services = await _servicetypeService.GetAllActiveService();
            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = services,
                Message = PubicMessages.SuccessMessage
            },
           PubicMessages.SuccessMessage
          ));
        }





        [HttpGet("GetTagsForService")]
        [Authorize]
        public async Task<ActionResult> GetTagsForService(int? Id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            if (Id == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            var tags = _context.ServiceTags
                .AsNoTracking()
                .Where(c => c.ServiceId == Id)
                .Select(c => new
                {
                    c.Id,
                    c.IsEnglisTags,
                    c.PersianTagName,
                    c.TagName,
                }).ToList();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = tags,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
           _localizerShared["SuccessMessage"].Value.ToString()
           ));

        }





        /// <summary>
        /// قیمت هر حوضه وزمان جلسه
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetTimeAndPriceForService/{Id}")]
        [Authorize]
        public async Task<ActionResult> GetTimeAndPriceForService(int? Id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            if (Id == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            var serviceTimsandProice = _context
                .ServiceTBL
                .AsNoTracking()
                .Where(c => c.Id == Id)
                .Select(c => new
                {
                    c.MinPriceForService,
                    c.MinSessionTime,
                }).ToList();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = serviceTimsandProice,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
           _localizerShared["SuccessMessage"].Value.ToString()
           ));

        }





        /// <summary>
        /// قیمت هر حوضه وزمان جلسه
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetTimeAndPriceForService")]
        [Authorize]
        public async Task<ActionResult> GetTimeAndPriceForService()
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            var serviceTimsandProice = _context
                .ServiceTBL
                .AsNoTracking()
                .Select(c => new
                {
                    c.MinPriceForService,
                    c.MinSessionTime
                }).ToList();

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = serviceTimsandProice,
                Message = _localizerShared["SuccessMessage"].Value.ToString()
            },
           _localizerShared["SuccessMessage"].Value.ToString()
           ));

        }





        /// <summary>
        /// ایجاد  سرویس
        /// </summary>
        /// <param name="CreateServiceDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> CreateForAdmin([FromBody] CreateServiceDTO model)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var result = await _servicetypeService.Create(model);
            if (result)
            {
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = new { },
                    Message = PubicMessages.SuccessMessage
                },
                   PubicMessages.SuccessMessage
                  ));
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

            //return badrequest(new ApiResponse(401, _localizerShared["InvalidPhoneNumber"].Value.ToString()));

        }





        [HttpPut("UpdateServiceForAdmin")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<IActionResult> UpdateServiceForAdmin([FromBody] CreateServiceDTO model)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var service = await _servicetypeService.GetByIdWithJoin(model.Id);
            if (service == null)
            {
                return NotFound(new ApiResponse(404, "service " + PubicMessages.NotFoundMessage));
            }


            var result = await _servicetypeService.Update(service, model);
            if (result)
            {
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = new { },
                    Message = PubicMessages.SuccessMessage
                },
                   PubicMessages.SuccessMessage
                  ));
            }


            return StatusCode(StatusCodes.Status500InternalServerError,
              new ApiResponse(500, PubicMessages.InternalServerMessage)
            );

        }



        #endregion
        #region  MyService





        /// <summary>
        /// گرفتن تمام سرویس های من 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyService")]
        [Authorize]
        public async Task<ActionResult> GetAllMyService(int ServiecTypeId)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var currentUsername = _accountService.GetCurrentUserName();

            var Service = await _context.BaseMyServiceTBL
               .AsNoTracking()
               .Where(c => c.ServiceId == ServiecTypeId && c.UserName == currentUsername)
               .Select(c => new
               {
                   c.Id,
                   c.ServiceName,
                   c.ServiceType,
                   c.IsActive
               }).ToListAsync();

            if (Service == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            return Ok(new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = Service,
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
        [HttpPost("/api/userService/AddChatServiceForUser")]
        [Authorize]
        public async Task<ActionResult> AddChatServiceForUser([FromBody] AddChatServiceForUsersDTO model)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

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
                IsActive = model.IsActive
            };


            var MyChatService = new MyChatServiceTBL()
            {
                //UserName = model.UserName,
                //ServiceName = model.ServiceName,
                PackageType = model.PackageType,
                BeTranslate = model.BeTranslate,
                FreeMessageCount = (int)model.FreeMessageCount,
                IsServiceReverse = model.IsServiceReverse,
                PriceForNativeCustomer = (int)model.PriceForNativeCustomer,
                PriceForNonNativeCustomer = (int)model.PriceForNonNativeCustomer,
                //CreateDate = DateTime.Now,
                //IsCheckedByAdmin = false,
                //ConfirmedServiceType = ConfirmedServiceType.Rejected,
                BaseMyChatTBL = BaseMyService,
            };

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
        public async Task<ActionResult> GetChatServiceForUser(int Id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var serviceFromDB = await _context
                .MyChatServiceTBL
               .AsNoTracking()
               .Where(c => c.Id == Id)
               .Select(c => new
               {
                   c.Id,
                   c.PackageType,
                   c.BeTranslate,
                   c.FreeMessageCount,
                   c.IsServiceReverse,
                   c.PriceForNativeCustomer,
                   c.PriceForNonNativeCustomer,
                   c.BaseMyChatTBL.CatId,
                   c.BaseMyChatTBL.SubCatId,
                   c.BaseMyChatTBL.ServiceName,
                   c.BaseMyChatTBL.ServiceType,
                   c.BaseMyChatTBL.UserName,
                   c.BaseMyChatTBL.ConfirmedServiceType,
                   c.BaseMyChatTBL.IsActive
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
        public async Task<ActionResult> UpdateChatServiceForUser([FromBody] AddChatServiceForUsersDTO model)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            var res = await _servicetypeService.ValidateChatService(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var currentUsername = _accountService.GetCurrentUserName();

            var serviceFromDB = await _context
                .MyChatServiceTBL
                .Where(c => c.Id == model.Id)
                .Include(c => c.BaseMyChatTBL)
                .FirstOrDefaultAsync();

            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

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
            serviceFromDB.BaseMyChatTBL.ServiceType = (ServiceType)model.ServiceType;
            serviceFromDB.BaseMyChatTBL.CatId = model.CatId;
            serviceFromDB.BaseMyChatTBL.SubCatId = model.SubCatId;
            serviceFromDB.BaseMyChatTBL.IsActive = model.IsActive;

            serviceFromDB.PackageType = model.PackageType;
            serviceFromDB.BeTranslate = model.BeTranslate;
            serviceFromDB.FreeMessageCount = (int)model.FreeMessageCount;
            serviceFromDB.IsServiceReverse = model.IsServiceReverse;
            serviceFromDB.PriceForNativeCustomer = (int)model.PriceForNativeCustomer;
            serviceFromDB.PriceForNonNativeCustomer = (int)model.PriceForNonNativeCustomer;


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
        /// ایجاد  سرویس  service برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/userService/AddServiceServiceForUser")]
        [Authorize]
        public async Task<ActionResult> AddServiceServiceForUser([FromBody] AddServiceServiceForUsersDTO model)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));


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
                IsActive = model.IsActive
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
        public async Task<ActionResult> GetServiceServiceForUser(int Id)
        {
            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, PubicMessages.UnAuthorizeMessage));

            var serviceFromDB = await _context
                .MyServiceServiceTBL
               .AsNoTracking()
               .Where(c => c.Id == Id)
               .Select(c => new
               {
                   c.Id,
                   c.Description,
                   c.BeTranslate,
                   c.FileNeeded,
                   c.FileDescription,
                   c.Price,
                   c.WorkDeliveryTimeEstimation,
                   c.HowWorkConducts,
                   c.DeliveryItems,
                   c.Tags,
                   //Speciality
                   //Area
                   c.BaseMyChatTBL.CatId,
                   c.BaseMyChatTBL.SubCatId,
                   c.BaseMyChatTBL.ServiceName,
                   c.BaseMyChatTBL.ServiceType,
                   c.BaseMyChatTBL.ServiceId,
                   c.BaseMyChatTBL.UserName,
                   c.BaseMyChatTBL.ConfirmedServiceType,
                   c.BaseMyChatTBL.IsActive
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
        /// ایجاد  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/api/userService/UpdateServiceServiceForUser")]
        [Authorize]
        public async Task<ActionResult> UpdateServiceServiceForUser([FromBody] AddServiceServiceForUsersDTO model)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));

            var res = await _servicetypeService.ValidateServiceService(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            var serviceFromDB = await _context
                .MyServiceServiceTBL
                .Where(c => c.Id == model.Id)
                .Include(c => c.BaseMyChatTBL)
                .FirstOrDefaultAsync();

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
            serviceFromDB.BaseMyChatTBL.ServiceType = (ServiceType)model.ServiceType;
            serviceFromDB.BaseMyChatTBL.CatId = model.CatId;
            serviceFromDB.BaseMyChatTBL.SubCatId = model.SubCatId;
            serviceFromDB.BaseMyChatTBL.IsActive = model.IsActive;


            serviceFromDB.Description = model.Description;
            serviceFromDB.BeTranslate = model.BeTranslate;
            serviceFromDB.FileNeeded = model.FileNeeded;
            serviceFromDB.FileDescription = model.FileDescription;
            serviceFromDB.Price = (double)model.Price;
            serviceFromDB.WorkDeliveryTimeEstimation = model.WorkDeliveryTimeEstimation;
            serviceFromDB.HowWorkConducts = model.HowWorkConducts;
            serviceFromDB.DeliveryItems = model.DeliveryItems;
            serviceFromDB.Tags = model.Tags + "," + model.CustomTags;


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






        #endregion
    }
}
