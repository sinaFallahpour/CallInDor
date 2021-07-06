using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum BlockMonyStatus
    {

        /// <summary>
        /// بلاک شده
        /// </summary>
        [Description("Blocked")]
        [Display(Name = "Blocked")]
        Blocked,

        /// <summary>
        /// انتقال داده شده
        /// </summary>
        [Description("Swaped")]
        [Display(Name = "Swaped")]
        Swaped,
        
        /// <summary>
        /// پس داده شده
        /// </summary>
        [Description("MoneyBack")]
        [Display(Name = "MoneyBack")]
        MoneyBack,




    }
}
