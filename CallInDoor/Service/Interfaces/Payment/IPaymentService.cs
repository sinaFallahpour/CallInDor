using Domain.DTO.CheckDiscount;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Payment
{
    public interface IPaymentService
    {
        //(bool succsseded, List<string> result) ValidateBuyPackage(ServiceRequestTBL model);

        Task<(bool succsseded, List<string> result)> ValidateBuyPackage(ServiceRequestTBL model);

        /// ولیدت کردن پول برای خرید پکیج هایه سرویسی مثل چت و...
        (bool succsseded, List<string> result) ValidateWallet(AppUser clientFromDB, bool isNative, int SitePercent, double? PriceForNativeCustomer, double? PriceForNonNativeCustomer, DiscountVM disCountPercentFromDb);

        /// ولیدیت کردن پول برای خرید تاپ تن
        public (bool succsseded, List<string> result) ValidateTopTenWallet(AppUser userFromDB, double? price, DiscountVM disCountPercentFromDb);

        DateTime returnTopTenExpireTime(TopTenPackageTBL topTenPackageFromDB);

        bool IsPersianLanguage();


    }
}
