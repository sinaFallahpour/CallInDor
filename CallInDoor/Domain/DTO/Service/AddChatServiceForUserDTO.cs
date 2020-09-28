using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class AddChatServiceForUserDTO
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        //[MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        //[MaxLength(10, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }






        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "ServiceName")]
        public string ServiceName { get; set; }


        /// <summary>
        ///   نوع  چت  و پولی که با ازای زمان از حسابش کم میشه   با این معلوم میشه
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "PackageType")]
        public PackageType PackageType { get; set; }


        /// <summary>
        /// این فیلد فقط و فقط مخصوص سرویس هایی است که از نوع ترنسلیت هستند
        /// </summary>
        //[Required(ErrorMessage = "{0} is  Required")]
        public bool BeTranslate { get; set; }



        /// <summary>
        /// تعداد مسیج هایی  رایگانی که برای مشتری در نظر میگیرد
        /// </summary>
        //////////[Required(ErrorMessage = "{0} is  Required")]
        //////////[Display(Name = "FreeMessageCount")]

        public int FreeMessageCount { get; set; }


        /// <summary>
        /// اگر  این را تیک زد باید تا 8 ساعت بعد از درخواست کاربر 
        /// به او پاسخ دهد در غیر این صورت جریمه میشود
        /// </summary>
        //[Required(ErrorMessage = "{0} is  Required")]
        public bool IsServiceReverse { get; set; }


        /// <summary>
        /// قیمت برای کاربران محلی یا هم کشور
        /// </summary>
        //////[Required(ErrorMessage = "{0} is  Required")]
        //////[Display(Name = "PriceForNativeCustomer")]
        public double PriceForNativeCustomer { get; set; }


        /// <summary>
        /// قیمت برای کاربران غیر محلی یاغیر  هم کشور
        /// </summary>
        ////////////[Required(ErrorMessage = "{0} is  Required")]
        ////////////[Display(Name = "PriceForNonNativeCustomer")]
        public double PriceForNonNativeCustomer { get; set; }


        #region  دیفالت بین همشونه


        /// <summary>
        ///  تاریخ درج
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// vide or chat or seice or course ,...
        /// </summary>
        //////////[Required(ErrorMessage = "{0} is  Required")]
        //////////[Display(Name = "ServiceType")]
        public ServiceType ServiceType { get; set; }



        ///// <summary>
        /////آیا ادمین چک کرده
        ///// </summary>
        //public bool IsCheckedByAdmin { get; set; }


        ///// <summary>
        ///// وضعیت تایید سرویس بوسیله ادمین
        ///// </summary>
        //public ConfirmedServiceType ConfirmedServiceType { get; set; }


        #endregion



        #region  Relation 

        //public string UserId { get; set; }

        public int? CatId { get; set; }

        public int? SubCatId { get; set; }

        #endregion







    }
}
