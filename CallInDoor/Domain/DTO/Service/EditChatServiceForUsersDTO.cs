﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class EditChatServiceForUsersDTO
    {

        public int? Id { get; set; }

        //[Required(ErrorMessage = "{0} is  Required")]
        //[MaxLength(30, ErrorMessage = "The maximum {0} length is {1} characters")]
        //[Display(Name = "UserName")]
        //public string UserName { get; set; }


        //[Required(ErrorMessage = "{0} is  Required")]
        //////////////[Range(-90, +90, ErrorMessage = "The  {0} should be between {1} and {2}")]
        //////////////[Display(Name = "Latitude")]
        //////////////public double? Latitude { get; set; }



        ////////////////[Required(ErrorMessage = "{0} is  Required")]
        //////////////[Range(-180, +180, ErrorMessage = "The  {0} should be between {1} and {2}")]
        //////////////[Display(Name = "Longitude")]
        //////////////public double? Longitude { get; set; }




        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "ServiceName")]
        public string ServiceName { get; set; }


        ///// <summary>
        ///// نوع  چت  و پولی که با ازای زمان از حسابش کم میشه   با این معلوم میشه
        ///// </summary>
        //[Required(ErrorMessage = "{0} is  Required")]
        //[Display(Name = "PackageType")]
        //public PackageType? PackageType { get; set; }


        /// <summary>
        /// این فیلد فقط و فقط مخصوص سرویس هایی است که از نوع ترنسلیت هستند
        /// </summary>
        public bool BeTranslate { get; set; }



        /// <summary>
        /// تعداد مسیج هایی رایگانی که برای مشتری در نظر میگیرد
        /// </summary>
        //[Required(ErrorMessage = "{0} is  Required")]
        [Range(0, int.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "FreeMessageCount")]
        public int? FreeMessageCount { get; set; }




        /// <summary>
        /// تعداد پیام
        /// فقط برای چت وویس ها 
        /// نه ویدیو کال یا وویس کال
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "MessageCount")]
        public int? MessageCount { get; set; }



        ///// <summary>
        ///// برحسب دقیقه
        ///// </summary>
        //[Range(0, int.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        //[Display(Name = "Duration")]
        //public int? Duration { get; set; }





        /// <summary>
        ///مدت زمان برای ویدیو کال یا وویس کال
        /// این زمانی مقدار میگیرد اگه سرویس تایپ 
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "Duration")]
        public int? Duration { get; set; }




        /// <summary>
        /// اگر  این را تیک زد باید تا 8 ساعت بعد از درخواست کاربر 
        /// به او پاسخ دهد در غیر این صورت جریمه میشود
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        //[Display(Name = "IsServiceReverse")]
        public bool IsServiceReverse { get; set; }


        /////// <summary>
        /////// قیمت برای کاربران محلی یا هم کشور
        /////// </summary>
        ////[Required(ErrorMessage = "{0} is  Required")]
        //[Range(0, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        //[Display(Name = "PriceForNativeCustomer")]
        //public double? PriceForNativeCustomer { get; set; }


        /////// <summary>
        /////// قیمت برای کاربران غیر محلی یاغیر  هم کشور
        /////// </summary>
        ////[Required(ErrorMessage = "{0} is  Required")]
        //////[Range(0, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        //[Range(0, double.MaxValue, ErrorMessage = "The {0} should be between {1} and {2}")]
        //[Display(Name = "PriceForNonNativeCustomer")]
        //public double? PriceForNonNativeCustomer { get; set; }


        ///// <summary>
        ///// قیمت برای کاربران غیر محلی یاغیر  هم کشور
        ///// </summary>
        //[Required(ErrorMessage = "{0} is  Required")]
        ////[Range(0, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Range(0, double.MaxValue, ErrorMessage = "The {0} should be between {1} and {2}")]
        [Display(Name = "Price")]
        public double? Price { get; set; }



        //public bool IsActive { get; set; }

        #region  دیفالت بین همشونه




        ///// <summary>
        ///// vide or chat or seice or course ,...
        ///// </summary>
        //[Required(ErrorMessage = "{0} is  Required")]
        //[Display(Name = "ServiceType")]
        //public ServiceType? ServiceType { get; set; }




        #endregion


        //public string UserId { get; set; }

        //public int? CatId { get; set; }

        //public int? SubCatId { get; set; }


        //[Required(ErrorMessage = "{0} is  Required")]
        //[Display(Name = "Service")]
        //public int? ServiceId { get; set; }



    }
}
