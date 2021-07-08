﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.Account;
using Domain.DTO.Response;
using Domain.DTO.Transaction;
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
using Service.Interfaces.Transaction;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class TransactoinController : BaseControlle
    {

        #region ctor

        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly ITransactionService _transactionService;


        //private IStringLocalizer<TransactoinController> _localizer;
        //private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;

        public TransactoinController(
        UserManager<AppUser> userManager,
            DataContext context,
                   IAccountService accountService,
                   ICommonService commonService,
                   ITransactionService transactionService,
                //IStringLocalizer<TransactoinController> localizer,
                // IStringLocalizer<ShareResource> localizerShared,
                IResourceServices resourceServices
            )
        {
            _context = context;
            _userManager = userManager;
            _accountService = accountService;
            _commonService = commonService;
            _transactionService = transactionService;
            //_localizer = localizer;
            //_localizerShared = localizerShared;
            _resourceServices = resourceServices;

        }

        #endregion ctor



        #region Admin

        /// <summary>
        ///گرفتن لیست تراکنش ها در ادمین    
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("GetAllTransactionInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllTransactionInAdmin([FromBody] GetAllTransactionInAdmin model)
        {
            var QueryAble = _context.TransactionTBL
                                         .AsNoTracking()
                                           .AsQueryable();

            QueryAble = QueryAble = _transactionService.Filter(model, QueryAble);


            model.Page = model.Page ?? 0;
            model.PerPage = model.PerPage ?? 10;


            var count = QueryAble.Count();
            double len = (double)count / (double)model.PerPage;
            var totalPages = Math.Ceiling((double)len);

            var transactions = await QueryAble.Skip((int)model.Page * (int)model.PerPage)
                  .Take((int)model.PerPage)
                  .OrderByDescending(c => c.CreateDate)
                  .Select(c => new
                  {
                      c.Id,
                      c.Username,
                      c.Amount,
                      CreateDate = c.CreateDate.ToString("MM/dd/yyyy h:mm tt"),
                      c.TransactionConfirmedStatus,
                      c.TransactionType,
                      c.ServiceTypeWithDetails,
                      c.ProviderUserName,
                      c.TransactionStatus,
                      c.ClientUserName,
                      c.BaseMyServiceTBL.ServiceName,
                      //c.CardTBL.CardName,
                  })
                  .ToListAsync();

            var data = new
            {
                transactions,
                TotalPages = totalPages
            };

            return Ok(_commonService.OkResponse(data, true));
        }




        /// <summary>
        ///گرفتن لیست تراکنش هایی که حاصل حاصل درخواست بین کاربران با  هم هستند        
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("GetAllServiceTransactionInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllServiceTransactionInAdmin([FromBody] GetAllTransactionInAdmin model)
        {

            var QueryAble = _context.TransactionTBL.Where(C => C.TransactionStatus == TransactionStatus.ServiceTransaction)
                                         .AsNoTracking()
                                           .AsQueryable();

            QueryAble = _transactionService.Filter(model, QueryAble);

            model.Page = model.Page ?? 0;
            model.PerPage = model.PerPage ?? 10;


            var count = QueryAble.Count();
            double len = (double)count / (double)model.PerPage;
            var totalPages = Math.Ceiling((double)len);

            var transactions = await QueryAble.Skip((int)model.Page * (int)model.PerPage)
                  .Take((int)model.PerPage)
                  .OrderByDescending(c => c.CreateDate)
                  .Select(c => new
                  {
                      c.Id,
                      c.Username,
                      c.Amount,
                      CreateDate = c.CreateDate.ToString("MM/dd/yyyy h:mm tt"),
                      c.TransactionConfirmedStatus,
                      c.TransactionType,
                      c.ServiceTypeWithDetails,
                      c.ProviderUserName,
                      c.TransactionStatus,
                      c.ClientUserName,
                      c.BaseMyServiceTBL.ServiceName,
                      //c.CardTBL.CardName,
                  })
                  .ToListAsync();

            var data = new
            {
                transactions,
                TotalPages = totalPages
            };

            return Ok(_commonService.OkResponse(data, true));
        }





        /// <summary>
        ///گرفتن لیست تراکنش هایی که حاصل برداشت از کیف پول خود یا اضافه کردن به کیف پول        
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("GetAllNormalTransactionInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllNormalTransactionInAdmin([FromBody] GetAllTransactionInAdmin model)
        {

            var QueryAble = _context.TransactionTBL.Where(C => C.TransactionStatus == TransactionStatus.NormalTransaction)
                                         .AsNoTracking()
                                           .AsQueryable();

            QueryAble = _transactionService.Filter(model, QueryAble);

            model.Page = model.Page ?? 0;
            model.PerPage = model.PerPage ?? 10;


            var count = QueryAble.Count();
            double len = (double)count / (double)model.PerPage;
            var totalPages = Math.Ceiling((double)len);

            var transactions = await QueryAble.Skip((int)model.Page * (int)model.PerPage)
                  .Take((int)model.PerPage)
                  .OrderByDescending(c => c.CreateDate)
                  .Select(c => new
                  {
                      c.Id,
                      c.Username,
                      c.Amount,
                      CreateDate = c.CreateDate.ToString("MM/dd/yyyy h:mm tt"),
                      c.TransactionConfirmedStatus,
                      c.TransactionType,
                      //c.ServiceTypeWithDetails,
                      c.ProviderUserName,
                      c.TransactionStatus,
                      //c.ClientUserName,
                      //c.BaseMyServiceTBL.ServiceName,
                      //c.CardTBL.CardName,
                  })
                  .ToListAsync();

            var data = new
            {
                transactions,
                TotalPages = totalPages
            };

            return Ok(_commonService.OkResponse(data, true));
        }



        /// <summary>
        ///گرفتن لیست تراکنش هایی که حاصل برداشت از کیف پول خود یا اضافه کردن به کیف پول        
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("GetAllTopTenTransactionInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllTopTenTransactionInAdmin([FromBody] GetAllTransactionInAdmin model)
        {

            var QueryAble = _context.TransactionTBL.Where(C => C.TransactionStatus == TransactionStatus.TopTenPackage)
                                         .AsNoTracking()
                                           .AsQueryable();

            QueryAble = _transactionService.Filter(model, QueryAble);

            model.Page = model.Page ?? 0;
            model.PerPage = model.PerPage ?? 10;


            var count = QueryAble.Count();
            double len = (double)count / (double)model.PerPage;
            var totalPages = Math.Ceiling((double)len);

            var transactions = await QueryAble.Skip((int)model.Page * (int)model.PerPage)
                  .Take((int)model.PerPage)
                  .OrderByDescending(c => c.CreateDate)
                  .Select(c => new
                  {
                      c.Id,
                      c.Username,
                      c.Amount,
                      CreateDate = c.CreateDate.ToString("MM/dd/yyyy h:mm tt"),
                      c.TransactionConfirmedStatus,
                      c.TransactionType,
                      //c.ServiceTypeWithDetails,
                      c.ProviderUserName,
                      c.TransactionStatus,
                      //c.ClientUserName,
                      //c.BaseMyServiceTBL.ServiceName,
                      //c.CardTBL.CardName,
                  })
                  .ToListAsync();

            var data = new
            {
                transactions,
                TotalPages = totalPages
            };

            return Ok(_commonService.OkResponse(data, true));
        }


        /// <summary>
        ///گرفتن لیست تراکنش های یک کاربر        
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("GetTarnsactonByUserNameInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetTarnsactonByUserNameInAdmin([FromBody] GetUserByUserName model)
        {
            var transactions = await _context.TransactionTBL.Where(c => c.Username == model.Username)
                  .OrderByDescending(c => c.CreateDate)
                  .Select(c => new
                  {
                      c.Id,
                      c.Username,
                      c.Amount,
                      CreateDate = c.CreateDate.ToString("MM/dd/yyyy h:mm tt"),
                      c.TransactionConfirmedStatus,
                      c.TransactionType,
                      c.ServiceTypeWithDetails,
                      c.ProviderUserName,
                      c.TransactionStatus,
                      c.ClientUserName,
                      c.BaseMyServiceTBL.ServiceName,
                      //c.CardTBL.CardName,
                  })
                  .ToListAsync();

            return Ok(_commonService.OkResponse(transactions, PubicMessages.SuccessMessage));
        }





        /// <summary>
        ///گرفتن لیست تراکنش هایی که حاصل کمیسیون یا کارمزد کاربران است        
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("GetAllCommissionTransactionInAdmin")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = true)]
        public async Task<ActionResult> GetAllCommissionTransactionInAdmin([FromBody] GetAllTransactionInAdmin model)
        {

            var QueryAble = _context.TransactionTBL.Where(C => C.TransactionStatus == TransactionStatus.Commission)
                                         .AsNoTracking()
                                           .AsQueryable();

            QueryAble = _transactionService.Filter(model, QueryAble);

            model.Page = model.Page ?? 0;
            model.PerPage = model.PerPage ?? 10;


            var count = QueryAble.Count();
            double len = (double)count / (double)model.PerPage;
            var totalPages = Math.Ceiling((double)len);

            var transactions = await QueryAble.Skip((int)model.Page * (int)model.PerPage)
                  .Take((int)model.PerPage)
                  .OrderByDescending(c => c.CreateDate)
                  .Select(c => new
                  {
                      c.Id,
                      c.Username,
                      c.Amount,
                      CreateDate = c.CreateDate.ToString("MM/dd/yyyy h:mm tt"),
                      c.TransactionConfirmedStatus,
                      c.TransactionType,
                      c.ServiceTypeWithDetails,
                      c.ProviderUserName,
                      c.TransactionStatus,
                      c.ClientUserName,
                      c.BaseMyServiceTBL.ServiceName,
                      //c.CardTBL.CardName,
                  })
                  .ToListAsync();

            var data = new
            {
                transactions,
                TotalPages = totalPages
            };

            return Ok(_commonService.OkResponse(data, true));
        }



        #endregion



        #region User side

        [HttpGet("GetTransactionsByIdForUser")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetTransactionsByIdForUser(int transactionId)
        {
            var currentusername = _accountService.GetCurrentUserName();

            var transaction = await (from t in _context.TransactionTBL.Where(c => c.Username == currentusername && c.Id == transactionId)
                                     join bs in _context.BaseMyServiceTBL
                                     on t.BaseMyServiceId equals bs.Id into Detqails
                                     from m in Detqails.DefaultIfEmpty()

                                     join u in _context.Users
                                     on m.UserName equals u.UserName into users
                                     from User in users.DefaultIfEmpty()

                                     select new
                                     {
                                         t.Id,
                                         t.Amount,
                                         t.Description,
                                         t.CreateDate,
                                         t.TransactionStatus,
                                         t.TransactionType,

                                         m.ServiceName,
                                         ServiceType = m.ServiceTypes,

                                         //m.ServiceType,

                                         //bs.ServiceName,
                                         //bs.ServiceType,

                                         User.Name,
                                         User.LastName,
                                         User.ImageAddress,

                                     }).AsQueryable().OrderByDescending(c => c.CreateDate).ToListAsync();

            return Ok(_commonService.OkResponse(transaction, false));
        }





        [HttpGet("GetAlltransactionsForUser")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetAlltransactionsForUser()
        {
            var currentusername = _accountService.GetCurrentUserName();

            var transaction = await (from t in _context.TransactionTBL.Where(c => c.Username == currentusername)
                                     join bs in _context.BaseMyServiceTBL
                                     on t.BaseMyServiceId equals bs.Id into Detqails
                                     from m in Detqails.DefaultIfEmpty()

                                     join u in _context.Users
                                     on m.UserName equals u.UserName into users
                                     from User in users.DefaultIfEmpty()

                                     select new
                                     {
                                         t.Id,
                                         t.Amount,
                                         t.Description,
                                         t.CreateDate,
                                         t.TransactionStatus,
                                         t.TransactionType,

                                         m.ServiceName,
                                         ServiceType = m.ServiceTypes,
                                         //m.ServiceType,

                                         //bs.ServiceName,
                                         //bs.ServiceType,

                                         User.Name,
                                         User.LastName,
                                         User.ImageAddress,

                                     }).AsQueryable().OrderByDescending(c => c.CreateDate).ToListAsync();

            return Ok(_commonService.OkResponse(transaction, false));
        }




        ///////////////// /////////////////  fuckkkk ///////////////// ///////////////// 
        ///////////////// <summary>
        ///////////////// برداشت از کیف پول نه برای خرید یا فروش سرویس
        ///////////////// </summary>
        ///////////////// <param name="model"></param>
        ///////////////// <returns></returns>
        //////////////[HttpPost("NormalWithDrawl")]
        ////////////////[Authorize]
        //////////////[ClaimsAuthorize(IsAdmin = false)]
        //////////////public async Task<ActionResult> NormalWithDrawl([FromBody] AddTransactionDTO model)
        //////////////{

        //////////////    //validation

        //////////////    var cardExist = await _context.CardTBL.AnyAsync(c => c.Id == model.CardId);
        //////////////    if (!cardExist)
        //////////////        return BadRequest(_commonService.NotFoundErrorReponse(false));


        //////////////    var allMyTransactions = await _context.TransactionTBL.Where(c =>
        //////////////                   c.TransactionConfirmedStatus == TransactionConfirmedStatus.Confirmed)
        //////////////        .SumAsync(c => c.Amount);

        //////////////    var curretnUserName = _accountService.GetCurrentUserName();
        //////////////    var userFromDB = await _userManager.FindByNameAsync(curretnUserName);

        //////////////    if (userFromDB == null)
        //////////////        return NotFound(_commonService.NotFoundErrorReponse(false));


        //////////////    var userAcceptedBalance = userFromDB.WalletBalance - allMyTransactions;

        //////////////    if (userAcceptedBalance <= 0 || userAcceptedBalance - model.Amount <= 0)
        //////////////    {
        //////////////        List<string> erros = new List<string> {
        //////////////        _resourceServices.GetErrorMessageByKey("InvaliAmountForTransaction")
        //////////////        };
        //////////////        return BadRequest(new ApiBadRequestResponse(erros));
        //////////////    }

        //////////////    var transaction = new TransactionTBL()
        //////////////    {
        //////////////        Amount = (double)model.Amount,
        //////////////        CreateDate = DateTime.Now,
        //////////////        Description = $"The amount of ${model.Amount} was withdrawn from cardId {model.CardId}",
        //////////////        TransactionConfirmedStatus = TransactionConfirmedStatus.Pending,
        //////////////        Username = curretnUserName,
        //////////////        TransactionType = TransactionType.WhiteDrawl,
        //////////////        TransactionStatus = TransactionStatus.NormalTransaction,
        //////////////        BaseMyServiceTBL = null,
        //////////////        BaseMyServiceId = null,
        //////////////        CardId = model.CardId,
        //////////////        ProviderUserName = null,
        //////////////        ServiceTypeWithDetails = null,
        //////////////        User_TopTenPackageTBL = null,
        //////////////        CheckDiscountTBL = null,

        //////////////    };


        //////////////    await _context.TransactionTBL.AddAsync(transaction);
        //////////////    await _context.SaveChangesAsync();
        //////////////    return Ok(_commonService.OkResponse(null, false));


        //////////////}






        /////////////////  fuckkkk /////////////////

        ////////////////[HttpPost("NormalDeposit")]
        //////////////////[Authorize]
        ////////////////[ClaimsAuthorize(IsAdmin = false)]
        ////////////////public async Task<ActionResult> NormalDeposit([FromBody] AddDepositDTO model)
        ////////////////{
        ////////////////    //validation

        ////////////////    var curretnUserName = _accountService.GetCurrentUserName();
        ////////////////    var userFromDB = await _userManager.FindByNameAsync(curretnUserName);

        ////////////////    if (userFromDB == null)
        ////////////////        return NotFound(_commonService.NotFoundErrorReponse(false));



        ////////////////    //  *************************************  go to argah  *************************************
        ////////////////    //**                                              
        ////////////////    //*************************************  go to argah  *************************************

        ////////////////    var transaction = new TransactionTBL()
        ////////////////    {
        ////////////////        Amount = (double)model.Amount,
        ////////////////        CreateDate = DateTime.Now,
        ////////////////        Description = $"The amount of ${model.Amount} was deposit ",
        ////////////////        TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
        ////////////////        Username = curretnUserName,
        ////////////////        TransactionType = TransactionType.Deposit,
        ////////////////        TransactionStatus = TransactionStatus.NormalTransaction,
        ////////////////        BaseMyServiceTBL = null,
        ////////////////        BaseMyServiceId = null,
        ////////////////        CardId = null,
        ////////////////        ProviderUserName = null,
        ////////////////        ServiceTypeWithDetails = null,
        ////////////////        User_TopTenPackageTBL = null,
        ////////////////        CheckDiscountTBL = null,
        ////////////////    };



        ////////////////    var userfromDB = await _userManager.FindByNameAsync(curretnUserName);
        ////////////////    userfromDB.WalletBalance += (double)model.Amount;

        ////////////////    await _context.TransactionTBL.AddAsync(transaction);
        ////////////////    await _context.SaveChangesAsync();
        ////////////////    return Ok(_commonService.OkResponse(null, false));
        ////////////////}




        [HttpGet("GetWalteBalance")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> GetWalteBalance()
        {
            var currentusername = _accountService.GetCurrentUserName();
            var userWaletBalance = await _context.Users.Where(c => c.UserName.ToLower() == currentusername.ToLower())
                  .Select(c => c.WalletBalance).FirstOrDefaultAsync();

            return Ok(_commonService.OkResponse(Math.Round(userWaletBalance.Value, 3), false));
        }



        #endregion


    }
}
