using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum RequestStatusForRedis
    {
        [Description("no plan or expire plan")]
        [Display(Name = "no plan or expire plan")]
        BadPlan,


        [Description("NofFound RequestTBL")]
        [Display(Name = "NofFound RequestTBL")]
        NofFoundRequestTBL,


        [Description("ok plan")]
        [Display(Name = "ok plan")]
        OkPlan,



    }
}
