using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum  TiketStatus
    {


        [Description("باز")]
        [Display(Name = "باز")]
        Open,

        [Description("بسته شده")]
        [Display(Name = "بسته شده")]
        Closed,

        //[Description("درحال بررسی")]
        //[Display(Name = "در حال بررسی")]
        //Pendings


    }
}
