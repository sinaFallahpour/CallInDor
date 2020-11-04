using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum Gender
    {
        [Description("Mail")]
        [Display(Name = "Mail")]
        Mail,
        [Description("Femail")]
        [Display(Name = "Femail")]
        Femail
    }
}
