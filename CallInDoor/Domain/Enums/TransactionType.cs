using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum TransactionType
    {

        [Description("برداشت از حساب")]
        [Display(Name = "برداشت از حساب")]
        WhiteDrawl,

        [Description("افزودن به حساب")]
        [Display(Name = "افزودن به حساب")]
        Deposit,

    }
}
