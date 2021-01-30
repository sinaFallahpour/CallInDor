using Domain;
using Domain.DTO.RequestService;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.RequestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace CallInDoor.Hubs
{

    //[Authorize]
    public class ChatHub : Hub
    {

        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommonService _commonService;
        private readonly MemoryCache _cache;

        private readonly IAccountService _accountService;
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IRequestService _requestService;

        //AppUser vd;
        public ChatHub(UserManager<AppUser> userManager,
            DataContext context,
            ICommonService commonService,
             IAccountService accountService,
              IStringLocalizer<ShareResource> localizerShared,
              IRequestService requestService
            )
        {
            _cache = new MemoryCache(new MemoryCacheOptions { });
            _accountService = accountService;
            _context = context;
            _userManager = userManager;
            _commonService = commonService;
            _localizerShared = localizerShared;
            _requestService = requestService;
        }





        //public override async Task OnConnectedAsync()
        //{
        //var connectionId = Context.ConnectionId;
        //var currentUserName = Context.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        //var currentRequestId = await _context.Users.Where(c => c.ChatNotificationId == connectionId)
        //                                     .Select(c => c.CurentRequestId).FirstOrDefaultAsync();

        //var requestFromDB = await _context.ServiceRequestTBL
        //                    .Where(c => c.Id == currentRequestId && c.ServiceRequestStatus == ServiceRequestStatus.Confirmed)
        //                    .FirstOrDefaultAsync();


        ////از نوع
        //if (requestFromDB != null && requestFromDB.IsPerodedOrsesionChat)
        //{
        //    RequestStatusForRedis requestStatusForRedis = RequestStatusForRedis.OkPlan;
        //    if (requestFromDB == null)
        //        requestStatusForRedis = RequestStatusForRedis.NofFoundRequestTBL;
        //    else if (requestFromDB.IsExpired_PeriodedChatVoice == false || requestFromDB.HasPlan_PeriodedChatVoice == false)
        //        requestStatusForRedis = RequestStatusForRedis.BadPlan;

        //    //check kon zam baghi mande da redis ra
        //    var redisKey = PublicHelper.Redis_ChatVoiceDurationKeyName + currentRequestId;
        //    var chatVoiceDuration = new RedisValueForDurationChatVoice()
        //    {
        //        StartTime = DateTime.Now,
        //        EndTime = DateTime.Now.AddSeconds((int)requestFromDB.ReaminingTime_PeriodedChatVoice),
        //        RequestStatusForRedis = requestStatusForRedis,
        //        Chats = new List<ChatsVM>()
        //    };
        //    _cache.Set(redisKey, chatVoiceDuration);
        //}

        //    await base.OnConnectedAsync();

        //}





        //public override async Task OnDisconnectedAsync(Exception exception)
        //{

        //    var connectionId = Context.ConnectionId;
        //    var currentUserName = Context.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        //    var currentRequestId = await _context.Users
        //                          .Where(c => c.ChatNotificationId == connectionId)
        //                              .Select(c => c.CurentRequestId)
        //                                    .FirstOrDefaultAsync();

        //    var requestFromDB = await _context.ServiceRequestTBL.Include(c => c.ChatForPeriodedOrSessionServiceMessagesTBLs)
        //                                                        .Include(c => c.UsedPeriodedChatTBLs)
        //                                                        .Where(c => c.Id == currentRequestId)
        //                                                        .FirstOrDefaultAsync();

        //    if (requestFromDB != null && requestFromDB.IsPerodedOrsesionChat && requestFromDB.HasPlan_PeriodedChatVoice != false)
        //    {

        //        //اگرکلاینت دیسکانکت شد
        //        if (currentUserName == requestFromDB.ClienUserName)
        //        {

        //            //var redisKey = PublicHelper.Redis_ChatVoiceDurationKeyName + currentRequestId;
        //            //var chatVoiceValueFromRedis = _cache.Get<RedisValueForDurationChatVoice>(redisKey);
        //            //if (chatVoiceValueFromRedis != null && chatVoiceValueFromRedis.RequestStatusForRedis == RequestStatusForRedis.OkPlan)
        //            //{


        //            var isExpirePackage = DateTime.Now >= requestFromDB.EndTime_PeriodedChatVoice;
        //            //var isExpirePackage = DateTime.Now <= chatVoiceValueFromRedis.EndTime;

        //            if (!isExpirePackage)
        //            {
        //                //var remainingTimePersecond = chatVoiceValueFromRedis.EndTime
        //                //                                            .DifferenceOfTwoDateTimePerSecond(DateTime.Now);

        //                var remainingTimePersecond = requestFromDB.EndTime_PeriodedChatVoice
        //                                                    .DifferenceOfTwoDateTimePerSecond(DateTime.Now);

        //                ////////chatVoiceValueFromRedis.EndTime
        //                ////////                                        .DifferenceOfTwoDateTimePerSecond(DateTime.Now);

        //                requestFromDB.ReaminingTime_PeriodedChatVoice = (int)remainingTimePersecond;

        //                //var usedPerodedChatTBL = new UsedPeriodedChatTBL()
        //                //{
        //                //    ClientUserName = requestFromDB.ClienUserName,
        //                //    ProviderUserName = requestFromDB.ProvideUserName,
        //                //    ServiceRequestId = requestFromDB.Id,
        //                //    UsedTime = (int)(DateTime.Now - chatVoiceValueFromRedis.StartTime).TotalSeconds,
        //                //};

        //                var usedPerodedChatTBL = new UsedPeriodedChatTBL()
        //                {
        //                    ClientUserName = requestFromDB.ClienUserName,
        //                    ProviderUserName = requestFromDB.ProvideUserName,
        //                    ServiceRequestId = requestFromDB.Id,
        //                    UsedTime = (int)(DateTime.Now - requestFromDB.StartTime_PeriodedChatVoice).TotalSeconds,
        //                    EndTime = DateTime.Now,
        //                    StartTime = requestFromDB.StartTime_PeriodedChatVoice
        //                };

        //                //var chats = chatVoiceValueFromRedis.Chats;
        //                //var chatForPerodedOrSession = new List<ChatForPeriodedOrSessionServiceMessagesTBL>();
        //                //foreach (var item in chats)
        //                //{
        //                //    var chatMessages = new ChatForPeriodedOrSessionServiceMessagesTBL()
        //                //    {
        //                //        ClientUserName = item.ClientUserName,
        //                //        ProviderUserName = item.ProviderUserName,
        //                //        FileOrVoiceAddress = item.FileOrVoiceAddress,
        //                //        ChatMessageType = item.ChatMessageType,
        //                //        CreateDate = item.CreateDate,
        //                //        IsProviderSend = item.IsProviderSend,
        //                //        ServiceRequestId = item.ServiceRequestId,
        //                //        Text = item.Text,
        //                //        SendetMesageType = item.SendetMesageType,
        //                //    };
        //                //    chatForPerodedOrSession.Add(chatMessages);
        //                //}

        //                //update used chats
        //                requestFromDB.UsedPeriodedChatTBLs.Add(usedPerodedChatTBL);
        //                //updaqtes chats
        //                //requestFromDB.ChatForPeriodedOrSessionServiceMessagesTBLs.AddRange(chatForPerodedOrSession);
        //            }
        //            else
        //            {
        //                requestFromDB.IsExpired_PeriodedChatVoice = true;
        //            }

        //            await _context.SaveChangesAsync();
        //            //_cache.Remove(redisKey);



        //            //}
        //        }
        //    }

        //    await base.OnDisconnectedAsync(exception);

        //}










        //public override async Task OnDisconnectedAsync(Exception exception)
        //{


        //    var connectionId = Context.ConnectionId;
        //    var currentUserName = Context.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        //    var currentRequestId = await _context.Users
        //                          .Where(c => c.ChatNotificationId == connectionId)
        //                              .Select(c => c.CurentRequestId)
        //                                    .FirstOrDefaultAsync();

        //    var requestFromDB = await _context.ServiceRequestTBL.Include(c => c.ChatForPeriodedOrSessionServiceMessagesTBLs)
        //                                                        .Include(c => c.UsedPeriodedChatTBLs)
        //                                                        .Where(c => c.Id == currentRequestId)
        //                                                            .FirstOrDefaultAsync();


        //    if (requestFromDB != null && requestFromDB.IsPerodedOrsesionChat != false && requestFromDB.HasPlan_PeriodedChatVoice != false)
        //    {
        //        var redisKey = PublicHelper.Redis_ChatVoiceDurationKeyName + currentRequestId;
        //        var chatVoiceValueFromRedis = _cache.Get<RedisValueForDurationChatVoice>(redisKey);
        //        if (chatVoiceValueFromRedis != null && chatVoiceValueFromRedis.RequestStatusForRedis == RequestStatusForRedis.OkPlan)
        //        {


        //            var isExpirePackage = DateTime.Now <= chatVoiceValueFromRedis.EndTime;

        //            if (!isExpirePackage)
        //            {
        //                var remainingTimePersecond = chatVoiceValueFromRedis.EndTime
        //                                                            .DifferenceOfTwoDateTimePerSecond(DateTime.Now);

        //                requestFromDB.ReaminingTime_PeriodedChatVoice = (int)remainingTimePersecond;

        //                var usedPerodedChatTBL = new UsedPeriodedChatTBL()
        //                {
        //                    ClientUserName = requestFromDB.ClienUserName,
        //                    ProviderUserName = requestFromDB.ProvideUserName,
        //                    ServiceRequestId = requestFromDB.Id,
        //                    UsedTime = (int)(DateTime.Now - chatVoiceValueFromRedis.StartTime).TotalSeconds
        //                };

        //                var chats = chatVoiceValueFromRedis.Chats;
        //                var chatForPerodedOrSession = new List<ChatForPeriodedOrSessionServiceMessagesTBL>();
        //                foreach (var item in chats)
        //                {
        //                    var chatMessages = new ChatForPeriodedOrSessionServiceMessagesTBL()
        //                    {
        //                        ClientUserName = item.ClientUserName,
        //                        ProviderUserName = item.ProviderUserName,
        //                        FileOrVoiceAddress = item.FileOrVoiceAddress,
        //                        ChatMessageType = item.ChatMessageType,
        //                        CreateDate = item.CreateDate,
        //                        IsProviderSend = item.IsProviderSend,
        //                        ServiceRequestId = item.ServiceRequestId,
        //                        Text = item.Text,
        //                        SendetMesageType = item.SendetMesageType,
        //                    };
        //                    chatForPerodedOrSession.Add(chatMessages);
        //                }

        //                //update used chats
        //                requestFromDB.UsedPeriodedChatTBLs.Add(usedPerodedChatTBL);
        //                //updaqtes chats
        //                requestFromDB.ChatForPeriodedOrSessionServiceMessagesTBLs.AddRange(chatForPerodedOrSession);
        //            }
        //            else
        //            {
        //                requestFromDB.IsExpired_PeriodedChatVoice = true;
        //            }

        //            _cache.Remove(redisKey);

        //        }
        //    }

        //    await base.OnDisconnectedAsync(exception);

        //}









        ///// <summary>
        /////  چت هایه از نوع چت وویس هاه زمانی
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("SendChatToDurationChatService")]
        //[Authorize]
        ////[ClaimsAuthorize(IsAdmin = false)]
        //public async Task<ActionResult> MyFakeOnConnectedAsync(int requestuestId)
        //{

        //    var connectionId = Context.ConnectionId;
        //    var currentUserName = Context.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        //    var currentRequestId = await _context.Users.Where(c => c.ChatNotificationId == connectionId)
        //                                         .Select(c => c.CurentRequestId).FirstOrDefaultAsync();

        //    var requestFromDB = await _context.ServiceRequestTBL
        //                        .Where(c => c.Id == currentRequestId && c.ServiceRequestStatus == ServiceRequestStatus.Confirmed)
        //                        .FirstOrDefaultAsync();


        //    //از نوع
        //    if (requestFromDB != null && requestFromDB.IsPerodedOrsesionChat)
        //    {
        //        RequestStatusForRedis requestStatusForRedis = RequestStatusForRedis.OkPlan;
        //        if (requestFromDB == null)
        //            requestStatusForRedis = RequestStatusForRedis.NofFoundRequestTBL;
        //        else if (requestFromDB.IsExpired_PeriodedChatVoice == false || requestFromDB.HasPlan_PeriodedChatVoice == false)
        //            requestStatusForRedis = RequestStatusForRedis.BadPlan;

        //        //check kon zam baghi mande da redis ra
        //        var redisKey = PublicHelper.Redis_ChatVoiceDurationKeyName + currentRequestId;
        //        var chatVoiceDuration = new RedisValueForDurationChatVoice()
        //        {
        //            StartTime = DateTime.Now,
        //            EndTime = DateTime.Now.AddSeconds((int)requestFromDB.ReaminingTime_PeriodedChatVoice),
        //            RequestStatusForRedis = requestStatusForRedis,
        //            Chats = new List<ChatsVM>()
        //        };
        //        _cache.Set(redisKey, chatVoiceDuration);
        //    }

        //}









        ///// <summary>
        /////  چت هایه از نوع چت وویس هاه زمانی
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("SendChatToDurationChatService")]
        //[Authorize]
        ////[ClaimsAuthorize(IsAdmin = false)]
        //public async Task<ActionResult> SendChatToDurationChatService([FromForm] SendChatToChatServiceDTO model)
        //{
        //    var currentUsername = _accountService.GetCurrentUserName();

        //    if ((!model.IsFile && !model.IsVoice) && string.IsNullOrEmpty(model.Text))
        //    {
        //        List<string> erros = new List<string> { _localizerShared["TextIsRequired"].Value.ToString() };
        //        return new JsonResult(new { Status = false, Errors = erros });
        //        //return NotFound(new ApiBadRequestResponse(erros));
        //    }

        //    ServiceRequestTBL requestfromDB = await _context.ServiceRequestTBL
        //                                                        .FindAsync(model.ServiceRequestId);

        //    #region validate request
        //    var res = _requestService.ValidateSendChatVoiceDuration(requestfromDB);
        //    if (!res.succsseded)
        //        return new JsonResult(new { Status = false, Errors = res.result });
        //    //return BadRequest(new ApiBadRequestResponse(res.result));
        //    #endregion
        //    //check kon zam baghi mande da redis ra
        //    var redisKey = PublicHelper.Redis_ChatVoiceDurationKeyName + model.ServiceRequestId;
        //    var chatVoiceValueFromRedis = _cache.Get<RedisValueForDurationChatVoice>(redisKey);




        //    ///  if  cuurentusername ==clientUSername  ==>   validate redis 
        //    #region redis validation

        //    var resValidate = _requestService.ValidateRedisSendChatVoiceDuration(chatVoiceValueFromRedis);
        //    if (!resValidate.succsseded)
        //        return new JsonResult(new { Status = false, Errors = resValidate.result });
        //    //return BadRequest(new ApiBadRequestResponse(resValidate.result));

        //    #endregion  redis validation
        //    #region file validation

        //    //validate the file an voice
        //    var resFile = _requestService.ValidateSendChatToChatService(model);
        //    if (!resFile.succsseded)
        //        return new JsonResult(new { Status = false, Errors = resFile.result });

        //    //return BadRequest(new ApiBadRequestResponse(resFile.result));

        //    #endregion  file validation

        //    var SendeMesageType = SendetMesageType.Client;
        //    SendeMesageType = requestfromDB.ProvideUserName == currentUsername ? SendetMesageType.Provider : SendetMesageType.Client;

        //    var clienUserName = requestfromDB.ClienUserName;
        //    var providerUserName = requestfromDB.ProvideUserName;

        //    //********************************* from mainDb ****************************************
        //    List<AppUser> userFromDB = await _context.Users
        //        .Where(c => c.UserName == clienUserName || c.UserName == providerUserName)
        //        //.Select(c => new { c.CountryCode, c.WalletBalance, c.UserName })
        //        .ToListAsync();

        //    AppUser clientFromDB = userFromDB.Where(c => c.UserName == clienUserName).FirstOrDefault();
        //    AppUser providerFromDB = userFromDB.Where(c => c.UserName == providerUserName).FirstOrDefault();

        //    string recieveConnectoinId;
        //    if (SendeMesageType == SendetMesageType.Client)
        //        //بگیر پرووایدر را
        //        recieveConnectoinId = await _context.Users.Where(c => c.UserName.ToLower() == requestfromDB.ProvideUserName).Select(c => c.ChatNotificationId).FirstOrDefaultAsync();
        //    else
        //        //بگیر ریکوست دهنده
        //        recieveConnectoinId = await _context.Users.Where(c => c.UserName.ToLower() == requestfromDB.ClienUserName)
        //                                                .Select(c => c.ChatNotificationId).FirstOrDefaultAsync();

        //    #region  uploading file
        //    //uploading the file
        //    string fileAddress = "";
        //    if (model.IsFile || model.IsVoice)
        //    {
        //        try
        //        {
        //            IFormFile file = model.IsFile ? model.File : model.Voice;
        //            bool isVoice = model.IsFile ? false : true;

        //            fileAddress = await _requestService.SaveFileToHost("Upload/ChatRequestFile/", null, file, isVoice);
        //            //tiketMessage.FileAddress = fileAddress;
        //        }
        //        catch
        //        {
        //            //throw new HubException(UnauthorizedAccessException,)
        //            var errors = new List<string>() { "problem uploading the file" };
        //            return new JsonResult(new { Status = false, Errors = errors });
        //            //return StatusCode(StatusCodes.Status500InternalServerError,
        //            //             new ApiResponse(500, "problem uploading the file"));
        //        }
        //    }

        //    #endregion

        //    //send to client with signalr
        //    if (!string.IsNullOrEmpty(recieveConnectoinId))
        //        await Clients.Client(recieveConnectoinId).SendAsync("GetDurationChatVoiceChat", (model.IsFile || model.IsVoice) ? fileAddress : model.Text, DateTime.Now, _requestService.ReturnChatMessageType(model));




        //    //be jaye in bere to redis
        //    var chatServiceMessagesTBL = new ChatsVM()
        //    {
        //        Text = model.Text,
        //        CreateDate = DateTime.Now,
        //        ProviderUserName = requestfromDB.ProvideUserName,
        //        ClientUserName = requestfromDB.ClienUserName,
        //        ServiceRequestId = requestfromDB.Id,
        //        SendetMesageType = SendeMesageType,
        //        IsProviderSend = SendeMesageType != SendetMesageType.Client ? true : false,
        //        //Price = isNativeCustomer ? myChatServiceFromDB?.PriceForNativeCustomer : myChatServiceFromDB?.PriceForNonNativeCustomer,
        //        ChatMessageType = _requestService.ReturnChatMessageType(model),
        //        FileOrVoiceAddress = fileAddress,
        //    };

        //    chatVoiceValueFromRedis.Chats.Add(chatServiceMessagesTBL);
        //    _cache.Set(redisKey, chatVoiceValueFromRedis);

        //    try
        //    {
        //        //await _context.ChatServiceMessagesTBL.AddAsync(chatServiceMessagesTBL);
        //        await _context.SaveChangesAsync();
        //        return new JsonResult(new { Status = true, data = _commonService.OkResponse((model.IsFile || model.IsVoice) ? fileAddress : model.Text, false) });

        //        //return Ok(_commonService.OkResponse((model.IsFile || model.IsVoice) ? fileAddress : model.Text, false));
        //    }
        //    catch
        //    {
        //        List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
        //        return new JsonResult(new { Status = false, Errors = erros });


        //        //return StatusCode(StatusCodes.Status500InternalServerError,
        //        //   new ApiBadRequestResponse(erros, 500));
        //    }


        //}





        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }



        public async Task UserConnection(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = await _userManager.FindByIdAsync(id);
                ////._context..RestaurantRequests.SingleOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    user.ChatNotificationId = Context.ConnectionId;
                    //_context.Update(request);
                    await _context.SaveChangesAsync();
                }
            }
        }







        ///// <summary>
        ///// update curetnRequestId and comnnectionId connectionId
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<object> UpdateCurrentRequestIdAndConnectionId(string id, int requestId)
        //{
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        var user = await _userManager.FindByIdAsync(id);

        //        if (user != null)
        //        {
        //            user.ChatNotificationId = Context.ConnectionId;
        //            user.CurentRequestId = requestId;
        //            //_context.Update(request);
        //            await _context.SaveChangesAsync();
        //            return new { status = true, };
        //        }
        //        return new { status = false, Errors = _localizerShared["NotFound"].Value.ToString() };
        //    }

        //    return new { status = false, Errors = _localizerShared["NotFound"].Value.ToString() };

        //}


    }
}
