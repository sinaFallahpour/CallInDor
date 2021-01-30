using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum BuyiedPackageStatus
    {

        [Description("از کیف پول کم شده")]
        [Display(Name = "از کیف پول کم شده")]
        FromWallet,

        [Description("ازدرگاه کم شده")]
        [Display(Name = "از درگاه کم شده")]
        FromDarGah,



    }
}
