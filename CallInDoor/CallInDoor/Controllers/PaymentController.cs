using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallInDoor.Config.Attributes;
using Domain;
using Domain.DTO.CheckDiscount;
using Domain.DTO.Payment;
using Domain.DTO.RequestService;
using Domain.DTO.Response;
using Domain.DTO.TopTen;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Payment;

namespace CallInDoor.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PaymentController : BaseControlle
    {
        #region ctor

        private readonly DataContext _context;
        private readonly IPaymentService _paymentService;

        private readonly IAccountService _accountService;

        private readonly ICommonService _commonService;
        private IStringLocalizer<ShareResource> _localizerShared;
        public PaymentController(
            DataContext context,
            IPaymentService paymentService,
              IAccountService accountService,
              ICommonService commonService,
             IStringLocalizer<ShareResource> localizerShared
            )
        {
            _context = context;
            _paymentService = paymentService;
            _commonService = commonService;
            _accountService = accountService;
            _localizerShared = localizerShared;
        }

        #endregion


        /// <summary>
        /// خرید بسته برای یک درخواست از یک سرویس با کیف پول
        /// </summary>
        /// <param name="baseServiceId"></param>
        /// <param name="disCountCode"></param>
        /// <returns></returns>
        [HttpPost("BuyPackageWithWallet")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> BuyPackageWithWallet([FromBody] BuyPackageDTO model)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var query = await _context.ServiceRequestTBL
                        .Where(c => c.Id == model.RequestId &&
                         c.ClienUserName == currentUsername)
                       .Select(c => new
                       {
                           requestFromDB = c,
                           //c.BaseMyServiceTBL.MyChatsService.PriceForNativeCustomer,
                           //c.BaseMyServiceTBL!.MyChatsService!.PriceForNonNativeCustomer,
                           c.BaseMyServiceTBL.ServiceTbl.SitePercent,
                           ServiceTBLId = c.BaseMyServiceTBL.ServiceTbl.Id,
                       })
                        .FirstOrDefaultAsync();

            #region wsome validation
            if (query == null)
            {
                List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            if (query.requestFromDB.ServiceRequestStatus != ServiceRequestStatus.Confirmed)
            {
                List<string> erros = new List<string> { "Provider dont accept your service up to now" };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            #endregion 
            var disCountPercentFromDb = await _context.CheckDiscountTBL.Where(c => c.Code == model.DisCountCode)
                                       .Select(c => new DiscountVM { Percent = c.Percent, Id = c.Id, ExpireTime = c.ExpireTime, ServiceId = c.ServiceId })
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync();

            #region validation discountCode
            if (!string.IsNullOrEmpty(model.DisCountCode) && (disCountPercentFromDb == null || disCountPercentFromDb.ExpireTime < DateTime.Now
                || disCountPercentFromDb.ServiceId != query.ServiceTBLId))
            {
                List<string> errors = new List<string>();
                errors.Add(_localizerShared["InvalidDiscountCode"].Value.ToString());
                return BadRequest(new ApiBadRequestResponse(errors));
            }
            #endregion


            //validation BuyPackage
            var res = await _paymentService.ValidateBuyPackage(query.requestFromDB);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));


            #region getClient abd Provider from DB
            List<AppUser> userFromDB = await _context.Users
                   .Where(c => c.UserName == query.requestFromDB.ClienUserName || c.UserName == query.requestFromDB.ProvideUserName).ToListAsync();

            AppUser clientFromDB = userFromDB.Where(c => c.UserName == query.requestFromDB.ClienUserName).FirstOrDefault();
            AppUser providerFromDB = userFromDB.Where(c => c.UserName == query.requestFromDB.ProvideUserName).FirstOrDefault();

            #endregion

            if (currentUsername != clientFromDB?.UserName)
            {
                List<string> erros = new List<string> { _localizerShared["YouAreProviderOfThisService"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            var isNative = _accountService.IsNative(clientFromDB, providerFromDB);
            #region validate Wallet
            var waletRes = _paymentService.ValidateWallet(clientFromDB, isNative, query.SitePercent, query.requestFromDB.PriceForNativeCustomer, query.requestFromDB.PriceForNonNativeCustomer, disCountPercentFromDb);
            if (waletRes.succsseded == false)
                return BadRequest(new ApiBadRequestResponse(waletRes.result));
            #endregion


            if (isNative)
            {

                //if (model.IsByWallet)
                //{
                //if (clientFromDB.WalletBalance > query?.PriceForNativeCustomer)
                //{
                //1-//lahaz % takhfif
                //2- //lahaz darsad callinDoor

                //if (disCountPercentFromDb != null)
                //{

                var reducepercentForClient = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
                var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                //double? finalePrice = ((100 - reducepercent) * query.requestFromDB.PriceForNativeCustomer) / 100;
                double? finalePriceForClient = ((100 - reducepercentForClient) * query.requestFromDB.PriceForNativeCustomer) / 100;
                double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.requestFromDB.PriceForNativeCustomer) / 100;


                clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                //providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePrice;
                providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;


                //add transaction(add to provider wallet and reduce from Client wallet)
                await AddTrasanctionForNative(query.requestFromDB, disCountPercentFromDb?.Id, finalePriceForClient, finalePriceFirProvider);
                //add package
                await AddPackage(query.requestFromDB, disCountPercentFromDb?.Id, false, BuyiedPackageStatus.FromWallet, query.requestFromDB.PriceForNativeCustomer, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));

                //}
                //else
                //{
                //    var reducepercentForClient = 0 /*+ query.SitePercent*/;
                //    var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                //    double? finalePriceForClient = ((100 - reducepercentForClient) * query.requestFromDB.PriceForNativeCustomer) / 100;
                //    double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.requestFromDB.PriceForNativeCustomer) / 100;

                //    //lahaz site percnt
                //    var reducepercent = query.SitePercent;

                //    //double? finalPrice = ((100 - reducepercent) * query.requestFromDB.PriceForNativeCustomer) / 100;
                //    clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                //    providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;

                //    //add transaction
                //    await AddTrasanctionForNative(query.requestFromDB, null, finalePriceForClient, finalePriceFirProvider);
                //    //add package
                //    await AddPackage(query.requestFromDB, null, false, BuyiedPackageStatus.FromWallet, query.requestFromDB.PriceForNativeCustomer, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));
                //}

                //check the disCountId
                //query.requestFromDB.HasPlan_LimitedChatVoice = true;
                //query.requestFromDB.ExpireTime_LimitedChatVoice = DateTime.Now.AddMonths(2);

                //}
                //else
                //{
                //    List<string> erros = new List<string> { _localizerShared["YouDOntAnyWallet"].Value.ToString() };
                //    return BadRequest(new ApiBadRequestResponse(erros));
                //}
                //}



                //#region   should delete
                //else
                //{


                //    List<string> erros = new List<string> { "***********************درگاه نداریم که***************" };
                //    return BadRequest(new ApiBadRequestResponse(erros));


                //    //errrorrrrr

                //    //lahaz % takhfif
                //    // go to dargah

                //    //add Transaction
                //    //add payment -------> inja na
                //}


                //#endregion should delete


            }
            else
            {
                //if (model.IsByWallet)
                //{


                //if (clientFromDB.WalletBalance > query?.PriceForNonNativeCustomer)
                //{
                //if (disCountPercentFromDb != null)
                //{

                var reducepercentForClient = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
                var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                //double? finalePrice = ((100 - reducepercent) * query.requestFromDB.PriceForNativeCustomer) / 100;
                double? finalePriceForClient = ((100 - reducepercentForClient) * query.requestFromDB.PriceForNonNativeCustomer) / 100;
                double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.requestFromDB.PriceForNonNativeCustomer) / 100;


                clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                //providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePrice;
                providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;


                //add transaction(add to provider wallet and reduce from Client wallet)
                await AddTrasanctionForNonNative(query.requestFromDB, disCountPercentFromDb?.Id, finalePriceForClient, finalePriceFirProvider);
                //add package
                await AddPackage(query.requestFromDB, disCountPercentFromDb?.Id, false, BuyiedPackageStatus.FromWallet, query.requestFromDB.PriceForNativeCustomer, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));


                //}
                //else
                //{

                //    var reducepercent = query.SitePercent;

                //    //lahaz % takhfif

                //    double? finalPrice = ((100 - reducepercent) * query.requestFromDB.PriceForNonNativeCustomer) / 100;

                //    clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalPrice;
                //    providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalPrice;

                //    await AddTrasanctionForNative(query.requestFromDB, null, finalPrice);
                //    //add package
                //    await AddPackage(query.requestFromDB, null, false, BuyiedPackageStatus.FromWallet, query.requestFromDB.PriceForNonNativeCustomer, finalPrice, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));
                //}


                ///check the disCountId
                //query.requestFromDB.HasPlan_LimitedChatVoice = true;
                //query.requestFromDB.ExpireTime_LimitedChatVoice = DateTime.Now.AddMonths(2);



                //}
                //else
                //{
                //    List<string> erros = new List<string> { _localizerShared["YouDOntAnyWallet"].Value.ToString() };
                //    return BadRequest(new ApiBadRequestResponse(erros));



                //add Transaction
                //add payment -------> inja na
                //}

                //}

                //#region   should delete

                //else
                //{

                //    List<string> erros = new List<string> { "***********************درگاه نداریم که***************" };
                //    return BadRequest(new ApiBadRequestResponse(erros));


                //    //lahaz % takhfif
                //    // go to dargah

                //    //add Transaction
                //    //add payment -------> inja na
                //}

                //#endregion


            }


            query.requestFromDB.HasPlan_LimitedChatVoice = true;
            query.requestFromDB.ExpireTime_LimitedChatVoice = DateTime.Now.AddMonths(2);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(null, false));
            }
            catch
            {
                List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiBadRequestResponse(erros, 500));
            }
        }





        /// <summary>
        ///// خرید بسته برای یک درخواست از یک سرویس با رفتن به درگاه پرداخت
        /// </summary>
        /// <returns></returns>
        [HttpPost("BuyPackageWithPayMentGateWay")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> BuyPackageWithPayMentGateWay([FromBody] BuyPackageDTO model)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var query = await _context.ServiceRequestTBL
                        .Where(c => c.Id == model.RequestId &&
                         c.ClienUserName == currentUsername
                         )
                       .Select(c => new
                       {
                           requestFromDB = c,
                           //c.BaseMyServiceTBL.MyChatsService.PriceForNativeCustomer,
                           //c.BaseMyServiceTBL!.MyChatsService!.PriceForNonNativeCustomer,
                           c.BaseMyServiceTBL.ServiceTbl.SitePercent,
                           ServiceTBLId = c.BaseMyServiceTBL.ServiceTbl.Id,
                       })
                        .FirstOrDefaultAsync();
            #region some validatio
            if (query == null)
            {
                List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            if (query.requestFromDB.ServiceRequestStatus != ServiceRequestStatus.Confirmed)
            {
                List<string> erros = new List<string> { "Provider dont accept your service up to now" };
                return BadRequest(new ApiBadRequestResponse(erros));
            }
            #endregion

            var disCountPercentFromDb = await _context.CheckDiscountTBL.Where(c => c.Code == model.DisCountCode)
                                       .Select(c => new DiscountVM { Percent = c.Percent, Id = c.Id, ExpireTime = c.ExpireTime, ServiceId = c.ServiceId })
                                       .AsNoTracking().FirstOrDefaultAsync();

            #region validation discountCode
            if (!string.IsNullOrEmpty(model.DisCountCode) && (disCountPercentFromDb == null || disCountPercentFromDb.ExpireTime < DateTime.Now
                || disCountPercentFromDb.ServiceId != query.ServiceTBLId))
            {
                List<string> errors = new List<string>();
                errors.Add(_localizerShared["InvalidDiscountCode"].Value.ToString());
                return BadRequest(new ApiBadRequestResponse(errors));
            }
            #endregion


            //validation BuyPackage
            var res = await _paymentService.ValidateBuyPackage(query.requestFromDB);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));


            #region getClient abd Provider from DB
            List<AppUser> userFromDB = await _context.Users
                          .Where(c => c.UserName == query.requestFromDB.ClienUserName ||
                                                            c.UserName == query.requestFromDB.ProvideUserName).ToListAsync();
            AppUser clientFromDB = userFromDB.Where(c => c.UserName == query.requestFromDB.ClienUserName).FirstOrDefault();
            AppUser providerFromDB = userFromDB.Where(c => c.UserName == query.requestFromDB.ProvideUserName).FirstOrDefault();
            #endregion

            if (currentUsername != clientFromDB?.UserName)
            {
                List<string> erros = new List<string> { _localizerShared["YouAreProviderOfThisService"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }
            var isNative = _accountService.IsNative(clientFromDB, providerFromDB);


            ////////#region validate Wallet
            ////////var waletRes = _paymentService.ValidateWallet(clientFromDB, isNative, query.SitePercent, query.PriceForNativeCustomer, query.PriceForNonNativeCustomer, disCountPercentFromDb);
            ////////if (waletRes.succsseded == false)
            ////////    return BadRequest(new ApiBadRequestResponse(waletRes.result));
            ////////#endregion


            if (isNative)
            {
                //1-//lahaz % takhfif
                //2- //lahaz darsad callinDoor
                var reducepercentForClient = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
                var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                double? finalePriceForClient = ((100 - reducepercentForClient) * query.requestFromDB.PriceForNativeCustomer) / 100;
                double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.requestFromDB.PriceForNativeCustomer) / 100;
                //این هارا بفرست برای درگاه بعد بگیر از درگاه
                //cuurent username
                //requestId,
                //finalePriceForClient
                //finalePriceFirProvider
                //main price == priceFornativcutomer
                //discountId,


                ///boro be dargah

                //********  این  4 خط را بعد از بازگشت از درگاه برو
                ////////////////clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                ////////////////providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;
                ////////////////await AddTrasanctionForNative(query.requestFromDB, disCountPercentFromDb?.Id, finalePriceForClient, finalePriceFirProvider);
                ////////////////await AddPackage(query.requestFromDB, disCountPercentFromDb?.Id, false, BuyiedPackageStatus.FromWallet, query.requestFromDB.PriceForNativeCustomer, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));

            }
            else
            {

                //1-//lahaz % takhfif
                //2- //lahaz darsad callinDoor
                var reducepercentForClient = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
                var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                double? finalePriceForClient = ((100 - reducepercentForClient) * query.requestFromDB.PriceForNonNativeCustomer) / 100;
                double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.requestFromDB.PriceForNonNativeCustomer) / 100;


                //این هارا بفرست برای درگاه بعد بگیر از درگاه
                //cuurent username
                //requestId,
                //finalePriceForClient
                //finalePriceFirProvider
                //main price == priceFornativcutomer
                //discountId,


                ///boro be dargah


                //********  این  4 خط را بعد از بازگشت از درگاه برو
                //clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                //providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;
                //await AddTrasanctionForNonNative(query.requestFromDB, disCountPercentFromDb?.Id, finalePriceForClient, finalePriceFirProvider);
                //await AddPackage(query.requestFromDB, disCountPercentFromDb?.Id, false, BuyiedPackageStatus.FromWallet, query.requestFromDB.PriceForNativeCustomer, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));

            }

            //********  این  2 خط را بعد از بازگشت از درگاه برو
            //query.requestFromDB.HasPlan_LimitedChatVoice = true;
            //query.requestFromDB.ExpireTime_LimitedChatVoice = DateTime.Now.AddMonths(2);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(null, false));
            }
            catch
            {
                List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiBadRequestResponse(erros, 500));
            }
        }

        #region  تمدید  بسته


        /// <summary>
        /// تمدید بسته برای یک درخواست از یک سرویس با کیف پول
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("RenewPackageWithWallet")]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> RenewPackageWithWallet([FromBody] BuyPackageDTO model)
        {
            var currentUsername = _accountService.GetCurrentUserName();
            var query = await _context.ServiceRequestTBL
                        .Where(c => c.Id == model.RequestId &&
                         c.ClienUserName == currentUsername &&
                         c.ServiceRequestStatus == ServiceRequestStatus.Confirmed)
                       .Select(c => new RequestWithBaseseServeicVM
                       {
                           requestFromDB = c,
                           SitePercent = c.BaseMyServiceTBL.ServiceTbl.SitePercent,
                           ServiceTBLId = c.BaseMyServiceTBL.ServiceTbl.Id,

                           IsDeleted_baseService = c.BaseMyServiceTBL.IsDeleted,
                           MessageCount_baseService = c.BaseMyServiceTBL.MyChatsService.MessageCount,
                           PriceForNativeCustomer_baseService = c.BaseMyServiceTBL.MyChatsService.PriceForNativeCustomer,
                           PriceForNonNativeCustomer_baseService = c.BaseMyServiceTBL.MyChatsService.PriceForNonNativeCustomer,
                       })
                        .FirstOrDefaultAsync();

            if (query == null)
            {
                List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            var disCountPercentFromDb = await _context.CheckDiscountTBL.Where(c => c.Code == model.DisCountCode)
                                       .Select(c => new DiscountVM { Percent = c.Percent, Id = c.Id, ExpireTime = c.ExpireTime, ServiceId = c.ServiceId })
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync();

            #region validation discountCode
            if (!string.IsNullOrEmpty(model.DisCountCode) && (disCountPercentFromDb == null || disCountPercentFromDb.ExpireTime < DateTime.Now
                || disCountPercentFromDb.ServiceId != query.ServiceTBLId))
            {
                List<string> errors = new List<string>();
                errors.Add(_localizerShared["InvalidDiscountCode"].Value.ToString());
                return BadRequest(new ApiBadRequestResponse(errors));
            }
            #endregion


            //validation BuyPackage
            var res = await _paymentService.ValidateBuyPackage(query.requestFromDB);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));


            #region getClient abd Provider from DB
            List<AppUser> userFromDB = await _context.Users
                          .Where(c => c.UserName == query.requestFromDB.ClienUserName || c.UserName == query.requestFromDB.ProvideUserName).ToListAsync();
            AppUser clientFromDB = userFromDB.Where(c => c.UserName == query.requestFromDB.ClienUserName).FirstOrDefault();
            AppUser providerFromDB = userFromDB.Where(c => c.UserName == query.requestFromDB.ProvideUserName).FirstOrDefault();
            #endregion

            if (currentUsername != clientFromDB?.UserName)
            {
                List<string> erros = new List<string> { _localizerShared["YouAreProviderOfThisService"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            var isNative = _accountService.IsNative(clientFromDB, providerFromDB);

            #region validate Wallet
            var waletRes = _paymentService.ValidateWallet(clientFromDB, isNative, query.SitePercent, query.PriceForNativeCustomer_baseService, query.PriceForNonNativeCustomer_baseService, disCountPercentFromDb);
            if (waletRes.succsseded == false)
                return BadRequest(new ApiBadRequestResponse(waletRes.result));
            #endregion

            if (isNative)
            {
                var reducepercentForClient = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
                var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                double? finalePriceForClient = ((100 - reducepercentForClient) * query.PriceForNativeCustomer_baseService) / 100;
                double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.PriceForNativeCustomer_baseService) / 100;

                clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;

                //add transaction(add to provider wallet and reduce from Client wallet)
                await AddTrasanctionForNative(query.requestFromDB, disCountPercentFromDb?.Id, finalePriceForClient, finalePriceFirProvider);
                //add package
                await AddPackage(query.requestFromDB, disCountPercentFromDb?.Id, true, BuyiedPackageStatus.FromWallet, query.PriceForNativeCustomer_baseService, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));

            }
            else
            {
                var reducepercentForClient = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
                var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                double? finalePriceForClient = ((100 - reducepercentForClient) * query.PriceForNonNativeCustomer_baseService) / 100;
                double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.PriceForNonNativeCustomer_baseService) / 100;

                clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;

                //add transaction(add to provider wallet and reduce from Client wallet)
                await AddTrasanctionForNonNative(query.requestFromDB, disCountPercentFromDb?.Id, finalePriceForClient, finalePriceFirProvider);
                //add package
                await AddPackage(query.requestFromDB, disCountPercentFromDb?.Id, true, BuyiedPackageStatus.FromWallet, query.PriceForNonNativeCustomer_baseService, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));
            }

            query.requestFromDB.HasPlan_LimitedChatVoice = true;
            query.requestFromDB.ExpireTime_LimitedChatVoice = DateTime.Now.AddMonths(2);
            query.requestFromDB.AllMessageCount_LimitedChat += (int)query.MessageCount_baseService;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(null, false));
            }
            catch
            {
                List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiBadRequestResponse(erros, 500));
            }
        }





        /// <summary>
        ///// تمدید بسته برای یک درخواست از یک سرویس با رفتن به درگاه پرداخت
        /// </summary>
        /// <returns></returns>
        [HttpPost("RenewPackageWithPayMentGateWay")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> RenewPackageWithPayMentGateWay([FromBody] BuyPackageDTO model)
        {

            var currentUsername = _accountService.GetCurrentUserName();

            var query = await _context.ServiceRequestTBL
                        .Where(c => c.Id == model.RequestId &&
                         c.ClienUserName == currentUsername)
                       .Select(c => new RequestWithBaseseServeicVM
                       {
                           requestFromDB = c,
                           //c.BaseMyServiceTBL.MyChatsService.PriceForNativeCustomer,
                           //c.BaseMyServiceTBL!.MyChatsService!.PriceForNonNativeCustomer,
                           SitePercent = c.BaseMyServiceTBL.ServiceTbl.SitePercent,
                           ServiceTBLId = c.BaseMyServiceTBL.ServiceTbl.Id,


                           IsDeleted_baseService = c.BaseMyServiceTBL.IsDeleted,
                           MessageCount_baseService = c.BaseMyServiceTBL.MyChatsService.MessageCount,
                           PriceForNativeCustomer_baseService = c.BaseMyServiceTBL.MyChatsService.PriceForNativeCustomer,
                           PriceForNonNativeCustomer_baseService = c.BaseMyServiceTBL.MyChatsService.PriceForNonNativeCustomer,
                       })
                        .FirstOrDefaultAsync();
            #region some validation
            if (query == null)
            {
                List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }
            if (query.requestFromDB.ServiceRequestStatus != ServiceRequestStatus.Confirmed)
            {
                List<string> erros = new List<string> { "Provider dont accept your service up to now" };
                return BadRequest(new ApiBadRequestResponse(erros));
            }
            #endregion

            var disCountPercentFromDb = await _context.CheckDiscountTBL.Where(c => c.Code == model.DisCountCode)
                                       .Select(c => new DiscountVM { Percent = c.Percent, Id = c.Id, ExpireTime = c.ExpireTime, ServiceId = c.ServiceId })
                                       .AsNoTracking().FirstOrDefaultAsync();


            #region validation discountCode
            if (!string.IsNullOrEmpty(model.DisCountCode) && (disCountPercentFromDb == null || disCountPercentFromDb.ExpireTime < DateTime.Now
                || disCountPercentFromDb.ServiceId != query.ServiceTBLId))
            {
                List<string> errors = new List<string>();
                errors.Add(_localizerShared["InvalidDiscountCode"].Value.ToString());
                return BadRequest(new ApiBadRequestResponse(errors));
            }
            #endregion


            //validation BuyPackage
            var res = await _paymentService.ValidateBuyPackage(query.requestFromDB);
            if (!res.succsseded)
                return BadRequest(new ApiBadRequestResponse(res.result));


            #region getClient abd Provider from DB
            List<AppUser> userFromDB = await _context.Users
                          .Where(c => c.UserName == query.requestFromDB.ClienUserName || c.UserName == query.requestFromDB.ProvideUserName).ToListAsync();
            AppUser clientFromDB = userFromDB.Where(c => c.UserName == query.requestFromDB.ClienUserName).FirstOrDefault();
            AppUser providerFromDB = userFromDB.Where(c => c.UserName == query.requestFromDB.ProvideUserName).FirstOrDefault();


            #endregion

            if (currentUsername != clientFromDB?.UserName)
            {
                List<string> erros = new List<string> { _localizerShared["YouAreProviderOfThisService"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            var isNative = _accountService.IsNative(clientFromDB, providerFromDB);


            ////////#region validate Wallet
            ////////var waletRes = _paymentService.ValidateWallet(clientFromDB, isNative, query.SitePercent, query.PriceForNativeCustomer, query.PriceForNonNativeCustomer, disCountPercentFromDb);
            ////////if (waletRes.succsseded == false)
            ////////    return BadRequest(new ApiBadRequestResponse(waletRes.result));
            ////////#endregion


            if (isNative)
            {

                //1-//lahaz % takhfif
                //2- //lahaz darsad callinDoor
                var reducepercentForClient = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
                var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                double? finalePriceForClient = ((100 - reducepercentForClient) * query.PriceForNativeCustomer_baseService) / 100;
                double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.PriceForNativeCustomer_baseService) / 100;
                //این هارا بفرست برای درگاه بعد بگیر از درگاه
                //cuurent username
                //requestId,
                //finalePriceForClient
                //finalePriceFirProvider
                //main price == priceFornativcutomer
                //discountId,


                ///boro be dargah

                //********  این  4 خط را بعد از بازگشت از درگاه برو
                //////////////clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                //////////////providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;
                //////////////await AddTrasanctionForNative(query.requestFromDB, disCountPercentFromDb?.Id, finalePriceForClient, finalePriceFirProvider);
                //////////////await AddPackage(query.requestFromDB, disCountPercentFromDb?.Id, true, BuyiedPackageStatus.FromWallet, query.PriceForNativeCustomer_baseService, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));



            }
            else
            {

                //1-//lahaz % takhfif
                //2- //lahaz darsad callinDoor
                var reducepercentForClient = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
                var reducepercentForProvider = /*disCountPercentFromDb.Percent +*/ query.SitePercent;

                double? finalePriceForClient = ((100 - reducepercentForClient) * query.PriceForNonNativeCustomer_baseService) / 100;
                double? finalePriceFirProvider = ((100 - reducepercentForProvider) * query.PriceForNonNativeCustomer_baseService) / 100;


                //این هارا بفرست برای درگاه بعد بگیر از درگاه
                //cuurent username
                //requestId,
                //finalePriceForClient
                //finalePriceFirProvider
                //main price == priceFornativcutomer
                //discountId,


                ///boro be dargah


                //********  این  4 خط را بعد از بازگشت از درگاه برو
                ////////////clientFromDB.WalletBalance = clientFromDB.WalletBalance - finalePriceForClient;
                ////////////providerFromDB.WalletBalance = providerFromDB.WalletBalance + finalePriceFirProvider;
                ////////////await AddTrasanctionForNonNative(query.requestFromDB, disCountPercentFromDb?.Id, finalePriceForClient, finalePriceFirProvider);
                ////////////await AddPackage(query.requestFromDB, disCountPercentFromDb?.Id, false, BuyiedPackageStatus.FromWallet, query.PriceForNonNativeCustomer_baseService, finalePriceForClient, query.SitePercent, query.requestFromDB.AllMessageCount_LimitedChat, BuyiedPackageType.PeriodedOrSessionChat, DateTime.Now.AddMonths(2));


            }

            //********  این  3 خط را بعد از بازگشت از درگاه برو
            //////////query.requestFromDB.HasPlan_LimitedChatVoice = true;
            //////////query.requestFromDB.ExpireTime_LimitedChatVoice = DateTime.Now.AddMonths(2);
            //////////query.requestFromDB.AllMessageCount_LimitedChat += (int)query.MessageCount_baseService;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_commonService.OkResponse(null, false));
            }
            catch
            {
                List<string> erros = new List<string> { _localizerShared["InternalServerMessage"].Value.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ApiBadRequestResponse(erros, 500));
            }
        }




        #endregion



        [NonAction]
        public async Task AddPackage(ServiceRequestTBL serviceRequest, int? discountId, bool isRenew, BuyiedPackageStatus buyiedPackageStatus, double? mainPrice, double? finalPrice, int sitePecent, int messageCount, BuyiedPackageType buyiedPackageType, DateTime expireTime)
        {
            var currentUserName = _accountService.GetCurrentUserName();
            var buyiedPackageTBL = new BuyiedPackageTBL()
            {
                IsRenewPackage = isRenew,
                CreateDate = DateTime.Now,
                ExpireTime = expireTime,
                BuyiedPackageStatus = buyiedPackageStatus,
                //Price = price,
                MainPrice = mainPrice,
                FinalPrice = finalPrice,
                SitePercent = sitePecent,
                MessgaeCount = messageCount,
                UserName = currentUserName,
                BuyiedPackageType = buyiedPackageType,
                ServiceRequestId = serviceRequest.Id,
                CheckDiscountId = discountId,

            };
            await _context.BuyiedPackageTBL.AddAsync(buyiedPackageTBL);
        }




        [NonAction]
        public async Task AddTrasanctionForNative(ServiceRequestTBL serviceRequest, int? discountId, double? amountForClient, double? amountForProvider)
        {
            var clientTransaction = new TransactionTBL()
            {
                Amount = amountForClient,
                Username = serviceRequest.ClienUserName,
                ProviderUserName = serviceRequest.ProvideUserName,
                ClientUserName = serviceRequest.ClienUserName,
                CreateDate = DateTime.Now,
                BaseMyServiceId = serviceRequest.BaseServiceId,
                ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceLimited,
                TransactionType = TransactionType.WhiteDrawl,
                TransactionStatus = TransactionStatus.ServiceTransaction,
                TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                CardTBL = null,
                CheckDiscountId = discountId,
                Description = $"deposit (as client of service) its (session Or Perioded) chat mesage provider=[${serviceRequest.ProvideUserName}] TransactionStatus=[${TransactionStatus.ServiceTransaction}]"
            };

            var providerTransaction = new TransactionTBL()
            {
                Amount = amountForProvider,
                Username = serviceRequest.ProvideUserName,
                ProviderUserName = serviceRequest.ProvideUserName,
                ClientUserName = serviceRequest.ClienUserName,
                CreateDate = DateTime.Now,
                BaseMyServiceId = serviceRequest.BaseServiceId,
                ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceLimited,
                TransactionType = TransactionType.Deposit,
                TransactionStatus = TransactionStatus.ServiceTransaction,
                TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                CardTBL = null,
                CheckDiscountId = discountId,
                Description = $"Withdrawal (as provider of service) its (session Or Perioded) mesage client=[${serviceRequest.ClienUserName}] TransactionStatus=[${TransactionStatus.ServiceTransaction}]"
            };

            await _context.TransactionTBL.AddAsync(providerTransaction);
            await _context.TransactionTBL.AddAsync(clientTransaction);
        }

        [NonAction]
        public async Task AddTrasanctionForNonNative(ServiceRequestTBL serviceRequest, int? discountId, double? amountForClient, double? amountForProvider)
        {
            var clientTransaction = new TransactionTBL()
            {
                Amount = amountForClient,
                Username = serviceRequest.ClienUserName,
                ProviderUserName = serviceRequest.ProvideUserName,
                ClientUserName = serviceRequest.ClienUserName,
                CreateDate = DateTime.Now,
                BaseMyServiceId = serviceRequest.BaseServiceId,
                ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceLimited,
                TransactionType = TransactionType.WhiteDrawl,
                TransactionStatus = TransactionStatus.ServiceTransaction,
                TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                CardTBL = null,
                CheckDiscountId = discountId,
                Description = $"deposit (as client of service) its (session Or Perioded) chat mesage provider=[${serviceRequest.ProvideUserName }] TransactionStatus=[${TransactionStatus.ServiceTransaction}]"
            };

            var providerTransaction = new TransactionTBL()
            {
                Amount = amountForProvider,
                Username = serviceRequest.ProvideUserName,
                ProviderUserName = serviceRequest.ProvideUserName,
                ClientUserName = serviceRequest.ClienUserName,
                CreateDate = DateTime.Now,
                BaseMyServiceId = serviceRequest.BaseServiceId,
                ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceLimited,
                TransactionType = TransactionType.Deposit,
                TransactionStatus = TransactionStatus.ServiceTransaction,
                TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                CardTBL = null,
                CheckDiscountId = discountId,
                Description = $"Withdrawal (as provider of service) its (session Or Perioded) mesage client=[${serviceRequest.ClienUserName}] TransactionStatus=[${TransactionStatus.ServiceTransaction}]"
            };

            await _context.TransactionTBL.AddAsync(providerTransaction);
            await _context.TransactionTBL.AddAsync(clientTransaction);
        }








        #region  buy Top 10 Package






        /// <summary>
        /// خرید بسته برای تاپ  تن شدن برای یک سرویس تایپ خواص
        /// </summary>
        /// <returns></returns>
        [HttpPost("BuyTopTenPackageWithWallet")]
        //[Authorize]
        [ClaimsAuthorize(IsAdmin = false)]
        public async Task<ActionResult> BuyTopTenPackageWithWallet([FromBody] BuyTopTenPackageDTO model)
        {
            var currentUsername = _accountService.GetCurrentUserName();
            TopTenPackageTBL topTenPackageFromDB = await _context.TopTenPackageTBL
                                                            .Where(c => c.Id == model.PackageId).FirstOrDefaultAsync();
            if (topTenPackageFromDB == null)
            {
                List<string> erros = new List<string> { _localizerShared["NotFound"].Value.ToString() };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            var isEnabled = await _context.ServiceTBL.AsNoTracking()
                                        .Where(c => c.Id == topTenPackageFromDB.ServiceId)
                                        .Select(c => c.IsEnabled)
                                        .FirstOrDefaultAsync();
            if (!isEnabled)
            {
                List<string> erros = new List<string> { "this service category is not Enabled" };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            //آیا شما بسته فعال دارید یا نه
            bool exsist = await _context.User_TopTenPackageTBL
                                .AsNoTracking()
                                .AnyAsync(x => x.ServiceId == topTenPackageFromDB.ServiceId
                                && x.UserName.ToLower() == currentUsername &&
                                x.ExpireTime < DateTime.Now);
            if (exsist)
            {
                List<string> erros = new List<string> { "you have active package for top ten" };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            int reservedPackaged = await _context.User_TopTenPackageTBL.AsNoTracking()
                                                   .CountAsync(c => c.TopTenPackageId == topTenPackageFromDB.Id && c.ExpireTime > DateTime.Now);

            //ظزفیت بسته پر شده 
            if (reservedPackaged >= topTenPackageFromDB.Count)
            {
                List<string> erros = new List<string> { "This package is over" };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            //آیا سرویسی در این دسته بندی که این پکیج هسن دارید که میخواید بخرید؟؟
            var haveService = await _context.BaseMyServiceTBL.AsNoTracking().AnyAsync(c => c.ServiceId == topTenPackageFromDB.ServiceId && c.UserName == currentUsername);
            if (!haveService)
            {
                List<string> erros = new List<string> { "You have not registered any services in this category" };
                return BadRequest(new ApiBadRequestResponse(erros));
            }

            var disCountPercentFromDb = await _context.CheckDiscountTBL.Where(c => c.Code == model.DisCountCode)
                                       .Select(c => new DiscountVM { Percent = c.Percent, Id = c.Id, ExpireTime = c.ExpireTime, ServiceId = c.ServiceId })
                                       .AsNoTracking().FirstOrDefaultAsync();

            #region validaton discountCode

            //آیا کد تخفیف معتبر است یا نه
            if (!string.IsNullOrEmpty(model.DisCountCode) && (disCountPercentFromDb == null || disCountPercentFromDb.ExpireTime > DateTime.Now
                || disCountPercentFromDb.ServiceId != topTenPackageFromDB.ServiceId /*query.ServiceTBLId*/))
            {
                List<string> errors = new List<string>();
                errors.Add(_localizerShared["InvalidDiscountCode"].Value.ToString());
                return BadRequest(new ApiBadRequestResponse(errors));
            }
            #endregion

            #region getClient abd Provider from DB
            AppUser userFromDB = await _context.Users
                          .Where(c => c.UserName == currentUsername).FirstOrDefaultAsync();
            #endregion

            #region validate Wallet
            var waletRes = _paymentService.ValidateTopTenWallet(userFromDB, topTenPackageFromDB.Price, disCountPercentFromDb);
            if (waletRes.succsseded == false)
                return BadRequest(new ApiBadRequestResponse(waletRes.result));
            #endregion

            int reducepercent = disCountPercentFromDb != null ? disCountPercentFromDb.Percent : 0;
            double? finalePrice = ((100 - reducepercent) * topTenPackageFromDB.Price) / 100;
            userFromDB.WalletBalance = userFromDB.WalletBalance - finalePrice;

            TransactionTBL transactoin = new TransactionTBL()
            {
                Amount = finalePrice,
                Username = currentUsername,
                ProviderUserName = null,
                ClientUserName = null,
                CreateDate = DateTime.Now,
                BaseMyServiceId = null,
                ServiceTypeWithDetails = null,
                TransactionType = TransactionType.WhiteDrawl,
                TransactionStatus = TransactionStatus.TopTenPackage,
                TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                CardTBL = null,
                CheckDiscountId = disCountPercentFromDb?.Id,
                Description = $"WhiteDrawl  (it is transaction is for buying topTen package)  TransactionStatus=[${TransactionStatus.TopTenPackage}]"
            };

            User_TopTenPackageTBL userTopTenPackage = new User_TopTenPackageTBL()
            {
                ServiceId = topTenPackageFromDB.ServiceId,
                TopTenPackageTBL = topTenPackageFromDB,
                UserName = currentUsername,
                TransactionTBL = transactoin,
                ExpireTime = _paymentService.returnTopTenExpireTime(topTenPackageFromDB),
                CreateDate = DateTime.Now
            };

            await _context.TransactionTBL.AddAsync(transactoin);
            await _context.User_TopTenPackageTBL.AddAsync(userTopTenPackage);


            await _context.SaveChangesAsync();
            return Ok(_commonService.OkResponse(null, false));

        }





        #endregion 



    }
}
