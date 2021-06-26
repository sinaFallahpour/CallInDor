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

        ////[Key]
        ////[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        ////public int Id { get; set; }



        /// <summary>
        ///   نوع  چت  و پولی که با ازای زمان از حسابش کم میشه   با این معلوم میشه
        /// </summary>
        public PackageType? PackageType { get; set; }




        /// <summary>
        /// این فیلد فقط و فقط مخصوص سرویس هایی است که از نوع ترنسلیت هستند
        /// </summary>
        public bool BeTranslate { get; set; }



        /// <summary>
        /// تعداد مسیج هایی  رایگانی که برای مشتری در نظر میگیرد
        /// </summary>
        public int? FreeMessageCount { get; set; }


        /// <summary>
        /// زمان جلسه
        ///اگر آزاد نبود  این مقدار کیگیرد  
        /// </summary>
        public int? Duration { get; set; }



        /// <summary>
        ///  تعداد پیام
        ///اگر آزاد نبود این مقدار میگیرد  
        /// </summary>
        public int? MessageCount { get; set; }


        /// <summary>
        /// اگر  این را تیک زد باید تا 8 ساعت بعد از درخواست کاربر 
        /// به او پاسخ دهد در غیر این صورت جریمه میشود
        /// </summary>
        public bool IsServiceReverse { get; set; }


        ///////////// <summary>
        ///////////// قیمت برای کاربران محلی یا هم کشور
        ///////////// </summary>
        ///////////// 
        //////////[Range(0, double.MaxValue)]
        //////////public double? PriceForNativeCustomer { get; set; }


        ///////////// <summary>
        ///////////// قیمت برای کاربران غیر محلی یاغیر  هم کشور
        ///////////// </summary>
        //////////[Range(0, double.MaxValue)]
        //////////public double? PriceForNonNativeCustomer { get; set; }





        #region Relation

        //[ForeignKey("UserName")]
        //public AppUser User { get; set; }

        //public string UserId { get; set; }


        //[ForeignKey("CatId")]
        //public virtual CategoryTBL CategoryTBL { get; set; }

        //public int? CatId { get; set; }


        //[ForeignKey("SubCatId")]
        //public virtual CategoryTBL SubCategoryTBL { get; set; }

        //public int? SubCatId { get; set; }


        // relation With BaseMyChatTBL
        public BaseMyServiceTBL BaseMyChatTBL { get; set; }

        [ForeignKey("BaseMyChatTBL")]
        public int? BaseId { get; set; }


        #endregion 

    }
}