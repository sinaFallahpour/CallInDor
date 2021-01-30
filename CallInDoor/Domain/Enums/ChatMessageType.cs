using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{

    /// <summary>
    /// نوع پیام که ممکنه وویس یا چت یا فایل باشد
    /// </summary>
    public enum ChatMessageType
    {

        [Description("فایل")]
        [Display(Name = "فایل")]
        File,
        
        [Description("متن")]
        [Display(Name = "متن")]
        Text,
        
        [Description("وویس")]
        [Display(Name = "وویس")]
        Voice

    }
}
