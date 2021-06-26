using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    ///  جدول تراکنش ها
    [Table("Transaction")]
    public class TransactionTBL : BaseEntity<int>
    {


        /// <summary>
        /// این تراکنش برای چه کسی است
        /// </summary>
        public string Username { get; set; }


        //مبلغ تراکنش شده
        [Required]
        [Range(0, double.MaxValue)]
        public double? Amount { get; set; }


        public DateTime CreateDate { get; set; }///===========



        [MaxLength(500)]
        public string Description { get; set; }///============================


        public TransactionStatus TransactionStatus { get; set; }


        public TransactionType TransactionType { get; set; }


        ///// <summary>
        ///// اگر این تارکنش سرویسی بود حالا از کدام نوع سرویسی است
        ///// </summary>
        //public ServiceTypeWithDetails? ServiceTypeWithDetails { get; set; }

        /// <summary>
        /// اگر این تارکنش سرویسی بود حالا از کدام نوع سرویسی است
        /// </summary>
        public string ServiceTypeWithDetails { get; set; }



        ///اگر ترا کنش از نوع سرویس بود  حالا یوز نیم پروابدر
        public string ProviderUserName { get; set; }

        ///اگر ترا کنش از نوع سرویس بود  حالا یوز نیم کلاینت 
        public string ClientUserName { get; set; }




        #region Relation


        [ForeignKey("BaseMyServiceId")]
        public BaseMyServiceTBL BaseMyServiceTBL { get; set; }
        public int? BaseMyServiceId { get; set; }

        public TransactionConfirmedStatus TransactionConfirmedStatus { get; set; }



        /// <summary>
        /// به کدام کارت واریز یا برداشت شود
        /// </summary>
        [ForeignKey("CardId")]
        public CardTBL CardTBL { get; set; }
        public int? CardId { get; set; }





        /// <summary>
        ///  کد تخفیف  که این تراکنش استفاده میکند
        /// </summary>
        [ForeignKey("CheckDiscountId")]
        public CheckDiscountTBL CheckDiscountTBL { get; set; }
        public int? CheckDiscountId { get; set; }






        /// <summary>
        ///  این فیلد زمانی مفدار میگیره که تراکنش ها از نوع تاب تن باشد
        ///این تراکنش برای کدوم یک از بسته هایه خریداری شده برای تاپ تن است
        /// </summary>
        [ForeignKey("User_TopTenPackageId")]
        public User_TopTenPackageTBL User_TopTenPackageTBL { get; set; }
        public int? User_TopTenPackageId { get; set; }


        /// <summary>
        ///  اگر ترکنش از جنس رفتن به درگاه شارژه کیف پول بود این مقدار میگیره
        /// </summary>
        [ForeignKey("PaymentId")]
        public PaymentTBL PaymentTBL { get; set; }
        public int? PaymentId { get; set; }





        #endregion


    }
}
