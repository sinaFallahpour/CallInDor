using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Response;
using Domain.DTO.Tiket;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Resource;
using Service.Interfaces.Ticket;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class TiketController : BaseControlle
    {

        #region ctor

        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly ITicketService _ticketService;



        //private IStringLocalizer<TiketController> _localizer;
        //private IStringLocalizer<ShareResource> _localizerShared;

        private readonly IResourceServices _resourceServices;

        public TiketController(
        UserManager<AppUser> userManager,
            DataContext context,
                   IAccountService accountService,
                   ICommonService commonService,
                   ITicketService ticketService,
                //IStringLocalizer<TiketController> localizer,
                // IStringLocalizer<ShareResource> localizerShared,
                IResourceServices resourceServices
            )
        {
            _context = context;
            _userManager = userManager;
            _accountService = accountService;
            _commonService = commonService;
            _ticketService = ticketService;
            //_localizer = localizer;
            //_localizerShared = localizerShared;
            _resourceServices = resourceServices;
        }

        #endregion ctor



        /// <summary>
        ///گرفتن لیست تیکت ها
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllTiketsInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllTiketsInAdmin(int? Page, int? PerPage,
            string searchedWord, DateTime? CreateDate, TiketStatus? TiketStatus, PriorityStatus? priorityStatus)
        {


            var QueryAble = _context.TiketTBL
                              .AsNoTracking()
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchedWord))
            {
                QueryAble = QueryAble.Where(c =>
                           c.Title.ToLower().StartsWith(searchedWord.ToLower())
                           || c.Title.ToLower().Contains(searchedWord.ToLower()));
            }

            if (CreateDate != null)
            {
                QueryAble = QueryAble
                  .Where(c => c.CreateDate > CreateDate);
            }

            if (TiketStatus != null)
            {
                QueryAble = QueryAble
                 .Where(c => c.TiketStatus == TiketStatus);
            }

            if (priorityStatus != null)
            {
                QueryAble = QueryAble
                 .Where(c => c.PriorityStatus == priorityStatus);
            }


            Page = Page ?? 0;
            PerPage = PerPage ?? 10;


            var count = QueryAble.Count();
            double len = (double)count / (double)PerPage;
            var totalPages = Math.Ceiling((double)len);

            var tikets = await QueryAble.Skip((int)Page * (int)PerPage)
                  .Take((int)PerPage)
                  .OrderBy(c => (int)c.TiketStatus)
                  .ThenBy(c => c.IsUserSendNewMessgae == true)
                  .ThenByDescending(c => c.UserLastUpdateDate)
                  .ThenByDescending(c => c.IsAdminSendNewMessgae == true)
                   .ThenByDescending(c => c.CreateDate)
                   .Select(c => new
                   {
                       c.Id,
                       c.Title,
                       c.CreateDate,
                       c.PriorityStatus,
                       c.IsUserSendNewMessgae,
                       c.TiketStatus,
                       c.UserName,
                       c.UserLastUpdateDate,
                       c.AdminLastUpdateDate,
                   })
                  .ToListAsync();

            var data = new
            {
                tikets,
                TotalPages = totalPages
            };


            return Ok(_commonService.OkResponse(data, true));
        }





        /// <summary>
        ///گرفتن  لیست تیکت های من
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyTikets")]
        //[Authorize]
        [ClaimsAuthorize]
        public async Task<ActionResult> GetAllMyTikets()
        {
            var currentUsername = _accountService.GetCurrentUserName();
            var isPersian = _commonService.IsPersianLanguage();

            var tikets = await _context
               .TiketTBL.AsNoTracking()
               .Where(c => c.UserName == currentUsername)
               .Select(c => new
               {
                   c.Title,
                   c.Id,
                   c.CreateDate,
                   c.TiketStatus,
                   c.IsAdminSendNewMessgae,
               }).ToListAsync();
            //return Ok(_commonService.OkResponse(tikets, _localizerShared["SuccessMessage"].Value.ToString()));
            return Ok(_commonService.OkResponse(tikets, true));
        }






        /// <summary>
        /// افزودن یک تیکت 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddTiket")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddTiket([FromBody] AddTicketDTO model)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var tiket = new TiketTBL()
            {
                UserName = currentUsername,
                Title = model.Title,
                CreateDate = DateTime.Now,
                TiketStatus = TiketStatus.Open,
                IsUserSendNewMessgae = false,
                IsAdminSendNewMessgae = false,
                UserLastUpdateDate = DateTime.Now,
                AdminLastUpdateDate = DateTime.Now,
                PriorityStatus = model.PriorityStatus,
            };


            tiket.TiketMessagesTBL = new List<TiketMessagesTBL>() {
                    new TiketMessagesTBL()
                        {
                            CreateDate = DateTime.Now,
                            IsFile = false,
                            IsAdmin = false,
                            Text = model.StartText,
                            TiketTBL = tiket,
                        },
            };


            try
            {
                await _context.TiketTBL.AddAsync(tiket);
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(tiket.Id, false));
            }
            catch
            {
                List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));


                ////////List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
                ////////return StatusCode(StatusCodes.Status500InternalServerError,
                ////////   new ApiBadRequestResponse(erros, 500));

            }
        }






        /// <summary>
        ///   افزودن پیام از نوع چت(نه فایل) به تیکت   
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddChatMessageToTiketInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> AddChatMessageToTiketInAdmin([FromBody] AddChatMessageToTicketDTO model)
        {

            //validaton
            //is ticket closed
            var ticketFromDB = await _context.TiketTBL.Where(c => c.Id == model.TicketId).FirstOrDefaultAsync();
            if (ticketFromDB == null)
                return NotFound(_commonService.NotFoundErrorReponse(true));
            if (ticketFromDB.TiketStatus == TiketStatus.Closed)
            {
                var errors = new List<string>();
                errors.Add("the ticket is closed");
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            var currentUsername = _accountService.GetCurrentUserName();

            var tiketMessage = new TiketMessagesTBL()
            {
                Text = model.Text,
                IsFile = false,
                IsAdmin = true,
                TiketId = model.TicketId,
                CreateDate = DateTime.Now,
            };

            try
            {
                await _context.TiketMessagesTBL.AddAsync(tiketMessage);
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(null, false));
            }
            catch
            {

                List<string> erroses2 = new List<string> { PubicMessages.InternalServerMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));

                ////////////return StatusCode(StatusCodes.Status500InternalServerError,
                //////////// new ApiResponse(500, PubicMessages.InternalServerMessage));

            }
        }









        /// <summary>
        ///   افزودن پیام از نوع چت(نه فایل) به تیکت   
        ///   اینapi  برای کاربران سایته
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddChatMessageToTiket")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddChatMessageToTiket([FromBody] AddChatMessageToTicketInUserDTO model)
        {

            //validaton
            //is ticket closed
            var ticketFromDB = await _context.TiketTBL.Where(c => c.Id == model.TicketId).FirstOrDefaultAsync();
            if (ticketFromDB == null)
                return NotFound(_commonService.NotFoundErrorReponse(false));
            if (ticketFromDB.TiketStatus == TiketStatus.Closed)
            {
                var errors = new List<string>();
                errors.Add(_resourceServices.GetErrorMessageByKey("TicketIsClosedMessage"));
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            var currentUsername = _accountService.GetCurrentUserName();

            var tiketMessage = new TiketMessagesTBL()
            {
                Text = model.Text,
                IsFile = false,
                IsAdmin = false,
                TiketId = model.TicketId,
                CreateDate = DateTime.Now,
            };


            await _context.TiketMessagesTBL.AddAsync(tiketMessage);
            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));

        }







        /// <summary>
        /// افزودن پیام از نوع فایل(نه چت) به تیکت   
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddFileToTiketInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> AddFileToTiketInAdmin([FromForm] AddFileToTicketDTO model)
        {

            var tiketMessage = new TiketMessagesTBL()
            {
                //FileAddress=
                //Text = model.Text,
                IsFile = true,
                IsAdmin = true,
                TiketId = model.TicketId,
                CreateDate = DateTime.Now,

            };

            //validate the file
            var res = _ticketService.ValidateTicketFile(model);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));

            //is ticket closed
            var ticketFromDB = await _context.TiketTBL.Where(c => c.Id == model.TicketId).FirstOrDefaultAsync();
            if (ticketFromDB == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                //////////return NotFound(_commonService.NotFoundErrorReponse(true));

            }
            if (ticketFromDB.TiketStatus == TiketStatus.Closed)
            {
                var errors = new List<string>();
                errors.Add("the ticket is closed");
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            try
            {
                var fileAddress = await _ticketService.SaveFileToHost("Upload/Tickets/", null, model.File);
                tiketMessage.FileAddress = fileAddress;
            }
            catch
            {
                List<string> erroses2 = new List<string> { _resourceServices.GetErrorMessageByKey("ProblemUploadingTheFile") };
                return BadRequest(erroses2);
                //return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));
            }

            await _context.TiketMessagesTBL.AddAsync(tiketMessage);
            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(tiketMessage.FileAddress, true));

        }






        /// <summary>
        /// افزودن پیام از نوع فایل(نه چت) به تیکت   
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddFileToTiket")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> AddFileToTiket([FromForm] AddFileToTicketDTO model)
        {

            var tiketMessage = new TiketMessagesTBL()
            {
                //FileAddress=
                //Text = model.Text,
                IsFile = true,
                IsAdmin = false,
                TiketId = model.TicketId,
                CreateDate = DateTime.Now,

            };

            //validate the file
            var res = _ticketService.ValidateTicketFile(model);
            if (!res.succsseded)
            {
                //List<string> erros = new List<string> { _localizerShared["ServiceNotFound"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(res.result));

                //return BadRequest(new ApiBadRequestResponse(res.result));
            }

            //is ticket closed
            var ticketFromDB = await _context.TiketTBL.Where(c => c.Id == model.TicketId).FirstOrDefaultAsync();
            if (ticketFromDB == null)
                return NotFound(_commonService.NotFoundErrorReponse(false));
            if (ticketFromDB.TiketStatus == TiketStatus.Closed)
            {
                var errors = new List<string>();
                //errors.Add(_localizerShared["TicketIsClosedMessage"].Value.ToString());
                errors.Add(_resourceServices.GetErrorMessageByKey("TicketIsClosedMessage"));
                return BadRequest(new ApiBadRequestResponse(errors));
            }

            try
            {
                var fileAddress = await _ticketService.SaveFileToHost("Upload/Tickets/", null, model.File);
                tiketMessage.FileAddress = fileAddress;
            }
            catch
            {
                List<string> erroses2 = new List<string> { _resourceServices.GetErrorMessageByKey("ProblemUploadingTheFile") };
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiBadRequestResponse(erroses2, 500));


                //////////////return StatusCode(StatusCodes.Status500InternalServerError,
                //////////////             new ApiResponse(500, _localizerShared["ProblemUploadingTheFileMessage"].Value.ToString()));

            }



            await _context.TiketMessagesTBL.AddAsync(tiketMessage);
            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(tiketMessage.FileAddress, false));

        }




        [HttpGet("CloseOrOpenTicket")]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> CloseOrOpenTicket(int ticketId)
        {
            var ticketFromDB = await _context.TiketTBL.Where(c => c.Id == ticketId).FirstOrDefaultAsync();
            if (ticketFromDB == null)
            {
                List<string> erros = new List<string> { PubicMessages.NotFoundMessage };
                return BadRequest(new ApiBadRequestResponse(erros, 404));

                ////////////return NotFound(_commonService.NotFoundErrorReponse(true));
            }
            var tickStatus = TiketStatus.Closed;
            if (ticketFromDB.TiketStatus == TiketStatus.Closed)
            {
                ticketFromDB.TiketStatus = TiketStatus.Open;
                tickStatus = TiketStatus.Open;
            }
            else
            {
                ticketFromDB.TiketStatus = TiketStatus.Closed;
                tickStatus = TiketStatus.Closed;
            }

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(tickStatus, true));
        }







        [HttpGet("GetTiketsDetails")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetTiketsDetails(int tiketId)
        {
            var currentUSername = _accountService.GetCurrentUserName();

            var tiketFromDB = await _context
                .TiketTBL
                .Where(c => c.Id == tiketId && c.UserName == currentUSername)
                .Select(c => new
                {
                    c.Id,
                    c.TiketStatus,
                    c.PriorityStatus,
                    c.Title,
                    c.CreateDate,
                })
                .FirstOrDefaultAsync();

            if (tiketFromDB == null)
            {
                _commonService.NotFoundErrorReponse(false);
                //////////////List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                //////////////return NotFound(new ApiBadRequestResponse(erros, 404));
            }

            return Ok(_commonService.OkResponse(tiketFromDB, false));
        }





        [HttpGet("GetTiketsDetailsInAdmin")]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetTiketsDetailsInAdminGetTiketsDetailsInAdmin(int tiketId)
        {
            var tiketInfo = await (from t in _context.TiketTBL.Where(c => c.Id == tiketId)
                                   join u in _context.Users
                                     on t.UserName equals u.UserName
                                   select new
                                   {
                                       u.UserName,
                                       u.Name,
                                       u.LastName,
                                       u.ImageAddress,


                                       t.Id,
                                       t.Title,
                                       messages = t.TiketMessagesTBL
                                                   .Select(c => new { c.Id, c.IsAdmin, c.IsFile, c.Text, c.CreateDate, c.FileAddress })
                                                   .OrderBy(c => c.CreateDate).ToList()
                                   })
                                   .AsQueryable()
                                   .FirstOrDefaultAsync();

            var ticketFromDBs = _context.TiketTBL.Find(tiketId);

            if (ticketFromDBs != null)
                ticketFromDBs.IsUserSendNewMessgae = false;

            //var   _context.TiketTBL.Where(c => c.Id == tiketId).FirstOrDefaultAsync();
            await _context.SaveChangesAsync();

            return Ok(_commonService.OkResponse(tiketInfo, true));
        }





        /// <summary>
        ///گرفتن  لیست پیام های یک تیکت    
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetAllTiketsMessages")]
        //[Authorize]
        [ClaimsAuthorize]
        public async Task<ActionResult> GetAllTiketsMessages(int tiketId)
        {
            var currentUsername = _accountService.GetCurrentUserName();

            var messages = await _context
               .TiketTBL.AsNoTracking()
               .Where(c => c.UserName == currentUsername && c.Id == tiketId)
               .Select(c => new
               {
                   c.Title,
                   Messages = c.TiketMessagesTBL
                                    .Select(c => new { c.Id, c.IsAdmin, c.IsFile, c.FileAddress, c.CreateDate, c.Text })
                                    .OrderBy(c => c.CreateDate)
                                    .ToList(),
                   HasNewMessage = c.IsAdminSendNewMessgae,
                   c.TiketStatus
               }).FirstOrDefaultAsync();
            return Ok(_commonService.OkResponse(messages, false));
        }





        /// <summary>
        ///خوانده شده کردن یک تیکتی که ادمین فرستاد
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut("ReadAdminsTiket")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> ReadAdminsTiket(int Id)
        {
            var currentusername = _accountService.GetCurrentUserName();

            var tiket = await _context
               .TiketTBL
               .Where(C => C.UserName == currentusername && C.Id == Id)
              .FirstOrDefaultAsync();

            if (tiket == null)
                _commonService.NotFoundErrorReponse(false);

            tiket.IsAdminSendNewMessgae = false;

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));

        }





        /// <param name="Id"></param>
        ///خوانده شده کردن یک تیکتی که یوزر فرستاد
        /// <returns></returns>
        [HttpPut("ReadUsersTiket")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> ReadUsersTiketInAdmin(int Id)
        {
            var currentusername = _accountService.GetCurrentUserName();

            var tiket = await _context
               .TiketTBL
               .Where(C => C.UserName == currentusername && C.Id == Id)
              .FirstOrDefaultAsync();

            if (tiket == null)
                _commonService.NotFoundErrorReponse(false);

            tiket.IsUserSendNewMessgae = false;

            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));

        }


    }
}
