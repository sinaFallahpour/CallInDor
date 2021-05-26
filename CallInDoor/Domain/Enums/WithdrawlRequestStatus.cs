using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum WithdrawlRequestStatus
    {
        [Description("تایید شده")]
        [Display(Name = "تایید شده")]
        Confirmed,

        [Description("رد شده")]
        [Display(Name = "رد شده")]
        Rejected,

        [Description("درحال بررسی")]
        [Display(Name = "در حال بررسی")]
        Pending


    }
}
