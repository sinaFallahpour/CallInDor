using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// <summary>
    ///   بسته خریداری شده برای یک ریکوست
    /// </summary>
    [Table("BuyiedPackage")]
    public class BuyiedPackageTBL : BaseEntity<int>
    {
        /// <summary>
        /// این پکیج را چه کاربری خریده
        /// </summary>
        public string UserName { get; set; }



        /// <summary>
        /// نحوه پرداخت شدن این پکیج
        /// </summary>
        public BuyiedPackageStatus BuyiedPackageStatus { get; set; }


        /// <summary>
        /// قیمت اولیه خود پکیج بدون درصد سایت و کارت تخفیف
        /// </summary>
        public double? MainPrice { get; set; }


        /// <summary>
        /// قیمت نهایی
        /// قیمت این بسته که برای یک درخواست از یک سرویس خواست است
        /// ثبمت نهایی مه پرداخت شده برای پگیج
        /// </summary>
        public double? FinalPrice { get; set; }


        /// <summary>
        /// درصدی که سایت برای خودش گرفت در
        /// درصدی پروایدر این پکیج به سایت میدهد   
        ///      
        /// </summary>
        public int SitePercent { get; set; }


        /// <summary>
        /// این بسته برای کدام نوع از سرویس هاست
        /// </summary>
        public BuyiedPackageType BuyiedPackageType { get; set; }


        /// <summary>
        /// آیا این پکیج تمدید است؟
        /// </summary>
        public bool IsRenewPackage { get; set; }


        /// <summary>
        /// اگر بسته از نوع خرید تعدادی پیام بود تعداد پیام
        /// </summary>
        public int MessgaeCount { get; set; }


        /// <summary>
        /// تاریخ پایان پیام
        /// </summary>
        public DateTime ExpireTime { get; set; }




        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        public DateTime CreateDate { get; set; }


        #region  Relation 


        //کارت تخفیف که استفاده شده
        public int? CheckDiscountId { get; set; }

        [ForeignKey("CheckDiscountId")]
        public CheckDiscountTBL CheckDiscountTBL { get; set; }





        //public int? BaseMyServiceId { get; set; }

        //[ForeignKey("BaseMyServiceId")]
        //public BaseMyServiceTBL BaseMyServiceTBL { get; set; }


        public int? ServiceRequestId { get; set; }

        [ForeignKey("ServiceRequestId")]
        public ServiceRequestTBL ServiceRequestTBL { get; set; }



        #endregion






    }
}
