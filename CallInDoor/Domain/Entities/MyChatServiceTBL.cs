using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    /// <summary>
    ///است  منظور از این تیبل فقط سرویس های  چت نیست این هم سرویس چت و هم سرویس وویس و هم سرویس  ویدیو 
    /// </summary>
    [Table("MyChatService")]
    public class MyChatServiceTBL : BaseEntity<int>
    {

        [MaxLength(200)]
        public string ServiceName { get; set; }

        /// <summary>
        ///   نوع  چت  و پولی که با ازای زمان از حسابش کم میشه   با این معلوم میشه
        /// </summary>
        public PackageType PackageType { get; set; }


        public string  UserName { get; set; }

        /// <summary>
        /// این فیلد فقط و فقط مخصوص سرویس هایی است که از نوع ترنسلیت هستند
        /// </summary>
        public bool BeTranslate { get; set; }



        /// <summary>
        /// تعداد مسیج هایی  رایگانی که برای مشتری در نظر میگیرد
        /// </summary>
        public int FreeMessageCount { get; set; }


        /// <summary>
        /// اگر  این را تیک زد باید تا 8 ساعت بعد از درخواست کاربر 
        /// به او پاسخ دهد در غیر این صورت جریمه میشود
        /// </summary>
        public bool IsServiceReverse { get; set; }


        /// <summary>
        /// قیمت برای کاربران محلی یا هم کشور
        /// </summary>
        public double PriceForNativeCustomer { get; set; }


        /// <summary>
        /// قیمت برای کاربران غیر محلی یاغیر  هم کشور
        /// </summary>
        public double PriceForNonNativeCustomer { get; set; }






        #region  دیفالت بین همشونه


        /// <summary>
        ///  تاریخ درج
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// vide or chat or seice or course ,...
        /// </summary>
        public ServiceType ServiceType { get; set; }



        /// <summary>
        ///آیا ادمین چک کرده
        /// </summary>
        public bool IsCheckedByAdmin { get; set; }


        /// <summary>
        /// وضعیت تایید سرویس بوسیله ادمین
        /// </summary>
        public ConfirmedServiceType ConfirmedServiceType { get; set; }


        #endregion  

        #region Relation

        //[ForeignKey("UserName")]
        //public AppUser User { get; set; }

        //public string UserId { get; set; }


        [ForeignKey("CatId")]
        public virtual CategoryTBL CategoryTBL { get; set; }

        public int? CatId { get; set; }


        [ForeignKey("SubCatId")]
        public virtual CategoryTBL SubCategoryTBL { get; set; }

        public int? SubCatId { get; set; }


        #endregion 

    }
}
