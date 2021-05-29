using CallInDoor.Config.Attributes;
using CallInDoor.Hubs;
using Domain;
using Domain.DTO.Notification;
using Domain.DTO.Response;
using Domain.DTO.UserWithdrawlRequest;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Notification;
using Service.Interfaces.Resource;
using Service.Interfaces.UserWithdrawlRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UserWithdrawlRequestController : BaseControlle
    {


        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly IUserWithdrawlRequestService _userWithdrawlRequestService;
        private readonly INotificationService _notificationService;



        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;

        public UserWithdrawlRequestController(DataContext context, IAccountService accountService,
            ICommonService commonService, IUserWithdrawlRequestService userWithdrawlRequestService,
            INotificationService notificationService,
                                IStringLocalizer<ShareResource> localizerShared, IResourceServices resourceServices)
        {
            _context = context;
            _accountService = accountService;
            _commonService = commonService;
            _userWithdrawlRequestService = userWithdrawlRequestService;
            _notificationService = notificationService;
            _localizerShared = localizerShared;
            _resourceServices = resourceServices;
        }





        #region Admin

        /// <summary>
        /// 
        ///تمام درخئاس هایه برداشت
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetAllRequestForAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllRequestForAdmin(GetAllRequestForAdminDTO model)
        {

            var QueryAble = _context.UserWithdrawlRequestTBL
                          .AsNoTracking()
                          .AsQueryable();



            if (!string.IsNullOrEmpty(model.SearchedWord))
            {
                QueryAble = QueryAble
                    .Where(c =>
                    c.UserName.ToLower().StartsWith(model.SearchedWord.ToLower()) ||
                    c.UserName.ToLower().Contains(model.SearchedWord.ToLower())
                    ||
                      c.CardTBL.CardName.ToLower().StartsWith(model.SearchedWord.ToLower()) ||
                      c.CardTBL.CardNumber.ToLower().Contains(model.SearchedWord.ToLower())
                    );
            };


            if (model.DateTime != null)
            {
                QueryAble = QueryAble.Where(c => c.CreateDate > model.DateTime);
            }


            if (model.WithdrawlRequestStatus != null)
            {
                QueryAble = QueryAble.Where(c => c.WithdrawlRequestStatus == model.WithdrawlRequestStatus);
            }




            if (model.PerPage == 0)
                model.PerPage = 1;
            model.Page = model.Page ?? 0;
            model.PerPage = model.PerPage ?? 10;

            var count = QueryAble.Count();
            double len = (double)count / (double)model.PerPage;
            var totalPages = Math.Ceiling((double)len);

            var items = await QueryAble
                .AsNoTracking()
              .OrderByDescending(c => c.CreateDate)
              .Skip((int)model.Page * (int)model.PerPage)
             .Take((int)model.PerPage)
             .Select(c => new
             {
                 c.Id,
                 c.Amount,
                 c.UserName,
                 CreateDate = c.CreateDate.ToString("MM/dd/yyyy h:mm tt"),
                 c.ResonOfReject,
                 c.WithdrawlRequestStatus,

                 c.CardTBL.CardName,
                 c.CardTBL.CardNumber,
             })
             .ToListAsync();


            var data = new
            {
                items = items,
                TotalPages = totalPages
            };


            return Ok(_commonService.OkResponse(data, false));
















            //var data = new ServiceProviderResponseTypeDTO()
            //{
            //    ProvidesdService = ProvidedServices,
            //    TotalPages = totalPages
            //};
            //return data;

            //var allRequests = await QueryAble.AsNoTracking()
            //    .Select(c => new
            //    {

            //        c.Id,
            //        c.Amount,
            //        c.UserName,
            //        c.CreateDate,
            //        c.ResonOfReject,
            //        c.WithdrawlRequestStatus,

            //        c.CardTBL.CardName,
            //        c.CardTBL.CardNumber,
            //    })
            //    .OrderBy(c => c.CreateDate)
            //    .ToListAsync();

            //return Ok(_commonService.OkResponse(allRequests, false));

        }






        [HttpGet("AcceptRequestInAdmin")]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> AcceptRequestInAdmin(int requestId)
        {
            var requestFromDB = await _context.UserWithdrawlRequestTBL
                .Where(c => c.Id == requestId)
                 .FirstOrDefaultAsync();

            if (requestFromDB == null)
            {

                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
                ////////return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }
            if (requestFromDB.WithdrawlRequestStatus != WithdrawlRequestStatus.Pending)
            {
                List<string> erros = new List<string> { "this request is not in  pending mode " };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            requestFromDB.WithdrawlRequestStatus = WithdrawlRequestStatus.Confirmed;
            requestFromDB.RejectOrConfirmTime = DateTime.Now;


            var userFromDB = await _context.Users.Where(c => c.UserName == requestFromDB.UserName).FirstOrDefaultAsync();

            if (userFromDB.WalletBalance < requestFromDB.Amount)
            {
                List<string> erros = new List<string> { "Inventory is not enough " };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            var currentUserCultureName = userFromDB.CultureName;
            currentUserCultureName = string.IsNullOrEmpty(currentUserCultureName) ? "en-US" : currentUserCultureName;

            //#TODO
            var notification = new NotificationTBL()
            {
                CreateDate = DateTime.Now,
                EnglishText = _resourceServices.GetErrorMessageByKeyAndHeader("ConfirmWithdrawalMessage", PublicHelper.EngCultureName),
                TextPersian = _resourceServices.GetErrorMessageByKeyAndHeader("ConfirmWithdrawalMessage", PublicHelper.persianCultureName),
                IsReaded = false,
                NotificationStatus = NotificationStatus.AcceptWidthrawlRequest,
                SenderUserName = requestFromDB.UserName,
                UserName = requestFromDB.UserName,
            };

            await _context.NotificationTBL.AddAsync(notification);

            userFromDB.WalletBalance -= requestFromDB.Amount;


            var transaction = new TransactionTBL()
            {
                Amount = requestFromDB.Amount,
                Username = requestFromDB.UserName,
                CardId = requestFromDB.CardItId,
                CreateDate = DateTime.Now,
                Description = $"واریز به کیف پول {requestFromDB.CardItId}  در تاریخ{requestFromDB.CreateDate} به مبلغ {requestFromDB.Amount}  یه کاربر {requestFromDB.UserName} واریز میشه توسط ادمین",
                TransactionType = TransactionType.WhiteDrawl,
                TransactionStatus = TransactionStatus.NormalTransaction,
                TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed
            };

            await _context.TransactionTBL.AddAsync(transaction);

            var notifVM = new SendNotificationVM()
            {
                Text = _resourceServices.GetErrorMessageByKeyAndHeader("ConfirmWithdrawalMessage", currentUserCultureName),
                UserName = requestFromDB.UserName,
            };

            await _notificationService.SendAcceptWidrwalRequestNotifications(notifVM);


            //ارسال اس ام اس
            //await _smsService.ConfirmServiceByAdmin(serviceFromDB.ServiceName, serviceFromDB.UserName);

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));

        }




        /// <summary>
        ///reject  
        /// </summary>
        /// <returns></returns>
        [HttpPost("RejectUserWithdrawlRequestInAdmin")]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> RejectUserWithdrawlRequestInAdmin([FromBody] RejectUserWithdrawlRequestInAdminDTO model)
        {
            var requestFromDB = await _context.UserWithdrawlRequestTBL
                .Where(c => c.Id == model.RequestId)
                 .FirstOrDefaultAsync();

            if (requestFromDB == null)
            {

                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));
                ////////return NotFound(new ApiResponse(404, PubicMessages.NotFoundMessage));
            }
            if (requestFromDB.WithdrawlRequestStatus != WithdrawlRequestStatus.Pending)
            {
                List<string> erros = new List<string> { "this request is not in  pending mode " };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            requestFromDB.WithdrawlRequestStatus = WithdrawlRequestStatus.Rejected;
            requestFromDB.RejectOrConfirmTime = DateTime.Now;
            requestFromDB.ResonOfReject = model.RejectReason;




            var userFromDB = await _context.Users.Where(c => c.UserName == requestFromDB.UserName).FirstOrDefaultAsync();

            //if (userFromDB.WalletBalance < requestFromDB.Amount)
            //{
            //    List<string> erros = new List<string> { "Inventory is not enough" };
            //    return BadRequest(new ApiBadRequestResponse(erros));
            //}

            var currentUserCultureName = userFromDB.CultureName;
            currentUserCultureName = string.IsNullOrEmpty(currentUserCultureName) ? "en-US" : currentUserCultureName;

            //#TODO
            var notification = new NotificationTBL()
            {
                CreateDate = DateTime.Now,
                EnglishText = _resourceServices.GetErrorMessageByKeyAndHeader("RejectWithdrawalMessage", PublicHelper.EngCultureName),
                TextPersian = _resourceServices.GetErrorMessageByKeyAndHeader("RejectWithdrawalMessage", PublicHelper.persianCultureName),
                IsReaded = false,
                NotificationStatus = NotificationStatus.RejectWidthrawlRequest,
                SenderUserName = requestFromDB.UserName,
                UserName = requestFromDB.UserName,
            };

            _context.NotificationTBL.Add(notification);


            var notifVM = new SendNotificationVM()
            {
                Text = _resourceServices.GetErrorMessageByKeyAndHeader("RejectWithdrawalMessage", currentUserCultureName),
                UserName = requestFromDB.UserName,
            };

            await _notificationService.SendRejectWidrwalRequestNotifications(notifVM);


            //ارسال اس ام اس
            //await _smsService.ConfirmServiceByAdmin(serviceFromDB.ServiceName, serviceFromDB.UserName);

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));


        }


        #endregion





        #region  User side



        /// <summary>
        /// درخواست به سرویسی که از جنس چت ووییس است و فقط free(است)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddWithdrawlRequest")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddWithdrawlRequest([FromBody] AddWithdrawlRequestDTO2 model)
        {
            var currentUsername = _accountService.GetCurrentUserName();


            var userFromDB = await _context.Users.Where(c => c.UserName == currentUsername).Select(c => new { c.UserName, c.WalletBalance }).FirstOrDefaultAsync();
            if (string.IsNullOrEmpty(userFromDB.UserName))
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }


            if (userFromDB.WalletBalance < model.Amount)
            {
                var err = new List<string>();
                err.Add(_resourceServices.GetErrorMessageByKey("InvaliAmountForTransaction"));
                return BadRequest(new ApiBadRequestResponse(err));
            }

            var havePendingRequest = await _context.UserWithdrawlRequestTBL
                            .AnyAsync(c => c.UserName == currentUsername && c.WithdrawlRequestStatus == WithdrawlRequestStatus.Pending);

            if (havePendingRequest)
            {
                var err = new List<string>();
                err.Add(_resourceServices.GetErrorMessageByKey("YouHavePendingRequest"));
                return BadRequest(new ApiBadRequestResponse(err));
            }


            var userWithdrawlRequestTBL = new UserWithdrawlRequestTBL()
            {
                CardItId = model.CardId,
                CreateDate = DateTime.Now,
                UserName = currentUsername,
                Amount = (double)model.Amount,
                WithdrawlRequestStatus = WithdrawlRequestStatus.Pending
            };

            await _context.UserWithdrawlRequestTBL.AddAsync(userWithdrawlRequestTBL);
            await _context.SaveChangesAsync();

            return Ok(_commonService.OkResponse(null, PubicMessages.SuccessMessage));
        }





        #endregion





    }
}
