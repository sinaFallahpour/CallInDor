using CallInDoor.Config.Attributes;
using CallInDoor.Hubs;
using Domain;
using Domain.DTO.Account;
using Domain.DTO.Report;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Discount;
using Service.Interfaces.RequestService;
using Service.Interfaces.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ReportController : BaseControlle
    {


        #region ctor


        //private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IHubContext<NotificationHub> _hubContext;


        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        //private readonly ITransactionService _transactionService;
        private readonly ICommonService _commonService;
        private readonly IRequestService _requestService;
        private readonly IDiscountService _discountService;
        //private IStringLocalizer<RequestV2Controller> _localizer;
        //private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;


        public ReportController(
            //IHubContext<ChatHub> chatHubContext,
            IHubContext<NotificationHub> hubContext,
            DataContext context,
                   IAccountService accountService,
                   //ITransactionService transactionService,
                   ICommonService commonService,
                   IRequestService requestService,
                   IDiscountService discountService,
                //IStringLocalizer<RequestV2Controller> localizer,
                //IStringLocalizer<ShareResource> localizerShared,
                IResourceServices resourceServices
            )
        {

            _hubContext = hubContext;
            _context = context;
            _accountService = accountService;
            _commonService = commonService;
            _requestService = requestService;
            _discountService = discountService;
            //_localizerShared = localizerShared;
            _resourceServices = resourceServices;
        }

        #endregion ctor




        /// <summary>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllReports")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllReports(string requestId)
        {
            var currentUsername = _accountService.GetCurrentUserName();

            var reports = await _context.ReportTBL.AsNoTracking().Select(c => new
            {
                c.Id,
                c.Text,
                c.CreateDate,
                c.UserName,
                //c.BaseRequestServiceTBL.BaseMyServiceTBL.ServiceName
                ProviderUsername = c.BaseRequestServiceTBL.ProvideUserName,
                requestId
            })
                .Where(c => c.requestId == requestId)
                .ToListAsync();
            return Ok(_commonService.OkResponse(reports, false));
        }




        /// <summary>
        /// ******************* changed to new Api
        /// ریپورت کردن یک درخواست
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPost("ReportRequest")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> ReportRequest([FromBody] ReportRequestDTO model)
        {
            var currentUserName = _accountService.GetCurrentUserName();
            var requestFromDB = await _context.BaseRequestServiceTBL
                                 .Where(c => c.Id == model.RequestId && c.ClienUserName == currentUserName)
                                      .FirstOrDefaultAsync();

            #region validation
            if (requestFromDB == null)
            {
                return BadRequest(_commonService.NotFoundErrorReponse(false));
            }
            if (requestFromDB.ClienUserName != currentUserName)
            {
                List<string> erros = new List<string> {
                    _resourceServices.GetErrorMessageByKey("InvalidAttamp")
                };
                return BadRequest(new ApiBadRequestResponse(erros, 400));
            }

            if (requestFromDB.ServiceRequestStatus != ServiceRequestStatus.Confirmed)
            {
                List<string> erros = new List<string> {
                    _resourceServices.GetErrorMessageByKey("InvalidAttamp")
                };
                return BadRequest(new ApiBadRequestResponse(erros, 400));
            }
            #endregion

            var hasReportBefore = await _context.ReportTBL.AnyAsync(c => c.UserName == currentUserName && c.BaseRequestServiceId == model.RequestId);

            if (hasReportBefore)
            {
                List<string> erros = new List<string> {
                    _resourceServices.GetErrorMessageByKey("YouReportThisRequestBefore")
                };
                return BadRequest(new ApiBadRequestResponse(erros, 400));
            }

            var report = new ReportTBL()
            {
                UserName = currentUserName,
                CreateDate = DateTime.Now,
                Text = model.Text,
                BaseRequestServiceTBL = requestFromDB,
            };

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));
        }





    }
}
