using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum BuyiedPackageType
    {

        [Description("PeriodedOrSessionChat")]
        [Display(Name = "PeriodedOrSessionChat")]
        PeriodedOrSessionChat,

        [Description("Video Call")]
        [Display(Name = "Video Call")]
        VideoCal,

        [Description("Voice Call")]
        [Display(Name = "Voice Call")]
        VoiceCall,


        /// <summary>
        /// افرادی که بسته هایی خریدن کخه در بالا تر از دراندر جستجوها نمایش داده شوند
        /// </summary>
        [Description("Trust Tick")]
        [Display(Name = "Trust tTick")]
        ShowonTop,







        //[Description("Service")]
        //[Display(Name = "Service")]
        //Service,

        //[Description("Course")]
        //[Display(Name = "Course")]
        //Course,


    }
}
