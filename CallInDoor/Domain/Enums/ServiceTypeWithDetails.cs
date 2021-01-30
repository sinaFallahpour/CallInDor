using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum ServiceTypeWithDetails
    {

        [Description("Chat/Voice(free)")]
        [Display(Name = "Chat/Voice(free)")]
        ChatVoiceFree,


        /// <summary>
        /// چت های لیمیتد یا اونیکه بسته باید بخرد
        /// </summary>
        [Description("Chat/Voice(Duration)")]
        [Display(Name = "Chat/Voice(Duration)")]
        ChatVoiceLimited,

        [Description("Video Call")]
        [Display(Name = "Video Call")]
        VideoCal,

        [Description("Voice Call")]
        [Display(Name = "Voice Call")]
        VoiceCall,

        [Description("Service")]
        [Display(Name = "Service")]
        Service,

        [Description("Course")]
        [Display(Name = "Course")]
        Course,

    }
}
