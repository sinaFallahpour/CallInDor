using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{

    /// <summary>
    ///وضعیت کاربر
    /// </summary>
    public enum UserStatus
    {

        [Description("آزاد")]
        [Display(Name = "آزاد")]
        Free,

        [Description("درحال کال")]
        [Display(Name = "درحال کال")]
        bussy
    }
}
