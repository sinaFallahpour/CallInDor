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
    //[Route("api/[controller]")]
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
        [HttpPost("Create")]
        [Authorize(Roles = PublicHelper.ADMINROLE)]
        public async Task<ActionResult> Create([FromBody] CreateServiceDTO model)
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



        #region  MyService



        /// <summary>
        /// ایجاد  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/userService/AddChatServiceForUser")]
        [Authorize]
        public async Task<ActionResult> AddChatServiceForUser([FromBody] AddChatServiceForUsersDTO model)
        {

            var checkToken = await _accountService.CheckTokenIsValid();
            if (!checkToken)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));


            var res = await _servicetypeService.ValidateChatService(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));


            var BaseMyService = new BaseMyServiceTBL()
            {
                ConfirmedServiceType = ConfirmedServiceType.Pending,
                CreateDate = DateTime.Now,
                ServiceName = model.ServiceName,
                ServiceType = (ServiceType)model.ServiceType,
                UserName = model.UserName,
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
                CatId = model.CatId,
                SubCatId = model.SubCatId,
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
        /// ایجاد  سرویس chat or voice or video  برای یک کاربر 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/userService/UpdateChatServiceForUser")]
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
                .BaseMyServiceTBL
                .AsNoTracking()
                .Where(c => c.Id == model.Id)
                .Include(c => c.MyChatsService)
                .FirstOrDefaultAsync();

            if (serviceFromDB == null)
                return NotFound(new ApiResponse(404, _localizerShared["NotFound"].Value.ToString()));

            if (serviceFromDB.UserName != currentUsername)
                return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));


            if (serviceFromDB.ConfirmedServiceType == ConfirmedServiceType.Pending)
            {
                var errors = new List<string>() {
                      _locaLizer["AfterAdminConfirmMessage"].Value.ToString()
               };
                return BadRequest(new ApiBadRequestResponse(errors));
            }




            //var BaseMyService = new BaseMyServiceTBL()
            //{
            //    ConfirmedServiceType = ConfirmedServiceType.Rejected,

            //    CreateDate = DateTime.Now,
            //    ServiceName = model.ServiceName,
            //    ServiceType = (ServiceType)model.ServiceType,
            //    UserName = model.UserName,
            //};


            serviceFromDB.ServiceName = model.ServiceName;
            serviceFromDB.ServiceType = (ServiceType)model.ServiceType;
            var dsd = serviceFromDB.MyChatsService.Where(c => c.BaseId == model.Id).FirstOrDefault();
            dsd.PackageType = model.PackageType;
            dsd.BeTranslate = model.BeTranslate;
            dsd.FreeMessageCount = (int)model.FreeMessageCount;
            dsd.IsServiceReverse = model.IsServiceReverse;
            dsd.PriceForNativeCustomer = (int)model.PriceForNativeCustomer;
            dsd.PriceForNonNativeCustomer = (int)model.PriceForNonNativeCustomer;
            dsd.CatId = model.CatId;
            dsd.SubCatId = model.SubCatId;




            //var MyChatService = new MyChatServiceTBL()
            //{
            //    //UserName = model.UserName,
            //    //ServiceName = model.ServiceName,
            //    PackageType = model.PackageType,
            //    BeTranslate = model.BeTranslate,
            //    FreeMessageCount = (int)model.FreeMessageCount,
            //    IsServiceReverse = model.IsServiceReverse,
            //    PriceForNativeCustomer = (int)model.PriceForNativeCustomer,
            //    PriceForNonNativeCustomer = (int)model.PriceForNonNativeCustomer,
            //    //CreateDate = DateTime.Now,
            //    //IsCheckedByAdmin = false,
            //    //ConfirmedServiceType = ConfirmedServiceType.Rejected,
            //    CatId = model.CatId,
            //    SubCatId = model.SubCatId,
            //    BaseMyChatTBL = BaseMyService,
            //};

            try
            {
                //await _context.BaseMyServiceTBL.AddAsync(BaseMyService);
                //await _context.MyChatServiceTBL.AddAsync(MyChatService);

                await _context.SaveChangesAsync();
                return Ok(new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = { },
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




























        ///// <summary>
        ///// ایجاد  سرویس chat or voice or video  برای یک کاربر 
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("/userService/UpdateChatServiceForUser")]
        //[Authorize]
        //public async Task<ActionResult> UpdateChatServiceForUser([FromBody] AddChatServiceForUsersDTO model)
        //{

        //    var checkToken = await _accountService.CheckTokenIsValid();
        //    if (!checkToken)
        //        return Unauthorized(new ApiResponse(401, _localizerShared["UnauthorizedMessage"].Value.ToString()));



        //    var res = await _servicetypeService.ValidateChatService(model);
        //    if (!res.succsseded)
        //        return BadRequest(new ApiBadRequestResponse(res.result));

        //    var MyChatService = new MyChatServiceTBL()
        //    {
        //        ////UserName = model.UserName,
        //        ////ServiceName = model.ServiceName,
        //        //PackageType = model.PackageType,
        //        //BeTranslate = model.BeTranslate,
        //        //FreeMessageCount = model.FreeMessageCount,
        //        //IsServiceReverse = model.IsServiceReverse,
        //        //PriceForNativeCustomer = model.PriceForNativeCustomer,
        //        //PriceForNonNativeCustomer = model.PriceForNonNativeCustomer,
        //        ////CreateDate = DateTime.Now,
        //        ////IsCheckedByAdmin = false,
        //        ////ConfirmedServiceType = ConfirmedServiceType.Rejected,
        //        //CatId = model.CatId,
        //        //SubCatId = model.SubCatId,
        //    };

        //    try
        //    {
        //        await _context.MyChatServiceTBL.AddAsync(MyChatService);

        //        await _context.SaveChangesAsync();
        //        return Ok(new ApiOkResponse(new DataFormat()
        //        {
        //            Status = 1,
        //            data = { },
        //            Message = _localizerShared["SuccessMessage"].Value.ToString()
        //        },
        //         _localizerShared["SuccessMessage"].Value.ToString()
        //    ));
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        new ApiResponse(500, _localizerShared["InternalServerMessage"].Value.ToString()));
        //    }


        //}






        #endregion 




























    }
}
