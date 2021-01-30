﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum TransactionStatus
    {

        [Description("تکمیل پروفایل")]
        [Display(Name = "تکمیل پروفایل")]
        CompleteProfileTransaction,


        /// <summary>
        /// این برای زمانیست که که کاربر فقط بخواد از کیفش پول,پول بگیره یا بریزه 
        /// نه برای خرید یا فروش سرویس ها
        /// </summary>
        [Description("تراکنش معمولی")]
        [Display(Name = "تراکنش معمولی")]
        NormalTransaction,


        /// <summary>
        ///این برای زمانیست که کاربر یک سرویس فروخت یا سرویس خرید 
        /// </summary>
        [Description("تراکنش خرید و فروش سرویس")]
        [Display(Name = "تراکنش خرید و فروش سرویس")]
        ServiceTransaction,

        //زمانی که تراکنش از نوع خرید پکیج تاپ تن باشد
        [Description("تراکنش خرید پکیج تاپ تن")]
        [Display(Name = "تراکنش خرید پکیج تاپ تن")]
        TopTenPackage


    }
}