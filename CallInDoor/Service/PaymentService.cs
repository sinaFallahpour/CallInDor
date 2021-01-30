using Domain;
using Domain.DTO.CheckDiscount;
using Domain.DTO.RequestService;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Account;
using Service.Interfaces.Payment;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PaymentService : IPaymentService
    {

        private readonly DataContext _context;
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IAccountService _accountService;


        public PaymentService(
            DataContext context,
            IStringLocalizer<ShareResource> localizerShared,
            IAccountService accountService)
        {
            _context = context;
            _localizerShared = localizerShared;
            _accountService = accountService;
        }



        public async Task<(bool succsseded, List<string> result)> ValidateBuyPackage(ServiceRequestTBL model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            if (model == null)
            {
                string err = _localizerShared["NotFound"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
                return (IsValid, Errors);
            }

            if (model.IsLimitedChat == false)
            {
                string err = _localizerShared["InvalidAttamp"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            if (model.ServiceType == ServiceType.Service || model.ServiceType == ServiceType.Course)
            {
                string err = _localizerShared["InvalidAttamp"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            if (model.PackageType == PackageType.Free)
            {
                string err = _localizerShared["InvalidAttamp"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            if (await HasPackage(model.Id))
            {
                string err = "You have plan";
                IsValid = false;
                Errors.Add(err);
            }

            if (model.HasPlan_LimitedChatVoice && (model.ExpireTime_LimitedChatVoice < DateTime.Now /*!model.IsExpired_LimitedChatVoice*/ && model.AllMessageCount_LimitedChat - model.UsedMessageCount_LimitedChat != 0))
            {
                string err = _localizerShared["YouCurrentlyHaveAnActivePackage"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            return (IsValid, Errors);
        }




        /// <summary>
        /// ولیدت کردن پول برای خرید پکیج هایه سرویسی مثل چت و...
        /// </summary>
        /// <param name="clientFromDB"></param>
        /// <param name="isNative"></param>
        /// <param name="SitePercent"></param>
        /// <param name="PriceForNativeCustomer"></param>
        /// <param name="PriceForNonNativeCustomer"></param>
        /// <param name="disCountPercentFromDb"></param>
        /// <returns></returns>
        public (bool succsseded, List<string> result) ValidateWallet(AppUser clientFromDB, bool isNative, int SitePercent, double? PriceForNativeCustomer, double? PriceForNonNativeCustomer, DiscountVM disCountPercentFromDb)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            if (clientFromDB.WalletBalance <= 0)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                return (IsValid, Errors);
            }



            if (isNative)
            {

                var discountPercent = disCountPercentFromDb == null ? 0 : disCountPercentFromDb.Percent;

                var reducepercent = discountPercent /*+ SitePercent*/;
                var calculatedBalanc = ((100 - reducepercent) * PriceForNativeCustomer) / 100;

                if (clientFromDB.WalletBalance < calculatedBalanc)
                {
                    IsValid = false;
                    Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                    return (IsValid, Errors);
                }
                if (clientFromDB.WalletBalance - calculatedBalanc <= 0)
                {
                    IsValid = false;
                    Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                    return (IsValid, Errors);
                }
            }
            else
            {
                var discountPercent = disCountPercentFromDb == null ? 0 : disCountPercentFromDb.Percent;
                var reducepercent = discountPercent;
                var calculatedBalanc = ((100 - reducepercent) * PriceForNonNativeCustomer) / 100;

                if (clientFromDB.WalletBalance < calculatedBalanc)
                {
                    IsValid = false;
                    Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                    return (IsValid, Errors);
                }
                if (clientFromDB.WalletBalance - calculatedBalanc <= 0)
                {
                    IsValid = false;
                    Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                    return (IsValid, Errors);
                }
            }

            return (IsValid, Errors);
        }








        /// <summary>
        /// ولیدیت کردن پول برای خرید تاپ تن
        /// </summary>
        /// <param name="userFromDB"></param>
        /// <param name="sitePercent"></param>
        /// <param name="price"></param>
        /// <param name="disCountPercentFromDb"></param>
        /// <returns></returns>
        public (bool succsseded, List<string> result) ValidateTopTenWallet(AppUser userFromDB, double? price, DiscountVM disCountPercentFromDb)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            if (userFromDB.WalletBalance <= 0)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                return (IsValid, Errors);
            }

            var discountPercent = disCountPercentFromDb == null ? 0 : disCountPercentFromDb.Percent;

            var reducepercent = discountPercent;
            var calculatedBalanc = ((100 - reducepercent) * price) / 100;

            if (userFromDB.WalletBalance < calculatedBalanc)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                return (IsValid, Errors);
            }
            if (userFromDB.WalletBalance - calculatedBalanc <= 0)
            {
                IsValid = false;
                Errors.Add(_localizerShared["NotEnoughtBalance"].Value.ToString());
                return (IsValid, Errors);
            }

            return (IsValid, Errors);
        }



        public async Task<(bool succsseded, List<string> result)> ValidateRenewPackage(RequestWithBaseseServeicVM model)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();

            if (model == null)
            {
                string err = _localizerShared["NotFound"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
                return (IsValid, Errors);
            }

            if (model.requestFromDB.IsLimitedChat == false)
            {
                string err = _localizerShared["InvalidAttamp"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            if (model.requestFromDB.ServiceType == ServiceType.Service || model.requestFromDB.ServiceType == ServiceType.Course)
            {
                string err = _localizerShared["InvalidAttamp"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            if (model.requestFromDB.PackageType == PackageType.Free)
            {
                string err = _localizerShared["InvalidAttamp"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            if (await HasPackage(model.requestFromDB.Id) == false)
            {
                //string err = _localizerShared["InvalidAttamp"].Value.ToString();
                string err = "you dont have package  up to now  then you cant renew this ";
                IsValid = false;
                Errors.Add(err);
            }

            if (model.requestFromDB.HasPlan_LimitedChatVoice && (model.requestFromDB.ExpireTime_LimitedChatVoice < DateTime.Now /*!model.IsExpired_LimitedChatVoice*/ && model.requestFromDB.AllMessageCount_LimitedChat - model.requestFromDB.UsedMessageCount_LimitedChat != 0))
            {
                string err = _localizerShared["YouCurrentlyHaveAnActivePackage"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            if (model.requestFromDB.HasPlan_LimitedChatVoice && (model.requestFromDB.ExpireTime_LimitedChatVoice < DateTime.Now /*!model.IsExpired_LimitedChatVoice*/ && model.requestFromDB.AllMessageCount_LimitedChat - model.requestFromDB.UsedMessageCount_LimitedChat != 0))
            {
                string err = _localizerShared["YouCurrentlyHaveAnActivePackage"].Value.ToString();
                IsValid = false;
                Errors.Add(err);
            }

            //آیا پروایدر این سرویس را حذف کرده یا نه
            if (model.IsDeleted_baseService)
            {
                string err = "این سرویس دیگر موجود نیست";
                IsValid = false;
                Errors.Add(err);
            }


            //آیا کاربر پ.ل دارد این را بخرد یا نه




            return (IsValid, Errors);
        }



        public DateTime returnTopTenExpireTime(TopTenPackageTBL topTenPackageFromDB)
        {

            DateTime expiretime = DateTime.Now;
            if (topTenPackageFromDB.DayCount != null)
                expiretime = expiretime.AddDays((int)topTenPackageFromDB.DayCount);
            if (topTenPackageFromDB.HourCount != null)
                expiretime = expiretime.AddHours((int)topTenPackageFromDB.HourCount);
            return expiretime;
        }


        public async Task<bool> HasPackage(int requestId)
        {
            var currentUserName = _accountService.GetCurrentUserName();
            var hasPlan = await _context.ServiceRequestTBL.AsNoTracking()
                .AnyAsync(c => c.Id == requestId && c.ClienUserName == currentUserName && c.HasPlan_LimitedChatVoice == true );
            //bool exist = await _context.BuyiedPackageTBL.AsNoTracking().AnyAsync(c => c.ServiceRequestId == requestId && c.UserName == currentUserName);
            return hasPlan;

        }

        public bool IsPersianLanguage()
        {
            if (CultureInfo.CurrentCulture.Name == PublicHelper.persianCultureName)
                return true;
            return false;
        }


    }
}
