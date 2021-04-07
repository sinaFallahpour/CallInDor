using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{

    /// <summary>
    ///       معلوم میکند کدام زبان در حال حاضر هستی
    /// </summary>
    public enum LanguageHeader
    {



        [Description("فارسی")]
        [Display(Name = "فارسی")]
        Persian,
        [Description("انگلیسی")]
        [Display(Name = "انگلیسی")]
        English,

        [Description("عرب")]
        [Display(Name = "عرب")]
       Arab,


    }
}
