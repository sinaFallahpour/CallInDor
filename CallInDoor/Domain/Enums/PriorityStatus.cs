using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum PriorityStatus
    {
        [Description("فوری")]
        [Display(Name = "فوری")]
        Imediately,

        [Description("عادی")]
        [Display(Name = "عادی")]
        Normal,

        [Description("جهت اطلاع")]
        [Display(Name = "جهت اطلاع")]
        ForInformation






    }
}
