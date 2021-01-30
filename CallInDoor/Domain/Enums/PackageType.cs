using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum PackageType
    {

        /// <summary>
        /// 
        /// </summary>
        [Description("Free")]
        [Display(Name = "Free")]
        Free,


        /// <summary>
        /// 
        /// </summary>
        [Description("Unlimited")]
        [Display(Name = "Unlimited")]
        limited,


        ///// <summary>
        ///// به ازای یه بازه زمانی از حساب کاربر برای چت پول کم میشه
        ///// </summary>
        //[Description("Prioded")]
        //[Display(Name = "Prioded")]
        //Prioded,

        ///// <summary>
        ///// 
        ///// </summary>
        //[Description("Session")]
        //[Display(Name = "Session")]
        //Session,

    }
}
