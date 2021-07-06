using Domain;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parbad;
using Service.Interfaces.Account;
using Service.Interfaces.Common;
using Service.Interfaces.Payment;
using Service.Interfaces.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallInDoor.Controllers
{
    public class VerifyController : Controller
    {

        #region ctor
        private readonly DataContext _context;
        private readonly IPaymentService _paymentService;
        private readonly IOnlinePayment _onlinePayment;

        private readonly IAccountService _accountService;

        private readonly ICommonService _commonService;
        //private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;

        public VerifyController(
            DataContext context,
            IPaymentService paymentService,
            IOnlinePayment onlinePayment,
              IAccountService accountService,
              ICommonService commonService,
             //IStringLocalizer<ShareResource> localizerShared,
             IResourceServices resourceServices
            )
        {
            _context = context;
            _paymentService = paymentService;
            _onlinePayment = onlinePayment;
            _commonService = commonService;
            _accountService = accountService;
            //_localizerShared = localizerShared;
            _resourceServices = resourceServices;
        }

        #endregion





        public async Task<IActionResult> Verify()
        {
            var error = new List<string>();
            try
            {
                var invoice = await _onlinePayment.FetchAsync();


                var result = await _onlinePayment.VerifyAsync(invoice);
                var payment = _context.PaymentTBL.FirstOrDefault(x => x.TrackingNumber == result.TrackingNumber.ToString());
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == payment.UserName);

                if (user == null)
                {
                    payment.IsSucceed = false;
                    payment.TransactionCode = result.TransactionCode;
                    payment.ErrorDescription = result.Message;
                    await _context.SaveChangesAsync();
                    error.Add(payment.ErrorDescription);
                    var url = "http://App.callInDoor.ir/payment/success?trackingnumber=" + payment.TrackingNumber;
                    return Redirect(url);
                }

                if (result.Amount == payment.Amount && result.IsSucceed == true)
                {
                    user.WalletBalance += result.Amount;
                    payment.IsSucceed = true;
                    payment.TransactionCode = result.TransactionCode;
                    payment.ErrorDescription = result.Message;

                    var transaction = new TransactionTBL()
                    {
                        CreateDate = DateTime.Now,
                        Description = $"deposit its transactionStatus=[${TransactionStatus.NormalTransaction}]",
                        Amount = payment.Amount,
                        PaymentId = payment.Id,
                        TransactionStatus = TransactionStatus.NormalTransaction,
                        TransactionType = TransactionType.Deposit,
                        TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                        PaymentTBL = payment,
                        Username = user.UserName,
                        //CheckDiscountId = payment.CheckDiscountID,
                        ServiceTypeWithDetails = "",

                    };
                    await _context.TransactionTBL.AddAsync(transaction);
                    await _context.SaveChangesAsync();

                    var url = "http://App.callInDoor.ir/payment/success?trackingnumber=" + payment.TrackingNumber;
                    return Redirect(url);
                }
                else
                {
                    payment.IsSucceed = false;
                    payment.TransactionCode = result.TransactionCode;
                    payment.ErrorDescription = result.Message;
                    await _context.SaveChangesAsync();

                    error.Add(payment.ErrorDescription);
                    return Redirect("http://App.callInDoor.ir/payment/failure?trackingnumber=" + payment.TrackingNumber);
                }
            }
            catch
            {
                return NotFound(_commonService.NotFoundErrorReponse(false));
            }




            //return View();
        }




        //[AllowAnonymous]
        //[HttpPost("Verify")]

        //public async Task<IActionResult> Verify()
        //{
        //    var error = new List<string>();
        //    try
        //    {
        //        var invoice = await _onlinePayment.FetchAsync();


        //        var result = await _onlinePayment.VerifyAsync(invoice);
        //        var payment = _context.PaymentTBL.FirstOrDefault(x => x.TrackingNumber == result.TrackingNumber.ToString());
        //        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == payment.UserName);

        //        if (user == null)
        //        {
        //            payment.IsSucceed = false;
        //            payment.TransactionCode = result.TransactionCode;
        //            payment.ErrorDescription = result.Message;
        //            await _context.SaveChangesAsync();
        //            error.Add(payment.ErrorDescription);
        //            var url = "http://App.callInDoor.ir/payment/success?trackingnumber=" + payment.TrackingNumber;
        //            return Redirect(url);
        //        }

        //        if (result.Amount == payment.Amount && result.IsSucceed == true)
        //        {
        //            user.WalletBalance += result.Amount;
        //            payment.IsSucceed = true;
        //            payment.TransactionCode = result.TransactionCode;
        //            payment.ErrorDescription = result.Message;

        //            var transaction = new TransactionTBL()
        //            {
        //                CreateDate = DateTime.Now,
        //                Description = $"deposit its transactionStatus=[${TransactionStatus.NormalTransaction}]",
        //                Amount = payment.Amount,
        //                PaymentId = payment.Id,
        //                TransactionStatus = TransactionStatus.NormalTransaction,
        //                TransactionType = TransactionType.Deposit,
        //                TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
        //                PaymentTBL = payment,
        //                Username = user.UserName,
        //                //CheckDiscountId = payment.CheckDiscountID,
        //                ServiceTypeWithDetails = "",

        //            };
        //            await _context.TransactionTBL.AddAsync(transaction);
        //            await _context.SaveChangesAsync();

        //            var url = "http://App.callInDoor.ir/payment/success?trackingnumber=" + payment.TrackingNumber;
        //            return Redirect(url);
        //        }
        //        else
        //        {
        //            payment.IsSucceed = false;
        //            payment.TransactionCode = result.TransactionCode;
        //            payment.ErrorDescription = result.Message;
        //            await _context.SaveChangesAsync();

        //            error.Add(payment.ErrorDescription);
        //            return Redirect("http://App.callInDoor.ir/payment/failure?trackingnumber=" + payment.TrackingNumber);
        //        }
        //    }
        //    catch
        //    {
        //        return NotFound(_commonService.NotFoundErrorReponse(false));
        //    }

        //}






    }
}
