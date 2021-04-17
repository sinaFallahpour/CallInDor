using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Domain.DTO.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Domain.DTO.Response;
using Domain.Enums;
using CallInDoor.Hubs;
using Microsoft.AspNetCore.SignalR;
using CallInDoor.Config.Attributes;
using Service.Interfaces.Company;
using Service.Interfaces.Resource;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CompanyController : BaseControlle
    {

        #region ctor


        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly ICompanyService _companyService;



        private readonly IHubContext<NotificationHub> _hubContext;
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;


        public CompanyController(

                    DataContext context,
                   IAccountService accountService,
                   ICommonService commonService,
                   ICompanyService companyService,
                   IHubContext<NotificationHub> hubContext,
                IStringLocalizer<ShareResource> localizerShared,
                IResourceServices resourceServices
            )
        {
            _context = context;
            _accountService = accountService;
            _commonService = commonService;
            _companyService = companyService;
            _hubContext = hubContext;
            _localizerShared = localizerShared;
            _resourceServices = resourceServices;
        }


        #endregion ctor




        /// <summary>
        /// 
        ///گرفتن ریکوست هایی که pending هستند      
        ///تمام ریکوست هایی که به من  داده  شده
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyRequests")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAllMyCompanyRequests()
        {
            var currentUsername = _accountService.GetCurrentUserName();

            var requests = await (from c in _context.CompanyServiceUserTBL.Where(c => c.CompanyUserName == currentUsername
                                  && c.ConfirmStatus == ConfirmStatus.Pending)
                                  join u in _context.Users
                                  on c.subSetUserName.ToLower() equals u.UserName.ToLower()
                                  select new
                                  {
                                      c.Id,
                                      c.ConfirmStatus,

                                      userId = u.Id,
                                      UserName = u.UserName,
                                      u.ImageAddress,
                                      u.Name,
                                      u.LastName,
                                  })
                                  .AsNoTracking()
                                  .AsQueryable()
                                  .ToListAsync();
            return Ok(_commonService.OkResponse(requests, false));
        }





        /// <summary>
        ///تمام درخواست هایی که به دیگران دادم  
        ///البته با فیلتر
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllMyCompanyClientRequests")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAllMyCompanyClientRequests(ConfirmStatus? confirmStatus)
        {
            var currentUsername = _accountService.GetCurrentUserName();
            var queryAble = _context.CompanyServiceUserTBL.AsQueryable().AsNoTracking();

            if (confirmStatus != null)
                queryAble = queryAble.Where(c => c.ConfirmStatus == confirmStatus);

            var requests = await (from c in queryAble.Where(c => c.subSetUserName == currentUsername)
                                  join u in _context.Users
                                  on c.CompanyUserName.ToLower() equals u.UserName.ToLower()
                                  select new
                                  {
                                      c.Id,
                                      c.ConfirmStatus,

                                      userId = u.Id,
                                      u.UserName,
                                      u.ImageAddress,
                                      u.Name,
                                      u.LastName,
                                  })
                                  .AsQueryable()
                                  .ToListAsync();

            return Ok(_commonService.OkResponse(requests, false));
        }









        /// <summary>
        /// درخواست به کمپانی ها برای عضویت در انها 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("RequestToCompany")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> RequestToCompany([FromBody] RequestCompany model)
        {
            var currentUsername = _accountService.GetCurrentUserName();
            var usersFromDb = await _context.Users.Where(c => c.UserName == currentUsername || c.UserName == model.CompanyUserName)
                                                            .AsNoTracking().Select(c => new { c.Id, c.UserName, c.IsCompany, c.ConnectionId }).ToListAsync();

            string companyId = usersFromDb.Where(c => c.UserName == model.CompanyUserName).FirstOrDefault().Id;
            string userId = usersFromDb.Where(c => c.UserName == currentUsername).FirstOrDefault().Id;


            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(companyId))
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            //validation

            #region  validation
            ///آیا این کمپانی این سرویس ها را رایه داده یا نه
            var isServiceExist = await _context.FirmServiceCategoryInterInterFaceTBL
                                            .AsNoTracking()
                                                       .AnyAsync(c => c.FirmProfileTBLId == companyId
                                                             && c.ServiceTBLId == model.ServiceCategoryId);
            if (isServiceExist == false)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            //آیا کاربر ریکوست داهنده قبلا در این سرویس به شرکت درخواست داده بود یا نه            
            var wasITReservedForMeBefore = await _context.CompanyServiceUserTBL
                            .AsNoTracking().AnyAsync(c => c.ConfirmStatus == ConfirmStatus.Pending || c.ConfirmStatus == ConfirmStatus.Pending
                               && c.CompanyUserName == model.CompanyUserName &&
                               c.ServiceId == model.ServiceCategoryId &&
                               c.subSetUserName == currentUsername);

            if (wasITReservedForMeBefore)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["YouHaveReservedService"].Value.ToString()
                _resourceServices.GetErrorMessageByKey("YouHaveReservedService")
            };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            var isProfileConfirmeds = await _accountService.IsProfileConfirmed(currentUsername, model.ServiceCategoryId);
            if (!isProfileConfirmeds)
            {
                List<string> erros = new List<string> {
                    //_localizerShared["InvalidaServiceCategory"].Value.ToString() 
                _resourceServices.GetErrorMessageByKey("InvalidaServiceCategory")
                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            string companyUserName = usersFromDb.Where(c => c.UserName == model.CompanyUserName).FirstOrDefault().UserName;
            //اگر من به خودم دارم request میدهم
            if (companyUserName == currentUsername)
            {
                List<string> erros = new List<string> {
                    //_localizerShared["YouCantRequestToYourSelf"].Value.ToString() 
                _resourceServices.GetErrorMessageByKey("YouCantRequestToYourSelf")

                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            bool isSubSetACompany = usersFromDb.Where(c => c.UserName == currentUsername).FirstOrDefault().IsCompany;

            if (isSubSetACompany)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["YouCantRequestToACompay"].Value.ToString() 
                _resourceServices.GetErrorMessageByKey("YouCantRequestToACompay")

                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }


            #endregion

            var CompanyServiceUserTBL = new CompanyServiceUserTBL()
            {
                CreateDate = DateTime.UtcNow,
                CompanyUserName = companyUserName,
                subSetUserName = currentUsername,
                ConfirmStatus = ConfirmStatus.Pending,
                ServiceId = model.ServiceCategoryId,
            };

            ///آیا شرکت برای ان سرویس مدارکش را آپلود کرده و تایید شده مدارکش
            //ServidceTypeRequiredCertificates.isdeletd ==  boro emal konnnnnn
            //_context.ServidceTypeRequiredCertificatesTBL.First().Isdeleted == false;
            //_context.ProfileCertificateTBL.First().ProfileConfirmType == 0

            var notif = new NotificationTBL()
            {
                CreateDate = DateTime.UtcNow,
                IsReaded = false,
                NotificationStatus = NotificationStatus.RequestToCompany,
                UserName = companyUserName,
                TextPersian = "شما یک درخواست عضویت در شرکت دارید",
                EnglishText = "you have new request as company ",
                SenderUserName = currentUsername,
            };

            await _context.CompanyServiceUserTBL.AddAsync(CompanyServiceUserTBL);
            #region send notification
            string companyConnectionId = usersFromDb.Where(c => c.UserName == currentUsername).FirstOrDefault().ConnectionId;
            if (!string.IsNullOrEmpty(companyConnectionId))
                await _hubContext.Clients.Client(companyConnectionId).SendAsync("RequestToCompany", "you have new request as company ");
            #endregion
            await _context.NotificationTBL.AddAsync(notif);
            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));

        }





        /// <summary>
        ///قبول کردن درخواست هایی که به عضویت در شرکت داده شده بود 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpGet("AcceptCompanyRequest")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AcceptCompanyRequest(int requestId)
        {
            var currentUserName = _accountService.GetCurrentUserName();


            var requestFromDB = await _context.CompanyServiceUserTBL
                            .Where(c => c.Id == requestId && c.CompanyUserName == currentUserName)
                            .AsNoTracking().Select(c => new { c.Id, c.subSetUserName }).FirstOrDefaultAsync();

            if (requestFromDB == null)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["NotFound"].Value.ToString() 
                _resourceServices.GetErrorMessageByKey("NotFound")

                };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
            }

            var companyServiceUserTBL = new CompanyServiceUserTBL() { Id = requestFromDB.Id, ConfirmStatus = ConfirmStatus.Confirmed };
            _context.CompanyServiceUserTBL.Attach(companyServiceUserTBL);
            _context.Entry(companyServiceUserTBL).Property(x => x.ConfirmStatus).IsModified = true;


            var userFromDB = await _context.Users.Where(c => c.UserName.ToLower() == requestFromDB.subSetUserName.ToLower())
                                                    .AsNoTracking()
                                                    .Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();


            var notification = new NotificationTBL()
            {
                CreateDate = DateTime.UtcNow,
                EnglishText = "your request has been accepted",
                TextPersian = "درخواست شما قبول شده است",
                IsReaded = false,
                NotificationStatus = NotificationStatus.AcceptRequestToCompany,
                SenderUserName = currentUserName,
                UserName = requestFromDB.subSetUserName,
            };
            bool isPersian = _commonService.IsPersianLanguage();
            string confirmMessage = isPersian ? notification.TextPersian : notification.EnglishText;

            if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
                await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("AcceptRequestToCompany", confirmMessage);

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));
        }





        /// <summary>
        ///رد کردن درخواست هایی که به عضویت در شرکت داده شده بود 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpGet("RejectCompanyRequest")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> RejectCompanyRequest(int requestId)
        {

            var currentUserName = _accountService.GetCurrentUserName();
            var requestFromDB = await _context.CompanyServiceUserTBL
                            .Where(c => c.Id == requestId && c.CompanyUserName == currentUserName)
                            .AsNoTracking().Select(c => new { c.Id, c.subSetUserName }).FirstOrDefaultAsync();

            if (requestFromDB == null)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["NotFound"].Value.ToString()
                _resourceServices.GetErrorMessageByKey("NotFound")
                };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
            }

            var companyServiceUserTBL = new CompanyServiceUserTBL() { Id = requestFromDB.Id, ConfirmStatus = ConfirmStatus.Rejected };
            _context.CompanyServiceUserTBL.Attach(companyServiceUserTBL);
            _context.Entry(companyServiceUserTBL).Property(x => x.ConfirmStatus).IsModified = true;

            var userFromDB = await _context.Users.Where(c => c.UserName.ToLower() == requestFromDB.subSetUserName.ToLower())
                                                    .AsNoTracking()
                                                    .Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();


            var notification = new NotificationTBL()
            {
                CreateDate = DateTime.UtcNow,
                EnglishText = "your request has been accepted",
                TextPersian = "درخواست شما قبول شده است",
                IsReaded = false,
                NotificationStatus = NotificationStatus.AcceptRequestToCompany,
                SenderUserName = currentUserName,
                UserName = requestFromDB.subSetUserName,
            };
            bool isPersian = _commonService.IsPersianLanguage();
            string confirmMessage = isPersian ? notification.TextPersian : notification.EnglishText;

            if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
                await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("AcceptRequestToCompany", confirmMessage);
            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));
        }





        /// <summary>
        ///تمام  زیر مجموعه های من
        /// </summary>
        /// <param name="serviceCategoryId"></param>
        /// <returns></returns>
        [HttpGet("GetAllMySubSet")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAllMySubSet(int? serviceCategoryId)
        {
            var currentUserName = _accountService.GetCurrentUserName();
            var requestFromDB = await _context.CompanyServiceUserTBL
                            .Where(c => c.IsDeleted == false && c.ServiceId == serviceCategoryId && c.CompanyUserName == currentUserName)
                            .AsNoTracking()
                            .Select(c => new
                            {
                                c.Id,
                                c.subSetUserName
                            }).FirstOrDefaultAsync();

            var queryAble = _context.CompanyServiceUserTBL.Where(c => c.CompanyUserName == currentUserName
                                                           && c.ConfirmStatus == ConfirmStatus.Confirmed);

            if (serviceCategoryId != null)
                queryAble = queryAble.Where(c => c.ServiceId == serviceCategoryId);

            var subsets = await (from c in queryAble
                                 join u in _context.Users
                                 on c.subSetUserName.ToLower() equals u.UserName.ToLower()
                                 select new
                                 {
                                     c.Id,
                                     c.ConfirmStatus,

                                     userId = u.Id,
                                     UserName = u.UserName,
                                     u.ImageAddress,
                                     u.Name,
                                     u.LastName,
                                 })
                                  .AsNoTracking()
                                  .AsQueryable()
                                  .ToListAsync();

            return Ok(_commonService.OkResponse(subsets, false));
        }






        /// <summary>
        /// فعال کردن یک سرویس برای یکی از زیر مجموعه  هایم که قبلا غیر فعال بوده
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPost("EnabledServiceForSubSet")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> EnabledServiceForSubSet(string userId, int baseServiceId)
        {
            var currentUserName = _accountService.GetCurrentUserName();


            var baseServiceFromDB = await _context.BaseMyServiceTBL.Where(c => c.Id == baseServiceId && c.IsDeleted == false)
                .Select(c => new { c.Id, serviceTBLId = c.ServiceTbl.Id, c.IsDisabledByCompany, c.ServiceTbl.IsEnabled, })
                .FirstOrDefaultAsync();


            #region validation



            if (baseServiceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }


            if (!baseServiceFromDB.IsEnabled)
            {
                List<string> erros = new List<string> { _localizerShared["ServiceIsDisabled"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
            }



            #endregion

            var companyUserFromDB = await _companyService.GetCompanyServiceUserTBL(userId, baseServiceFromDB.serviceTBLId);

            if (!companyUserFromDB)
            {
                List<string> erros = new List<string> {
                    _resourceServices.GetErrorMessageByKey("InvalidAttamp")
                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }


            var baseMyServiceTBL = new BaseMyServiceTBL { Id = baseServiceFromDB.Id, IsDisabledByCompany = true, CompanyId = currentUserName };
            _context.BaseMyServiceTBL.Attach(baseMyServiceTBL);
            _context.Entry(baseMyServiceTBL).Property(x => new { x.CompanyId, x.IsDisabledByCompany }).IsModified = true;

            //companyUserFromDB.IsDeleted = true;

            var notif = new NotificationTBL()
            {
                CreateDate = DateTime.UtcNow,
                IsReaded = false,
                NotificationStatus = NotificationStatus.CompanyDeleteTheSubset,
                UserName = userId,
                TextPersian = "یکی از شرکت هایی که در ان عضو هستید سرویس شمارا غیر فعال کرده",
                EnglishText = "One of the companies you are a member of has disabled your service",
                SenderUserName = currentUserName,
            };

             
                await _context.NotificationTBL.AddAsync(notif);
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(null, false));
             
        }





        /// <summary>
        ///غیر فعال کردن یک سرویس برای یکی از زیر مجمئعه هایم
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPost("DisableServiceForSubSet")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> DisableServiceForSubSet(string userId, int baseServiceId)
        {
            var currentUserName = _accountService.GetCurrentUserName();


            var baseServiceFromDB = await _context.BaseMyServiceTBL.Where(c => c.Id == baseServiceId)
                .Select(c => new { c.Id, serviceTBLId = c.ServiceTbl.Id, c.ServiceTbl.IsEnabled })
                .FirstOrDefaultAsync();


            #region validation

            if (baseServiceFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }

            if (!baseServiceFromDB.IsEnabled)
            {
                List<string> erros = new List<string> { _localizerShared["ServiceIsDisabled"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
            }


            #endregion

            var isCompanyExist = await _companyService.GetCompanyServiceUserTBL(userId, baseServiceFromDB.serviceTBLId);

            if (!isCompanyExist)
            {
                List<string> erros = new List<string> { 
                    //_localizerShared["InvalidAttamp"].Value.ToString() 
                    _resourceServices.GetErrorMessageByKey("InvalidAttamp")

                };
                return BadRequest(new ApiBadRequestResponse(erros));
            }


            var baseMyServiceTBL = new BaseMyServiceTBL { Id = baseServiceFromDB.Id, IsDisabledByCompany = true, CompanyId = currentUserName };
            _context.BaseMyServiceTBL.Attach(baseMyServiceTBL);
            _context.Entry(baseMyServiceTBL).Property(x => new { x.CompanyId, x.IsDisabledByCompany }).IsModified = true;

            //companyUserFromDB.IsDeleted = true;

            var notif = new NotificationTBL()
            {
                CreateDate = DateTime.UtcNow,
                IsReaded = false,
                NotificationStatus = NotificationStatus.CompanyDeleteTheSubset,
                UserName = userId,
                TextPersian = "یکی از شرکت هایی که در ان عضو هستید سرویس شمارا غیر فعال کرده",
                EnglishText = "One of the companies you are a member of has disabled your service",
                SenderUserName = currentUserName,
            };
 
                await _context.NotificationTBL.AddAsync(notif);
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(null, false));
             
        }













        //left from company

    }
}
