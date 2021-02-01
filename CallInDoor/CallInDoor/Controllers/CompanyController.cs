using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DTO.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Domain.DTO.Response;

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
        private IStringLocalizer<ShareResource> _localizerShared;

        public CompanyController(

            DataContext context,
                   IAccountService accountService,
                   ICommonService commonService,
                IStringLocalizer<ShareResource> localizerShared
            )
        {
            _context = context;
            _accountService = accountService;
            _commonService = commonService;
            _localizerShared = localizerShared;

        }


        #endregion ctor



        ///// <summary>
        ///// درخواست به کمپانی ها برای عضویت در انها 
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("RequestToCompany")]
        ////[Authorize]
        ////[ClaimsAuthorize(IsAdmin = false)]
        //public async Task<ActionResult> RequestToCompany([FromBody] RequestCompany model)
        //{
        //    var currentUsername = _accountService.GetCurrentUserName();

        //    var userId = await _context.Users.Where(c => c.UserName == currentUsername)
        //                                        .AsNoTracking().Select(c => c.Id).FirstOrDefaultAsync();

        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return BadRequest(_commonService.NotFoundErrorReponse(false));
        //    }

        //    //validation
        //    var isServiceExist = await _context.FirmServiceCategoryInterInterFaceTBL.AsNoTracking()
        //                                               .AnyAsync(c => c.FirmProfileTBLId == userId
        //                                                     && c.ServiceTBLId == model.ServiceCategoryId);

        //    if (isServiceExist == false)
        //    {
        //        return BadRequest(_commonService.NotFoundErrorReponse(false));
        //    }

        //    var isThisReseredBefore = await _context.CompanyServiceUserTBL
        //                                .AnyAsync(c => c.UserName == currentUsername && c.ServiceId == model.ServiceCategoryId);

        //    if (isThisReseredBefore)
        //    {
        //        List<string> erros = new List<string> { _localizerShared["YouHaveReservedService"].Value.ToString() };
        //        return BadRequest(new ApiBadRequestResponse(erros));
        //    }

        //    var isProfileExist = await _accountService.IsProfileConfirmed(currentUsername, model.ServiceCategoryId);
        //    if (isServiceExist == false)
        //    {
        //        List<string> erros = new List<string> { _localizerShared["InvalidaServiceCategory"].Value.ToString() };
        //        return BadRequest(new ApiBadRequestResponse(erros));
        //    }

        //    ///آیا شرکت برای ان سرویس مدارکش را آپلود کرده و تایید شده مدارکش
        //    //ServidceTypeRequiredCertificates.isdeletd     ==  boro emal konnnnnn

        //    _context.ServidceTypeRequiredCertificatesTBL.First().Isdeleted == false;
        //    _context.ProfileCertificateTBL.First().ProfileConfirmType == 0





        //    var baseServiceFromDB = await _context.BaseMyServiceTBL
        //                                        .Where(c => c.Id == model.BaseServiceId && c.IsDeleted == false)
        //                                                        .Include(c => c.MyChatsService)
        //                                                        .FirstOrDefaultAsync();


        //    var hasReserveRequest = await _context.ServiceRequestTBL.AnyAsync(c =>

        //                             (c.ServiceRequestStatus == ServiceRequestStatus.Confirmed || c.ServiceRequestStatus == ServiceRequestStatus.Pending)
        //                             &&
        //                          c.BaseServiceId == model.BaseServiceId &&
        //                            c.ClienUserName == currentUsername &&
        //                                    c.ServiceType == ServiceType.ChatVoice &&
        //                                    (c.PackageType == PackageType.Free));



        //    var res = await _requestService.ValidateRequestToChatService(baseServiceFromDB, hasReserveRequest);

        //    if (!res.succsseded)
        //        return BadRequest(new ApiBadRequestResponse(res.result));

        //    var request = new ServiceRequestTBL()
        //    {
        //        IsLimitedChat = false,

        //        FreeMessageCount = baseServiceFromDB.MyChatsService?.FreeMessageCount,
        //        FreeUsageMessageCount = 0,
        //        BaseServiceId = model.BaseServiceId,
        //        ServiceType = baseServiceFromDB.ServiceType,
        //        CreateDate = DateTime.Now,
        //        WhenTheRequestShouldBeAnswered = DateTime.Now.AddHours(8),
        //        ClienUserName = currentUsername,
        //        ProvideUserName = baseServiceFromDB.UserName,
        //        ServiceRequestStatus = ServiceRequestStatus.Pending,
        //        PackageType = (PackageType)baseServiceFromDB.MyChatsService.PackageType,
        //        PriceForNativeCustomer = baseServiceFromDB.MyChatsService.PriceForNativeCustomer,
        //        PriceForNonNativeCustomer = baseServiceFromDB.MyChatsService.PriceForNonNativeCustomer,
        //        //PackageType=baseServiceFromDB.p
        //    };

        //    request.ChatServiceMessagesTBL = new List<ChatServiceMessagesTBL>() {
        //        new ChatServiceMessagesTBL()
        //            {
        //                SenderUserName= currentUsername,
        //                IsSeen=false,
        //                ChatMessageType = ChatMessageType.Text,
        //                IsProviderSend = false,
        //                SendetMesageType = SendetMesageType.Client,
        //                ClientUserName = currentUsername,
        //                CreateDate = DateTime.Now,
        //                ProviderUserName = baseServiceFromDB.UserName,
        //                Text = model.FirstMessage,
        //             }
        //    };





        //    #region send notification
        //    var persianConfirmMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceConfimNotificationKeyName).SingleOrDefault()?.Value;
        //    var englishConfirmMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceConfimNotificationKeyName).SingleOrDefault()?.EnglishValue;

        //    var userFromDB = await _context.Users.Where(c => c.UserName == baseServiceFromDB.UserName)
        //        .Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();
        //    bool isPersian = _commonService.IsPersianLanguage();

        //    string confirmMessage = isPersian ? persianConfirmMessage : englishConfirmMessage;

        //    if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
        //        await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("Notifis", confirmMessage);
        //    #endregion

        //    try
        //    {
        //        await _context.ServiceRequestTBL.AddAsync(request);
        //        await _context.SaveChangesAsync();
        //        return Ok(_commonService.OkResponse(null, false));
        //    }
        //    catch
        //    {
        //        List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //           new ApiBadRequestResponse(erros, 500));
        //    }
        //}






    }
}
