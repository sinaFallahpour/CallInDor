using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum TransactionStatus
    {

        //adminMAnual

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

        /// <summary>
        ///زمانی که تراکنش از نوع خرید پکیج تاپ تن باشد
        /// </summary>
        [Description("تراکنش خرید پکیج تاپ تن")]
        [Display(Name = "تراکنش خرید پکیج تاپ تن")]
        TopTenPackage,


        /// <summary>
        /// مبلغ هایی که بابت کمسیون از تراکنش هایه کاربران کم مسشه
        ///این پولی است که از مبلغی که پروایدر باید میگرفت کم میشه
        /// </summary>
        [Description("کمسین با حق سایت")]
        [Display(Name = "کمسین با حق سایت")]
        Commission,


        /// <summary>
        ///  مبلغ که بابت کد تخفیف از پولی مه که کلاینت بایدمیداد باید کم میشد
        /// ولی پروایدر پواش را کامل باید میگرفت  پس ابن مبلغ را سایت پرداخت میکند
        /// </summary>
        [Description("مبلغ که بابت کد تخفیف از پولی مه که کلاینت بایدمیداد باید کم میشد")]
        [Display(Name = "مبلغ که بابت کد تخفیف از پولی مه که کلاینت بایدمیداد باید کم میشد")]
        Discount,


        ////ادمین دستی به حساب پول یه نفر دیگه پول واریز میکند
        //[Description("ادمین دستی به حساب پول یه نفر دیگه پول واریز میکند")]
        //[Display(Name = "ادمین دستی به حساب پول یه نفر دیگه پول واریز میکند")]
        //WidrawlByAdmin



    }
}