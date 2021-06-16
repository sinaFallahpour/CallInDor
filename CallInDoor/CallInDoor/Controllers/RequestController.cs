//using CallInDoor.Config.Attributes;
//using CallInDoor.Hubs;
//using Domain;
//using Domain.DTO.RequestService;
//using Domain.DTO.Response;
//using Domain.DTO.Service;
//using Domain.Entities;
//using Domain.Enums;
//using Domain.Utilities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.Extensions.Localization;
//using Service.Interfaces.Account;
//using Service.Interfaces.Common;
//using Service.Interfaces.RequestService;
//using Service.Interfaces.Resource;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CallInDoor.Controllers
//{
//    //[Route("api/[controller]")]
//    //[ApiController]
//    public class RequestController : BaseControlle
//    {
//        #region ctor


//        private readonly MemoryCache _cache;

//        private readonly IHubContext<ChatHub> _chatHubContext;
//        private readonly IHubContext<NotificationHub> _hubContext;


//        private readonly DataContext _context;
//        private readonly IAccountService _accountService;
//        private readonly ICommonService _commonService;
//        private readonly IRequestService _requestService;


//        private IStringLocalizer<RequestController> _localizer;
//        //private IStringLocalizer<ShareResource> _localizerShared;

//        private readonly IResourceServices _resourceServices;


//        public RequestController(
//            IHubContext<ChatHub> chatHubContext,
//            IHubContext<NotificationHub> hubContext,
//            DataContext context,
//                   IAccountService accountService,
//                   ICommonService commonService,
//                   IRequestService requestService,
//               IStringLocalizer<RequestController> localizer,
//                //IStringLocalizer<ShareResource> localizerShared,
//                IResourceServices resourceServices
//            )
//        {

//            _cache = new MemoryCache(new MemoryCacheOptions { });
//            _hubContext = hubContext;
//            _chatHubContext = chatHubContext;
//            _context = context;
//            _accountService = accountService;
//            _commonService = commonService;
//            _requestService = requestService;
//            _localizer = localizer;
//            //_localizerShared = localizerShared;
//            _resourceServices = resourceServices;
//        }

//        #endregion ctor





//        /// <summary>
//        ///گرفتن ریکوست هایی که pending هستند      
//        ///تمام ریکوست هایی که به من داده  شده
//        /// </summary>
//        /// <param name="Id"></param>
//        /// <returns></returns>
//        [HttpGet("GetAllMyRequests")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> GetAllMyRequests()
//        {
//            var currentUsername = _accountService.GetCurrentUserName();
//            var requests = await (from c in _context.ServiceRequestTBL.Where(c => c.ProvideUserName == currentUsername
//                                  && c.ServiceRequestStatus == ServiceRequestStatus.Pending)
//                                  join u in _context.Users
//                                  on c.ClienUserName.ToLower() equals u.UserName.ToLower()
//                                  select new
//                                  {
//                                      c.Id,
//                                      c.BaseMyServiceTBL.ServiceName,
//                                      ////c.ServiceType,
//                                      ServiceType = c.ServiceTypes,
//                                      c.PackageType,
//                                      c.CreateDate,
//                                      c.BaseServiceId,
//                                      c.ServiceRequestStatus,

//                                      u.ImageAddress,
//                                      u.Name,
//                                      u.LastName,
//                                  })
//                                  .AsQueryable()
//                                  .ToListAsync();

//            return Ok(_commonService.OkResponse(requests, false));
//        }





//        /// <summary>
//        ///تمام درخواست هایی که به دیگران دادم  
//        ///البته با فیلتر
//        /// </summary>
//        /// <param name="Id"></param>
//        /// <returns></returns>
//        [HttpGet("GetAllMyClientRequest")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> GetAllMyClientRequest(ServiceRequestStatus? serviceRequestStatus)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();
//            var queryAble = _context.ServiceRequestTBL.AsQueryable();

//            if (serviceRequestStatus != null)
//                queryAble = queryAble.Where(c => c.ServiceRequestStatus == serviceRequestStatus);

//            var requests = await (from c in queryAble.Where(c => c.ClienUserName == currentUsername)
//                                  join u in _context.Users
//                                  on c.ProvideUserName.ToLower() equals u.UserName.ToLower()
//                                  select new
//                                  {
//                                      c.Id,
//                                      c.BaseMyServiceTBL.ServiceName,
//                                      //c.ServiceType,
//                                      ServiceTypes = c.ServiceTypes,
//                                      c.PackageType,
//                                      c.CreateDate,
//                                      c.BaseServiceId,
//                                      c.ServiceRequestStatus,

//                                      u.ImageAddress,
//                                      u.Name,
//                                      u.LastName,
//                                  })
//                                  .AsQueryable()
//                                  .ToListAsync();

//            return Ok(_commonService.OkResponse(requests, false));
//        }







//        /// <summary>
//        ///  گرفتن لیست چت هایی که با افراد مختلف کردم           
//        /// </summary>
//        /// <param name="Id"></param>
//        /// <returns></returns>
//        [HttpGet("GetAllChatsList")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> GetAllChatsList()
//        {
//            var currentUsername = _accountService.GetCurrentUserName();

//            var myClientservices = _context.ServiceRequestTBL
//                                .Where(c =>
//                             c.ServiceRequestStatus == ServiceRequestStatus.Confirmed &&
//                             ////////c.ServiceType == ServiceType.ChatVoice
//                             c.ServiceTypes.Contains("0")
//                                //////== ServiceType.ChatVoice
//                                && c.ClienUserName == currentUsername)
//                                .Include(c => c.ChatServiceMessagesTBL)
//                                .Include(c => c.ChatForLimitedServiceMessagesTBL)
//                                .AsQueryable().ToList();

//            var myProviderservices = _context.ServiceRequestTBL
//                                .Where(c =>
//                                    c.ServiceRequestStatus == ServiceRequestStatus.Confirmed &&
//                                   //////c.ServiceType == ServiceType.ChatVoice &&
//                                   c.ServiceTypes.Contains("0") &&
//                                    c.ProvideUserName == currentUsername)
//                                .Include(c => c.ChatServiceMessagesTBL)
//                                .Include(c => c.ChatForLimitedServiceMessagesTBL)
//                                .AsQueryable().ToList();

//            var myClientRequestJoinWithUser = await (from c in myClientservices
//                                                         //join chat in _context.ChatServiceMessagesTBL
//                                                         //on c.Id equals chat.ServiceRequestId
//                                                     join u in _context.Users
//                                                     on c.ProvideUserName.ToLower() equals u.UserName.ToLower()
//                                                     select new AllSericesVM
//                                                     {
//                                                         ServiceName = c.BaseMyServiceTBL.ServiceName,
//                                                         CreateDate = c.CreateDate,
//                                                         LastTimeTheMessageWasSent = c.LastTimeTheMessageWasSent,
//                                                         UnreadMessageCount =
//                                                            c.IsLimitedChat ?
//                                                                   c.ChatForLimitedServiceMessagesTBL
//                                                                   .Where(X =>
//                                                                       X.IsSeen == false
//                                                                       &&
//                                                                       X.IsProviderSend).Count()

//                                                                       //(c.ClienUserName == currentUsername ? X.IsProviderSend :
//                                                                       //!X.IsProviderSend)).Count()
//                                                                       :

//                                                                   c.ChatServiceMessagesTBL
//                                                                   .Where(X =>
//                                                                       X.IsSeen == false
//                                                                       &&
//                                                                       X.IsProviderSend).Count()

//                                                                       //(c.ClienUserName == currentUsername ? X.IsProviderSend :
//                                                                       //         !X.IsProviderSend)).Count()
//                                                                       ,
//                                                         ImageAddress = u.ImageAddress,
//                                                         Name = u.Name,
//                                                         LastName = u.LastName,
//                                                         IsPerodedOrsesionChat = c.IsLimitedChat,
//                                                         Id = c.Id
//                                                     }).AsQueryable().ToListAsync();


//            var myProviderRequestJoinWithUser = await (from c in myProviderservices
//                                                           //join chat in _context.ChatServiceMessagesTBL
//                                                           //on c.Id equals chat.ServiceRequestId
//                                                       join u in _context.Users
//                                                       on c.ClienUserName.ToLower() equals u.UserName.ToLower()
//                                                       select new AllSericesVM
//                                                       {
//                                                           ServiceName = c.BaseMyServiceTBL.ServiceName,
//                                                           CreateDate = c.CreateDate,
//                                                           LastTimeTheMessageWasSent = c.LastTimeTheMessageWasSent,
//                                                           UnreadMessageCount =
//                                                          c.IsLimitedChat ?
//                                                                   c.ChatForLimitedServiceMessagesTBL
//                                                                   .Where(X =>
//                                                                       X.IsSeen == false
//                                                                       &&
//                                                                       !X.IsProviderSend).Count()
//                                                                       //(c.ProvideUserName == currentUsername ? !X.IsProviderSend :
//                                                                       //X.IsProviderSend)).Count()
//                                                                       :
//                                                                       c.ChatServiceMessagesTBL
//                                                                   .Where(X =>
//                                                                       X.IsSeen == false
//                                                                       &&
//                                                                       !X.IsProviderSend).Count()
//                                                                       //(c.ClienUserName == currentUsername ? !X.IsProviderSend :
//                                                                       //X.IsProviderSend)).Count()
//                                                                       ,
//                                                           ImageAddress = u.ImageAddress,
//                                                           Name = u.Name,
//                                                           LastName = u.LastName,
//                                                           IsPerodedOrsesionChat = c.IsLimitedChat,
//                                                           Id = c.Id
//                                                       }).AsQueryable().ToListAsync();

//            var allChatInfo = new List<AllSericesVM>();
//            allChatInfo.AddRange(myClientRequestJoinWithUser);
//            allChatInfo.AddRange(myProviderRequestJoinWithUser);
//            allChatInfo = allChatInfo.OrderBy(c => c.LastTimeTheMessageWasSent).ThenBy(c => c.CreateDate).ToList();
//            return Ok(_commonService.OkResponse(allChatInfo, false));
//        }





//        /// <summary>
//        /// درخواست به سرویسی که از جنس چت ووییس است و فقط free(است)
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("RequestToChatService")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> RequestToChatService([FromBody] RequestToChatServiceDTO model)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();

//            var baseServiceFromDB = await _context.BaseMyServiceTBL
//                                                     .Where(c => c.Id == model.BaseServiceId && c.IsDeleted == false)
//                                                                     .Include(c => c.MyChatsService)
//                                                                     .FirstOrDefaultAsync();

//            var hasReserveRequest = await _context.ServiceRequestTBL.AnyAsync(c =>

//                                     (c.ServiceRequestStatus == ServiceRequestStatus.Confirmed || c.ServiceRequestStatus == ServiceRequestStatus.Pending)
//                                     &&
//                                  c.BaseServiceId == model.BaseServiceId &&
//                                    c.ClienUserName == currentUsername &&
//                                            ////////c.ServiceType == ServiceType.ChatVoice &&
//                                            c.ServiceTypes.Contains("0") &&
//                                            (c.PackageType == PackageType.Free));



//            var res = await _requestService.ValidateRequestToChatService(baseServiceFromDB, hasReserveRequest);

//            if (!res.succsseded)
//                return BadRequest(new ApiBadRequestResponse(res.result));

//            var request = new ServiceRequestTBL()
//            {
//                IsLimitedChat = false,

//                FreeMessageCount = baseServiceFromDB.MyChatsService?.FreeMessageCount,
//                FreeUsageMessageCount = 0,
//                BaseServiceId = model.BaseServiceId,
//                //////ServiceType = baseServiceFromDB.ServiceType,
//                ServiceTypes = baseServiceFromDB.ServiceTypes,
//                CreateDate = DateTime.Now,
//                WhenTheRequestShouldBeAnswered = DateTime.Now.AddHours(8),
//                ClienUserName = currentUsername,
//                ProvideUserName = baseServiceFromDB.UserName,
//                ServiceRequestStatus = ServiceRequestStatus.Pending,
//                PackageType = (PackageType)baseServiceFromDB.MyChatsService.PackageType,
//                PriceForNativeCustomer = baseServiceFromDB.MyChatsService.PriceForNativeCustomer,
//                PriceForNonNativeCustomer = baseServiceFromDB.MyChatsService.PriceForNonNativeCustomer,
//                //PackageType=baseServiceFromDB.p
//            };

//            request.ChatServiceMessagesTBL = new List<ChatServiceMessagesTBL>() {
//                new ChatServiceMessagesTBL()
//                    {
//                        SenderUserName= currentUsername,
//                        IsSeen=false,
//                        ChatMessageType = ChatMessageType.Text,
//                        IsProviderSend = false,
//                        SendetMesageType = SendetMesageType.Client,
//                        ClientUserName = currentUsername,
//                        CreateDate = DateTime.Now,
//                        ProviderUserName = baseServiceFromDB.UserName,
//                        Text = model.FirstMessage,
//                     }
//            };





//            #region send notification
//            var persianConfirmMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceConfimNotificationKeyName).SingleOrDefault()?.Value;
//            var englishConfirmMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceConfimNotificationKeyName).SingleOrDefault()?.EnglishValue;

//            var userFromDB = await _context.Users.Where(c => c.UserName == baseServiceFromDB.UserName)
//                .Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();
//            bool isPersian = _commonService.IsPersianLanguage();

//            string confirmMessage = isPersian ? persianConfirmMessage : englishConfirmMessage;

//            if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
//                await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("Notifis", confirmMessage);
//            #endregion


//            await _context.ServiceRequestTBL.AddAsync(request);
//            await _context.SaveChangesAsync();
//            return Ok(_commonService.OkResponse(null, false));

//        }


//        /// <summary>
//        /// درخواست به سرویسی که از جنس چت ووییس است و فقط free(است)
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPut("SeenAllUnLimitedChats")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> SeenAllUnLimitedChats(int? requestId)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();



//            var chats = await _context.ChatServiceMessagesTBL.Where(c => c.ServiceRequestId == requestId && c.IsSeen == false
//                                      && (c.ClientUserName == currentUsername || c.ProviderUserName == currentUsername))
//                                                 .Select(c => new customModeWithId { Id = c.Id, SenderUserName = c.SenderUserName }).ToListAsync();

//            chats = chats.Where(c => c.SenderUserName != currentUsername).ToList();


//            foreach (var item in chats)
//            {
//                var myChatServiceTBL = new ChatServiceMessagesTBL { Id = item.Id, IsSeen = true };
//                _context.ChatServiceMessagesTBL.Attach(myChatServiceTBL);
//                _context.Entry(myChatServiceTBL).Property(x => x.IsSeen).IsModified = true;
//            }



//            if (chats == null || chats.Count == 0)
//                return Ok(_commonService.OkResponse(null, false));
//            await _context.SaveChangesAsync();

//            #region send notification 
//            var requestfromDB = await _context.ServiceRequestTBL.AsNoTracking().Where(c => c.Id == requestId)
//                       .Select(c => new customModel { ProviderUserName = c.ProvideUserName, ClientUserName = c.ClienUserName })
//                       .FirstOrDefaultAsync();

//            await SendNotificatoins(requestfromDB, chats);
//            #endregion

//            return Ok(_commonService.OkResponse(null, false));

//        }



//        /// <summary>
//        /// درخواست به سرویسی که از جنس چت ووییس است و فقط free(است)
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPut("SeenAllLimitedChats")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> SeenAllLimitedChats(int? requestId)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();

//            var chats = await _context.ChatForLimitedServiceMessagesTBL.Where(c => c.ServiceRequestId == requestId && c.IsSeen == false
//                                      && (c.ClientUserName == currentUsername || c.ProviderUserName == currentUsername))
//                                                 .Select(c => new customModeWithId { Id = c.Id, SenderUserName = c.SenderUserName }).ToListAsync();
//            chats = chats.Where(c => c.SenderUserName == currentUsername).ToList();


//            foreach (var item in chats)
//            {
//                var chatForLimitedServiceMessagesTBL = new ChatForLimitedServiceMessagesTBL { Id = item.Id, IsSeen = true };
//                _context.ChatForLimitedServiceMessagesTBL.Attach(chatForLimitedServiceMessagesTBL);
//                _context.Entry(chatForLimitedServiceMessagesTBL).Property(x => x.IsSeen).IsModified = true;
//            }


//            if (chats == null || chats.Count == 0)
//                return Ok(_commonService.OkResponse(null, false));

//            await _context.SaveChangesAsync();
//            #region send notification 
//            var requestfromDB = await _context.ServiceRequestTBL.AsNoTracking().Where(c => c.Id == requestId)
//                       .Select(c => new customModel { ProviderUserName = c.ProvideUserName, ClientUserName = c.ClienUserName })
//                       .FirstOrDefaultAsync();

//            await SendNotificatoins(requestfromDB, chats);
//            #endregion

//            return Ok(_commonService.OkResponse(null, false));
//        }


        

//        [NonAction]
//        public async Task SendNotificatoins(customModel requestfromDB, List<customModeWithId> chats)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();


//            var SendeMesageType = SendetMesageType.Client;
//            SendeMesageType = requestfromDB.ProviderUserName == currentUsername ? SendetMesageType.Provider : SendetMesageType.Client;

//            string recieveConnectoinId;
//            if (SendeMesageType == SendetMesageType.Client)
//            {
//                //بگیر پرووایدر را
//                recieveConnectoinId = await _context.Users.Where(c => c.UserName.ToLower() == requestfromDB.ProviderUserName)
//                      .Select(c => c.ChatNotificationId).FirstOrDefaultAsync();
//            }
//            else
//            {
//                //بگیر ریکوست دهنده
//                recieveConnectoinId = await _context.Users.Where(c => c.UserName.ToLower() == requestfromDB.ClientUserName)
//                      .Select(c => c.ChatNotificationId).FirstOrDefaultAsync();
//            }

//            if (!string.IsNullOrEmpty(recieveConnectoinId))
//                await _chatHubContext.Clients.Client(recieveConnectoinId).SendAsync("SeenAllChats", chats);

//        }





//        /// <summary>
//        /// گرفتن اطلاعات بیس سرویس با آیدی ریکوست
//        /// </summary>
//        /// <param name="requestId"></param>
//        /// <returns></returns>
//        [HttpGet("GetSeviceDatilsByRequestId")]
//        public async Task<ActionResult> GetSeviceDatilsByRequestId(int requestId)
//        {


//            var isPersian = _commonService.IsPersianLanguage();

//            var requestFromDB = await _context.ServiceRequestTBL.Where(c => c.Id == requestId).FirstOrDefaultAsync();

//            if (requestFromDB == null)
//            {
//                return BadRequest(_commonService.NotFoundErrorReponse(false));

//                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                //return BadRequest(new ApiBadRequestResponse(erros));
//            }

//            string clientCountryCode = await _context.Users.Where(c => c.UserName == requestFromDB.ClienUserName)
//                                                .Select(c => c.CountryCode).FirstOrDefaultAsync();


//            string providerCountryCode = await _context.Users.Where(c => c.UserName == requestFromDB.ProvideUserName)
//                                               .Select(c => c.CountryCode).FirstOrDefaultAsync();

//            bool isNativeCustomer = true;
//            if (clientCountryCode.ToLower() != providerCountryCode)
//                isNativeCustomer = false;

//            var userFromDB = await _context.BaseMyServiceTBL
//                .Where(c =>
//                c.Id == requestFromDB.BaseServiceId &&
//                c.IsDeleted == false
//                )
//               .Select(c => new
//               {
//                   c.StarCount,
//                   c.Under3StarCount,
//                   c.ServiceType,

//                   //UserId = user.Id,
//                   //UsersStar = user.StarCount,
//                   //Name = user.Name,
//                   //LastName = user.LastName,
//                   //ImageAddress = user.ImageAddress,

//                   c.ServiceName,
//                   CategoryName = isPersian ? c.CategoryTBL.PersianTitle : c.CategoryTBL.Title,
//                   ServiceStar = c.StarCount,
//                   ServiceUnder3Start = c.Under3StarCount,

//                   SubCategoryName = isPersian ? c.SubCategoryTBL.PersianTitle : c.SubCategoryTBL.Title,
//                   //SubCategoryPersianName = c.SubCategoryTBL.PersianTitle,

//                   ChatService = c.MyChatsService == null ? null :
//                        new
//                        {
//                            PackageType = c.MyChatsService != null ? c.MyChatsService.PackageType : null,
//                            //Price = c.MyChatsService != null ? c.MyChatsService.PriceForNativeCustomer : null,
//                            PriceForNativeCustomer = isNativeCustomer ? c.MyChatsService.PriceForNativeCustomer : c.MyChatsService.PriceForNonNativeCustomer,
//                            //PriceForNonNativeCustomer = c.MyChatsService != null ? c.MyChatsService.PriceForNonNativeCustomer : null,
//                            Duration = c.MyChatsService != null ? c.MyChatsService.Duration : null,
//                            FreeMessageCount = c.MyChatsService != null ? c.MyChatsService.FreeMessageCount : null,
//                            BeTranslate = c.MyChatsService != null ? c.MyChatsService.BeTranslate : false,
//                            IsServiceReverse = c.MyChatsService != null ? c.MyChatsService.IsServiceReverse : false,
//                        },

//                   ServiceService = c.MyServicesService == null ? null : new
//                   {
//                       Price = c.MyServicesService != null ? c.MyServicesService.Price : null,
//                       WorkDeliveryTimeEstimation = c.MyServicesService != null ? c.MyServicesService.WorkDeliveryTimeEstimation : null,
//                       Tags = c.MyServicesService != null ? c.MyServicesService.Tags : null,
//                       HowWorkConducts = c.MyServicesService != null ? c.MyServicesService.HowWorkConducts : null,
//                       FileDescription = c.MyServicesService != null ? c.MyServicesService.FileDescription : null,
//                       Description = c.MyServicesService != null ? c.MyServicesService.Description : null,
//                       DeliveryItems = c.MyServicesService != null ? c.MyServicesService.DeliveryItems : null,
//                       //Area = c.MyServicesService != null ? isPersian ? c.MyServicesService.AreaTBL.PersianTitle : c.MyServicesService.AreaTBL!.Title : null,
//                       //Speciality = c.MyServicesService != null ? isPersian ?  c.MyServicesService.SpecialityTBL.PersianName : c.MyServicesService.SpecialityTBL.EnglishName :   null,
//                   },

//                   CourseService = c.MyCourseService == null ? null : new
//                   {
//                       Price = c.MyCourseService != null ? c.MyCourseService.Price : null,
//                       Description = c.MyCourseService != null ? c.MyCourseService.Description : null,
//                       TotalLenght = c.MyCourseService != null ? c.MyCourseService.TotalLenght : null,
//                       DisCountPercent = c.MyCourseService != null ? c.MyCourseService.DisCountPercent : null,
//                       PreviewVideoAddress = c.MyCourseService != null ? c.MyCourseService.PreviewVideoAddress : null,
//                   },
//               })
//               .FirstOrDefaultAsync();


//            if (userFromDB == null)
//            {
//                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                //return BadRequest(new ApiBadRequestResponse(erros));

//                return BadRequest(_commonService.NotFoundErrorReponse(false));
//            }


//            return Ok(_commonService.OkResponse(userFromDB, false));
//            //return Ok(_commonService.OkResponse(userFromDB, _localizerShared["SuccessMessage"].Value.ToString()));
//        }


//        /// <summary>
//        ///قبول کردن سرویس که برای همه نوع سرویس ها فقط همین کال میشود
//        /// </summary>
//        /// <param name="requestId"></param>
//        /// <returns></returns>
//        [HttpGet("AcceptRequest")]
//        [Authorize]
//        public async Task<ActionResult> AcceptRequest(int requestId)
//        {
//            var currentUserName = _accountService.GetCurrentUserName();

//            var requestFromDB = await _context.ServiceRequestTBL
//                                    .Where(c => c.Id == requestId && c.ProvideUserName == currentUserName)
//                                    .AsNoTracking().Select(c => new { c.Id, c.ClienUserName })
//                                        .FirstOrDefaultAsync();

//            if (requestFromDB.Id == 0)
//            {
//                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                //return BadRequest(new ApiBadRequestResponse(erros, 404));

//                return BadRequest(_commonService.NotFoundErrorReponse(false));
//            }


//            var serviceRequestTBL = new ServiceRequestTBL() { Id = requestFromDB.Id, ServiceRequestStatus = ServiceRequestStatus.Confirmed };
//            _context.ServiceRequestTBL.Attach(serviceRequestTBL);
//            _context.Entry(serviceRequestTBL).Property(x => x.ServiceRequestStatus).IsModified = true;


//            var userFromDB = await _context.Users.Where(c => c.UserName.ToLower() == requestFromDB.ClienUserName.ToLower())
//                            .Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();

//            var notification = new NotificationTBL()
//            {
//                CreateDate = DateTime.UtcNow,
//                EnglishText = "your request has been accepted",
//                TextPersian = "درخواست شما قبول شده است",
//                IsReaded = false,
//                NotificationStatus = NotificationStatus.ServiceConfirmation,
//                SenderUserName = currentUserName,
//                UserName = requestFromDB.ClienUserName,
//            };
//            bool isPersian = _commonService.IsPersianLanguage();
//            string confirmMessage = isPersian ? notification.TextPersian : notification.EnglishText;

//            if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
//                await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("Notifis", confirmMessage);

//            await _context.SaveChangesAsync();
//            return Ok(_commonService.OkResponse(null, false));
//        }




//        [HttpGet("RejectRequest")]
//        [Authorize]
//        public async Task<ActionResult> RejectRequest(int requestId)
//        {
//            var currentUserName = _accountService.GetCurrentUserName();
//            var requestFromDB = await _context.ServiceRequestTBL
//                                           .Where(c => c.Id == requestId && c.ProvideUserName == currentUserName)
//                                                        .AsNoTracking().Select(c => new { c.Id, c.ClienUserName })
//                                                                                            .FirstOrDefaultAsync();

//            if (requestFromDB.Id == 0)
//            {
//                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                //return BadRequest(new ApiBadRequestResponse(erros, 404));

//                return BadRequest(_commonService.NotFoundErrorReponse(false));
//            }

//            //var requestFromDB = await _context.ServiceRequestTBL.FindAsync(requestId);
//            //if (requestFromDB == null)
//            //{
//            //    List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//            //    return BadRequest(new ApiBadRequestResponse(erros, 404));
//            //}

//            var serviceRequestTBL = new ServiceRequestTBL() { Id = requestFromDB.Id, ServiceRequestStatus = ServiceRequestStatus.Rejected };
//            _context.ServiceRequestTBL.Attach(serviceRequestTBL);
//            _context.Entry(serviceRequestTBL).Property(x => x.ServiceRequestStatus).IsModified = true;

//            //requestFromDB.ServiceRequestStatus = ServiceRequestStatus.Rejected;


//            var userFromDB = await _context.Users.Where(c => c.UserName.ToLower() == requestFromDB.ClienUserName.ToLower())
//                            .AsNoTracking()
//                            .Select(c => new { c.ConnectionId, c.UserName }).FirstOrDefaultAsync();


//            var notification = new NotificationTBL()
//            {
//                CreateDate = DateTime.Now,
//                EnglishText = "your request has been rejected",
//                TextPersian = "درخواست شما رد شده است",
//                IsReaded = false,
//                NotificationStatus = NotificationStatus.ServiceRejection,
//                SenderUserName = currentUserName,
//                UserName = requestFromDB.ClienUserName,
//            };

//            bool isPersian = _commonService.IsPersianLanguage();

//            string confirmMessage = isPersian ? notification.TextPersian : notification.EnglishText;

//            if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
//                await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("Notifis", confirmMessage);

//            await _context.SaveChangesAsync();
//            return Ok(_commonService.OkResponse(null, false));
//        }



//        [HttpGet("GetClientInFoWithRequestId")]
//        [Authorize]
//        public async Task<ActionResult> GetClientInFoWithRequestId(int requestId)
//        {
//            var currentUserName = _accountService.GetCurrentUserName();

//            var requestFromDB = await _context.ServiceRequestTBL.Where(c => c.Id == requestId &&
//                            (c.ProvideUserName == currentUserName || c.ClienUserName == currentUserName))
//                                 .FirstOrDefaultAsync();

//            if (requestFromDB == null)
//            {
//                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                //return NotFound(new ApiBadRequestResponse(erros, 404));
//                return BadRequest(_commonService.NotFoundErrorReponse(false));

//            }

//            if (requestFromDB.ProvideUserName == currentUserName)
//            {
//                var clientFromDB = await _context.Users.Where(c => c.UserName.ToLower() == requestFromDB.ClienUserName.ToLower())
//                        .Select(c => new { c.ImageAddress, c.Name, c.LastName }).FirstOrDefaultAsync();
//                if (clientFromDB == null)
//                {
//                    //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                    //return NotFound(new ApiBadRequestResponse(erros, 404));

//                    return BadRequest(_commonService.NotFoundErrorReponse(false));

//                }
//                return Ok(_commonService.OkResponse(clientFromDB, false));
//            }
//            else
//            {
//                var clientFromDB = await _context.Users.Where(c => c.UserName.ToLower() == requestFromDB.ProvideUserName.ToLower())
//                           .Select(c => new { c.ImageAddress, c.Name, c.LastName }).FirstOrDefaultAsync();
//                if (clientFromDB == null)
//                {
//                    //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                    //return NotFound(new ApiBadRequestResponse(erros, 404));
//                    return BadRequest(_commonService.NotFoundErrorReponse(false));

//                }
//                return Ok(_commonService.OkResponse(clientFromDB, false));
//            }


//        }



//        /// <summary>
//        ///  گرفتن  کل  چت هایی  پی وی که با افراد مختلف دارم           
//        /// </summary>
//        /// <param name="Id"></param>
//        /// <returns></returns>
//        [HttpGet("GetChatsDetails")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> GetChatsDetails(int requestId)
//        {

//            var currentUsername = _accountService.GetCurrentUserName();

//            var chats = await _context.ChatServiceMessagesTBL
//                .Where(c => c.ServiceRequestId == requestId
//                    && (c.ClientUserName == currentUsername || c.ProviderUserName == currentUsername))
//                .OrderBy(c => c.CreateDate)
//                .Select(c => new
//                {
//                    c.Id,
//                    c.CreateDate,
//                    c.Text,
//                    SendetMesageType = c.SendetMesageType,
//                    IsMyMessage = IsMyMessage(c.ServiceRequestTBL, c.IsProviderSend, currentUsername, c.ClientUserName, c.ProviderUserName),
//                    c.ChatMessageType,
//                    c.IsSeen,
//                    c.FileOrVoiceAddress,
//                })
//                .ToListAsync();

//            return Ok(_commonService.OkResponse(chats, false));
//        }





//        [NonAction]
//        private static bool IsMyMessage(ServiceRequestTBL ServiceRequestTBL, bool isProviderSend, string currentUserName, string clientUserName, string ProviderUserName)
//        {
//            if (ServiceRequestTBL?.ClienUserName == currentUserName)
//            {
//                if (isProviderSend)
//                    return false;
//                return true;
//            }
//            else
//            {
//                if (isProviderSend)
//                    return true;
//                return false;
//            }

//            //if (ServiceRequestTBL?.ClienUserName == currentUserName)
//            //    return (currentUserName == clientUserName && !isProviderSend);

//            //else
//            //    return (currentUserName == ProviderUserName && isProviderSend);

//        }





//        /// <summary>
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet("GetChatInfo")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> GetChatInfo(bool isLimited, int requestId)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();

//            ////************************************* in 2 ta if ja sho no bart  aks neveshtammm khodaaaaa vali same front handle kard*****************
//            if (isLimited)
//            {
//                var requestFromDB = await _context.ServiceRequestTBL.Where(c => c.Id == requestId && (c.ProvideUserName == currentUsername || c.ClienUserName == currentUsername))
//                      .Select(c => new
//                      {
//                          c.PriceForNativeCustomer,
//                          c.PriceForNonNativeCustomer,
//                          c.HasPlan_LimitedChatVoice,
//                          IsProvider = c.ProvideUserName == currentUsername ? true : false,
//                          c.ProvideUserName,
//                          c.ClienUserName,
//                          remaningFreeMessage = (c.FreeMessageCount - c.FreeUsageMessageCount) < 0 ? 0 : (c.FreeMessageCount - c.FreeUsageMessageCount)
//                      })
//                      .FirstOrDefaultAsync();
//                #region validation
//                if (requestFromDB == null)
//                {
//                    return BadRequest(_commonService.NotFoundErrorReponse(false));

//                }

//                if (requestFromDB.ProvideUserName != currentUsername && requestFromDB.ClienUserName != currentUsername)
//                {
//                    return BadRequest(_resourceServices.GetErrorMessageByKey("UnauthorizedMessage"));
//                }

//                #endregion
//                var isNative = await _accountService.IsNative(requestFromDB.ClienUserName, requestFromDB.ProvideUserName);
//                var response = new
//                {
//                    remaningFreeMessage = requestFromDB.remaningFreeMessage,
//                    requestFromDB.IsProvider,
//                    price = isNative ? requestFromDB.PriceForNativeCustomer : requestFromDB.PriceForNonNativeCustomer,
//                };
//                return Ok(_commonService.OkResponse(response, false));
//            }
//            else
//            {
//                var requestFromDB = await _context.ServiceRequestTBL.Where(c => c.Id == requestId)
//                      .Select(c => new
//                      {
//                          IsProvider = c.ProvideUserName == currentUsername ? true : false,
//                          remainedMessageCount = c.AllMessageCount_LimitedChat - c.UsedMessageCount_LimitedChat,
//                          c.UsedMessageCount_LimitedChat,
//                          c.ExpireTime_LimitedChatVoice,
//                          c.AllMessageCount_LimitedChat,
//                          c.ProvideUserName,
//                          c.ClienUserName,
//                      })
//                      .FirstOrDefaultAsync();


//                #region validation
//                if (requestFromDB == null)
//                {
//                    //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                    //return NotFound(new ApiBadRequestResponse(erros, 404));
//                    return BadRequest(_commonService.NotFoundErrorReponse(false));
//                }

//                if (requestFromDB.ProvideUserName != currentUsername && requestFromDB.ClienUserName != currentUsername)
//                {
//                    List<string> erros = new List<string> { 
//                        //_localizerShared["UnauthorizedMessage"].Value.ToString()
//                        _resourceServices.GetErrorMessageByKey("UnauthorizedMessage")

//                    };
//                    return BadRequest(new ApiBadRequestResponse(erros));
//                }

//                #endregion

//                //آیا برای آولینم بارشه که پکیج خریده یا باید تمدید کند
//                var IsRenew = await _context.BuyiedPackageTBL.AnyAsync(c => c.UserName == currentUsername
//                                            && c.ServiceRequestId == requestId && c.IsRenewPackage == false);

//                //var UserWallet = _context.Users.Where(c => c.UserName == currentUsername).Select(c => c.WalletBalance).FirstOrDefaultAsync();
//                var response = new
//                {
//                    hasPlan = (requestFromDB.AllMessageCount_LimitedChat - requestFromDB.UsedMessageCount_LimitedChat > 0) && requestFromDB.ExpireTime_LimitedChatVoice < DateTime.Now,
//                    requestFromDB.IsProvider,
//                    requestFromDB.remainedMessageCount,
//                    requestFromDB.AllMessageCount_LimitedChat,
//                    IsRenew = IsRenew,
//                };
//                return Ok(_commonService.OkResponse(response, false));


//            }
//        }



//        /// <summary>
//        /// چت کردن و گرفتن سرویس 
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("SendChatToChatService")]
//        [Authorize]
//        //[ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> SendChatToChatService([FromForm] SendChatToChatServiceDTO model)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();

//            if ((!model.IsFile && !model.IsVoice) && string.IsNullOrEmpty(model.Text))
//            {
//                List<string> erros = new List<string> {
//                    //_localizerShared["TextIsRequired"].Value.ToString() 
//                _resourceServices.GetErrorMessageByKey("TextIsRequired")
//                };
//                return BadRequest(new ApiBadRequestResponse(erros));
//            }


//            //*******   using dapper  *******//
//            ServiceRequestTBL requestfromDB = await _context.ServiceRequestTBL
//                                                                .FindAsync(model.ServiceRequestId);


//            if (requestfromDB == null)
//            {
//                //List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//                //return BadRequest(new ApiBadRequestResponse(erros, 404));
//                return BadRequest(_commonService.NotFoundErrorReponse(false));
//            }
//            if (requestfromDB.ServiceRequestStatus != ServiceRequestStatus.Confirmed)
//            {
//                List<string> erros = new List<string> { 
//                    //_localizerShared["RequestNotConfirmedMessgaes"].Value.ToString() 
//                _resourceServices.GetErrorMessageByKey("RequestNotConfirmedMessgaes")

//                };
//                return BadRequest(new ApiBadRequestResponse(erros));
//            }

//            if (requestfromDB.PackageType != PackageType.Free)
//            {
//                List<string> erros = new List<string> {
//                    //_localizerShared["InvalidPackageType"].Value.ToString()
//                _resourceServices.GetErrorMessageByKey("InvalidPackageType")

//                };
//                return BadRequest(new ApiBadRequestResponse(erros));
//            }


//            //validate the file  an  voice
//            var res = _requestService.ValidateSendChatToChatService(model);
//            if (!res.succsseded)
//                return BadRequest(new ApiBadRequestResponse(res.result));




//            //is is provide or is client
//            var SendeMesageType = SendetMesageType.Client;
//            SendeMesageType = requestfromDB.ProvideUserName == currentUsername ? SendetMesageType.Provider : SendetMesageType.Client;

//            var clienUserName = requestfromDB.ClienUserName;
//            var providerUserName = requestfromDB.ProvideUserName;



//            //******* using dapper   ********//
//            List<AppUser> userFromDB = await _context.Users
//                .Where(c => c.UserName == clienUserName || c.UserName == providerUserName)
//                //.Select(c => new { c.CountryCode, c.WalletBalance, c.UserName })
//                .ToListAsync();

//            AppUser clientFromDB = userFromDB.Where(c => c.UserName == clienUserName).FirstOrDefault();
//            AppUser providerFromDB = userFromDB.Where(c => c.UserName == providerUserName).FirstOrDefault();



//            string recieveConnectoinId;
//            bool isNativeCustomer = true;

//            PriceForNativeOrnonNativeDTO myChatServiceFromDB = null;
//            bool HasItFreeMessage = true;
//            if (SendeMesageType == SendetMesageType.Client)
//            {

//                //***** using dapper *****//
//                myChatServiceFromDB = await _context.BaseMyServiceTBL.Where(c => c.Id == requestfromDB.BaseServiceId)
//                              .AsNoTracking()
//                               .Select(c => new PriceForNativeOrnonNativeDTO
//                               {
//                                   BaseSeiveId = c.ServiceId,
//                                   SitePercent = c.ServiceTbl.SitePercent,
//                               }).FirstOrDefaultAsync();

//                if (clientFromDB.CountryCode.ToLower() != providerFromDB.CountryCode.ToLower())
//                    isNativeCustomer = false;

//                var cientBalance = clientFromDB.WalletBalance;
//                var providerBalance = providerFromDB.WalletBalance;


//                #region validation
//                var response = _requestService.ValidateWallet(cientBalance, requestfromDB, isNativeCustomer);
//                if (!response.succsseded)
//                    return BadRequest(new ApiBadRequestResponse(response.result));
//                #endregion  validation


//                //اگر کاربر پیام رایگان داشت
//                var HasFreeMessage = _requestService.HasFreeMessage(requestfromDB.FreeUsageMessageCount, requestfromDB.FreeMessageCount);
//                if (!HasFreeMessage)
//                {
//                    HasItFreeMessage = false;
//                    //کاهش از کیف پول کلاینت
//                    clientFromDB.WalletBalance = isNativeCustomer == true
//                                        ? (double)(clientFromDB.WalletBalance - requestfromDB.PriceForNativeCustomer  /*myChatServiceFromDB.PriceForNativeCustomer*/)
//                                        :
//                                        (double)(clientFromDB.WalletBalance - requestfromDB.PriceForNonNativeCustomer /* myChatServiceFromDB.PriceForNonNativeCustomer*/);

//                    //محاسبه درصد کالیندور
//                    var computePriceForNativeCustomer = _requestService.ComputePriceWithSitePercent(requestfromDB.PriceForNativeCustomer /*myChatServiceFromDB.PriceForNativeCustomer*/, myChatServiceFromDB.SitePercent  /*sitePercent*/);
//                    var computePriceForNonNativeCustomer = _requestService.ComputePriceWithSitePercent(requestfromDB.PriceForNonNativeCustomer /*myChatServiceFromDB.PriceForNonNativeCustomer*/, myChatServiceFromDB.SitePercent/*sitePercent*/);


//                    //افزودن به کیف پول پروایدر
//                    providerFromDB.WalletBalance = isNativeCustomer == true
//                        ? (double)(providerFromDB.WalletBalance + computePriceForNativeCustomer)
//                        :
//                        (double)(providerFromDB.WalletBalance + computePriceForNonNativeCustomer);
//                }
//                else
//                    ////افزایش یک پیام به تعداد کل پیام هایش
//                    requestfromDB.FreeUsageMessageCount++;

//                //بگیر پرووایدر را
//                recieveConnectoinId = providerFromDB.ChatNotificationId;
//            }
//            else
//            {
//                //بگیر ریکوست دهنده
//                recieveConnectoinId = clientFromDB.ChatNotificationId;
//            }

//            //uploading the file
//            string fileAddress = "";
//            if (model.IsFile || model.IsVoice)
//            {
//                try
//                {
//                    IFormFile file = model.IsFile ? model.File : model.Voice;
//                    bool isVoice = model.IsFile ? false : true;

//                    fileAddress = await _requestService.SaveFileToHost("Upload/ChatRequestFile/", null, file, isVoice);
//                    //tiketMessage.FileAddress = fileAddress;
//                }
//                catch
//                {
//                    return StatusCode(StatusCodes.Status500InternalServerError,
//                                 new ApiResponse(500, "problem uploading the file"));
//                }
//            }

//            //if (!string.IsNullOrEmpty(recieveConnectoinId))
//            //    await _chatHubContext.Clients.Client(recieveConnectoinId).SendAsync("GetChat", (model.IsFile || model.IsVoice) ? fileAddress : model.Text, DateTime.Now, _requestService.ReturnChatMessageType(model));

//            if (SendeMesageType == SendetMesageType.Client)
//            {

//                double? amount = isNativeCustomer ? requestfromDB.PriceForNativeCustomer : requestfromDB.PriceForNonNativeCustomer;
//                var clientTransaction = new TransactionTBL()
//                {
//                    Amount = amount,
//                    Username = requestfromDB.ClienUserName,
//                    ProviderUserName = requestfromDB.ProvideUserName,
//                    ClientUserName = requestfromDB.ClienUserName,
//                    CreateDate = DateTime.Now,
//                    BaseMyServiceId = requestfromDB.BaseServiceId,
//                    //ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceFree,
//                    ServiceTypeWithDetails = requestfromDB.ServiceTypes,
//                    TransactionType = TransactionType.WhiteDrawl,
//                    TransactionStatus = TransactionStatus.ServiceTransaction,
//                    TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
//                    CardTBL = null,
//                    Description = $"Withdrawal (as client of service) its free mesage provider=[${requestfromDB.ProvideUserName}] TransactionStatus=[${TransactionStatus.ServiceTransaction}]"
//                };

//                var providerTransaction = new TransactionTBL()
//                {
//                    Amount = amount,
//                    Username = requestfromDB.ProvideUserName,
//                    ProviderUserName = requestfromDB.ProvideUserName,
//                    ClientUserName = requestfromDB.ClienUserName,
//                    CreateDate = DateTime.Now,
//                    BaseMyServiceId = requestfromDB.BaseServiceId,
//                    //ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceFree,
//                    ServiceTypeWithDetails = requestfromDB.ServiceTypes,
//                    TransactionType = TransactionType.Deposit,
//                    TransactionStatus = TransactionStatus.ServiceTransaction,
//                    TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
//                    CardTBL = null,
//                    Description = $"deposit (as provider of service) its free mesage client=[${requestfromDB.ClienUserName}] TransactionStatus=[${TransactionStatus.ServiceTransaction}]"
//                };


//                if (!HasItFreeMessage)
//                {
//                    clientFromDB.WalletBalance -= isNativeCustomer ? requestfromDB.PriceForNativeCustomer : requestfromDB.PriceForNonNativeCustomer;
//                    //providerFromDB.WalletBalance += isNativeCustomer ? requestfromDB.PriceForNativeCustomer : requestfromDB.PriceForNonNativeCustomer;
//                }

//                //******using dapper*****//
//                await _context.TransactionTBL.AddAsync(providerTransaction);
//                await _context.TransactionTBL.AddAsync(clientTransaction);

//            }
//            var chatServiceMessagesTBL = new ChatServiceMessagesTBL()
//            {
//                SenderUserName = currentUsername,
//                Text = model.Text,
//                CreateDate = DateTime.Now,
//                ProviderUserName = requestfromDB.ProvideUserName,
//                ClientUserName = requestfromDB.ClienUserName,
//                ServiceRequestId = requestfromDB.Id,
//                SendetMesageType = SendeMesageType,
//                IsProviderSend = SendeMesageType != SendetMesageType.Client ? true : false,
//                Price = isNativeCustomer ? requestfromDB?.PriceForNativeCustomer /*myChatServiceFromDB?.PriceForNativeCustomer */: requestfromDB.PriceForNonNativeCustomer /*myChatServiceFromDB?.PriceForNonNativeCustomer*/,

//                ChatMessageType = _requestService.ReturnChatMessageType(model),
//                FileOrVoiceAddress = fileAddress,
//            };

//            ///زمانی که آخرین پیام ازین درخواست ثبت شده
//            //****using dapper  *******//
//            requestfromDB.LastTimeTheMessageWasSent = DateTime.Now;


//            //****using dapper  *******//
//            await _context.ChatServiceMessagesTBL.AddAsync(chatServiceMessagesTBL);
//            await _context.SaveChangesAsync();
//            int chatId = chatServiceMessagesTBL.Id;

//            //send to client with signalr
//            if (!string.IsNullOrEmpty(recieveConnectoinId))
//                await _chatHubContext.Clients.Client(recieveConnectoinId).SendAsync("GetChat", (model.IsFile || model.IsVoice) ? new { TextOrFileAddress = fileAddress, Id = chatId } : new { TextOrFileAddress = model.Text, Id = chatId }, DateTime.Now, _requestService.ReturnChatMessageType(model));
//            return Ok(_commonService.OkResponse((model.IsFile || model.IsVoice) ? new { TextOrFileAddress = fileAddress, Id = chatId } : new { TextOrFileAddress = model.Text, Id = chatId }, false));

//        }




//        #region Limited-ChatVoiceService





//        /// <summary>
//        ///گرفتن چت هایه 2 نفر که با هم داشتند در سرویس هایه لیمیتد 
//        /// </summary>
//        /// <param name="Id"></param>
//        /// <returns></returns>
//        [HttpGet("GetLimitedChatDetails")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> GetLimitedChatDetails(int requestId)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();

//            var chats = await _context.ChatForLimitedServiceMessagesTBL
//                .Where(c => c.ServiceRequestId == requestId
//                    && (c.ClientUserName == currentUsername || c.ProviderUserName == currentUsername))
//                .OrderBy(c => c.CreateDate)
//                .Select(c => new
//                {
//                    c.Id,
//                    c.CreateDate,
//                    c.Text,
//                    SendetMesageType = c.SendetMesageType,
//                    IsMyMessage = IsMyMessage(c.ServiceRequestTBL, c.IsProviderSend, currentUsername, c.ClientUserName, c.ProviderUserName),
//                    c.ChatMessageType,
//                    c.IsSeen,
//                    c.FileOrVoiceAddress,
//                })
//                .ToListAsync();

//            return Ok(_commonService.OkResponse(chats, false));
//        }





//        /// <summary>
//        /// درخواست به سرویسی که از جنس چت وویس است و فقط  از تو لیمیتد است
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("RequestToLimitedChatService")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> RequestToLimitedChatService([FromBody] RequestToChatServiceDTO model)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();
//            var baseServiceFromDB = await _context.BaseMyServiceTBL
//                                              .Where(c => c.Id == model.BaseServiceId && c.IsDeleted == false)
//                                                   .Include(c => c.MyChatsService)
//                                                   .FirstOrDefaultAsync();



//            var hasReserveRequest = await _context.ServiceRequestTBL.AnyAsync(c =>
//                                        //آیا من ریکوست در حال پندینگ یا اکسپدت از نوع  لیمیتد دارم یا نه
//                                        //اگه دارم دیگه نباید  ریکوست بزنم
//                                        ((c.ServiceRequestStatus == ServiceRequestStatus.Confirmed || c.ServiceRequestStatus == ServiceRequestStatus.Pending)
//                                             &&
//                                             c.BaseServiceId == model.BaseServiceId &&
//                                         c.ClienUserName == currentUsername &&
//                                           ////////c.ServiceType == ServiceType.ChatVoice &&
//                                           c.ServiceTypes.Contains("0") &&
//                                             (c.PackageType == PackageType.limited)
//                                        )
//                                ||
//                                (c.ServiceRequestStatus == ServiceRequestStatus.Confirmed || c.ServiceRequestStatus == ServiceRequestStatus.Pending)
//                                &&
//                            (c.HasPlan_LimitedChatVoice == true && c.ExpireTime_LimitedChatVoice < DateTime.Now /*c.IsExpired_LimitedChatVoice == false*/) &&
//                                         /*c.ServiceRequestStatus == ServiceRequestStatus.Pending &&*/
//                                         c.BaseServiceId == model.BaseServiceId &&
//                                         c.ClienUserName == currentUsername &&
//                                           //////c.ServiceType == ServiceType.ChatVoice &&
//                                           c.ServiceTypes.Contains("0") &&
//                                             (c.PackageType == PackageType.limited));

//            //validation
//            var res = await _requestService.ValidateRequestToPeriodedOrSessionChatService(baseServiceFromDB, hasReserveRequest);
//            if (!res.succsseded)
//                return BadRequest(new ApiBadRequestResponse(res.result));


//            var request = new ServiceRequestTBL()
//            {
//                //FreeMessageCount = baseServiceFromDB.MyChatsService?.FreeMessageCount,
//                //FreeUsageMessageCount = 0,

//                IsLimitedChat = true,

//                //DurationSecond_PeriodedChatVoice = baseServiceFromDB.MyChatsService?.Duration * 60,
//                HasPlan_LimitedChatVoice = false,
//                //IsExpired_LimitedChatVoice = true,
//                ExpireTime_LimitedChatVoice = DateTime.Now.AddYears(120),
//                BaseServiceId = model.BaseServiceId,
//                ////ServiceType = baseServiceFromDB.ServiceType,
//                ServiceTypes = baseServiceFromDB.ServiceTypes,
//                CreateDate = DateTime.Now,
//                WhenTheRequestShouldBeAnswered = DateTime.Now.AddHours(8),
//                ClienUserName = currentUsername,
//                ProvideUserName = baseServiceFromDB.UserName,
//                ServiceRequestStatus = ServiceRequestStatus.Pending,
//                PackageType = (PackageType)baseServiceFromDB.MyChatsService.PackageType,
//                AllMessageCount_LimitedChat = (int)baseServiceFromDB?.MyChatsService?.MessageCount,
//                UsedMessageCount_LimitedChat = 0,
//                PriceForNativeCustomer = baseServiceFromDB.MyChatsService.PriceForNativeCustomer,
//                PriceForNonNativeCustomer = baseServiceFromDB.MyChatsService.PriceForNonNativeCustomer,

//                //PackageType=baseServiceFromDB.p
//            };

//            request.ChatForLimitedServiceMessagesTBL = new List<ChatForLimitedServiceMessagesTBL>() {
//              new ChatForLimitedServiceMessagesTBL(){
//                  SenderUserName = currentUsername,
//                  ChatMessageType=ChatMessageType.Text,
//                  IsProviderSend=false,
//                  SendetMesageType=SendetMesageType.Client,

//                  ClientUserName = currentUsername,
//                  CreateDate=DateTime.Now,
//                  ProviderUserName=baseServiceFromDB.UserName,
//                  Text=model.FirstMessage,
//                  IsSeen=false,
//              }
//            };

//            #region send notification
//            var persianConfirmMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceConfimNotificationKeyName).SingleOrDefault()?.Value;
//            var englishConfirmMessage = _context.SettingsTBL.Where(c => c.Key == PublicHelper.ServiceConfimNotificationKeyName).SingleOrDefault()?.EnglishValue;

//            var userFromDB = await _context.Users.Where(c => c.UserName == baseServiceFromDB.UserName)
//                .Select(c => new { c.ConnectionId, c.UserName })
//                .FirstOrDefaultAsync();
//            bool isPersian = _commonService.IsPersianLanguage();
//            string confirmMessage = isPersian ? persianConfirmMessage : englishConfirmMessage;

//            if (!string.IsNullOrEmpty(userFromDB?.ConnectionId))
//                await _hubContext.Clients.Client(userFromDB?.ConnectionId).SendAsync("Notifis", confirmMessage);
//            #endregion


//            await _context.ServiceRequestTBL.AddAsync(request);
//            await _context.SaveChangesAsync();
//            return Ok(_commonService.OkResponse(null, false));
//        }





//        //-------------------------------------------------------------------
//        /// <summary>
//        /// آیا پلن دارد یا اکسپابر شده  یا تعداد پیام هایه مجازش تموم شده یا نه
//        /// </summary>
//        /// <param name="BaseServiceId"></param>
//        /// <returns></returns>
//        [HttpGet("HasLimitedPlanForServcie")]
//        //[Authorize]
//        [ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> HasLimitedPlanForServcie(int requestId)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();
//            var requestFromDB = await _context.ServiceRequestTBL
//                        .Where(c => c.Id == requestId)
//                         .FirstOrDefaultAsync();

//            if (requestFromDB == null)
//            {
//                return BadRequest(_commonService.NotFoundErrorReponse(false));
//            }

//            if (currentUsername != requestFromDB.ClienUserName && currentUsername != requestFromDB.ProvideUserName)
//            {
//                return BadRequest(_commonService.NotFoundErrorReponse(false));
//            }

//            var allResponse = true;
//            if (currentUsername == requestFromDB.ClienUserName)
//            {
//                if (requestFromDB.HasPlan_LimitedChatVoice && requestFromDB.ExpireTime_LimitedChatVoice > DateTime.Now &&
//                                   requestFromDB.UsedMessageCount_LimitedChat < requestFromDB.AllMessageCount_LimitedChat
//                    )
//                {
//                    var response = new
//                    {
//                        isExpired = requestFromDB.ExpireTime_LimitedChatVoice > DateTime.Now /* requestFromDB.IsExpired_LimitedChatVoice*/,
//                        hasPlan = requestFromDB.HasPlan_LimitedChatVoice,
//                        isProvider = currentUsername == requestFromDB.ProvideUserName,
//                        remaingChat = requestFromDB.AllMessageCount_LimitedChat - requestFromDB.UsedMessageCount_LimitedChat,
//                        result = true,
//                    };
//                    allResponse = true;
//                    return Ok(_commonService.OkResponse(response, false));
//                }

//                var res = new
//                {
//                    isExpired = requestFromDB.ExpireTime_LimitedChatVoice > DateTime.Now,
//                    hasPlan = requestFromDB.HasPlan_LimitedChatVoice,
//                    isProvider = currentUsername == requestFromDB.ProvideUserName,
//                    remaingChat = requestFromDB.AllMessageCount_LimitedChat - requestFromDB.UsedMessageCount_LimitedChat,
//                    result = false,
//                };

//                allResponse = false;
//                return Ok(_commonService.OkResponse(res, false));
//            }

//            else
//            {
//                var response = new
//                {
//                    isExpired = requestFromDB.ExpireTime_LimitedChatVoice > DateTime.Now,
//                    hasPlan = requestFromDB.HasPlan_LimitedChatVoice,
//                    isProvider = currentUsername == requestFromDB.ProvideUserName,
//                    remaingChat = requestFromDB.AllMessageCount_LimitedChat - requestFromDB.UsedMessageCount_LimitedChat,
//                    result = allResponse,
//                };
//                return Ok(_commonService.OkResponse(response, false));
//            }

//        }






//        ///// <summary>
//        /////زمان باقی مانده و زمان کل
//        ///// </summary>
//        ///// <param name="model"></param>
//        ///// <returns></returns>
//        //[HttpPost("GetTimeDetailsForPeriodedOrSessionChatService")]
//        ////[Authorize]
//        //[ClaimsAuthorize(IsAdmin = false)]
//        //public async Task<ActionResult> GetTimeDetailsForPeriodedOrSessionChatService(int requestId)
//        //{
//        //    //IsPerodedOrsesionChat - HasPlan_PeriodedChatVoice - 
//        //    //IsExpired - DurationSecond_PeriodedChatVoice - 
//        //    //PeriodedChatVoice - ReaminingTime_PeriodedChatVoice

//        //    var requestFromDB = await _context.ServiceRequestTBL.AsNoTracking()
//        //        .Where(c => c.Id == requestId && c.IsLimitedChat)
//        //           .Select(c => new
//        //           {
//        //               IsExpired = c.IsLimitedChat,
//        //               HasPlan = c.HasPlan_LimitedChatVoice,
//        //               DurationSecond = c.DurationSecond_PeriodedChatVoice,
//        //               ReaminingTime = c.ReaminingTime_PeriodedChatVoice,
//        //           })
//        //           .FirstOrDefaultAsync();
//        //    return Ok(requestFromDB);

//        //}









//        ///// <summary>
//        /////زمانی که کلاینت به سیگنال ار وصل شد و باید کش هارا پر کند
//        ///// </summary>
//        ///// <param name="model"></param>
//        ///// <returns></returns>
//        //[HttpPost("MyFakeOnConnectedAsyncForPerodedOrSessionService")]
//        ////[Authorize]
//        //[ClaimsAuthorize(IsAdmin = false)]
//        //public async Task<ActionResult> MyFakeOnConnectedAsyncForPerodedOrSessionService(int requestId)
//        //{

//        //    var curetnUserName = _accountService.GetCurrentUserName();

//        //    //var connectionId = Context.ConnectionId;
//        //    //var currentUserName = Context.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

//        //    //var currentRequestId = await _context.Users.Where(c => c.ChatNotificationId == connectionId)
//        //    //                                     .Select(c => c.CurentRequestId).FirstOrDefaultAsync();


//        //    var requestFromDB = await _context.ServiceRequestTBL
//        //                        .Where(c => c.Id == requestId && c.ServiceRequestStatus == ServiceRequestStatus.Confirmed)
//        //                        .FirstOrDefaultAsync();


//        //    if (curetnUserName.ToLower() != requestFromDB.ClienUserName && curetnUserName.ToLower() != requestFromDB.ProvideUserName)
//        //    {
//        //        List<string> erros = new List<string> { _localizerShared["UnauthorizedMessage"].Value.ToString() };
//        //        return BadRequest(new ApiBadRequestResponse(erros));
//        //    }


//        //    if (requestFromDB.ClienUserName == curetnUserName)
//        //    {
//        //        if (requestFromDB == null)
//        //        {
//        //            List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//        //            return BadRequest(new ApiBadRequestResponse(erros));
//        //        }

//        //        if (requestFromDB.IsPerodedOrsesionChat == false)
//        //        {
//        //            List<string> erros = new List<string> { _localizerShared["InValidServiceType"].Value.ToString() };
//        //            return BadRequest(new ApiBadRequestResponse(erros));
//        //        }


//        //        //RequestStatusForRedis requestStatusForRedis = RequestStatusForRedis.OkPlan;
//        //        //if (requestFromDB == null)
//        //        //{
//        //        //    List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
//        //        //    return BadRequest(new ApiBadRequestResponse(erros));

//        //        //    //requestStatusForRedis = RequestStatusForRedis.NofFoundRequestTBL;
//        //        //}


//        //        if (requestFromDB.IsExpired_PeriodedChatVoice == true || requestFromDB.HasPlan_PeriodedChatVoice == false)
//        //        {
//        //            List<string> erros = new List<string> { _localizerShared["YourPackageExpiredOrNoPlan"].Value.ToString() };
//        //            return BadRequest(new ApiBadRequestResponse(erros));
//        //            //requestStatusForRedis = RequestStatusForRedis.BadPlan;
//        //        }

//        //        //requestFromDB.EndTime_PeriodedChatVoice;
//        //        requestFromDB.StartTime_PeriodedChatVoice = DateTime.Now;
//        //        requestFromDB.EndTime_PeriodedChatVoice = DateTime.Now.AddSeconds((int)requestFromDB.ReaminingTime_PeriodedChatVoice);


//        //        await _context.SaveChangesAsync();



//        //        //check kon zam baghi mande da redis ra
//        //        //var redisKey = PublicHelper.Redis_ChatVoiceDurationKeyName + requestId;
//        //        //var chatVoiceDuration = new RedisValueForDurationChatVoice()
//        //        //{
//        //        //    StartTime = DateTime.Now,
//        //        //    EndTime = DateTime.Now.AddSeconds((int)requestFromDB.ReaminingTime_PeriodedChatVoice),
//        //        //    RequestStatusForRedis = requestStatusForRedis,
//        //        //    Chats = new List<ChatsVM>()
//        //        //};
//        //        //_cache.Set(redisKey, chatVoiceDuration);


//        //        return Ok(_commonService.OkResponse(new { isClient = true }, false));
//        //    }

//        //    return Ok(_commonService.OkResponse(new { isClient = false }, false));

//        //}








//        /// <summary>
//        ///  چت هایه از نوع چت وویس هایه زمانی
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("SendChatToLimitedChatService")]
//        [Authorize]
//        //[ClaimsAuthorize(IsAdmin = false)]
//        public async Task<ActionResult> SendChatToLimitedChatService([FromForm] SendChatToChatServiceDTO model)
//        {
//            var currentUsername = _accountService.GetCurrentUserName();

//            if ((!model.IsFile && !model.IsVoice) && string.IsNullOrEmpty(model.Text))
//            {
//                return BadRequest(_resourceServices.GetErrorMessageByKey("TextIsRequired"));
//            }

//            ServiceRequestTBL requestfromDB = await _context.ServiceRequestTBL.FindAsync(model.ServiceRequestId);

//            #region validate request
//            var res = _requestService.ValidateSendChatToLimitedChatService(requestfromDB);
//            if (!res.succsseded)
//            {
//                return BadRequest(_resourceServices.GetErrorMessageByKey("TextIsRequired"));
//            }

//            #endregion

//            #region file validation

//            //validate the file an voice
//            var resFile = _requestService.ValidateSendChatToChatService(model);
//            if (!resFile.succsseded)
//            {
//                return BadRequest(new ApiBadRequestResponse(resFile.result));
//            }

//            #endregion  file validation

//            var SendeMesageType = SendetMesageType.Client;
//            SendeMesageType = requestfromDB.ProvideUserName == currentUsername ? SendetMesageType.Provider : SendetMesageType.Client;

//            var clienUserName = requestfromDB.ClienUserName;
//            var providerUserName = requestfromDB.ProvideUserName;

//            //********************************* from mainDb ****************************************
//            List<AppUser> userFromDB = await _context.Users
//                .Where(c => c.UserName == clienUserName || c.UserName == providerUserName)
//                .ToListAsync();

//            AppUser clientFromDB = userFromDB.Where(c => c.UserName == clienUserName).FirstOrDefault();
//            AppUser providerFromDB = userFromDB.Where(c => c.UserName == providerUserName).FirstOrDefault();

//            string recieveConnectoinId;
//            if (SendeMesageType == SendetMesageType.Client)
//                //بگیر پرووایدر را
//                recieveConnectoinId = providerFromDB.ChatNotificationId;
//            else
//                //بگیر ریکوست دهنده
//                recieveConnectoinId = clientFromDB.ChatNotificationId;

//            #region  uploading file
//            //uploading the file
//            string fileAddress = "";
//            if (model.IsFile || model.IsVoice)
//            {
//                try
//                {
//                    IFormFile file = model.IsFile ? model.File : model.Voice;
//                    bool isVoice = model.IsFile ? false : true;

//                    fileAddress = await _requestService.SaveFileToHost("Upload/ChatRequestFile/", null, file, isVoice);
//                    //tiketMessage.FileAddress = fileAddress;
//                }
//                catch
//                {
//                    //throw new HubException(UnauthorizedAccessException,)
//                    var errors = new List<string>() { "problem uploading the file" };
//                    return new JsonResult(new { Status = false, Errors = errors });
//                    //return StatusCode(StatusCodes.Status500InternalServerError,
//                    //             new ApiResponse(500, "problem uploading the file"));
//                }
//            }

//            #endregion



//            //اگر آخرین پیامشه  باید "هز پلنش" فالز بشه 
//            if (SendeMesageType == SendetMesageType.Client && (requestfromDB.AllMessageCount_LimitedChat == 1 + requestfromDB.UsedMessageCount_LimitedChat))
//                requestfromDB.HasPlan_LimitedChatVoice = false;

//            var ChatForLimitedServiceMessagesTBL = new ChatForLimitedServiceMessagesTBL()
//            {
//                SenderUserName = currentUsername,
//                Text = model.Text,
//                CreateDate = DateTime.Now,
//                ProviderUserName = requestfromDB.ProvideUserName,
//                ClientUserName = requestfromDB.ClienUserName,
//                ServiceRequestId = requestfromDB.Id,
//                SendetMesageType = SendeMesageType,
//                IsProviderSend = SendeMesageType != SendetMesageType.Client ? true : false,
//                //Price = isNativeCustomer ? myChatServiceFromDB?.PriceForNativeCustomer : myChatServiceFromDB?.PriceForNonNativeCustomer,

//                ChatMessageType = _requestService.ReturnChatMessageType(model),
//                FileOrVoiceAddress = fileAddress,
//            };

//            if (SendeMesageType == SendetMesageType.Client)
//                requestfromDB.UsedMessageCount_LimitedChat++;
//            ///زمانی که آخرین پیام ازین درخواست ثبت شده
//            requestfromDB.LastTimeTheMessageWasSent = DateTime.Now;


//            await _context.ChatForLimitedServiceMessagesTBL.AddAsync(ChatForLimitedServiceMessagesTBL);
//            await _context.SaveChangesAsync();


//            int chatId = ChatForLimitedServiceMessagesTBL.Id;

//            //send to client with signalr
//            if (!string.IsNullOrEmpty(recieveConnectoinId))
//                await _chatHubContext.Clients.Client(recieveConnectoinId).SendAsync("GetLimitedChatVoice", (model.IsFile || model.IsVoice) ? new { TextOrFileAddress = fileAddress, Id = chatId } : new { TextOrFileAddress = model.Text, Id = chatId }, DateTime.Now, _requestService.ReturnChatMessageType(model));

//            return Ok(_commonService.OkResponse((model.IsFile || model.IsVoice) ? new { TextOrFileAddress = fileAddress, Id = chatId } : new { TextOrFileAddress = model.Text, Id = chatId }, false));



//        }





//        ///// <summary>
//        /////  چت هایه از توع چت وویس هاه زمانی
//        ///// </summary>
//        ///// <param name="model"></param>
//        ///// <returns></returns>
//        //[HttpPost("SendChatToDurationChatService")]
//        //[Authorize]
//        ////[ClaimsAuthorize(IsAdmin = false)]
//        //public async Task<ActionResult> SendChatToDurationChatService([FromForm] SendChatToChatServiceDTO model)
//        //{
//        //    var currentUsername = _accountService.GetCurrentUserName();

//        //    if ((!model.IsFile && !model.IsVoice) && string.IsNullOrEmpty(model.Text))
//        //    {
//        //        List<string> erros = new List<string> { _localizerShared["TextIsRequired"].Value.ToString() };
//        //        return NotFound(new ApiBadRequestResponse(erros));
//        //    }


//        //    ServiceRequestTBL requestfromDB = await _context.ServiceRequestTBL
//        //                                                        .FindAsync(model.ServiceRequestId);

//        //    #region validate request
//        //    var res = _requestService.ValidateSendChatVoiceDuration(requestfromDB);
//        //    if (!res.succsseded)
//        //        return BadRequest(new ApiBadRequestResponse(res.result));
//        //    #endregion 
//        //    //check kon zam baghi mande da redis ra
//        //    var redisKey = PublicHelper.Redis_ChatVoiceDurationKeyName + model.ServiceRequestId;
//        //    var chatVoiceValueFromRedis = _cache.Get<RedisValueForDurationChatVoice>(PublicHelper.Redis_ChatVoiceDurationKeyName);
//        //    #region redis validation

//        //    var resValidate = _requestService.ValidateRedisSendChatVoiceDuration(chatVoiceValueFromRedis);
//        //    if (!resValidate.succsseded)
//        //        return BadRequest(new ApiBadRequestResponse(resValidate.result));

//        //    #endregion  redis validation
//        //    #region file validation

//        //    //validate the file an voice
//        //    var resFile = _requestService.ValidateSendChatToChatService(model);
//        //    if (!resFile.succsseded)
//        //        return BadRequest(new ApiBadRequestResponse(resFile.result));

//        //    #endregion  file validation

//        //    var SendeMesageType = SendetMesageType.Client;
//        //    SendeMesageType = requestfromDB.ProvideUserName == currentUsername ? SendetMesageType.Provider : SendetMesageType.Client;

//        //    var clienUserName = requestfromDB.ClienUserName;
//        //    var providerUserName = requestfromDB.ProvideUserName;

//        //    //*********************************   from mainDb ****************************************
//        //    List<AppUser> userFromDB = await _context.Users
//        //        .Where(c => c.UserName == clienUserName || c.UserName == providerUserName)
//        //        //.Select(c => new { c.CountryCode, c.WalletBalance, c.UserName })
//        //        .ToListAsync();

//        //    AppUser clientFromDB = userFromDB.Where(c => c.UserName == clienUserName).FirstOrDefault();
//        //    AppUser providerFromDB = userFromDB.Where(c => c.UserName == providerUserName).FirstOrDefault();

//        //    string recieveConnectoinId;
//        //    if (SendeMesageType == SendetMesageType.Client)
//        //        //بگیر پرووایدر را
//        //        recieveConnectoinId = await _context.Users.Where(c => c.UserName.ToLower() == requestfromDB.ProvideUserName).Select(c => c.ChatNotificationId).FirstOrDefaultAsync();
//        //    else
//        //        //بگیر ریکوست دهنده
//        //        recieveConnectoinId = await _context.Users.Where(c => c.UserName.ToLower() == requestfromDB.ClienUserName).Select(c => c.ChatNotificationId).FirstOrDefaultAsync();

//        //    #region  uploading file
//        //    //uploading the file
//        //    string fileAddress = "";
//        //    if (model.IsFile || model.IsVoice)
//        //    {
//        //        try
//        //        {
//        //            IFormFile file = model.IsFile ? model.File : model.Voice;
//        //            bool isVoice = model.IsFile ? false : true;

//        //            fileAddress = await _requestService.SaveFileToHost("Upload/ChatRequestFile/", null, file, isVoice);
//        //            //tiketMessage.FileAddress = fileAddress;
//        //        }
//        //        catch
//        //        {
//        //            return StatusCode(StatusCodes.Status500InternalServerError,
//        //                         new ApiResponse(500, "problem uploading the file"));
//        //        }
//        //    }

//        //    #endregion

//        //    //send to client with signalr
//        //    if (!string.IsNullOrEmpty(recieveConnectoinId))
//        //        await _chatHubContext.Clients.Client(recieveConnectoinId).SendAsync("GetDurationChatVoiceChat", (model.IsFile || model.IsVoice) ? fileAddress : model.Text, DateTime.Now, _requestService.ReturnChatMessageType(model));

//        //    //be jaye in bere to redis

//        //    var chatServiceMessagesTBL = new ChatsVM()
//        //    {
//        //        Text = model.Text,
//        //        CreateDate = DateTime.Now,
//        //        ProviderUserName = requestfromDB.ProvideUserName,
//        //        ClientUserName = requestfromDB.ClienUserName,
//        //        ServiceRequestId = requestfromDB.Id,
//        //        SendetMesageType = SendeMesageType,
//        //        IsProviderSend = SendeMesageType != SendetMesageType.Client ? true : false,
//        //        //Price = isNativeCustomer ? myChatServiceFromDB?.PriceForNativeCustomer : myChatServiceFromDB?.PriceForNonNativeCustomer,
//        //        ChatMessageType = _requestService.ReturnChatMessageType(model),
//        //        FileOrVoiceAddress = fileAddress,
//        //    };

//        //    chatVoiceValueFromRedis.Chats.Add(chatServiceMessagesTBL);
//        //    _cache.Set(redisKey, chatVoiceValueFromRedis);

//        //    try
//        //    {
//        //        //await _context.ChatServiceMessagesTBL.AddAsync(chatServiceMessagesTBL);
//        //        await _context.SaveChangesAsync();

//        //        return Ok(_commonService.OkResponse((model.IsFile || model.IsVoice) ? fileAddress : model.Text, false));
//        //    }
//        //    catch
//        //    {
//        //        List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
//        //        return StatusCode(StatusCodes.Status500InternalServerError,
//        //           new ApiBadRequestResponse(erros, 500));
//        //    }
//        //}




//        #endregion






//    }
//}
